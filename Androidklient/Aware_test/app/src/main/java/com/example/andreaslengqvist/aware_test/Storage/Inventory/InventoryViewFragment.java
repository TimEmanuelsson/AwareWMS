package com.example.andreaslengqvist.aware_test.Storage.Inventory;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.SeekBar;
import android.widget.TextView;

import com.example.andreaslengqvist.aware_test.Connection.Connection;
import com.example.andreaslengqvist.aware_test.R;
import com.example.andreaslengqvist.aware_test.Storage.Product;
import com.google.gson.Gson;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;



/**
 * Created by andreaslengqvist on 15-04-15.
 */
public class InventoryViewFragment extends Fragment {

    private static final String PARCELABLE_PRODUCT_TAG = "Product";
    private static final int MAX_BALANCE = 10000;
    private static final int MIN_BALANCE = 0;

    private InventoryListener mCallback;
    private View mView;

    private Button btn_inventory_view_inventory_show;
    private Button btn_inventory_view_inventory_cancel;
    private Button btn_inventory_view_inventory_do_inventory;
    private TextView output_product_position;
    private TextView output_product_name;
    private TextView output_product_number;
    private TextView output_product_balance;
    private ImageView img_product_picture;
    private SeekBar seekBar_product_balance;

    private int current_product_balance;

    private Handler repeatUpdateHandler = new Handler();
    private boolean mAutoIncrement = false;
    private boolean mAutoDecrement = false;


    private Product product;

    private String jsonProduct;

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);

        // This makes sure that the container activity has implemented
        // the callback interface. If not, it throws an exception
        try {
            mCallback = (InventoryListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString()
                    + " must implement OnProductSelectedListener");
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        mView = inflater.inflate(R.layout.fragment_inventory_view, container, false);

        initializeVariables();

        // Get the bundled Product.
        Bundle bundle = getArguments();
        product = bundle.getParcelable(PARCELABLE_PRODUCT_TAG);
        setProduct(product);

        current_product_balance = product.getQuantity();

        // SeekBar (slider)
        seekBar_product_balance.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {

            @Override
            public void onProgressChanged(SeekBar seekBar, int progressValue, boolean fromUser) {
                current_product_balance = progressValue;
                output_product_balance.setText(String.valueOf(current_product_balance));
            }

            @Override
            public void onStartTrackingTouch(SeekBar seekBar) {
            }

            @Override
            public void onStopTrackingTouch(SeekBar seekBar) {
                output_product_balance.setText(String.valueOf(current_product_balance));
            }
        });



        mView.findViewById(R.id.btn_increase_balance).setOnLongClickListener(new View.OnLongClickListener() {
                 public boolean onLongClick(View arg0) {
                     if (current_product_balance < MAX_BALANCE) {
                         mAutoIncrement = true;
                         repeatUpdateHandler.post(new RptUpdater());
                     }
                     return false;
                 }
             }
        );

        mView.findViewById(R.id.btn_increase_balance).setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                if ((event.getAction() == MotionEvent.ACTION_UP || event.getAction() == MotionEvent.ACTION_CANCEL)
                        && mAutoIncrement) {
                    mAutoIncrement = false;
                }
                return false;
            }
        });

        mView.findViewById(R.id.btn_decrease_balance).setOnLongClickListener(new View.OnLongClickListener() {
                 public boolean onLongClick(View arg0) {
                     mAutoDecrement = true;
                     repeatUpdateHandler.post(new RptUpdater());
                     return false;
                 }
             }
        );

        mView.findViewById(R.id.btn_decrease_balance).setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                if ((event.getAction() == MotionEvent.ACTION_UP || event.getAction() == MotionEvent.ACTION_CANCEL)
                        && mAutoDecrement) {
                    mAutoDecrement = false;
                }
                return false;
            }
        });

        mView.findViewById(R.id.btn_increase_balance).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (current_product_balance < MAX_BALANCE) {
                    current_product_balance += 1;
                    output_product_balance.setText(String.valueOf(current_product_balance));
                }
            }
        });

        mView.findViewById(R.id.btn_decrease_balance).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (current_product_balance > MIN_BALANCE) {
                    current_product_balance -= 1;
                    output_product_balance.setText(String.valueOf(current_product_balance));
                }
            }
        });

        btn_inventory_view_inventory_show.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                btn_inventory_view_inventory_show.setVisibility(View.GONE);
                mView.findViewById(R.id.layout_inventory_bar).setVisibility(View.VISIBLE);
                btn_inventory_view_inventory_cancel.setVisibility(View.VISIBLE);
                btn_inventory_view_inventory_do_inventory.setVisibility(View.VISIBLE);
                mCallback.onInsideInventory(true);
            }
        });

        btn_inventory_view_inventory_cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                current_product_balance = product.getQuantity();
                output_product_balance.setText(Integer.toString(product.getQuantity()));
                mView.findViewById(R.id.layout_inventory_bar).setVisibility(View.GONE);
                btn_inventory_view_inventory_cancel.setVisibility(View.GONE);
                btn_inventory_view_inventory_do_inventory.setVisibility(View.GONE);
                btn_inventory_view_inventory_show.setVisibility(View.VISIBLE);
                mCallback.onInsideInventory(false);
            }
        });

        btn_inventory_view_inventory_do_inventory.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                product.setQuantity(current_product_balance);
                jsonProduct = new Gson().toJson(product);
                new PutInventory().execute();
                mView.findViewById(R.id.layout_inventory_bar).setVisibility(View.GONE);
                btn_inventory_view_inventory_cancel.setVisibility(View.GONE);
                btn_inventory_view_inventory_do_inventory.setVisibility(View.GONE);
                btn_inventory_view_inventory_show.setVisibility(View.VISIBLE);
                mCallback.onInsideInventory(false);
            }
        });

        return mView;
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
    }

    private void initializeVariables() {

        btn_inventory_view_inventory_show = (Button) mView.findViewById(R.id.btn_inventory_view_inventory_show);
        btn_inventory_view_inventory_cancel = (Button) mView.findViewById(R.id.btn_inventory_view_inventory_cancel);
        btn_inventory_view_inventory_do_inventory = (Button) mView.findViewById(R.id.btn_inventory_view_inventory_do_inventory);

        output_product_position = (TextView) mView.findViewById(R.id.output_product_position);
        output_product_name = (TextView) mView.findViewById(R.id.output_product_name);
        output_product_number = (TextView) mView.findViewById(R.id.output_product_number);
        img_product_picture = (ImageView) mView.findViewById(R.id.img_product_picture);
        output_product_balance = (TextView) mView.findViewById(R.id.output_product_balance);

        seekBar_product_balance = (SeekBar) mView.findViewById(R.id.seekBar_product_balance);
    }

    private void setProduct(Product product) {

        final int balance = product.getQuantity();

        /**
         * BUG FIX!
         * http://stackoverflow.com/questions/17313197/using-seekbar-and-setprogress-doesnt-change-seekbar-position
         */
        seekBar_product_balance.post(new Runnable() {
            @Override
            public void run() {
                seekBar_product_balance.setProgress(balance);
            }
        });
        seekBar_product_balance.setMax(MAX_BALANCE);
        seekBar_product_balance.setProgress(balance);

        output_product_position.setText(product.getStorageSpace());
        output_product_name.setText(product.getName());
        output_product_number.setText(product.getSKU());
        output_product_balance.setText(Integer.toString(balance));
        new ImageDownloader(img_product_picture).execute("https://psmedia.playstation.com/is/image/psmedia/the-last-of-us-remastered-two-column-01-ps4-us-28jul14?$TwoColumn_Image$");
    }


    /**
     * Helper class which loads the ProductPicture into the ImageView in background thread.
     */
    class ImageDownloader extends AsyncTask<String, Void, Bitmap> {
        ImageView bmImage;

        public ImageDownloader(ImageView bmImage) {
            this.bmImage = bmImage;
        }

        protected Bitmap doInBackground(String... urls) {
            String url = urls[0];
            Bitmap mIcon = null;
            try {
                InputStream in = new java.net.URL(url).openStream();
                mIcon = BitmapFactory.decodeStream(in);
            } catch (Exception e) {
                Log.e("Error", e.getMessage());
            }
            return mIcon;
        }

        protected void onPostExecute(Bitmap result) {
            mView.findViewById(R.id.img_product_picture);
            bmImage.setImageBitmap(result);
        }
    }

    class RptUpdater implements Runnable {
        public void run() {
            if (mAutoIncrement) {
                if (current_product_balance < MAX_BALANCE) {
                    increment();
                    repeatUpdateHandler.postDelayed(new RptUpdater(), 100);
                }
            } else if (mAutoDecrement) {
                if (current_product_balance > MIN_BALANCE) {
                    decrement();
                    repeatUpdateHandler.postDelayed(new RptUpdater(), 100);
                }
            }
        }
    }

    public void decrement(){
        current_product_balance--;
        output_product_balance.setText(String.valueOf(current_product_balance));
        seekBar_product_balance.setProgress(current_product_balance);
    }

    public void increment(){
        current_product_balance++;
        output_product_balance.setText(String.valueOf(current_product_balance));
        seekBar_product_balance.setProgress(current_product_balance);
    }


    /**
     *
     * AsyncTask which will run in the background and PUT a updated Product to the Server.
     *
     */
    private class PutInventory extends AsyncTask<Void, String, Boolean> {

        private Socket socket;


        @Override
        protected Boolean doInBackground(Void... arg0) {

            try {

                // Establish a Socket Connection.
                Connection OC = new Connection();
                socket = OC.establish();

                // Create a PrintWriter to PUT (inventory) product.
                PrintWriter out = new PrintWriter(socket.getOutputStream());

                // Write a PUT-method to the Server.
                out.println("PUT/products/inventory/json=" + jsonProduct);
                out.flush();

            } catch (UnknownHostException e) {

                Log.d("UnknownHostException: ", e.toString());
                return false;
            } catch (IOException e) {

                Log.d("IOException: ", e.toString());
                return false;
            } finally {
                try {
                    socket.close();
                } catch (IOException e) {
                    e.printStackTrace();
                }
            }

            return true;
        }

        @Override
        protected void onPostExecute(Boolean finished) {
            super.onPostExecute(finished);
            mCallback.onDoInventory();
        }
    }
}

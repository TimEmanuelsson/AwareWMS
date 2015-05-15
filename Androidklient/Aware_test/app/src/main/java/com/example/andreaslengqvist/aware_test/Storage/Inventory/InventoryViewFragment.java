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
import android.widget.RelativeLayout;
import android.widget.SeekBar;
import android.widget.TextView;
import com.example.andreaslengqvist.aware_test.Connection.Connection;
import com.example.andreaslengqvist.aware_test.R;
import com.example.andreaslengqvist.aware_test.Storage.Product;
import com.example.andreaslengqvist.aware_test.Storage.ProductListener;
import com.google.gson.Gson;
import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;



/**
 * Created by andreaslengqvist on 15-04-15.
 *
 * InventoryViewFragment handles a Product, displays it and also handles the Inventory of that Product.
 *
 */
public class InventoryViewFragment extends Fragment {

    private static final String PARCELABLE_PRODUCT_TAG = "PARCELABLE_PRODUCT_TAG";
    private static final String INVENTORY_LAYOUT_TAG = "INVENTORY_LAYOUT_TAG";

    private static final int MAX_BALANCE = 1000;
    private static final int MIN_BALANCE = 0;

    private Button btn_inventory_view_inventory_show;
    private Button btn_inventory_view_inventory_cancel;
    private Button btn_inventory_view_inventory_save;
    private TextView output_product_position;
    private TextView output_product_name;
    private TextView output_product_number;
    private TextView output_product_balance;
    private ImageView img_product_picture;
    private SeekBar seekBar_product_balance;
    private RelativeLayout layout_inventory_bar;

    private ProductListener mCallback;
    private View mView;
    private Product mProduct;
    private String mJSONProduct;
    private Integer mCurrentBalance;

    private Handler repeatUpdateHandler = new Handler();
    private boolean mAutoIncrement = false;
    private boolean mAutoDecrement = false;


    private void initializeVariables() {

        btn_inventory_view_inventory_show = (Button) mView.findViewById(R.id.btn_inventory_view_inventory_show);
        btn_inventory_view_inventory_cancel = (Button) mView.findViewById(R.id.btn_inventory_view_inventory_cancel);
        btn_inventory_view_inventory_save = (Button) mView.findViewById(R.id.btn_inventory_view_inventory_save);

        output_product_position = (TextView) mView.findViewById(R.id.output_product_position);
        output_product_name = (TextView) mView.findViewById(R.id.output_product_name);
        output_product_number = (TextView) mView.findViewById(R.id.output_product_number);
        img_product_picture = (ImageView) mView.findViewById(R.id.img_product_picture);
        output_product_balance = (TextView) mView.findViewById(R.id.output_product_balance);

        seekBar_product_balance = (SeekBar) mView.findViewById(R.id.seekBar_product_balance);

        layout_inventory_bar = (RelativeLayout) mView.findViewById(R.id.layout_inventory_bar);
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);

        // This makes sure that the container activity has implemented
        // the callback interface. If not, it throws an exception
        try {
            mCallback = (ProductListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString()
                    + " must implement OnProductSelectedListener");
        }
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {

        // Get the bundled Product and type of Activity.
        Bundle bundle = getArguments();
        mProduct = bundle.getParcelable(PARCELABLE_PRODUCT_TAG);
        boolean inventoryLayout = bundle.getBoolean(INVENTORY_LAYOUT_TAG);

        // If coming from InventoryFast or InventoryFull.
        if(inventoryLayout) {
            mView = inflater.inflate(R.layout.fragment_inventory_fast_view, container, false);
        } else {
            mView = inflater.inflate(R.layout.fragment_inventory_full_view, container, false);
        }
        return mView;
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        initializeVariables();


        // Set Product to all its elements.
        setProduct();


        // Show InventoryBar.
        btn_inventory_view_inventory_show.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setVisibility(View.GONE, View.VISIBLE, View.VISIBLE, View.VISIBLE);
                mCallback.onInsideInventory(true);
            }
        });


        // Hide InventoryBar.
        btn_inventory_view_inventory_cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                resetQuantity();
                setVisibility(View.VISIBLE, View.GONE, View.GONE, View.INVISIBLE);
                mCallback.onInsideInventory(false);
            }
        });


        // Save Inventory. Call PutInventory in background.
        btn_inventory_view_inventory_save.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mProduct.setQuantity(mCurrentBalance);
                mJSONProduct = new Gson().toJson(mProduct);
                new PutInventory().execute();
                setVisibility(View.VISIBLE, View.GONE, View.GONE, View.INVISIBLE);
                mCallback.onInsideInventory(false);
            }
        });


        // SeekBar - Slider to change Balance.
        seekBar_product_balance.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {

            @Override
            public void onProgressChanged(SeekBar seekBar, int progressValue, boolean fromUser) {
                mCurrentBalance = progressValue;
                output_product_balance.setText(String.valueOf(mCurrentBalance));
            }

            @Override
            public void onStartTrackingTouch(SeekBar seekBar) {
            }

            @Override
            public void onStopTrackingTouch(SeekBar seekBar) {
                output_product_balance.setText(String.valueOf(mCurrentBalance));
            }
        });


        // Increase Balance - OnTouch.
        mView.findViewById(R.id.btn_increase_balance).setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                if ((event.getAction() == MotionEvent.ACTION_DOWN)) {
                    if (mCurrentBalance < MAX_BALANCE) {
                        mCurrentBalance += 1;
                        output_product_balance.setText(String.valueOf(mCurrentBalance));
                        setSeekBar(mCurrentBalance);
                    }
                }
                if ((event.getAction() == MotionEvent.ACTION_UP || event.getAction() == MotionEvent.ACTION_CANCEL)
                        && mAutoIncrement) {
                    mAutoIncrement = false;
                }
                return false;
            }
        });


        // Increase Balance - LongClick.
        mView.findViewById(R.id.btn_increase_balance).setOnLongClickListener(new View.OnLongClickListener() {
            public boolean onLongClick(View arg0) {
                if (mCurrentBalance < MAX_BALANCE) {
                    mAutoIncrement = true;
                    repeatUpdateHandler.post(new RptUpdater());
                }
                return false;
            }
        });


        // Decrease Balance - OnTouch.
        mView.findViewById(R.id.btn_decrease_balance).setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                if ((event.getAction() == MotionEvent.ACTION_DOWN)) {
                    if (mCurrentBalance > MIN_BALANCE) {
                        mCurrentBalance -= 1;
                        output_product_balance.setText(String.valueOf(mCurrentBalance));
                        setSeekBar(mCurrentBalance);
                    }
                }
                if ((event.getAction() == MotionEvent.ACTION_UP || event.getAction() == MotionEvent.ACTION_CANCEL)
                        && mAutoDecrement) {
                    mAutoDecrement = false;
                }
                return false;
            }
        });


        // Decrease Balance - LongClick.
        mView.findViewById(R.id.btn_decrease_balance).setOnLongClickListener(new View.OnLongClickListener() {
            public boolean onLongClick(View arg0) {
                mAutoDecrement = true;
                repeatUpdateHandler.post(new RptUpdater());
                return false;
            }
        });
    }

    private void setVisibility(int show_visibility, int cancel_visibility, int save_visibility, int bar_visibility) {
        btn_inventory_view_inventory_show.setVisibility(show_visibility);
        btn_inventory_view_inventory_cancel.setVisibility(cancel_visibility);
        btn_inventory_view_inventory_save.setVisibility(save_visibility);
        layout_inventory_bar.setVisibility(bar_visibility);
    }

    private void setProduct() {
        mCurrentBalance = mProduct.getQuantity();

        output_product_position.setText(mProduct.getStorageSpace());
        output_product_name.setText(mProduct.getName());
        output_product_number.setText(mProduct.getSKU());
        output_product_balance.setText(Integer.toString(mCurrentBalance));
        setSeekBar(mCurrentBalance);

        new ImageDownloader(img_product_picture).execute("https://psmedia.playstation.com/is/image/psmedia/the-last-of-us-remastered-two-column-01-ps4-us-28jul14?$TwoColumn_Image$");
    }

    private void setSeekBar(int current_balance) {

        final Integer balance = current_balance;


        // BUG FIX
        // http://stackoverflow.com/questions/17313197/using-seekbar-and-setprogress-doesnt-change-seekbar-position
        seekBar_product_balance.post(new Runnable() {
            @Override
            public void run() {
                seekBar_product_balance.setProgress(balance);
            }
        });
        seekBar_product_balance.setMax(MAX_BALANCE);
        seekBar_product_balance.setProgress(balance);
    }

    private void resetQuantity() {
        mCurrentBalance = mProduct.getQuantity();
        output_product_balance.setText(Integer.toString(mProduct.getQuantity()));
        setSeekBar(mCurrentBalance);
    }

    private void decrement(){
        mCurrentBalance--;
        output_product_balance.setText(String.valueOf(mCurrentBalance));
        seekBar_product_balance.setProgress(mCurrentBalance);
        setSeekBar(mCurrentBalance);
    }

    private void increment(){
        mCurrentBalance++;
        output_product_balance.setText(String.valueOf(mCurrentBalance));
        setSeekBar(mCurrentBalance);
    }


    /**
     * Runnable class which increase or decrease the current balance on LongClick.
     */
    private class RptUpdater implements Runnable {
        public void run() {
            if (mAutoIncrement) {
                if (mCurrentBalance < MAX_BALANCE) {
                    increment();
                    repeatUpdateHandler.postDelayed(new RptUpdater(), 50);
                }
            } else if (mAutoDecrement) {
                if (mCurrentBalance > MIN_BALANCE) {
                    decrement();
                    repeatUpdateHandler.postDelayed(new RptUpdater(), 50);
                }
            }
        }
    }


    /**
     * Helper class which loads the ProductPicture into the ImageView in background thread.
     */
    private class ImageDownloader extends AsyncTask<String, Void, Bitmap> {
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


    /**
     *
     * AsyncTask which will run in the background and PUT inventory on a Product to the Server.
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
                out.println("PUT/products/inventory/json=" + mJSONProduct);
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
            mCallback.onPutInventory();
        }
    }
}

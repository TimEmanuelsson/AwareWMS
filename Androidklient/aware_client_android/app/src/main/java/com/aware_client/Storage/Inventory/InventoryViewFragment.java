package com.aware_client.Storage.Inventory;

import android.app.Activity;
import android.content.Context;
import android.content.SharedPreferences;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.SeekBar;
import android.widget.TextView;

import com.aware_client.Storage.Product;
import com.aware_client.Storage.ProductListener;
import com.aware_client.R;
import com.google.gson.Gson;
import java.io.IOException;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.Socket;



/**
 * Created by andreaslengqvist on 15-04-15.
 *
 * InventoryViewFragment handles displaying and inventory of a Product.
 *
 */
public class InventoryViewFragment extends Fragment {

    // Static Name variables.
    private static final String PARCELABLE_PRODUCT_TAG = "PARCELABLE_PRODUCT_TAG";
    private static final String INVENTORY_LAYOUT_TAG = "INVENTORY_LAYOUT_TAG";

    // Shared Preference Static variables.
    public static final String APP_PREFERENCES = "APP_PREFERENCES" ;
    public static final String SERVER_IP = "SERVER_IP";
    public static final String SERVER_PORT = "SERVER_PORT";
    public static final String SERVER_PW = "SERVER_PW";
    public static final String TABLET_HANDEDNESS = "TABLET_HANDEDNESS";
    public static final String MAX_QTY = "MAX_QTY";
    public static final String UPDATE_FREQ = "UPDATE_FREQ";

    // Range variables.
    private int MAX_QUANTITY;
    private static final int MIN_QUANTITY= 0;

    // Layout variables.
    private Button btn_inventory_view_inventory_show;
    private Button btn_inventory_view_inventory_cancel;
    private Button btn_inventory_view_inventory_save;
    private TextView output_product_position;
    private TextView output_product_name;
    private TextView output_product_number;
    private TextView output_product_quantity;
    private ImageView img_product_picture;
    private SeekBar seekBar_product_quantity;
    private RelativeLayout layout_inventory_bar;
    private RelativeLayout progress_loading_picture;

    // Member variables.
    private ProductListener mCallback;
    private View mView;
    private Product mProduct;
    private String mJSONProduct;
    private Integer mCurrentQuantity;
    private Handler mRepeatUpdateHandler;
    private boolean mAutoIncrement = false;
    private boolean mAutoDecrement = false;

    // Shared Preference variables.
    private SharedPreferences sharedpreferences;
    private String mServerIp;
    private String mServerPort;
    private String mServerPw;
    private Integer mUpdateFreq;



    /**
     * From onCreate
     *
     * Basically initialize all elements from the XML-layout
     * (res/layout/fragment_inventory_fast_view.xml) or (res/layout/fragment_inventory_full_view.xml).
     */
    private void initializeVariables() {

        // Set saved preferences.
        mServerIp = sharedpreferences.getString(SERVER_IP, "");
        mServerPort = sharedpreferences.getString(SERVER_PORT, "");
        mServerPw = sharedpreferences.getString(SERVER_PW, "");
        MAX_QUANTITY = sharedpreferences.getInt(MAX_QTY, 1000);
        mUpdateFreq = sharedpreferences.getInt(UPDATE_FREQ, 10000);

        // Set GUI-components.
        btn_inventory_view_inventory_show = (Button) mView.findViewById(R.id.btn_inventory_view_inventory_show);
        btn_inventory_view_inventory_cancel = (Button) mView.findViewById(R.id.btn_inventory_view_inventory_cancel);
        btn_inventory_view_inventory_save = (Button) mView.findViewById(R.id.btn_inventory_view_inventory_save);

        output_product_position = (TextView) mView.findViewById(R.id.output_product_position);
        output_product_name = (TextView) mView.findViewById(R.id.output_product_name);
        output_product_number = (TextView) mView.findViewById(R.id.output_product_number);
        img_product_picture = (ImageView) mView.findViewById(R.id.img_product_picture);
        output_product_quantity = (TextView) mView.findViewById(R.id.output_product_quantity);

        seekBar_product_quantity = (SeekBar) mView.findViewById(R.id.seekBar_product_quantity);
        layout_inventory_bar = (RelativeLayout) mView.findViewById(R.id.layout_inventory_bar);
        progress_loading_picture = (RelativeLayout) mView.findViewById(R.id.progress_loading_picture);
    }


    @Override
    /**
     * Called when this Fragment is being created.
     *
     * This makes sure that the container activity has implemented
     * the callback interface. If not, it throws an exception
     */
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            mCallback = (ProductListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString()
                    + " must implement OnProductSelectedListener");
        }
    }


    @Override
    /**
     * Called when this Fragments View is being created.
     *
     * Basically just do thing that needs to be done upon creation of the View.
     *
     * @param inflater that can be used to inflate any views in the fragment
     * @param container this can be used to generate the LayoutParams of the view
     * @param savedInstanceState saved data from a Configuration change
     *
     * @return mView inflated with the correct layout
     */
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {

        // Get the bundled Product and which type of Activity.
        Bundle bundle = getArguments();
        mProduct = bundle.getParcelable(PARCELABLE_PRODUCT_TAG);
        boolean inventoryLayout = bundle.getBoolean(INVENTORY_LAYOUT_TAG);

        sharedpreferences = getActivity().getSharedPreferences(APP_PREFERENCES, Context.MODE_PRIVATE);
        Boolean righthanded = sharedpreferences.getBoolean(TABLET_HANDEDNESS, true);

        // If coming from InventoryFast or InventoryFull.
        if(inventoryLayout) {
            mView = inflater.inflate(R.layout.fragment_inventory_fast_view, container, false);
        } else {
            if(righthanded) {
                mView = inflater.inflate(R.layout.fragment_inventory_full_view_right, container, false);
            } else {
                mView = inflater.inflate(R.layout.fragment_inventory_full_view_left, container, false);
            }
        }
        return mView;
    }


    @Override
    /**
     * Called when this Fragments Activity finished its creation.
     *
     * Basically just do thing that needs to be done upon creation of the Fragment.
     *
     * @param savedInstanceState saved data from a Configuration change
     */
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        initializeVariables();

        // Create a Handler for handling LongClicks.
        mRepeatUpdateHandler = new Handler();

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


        // Cancel InventoryBar.
        btn_inventory_view_inventory_cancel.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                resetBalance();
                setVisibility(View.VISIBLE, View.GONE, View.GONE, View.INVISIBLE);
                mCallback.onInsideInventory(false);
            }
        });


        // Save Inventory. Call PutInventory in background.
        btn_inventory_view_inventory_save.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mProduct.setQuantity(mCurrentQuantity);
                mJSONProduct = new Gson().toJson(mProduct);
                new PutInventory().execute();
                setVisibility(View.VISIBLE, View.GONE, View.GONE, View.INVISIBLE);
                mCallback.onInsideInventory(false);
            }
        });


        // SeekBar - Slider to change Quantity.
        seekBar_product_quantity.setOnSeekBarChangeListener(new SeekBar.OnSeekBarChangeListener() {

            @Override
            public void onProgressChanged(SeekBar seekBar, int progressValue, boolean fromUser) {
                mCurrentQuantity = progressValue;
                output_product_quantity.setText(String.valueOf(mCurrentQuantity));
            }

            @Override
            public void onStartTrackingTouch(SeekBar seekBar) {
            }

            @Override
            public void onStopTrackingTouch(SeekBar seekBar) {
                output_product_quantity.setText(String.valueOf(mCurrentQuantity));
            }
        });


        // Increase Quantity - OnTouch.
        mView.findViewById(R.id.btn_increase_balance).setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                if ((event.getAction() == MotionEvent.ACTION_DOWN)) {
                    if (mCurrentQuantity < MAX_QUANTITY) {
                        increment();
                    }
                }
                if ((event.getAction() == MotionEvent.ACTION_UP || event.getAction() == MotionEvent.ACTION_CANCEL)
                        && mAutoIncrement) {
                    mAutoIncrement = false;
                }
                return false;
            }
        });


        // Increase Quantity - LongClick.
        mView.findViewById(R.id.btn_increase_balance).setOnLongClickListener(new View.OnLongClickListener() {
            public boolean onLongClick(View arg0) {
                if (mCurrentQuantity < MAX_QUANTITY) {
                    mAutoIncrement = true;
                    mRepeatUpdateHandler.post(new RptUpdater());
                }
                return false;
            }
        });


        // Decrease Quantity - OnTouch.
        mView.findViewById(R.id.btn_decrease_balance).setOnTouchListener(new View.OnTouchListener() {
            public boolean onTouch(View v, MotionEvent event) {
                if ((event.getAction() == MotionEvent.ACTION_DOWN)) {
                    if (mCurrentQuantity > MIN_QUANTITY) {
                        decrement();
                    }
                }
                if ((event.getAction() == MotionEvent.ACTION_UP || event.getAction() == MotionEvent.ACTION_CANCEL)
                        && mAutoDecrement) {
                    mAutoDecrement = false;
                }
                return false;
            }
        });


        // Decrease Quantity - LongClick.
        mView.findViewById(R.id.btn_decrease_balance).setOnLongClickListener(new View.OnLongClickListener() {
            public boolean onLongClick(View arg0) {
                mAutoDecrement = true;
                mRepeatUpdateHandler.post(new RptUpdater());
                return false;
            }
        });
    }


    /**
     * From Show / Cancel / Save Inventory - OnClickListener
     *
     * Called when the elements of the Inventory should be visible or not.
     * Sets the Visibility to show or hide.
     *
     * @param show_visibility View.VISIBLE / View.GONE
     * @param cancel_visibility View.VISIBLE / View.GONE
     * @param save_visibility View.VISIBLE / View.GONE
     * @param bar_visibility View.VISIBLE / View.INVISIBLE
     */
    private void setVisibility(int show_visibility, int cancel_visibility, int save_visibility, int bar_visibility) {
        btn_inventory_view_inventory_show.setVisibility(show_visibility);
        btn_inventory_view_inventory_cancel.setVisibility(cancel_visibility);
        btn_inventory_view_inventory_save.setVisibility(save_visibility);
        layout_inventory_bar.setVisibility(bar_visibility);
    }


    /**
     * From onActivityCreated
     *
     * Called when Fragment is being created.
     * Sets the enclosed Product to the layout elements.
     */
    private void setProduct() {

        // Set the current Balance which will be used in the InventoryBar.
        mCurrentQuantity = mProduct.getQuantity();

        // Storage Space.
        output_product_position.setText(mProduct.getStorageSpace());

        // Name.
        output_product_name.setText(mProduct.getName());

        // SKU (Number).
        output_product_number.setText(mProduct.getSKU());

        // Quantity.
        output_product_quantity.setText(Integer.toString(mCurrentQuantity));

        // SeekBar (slider).
        setSeekBar(mCurrentQuantity);

        // Image.
        progress_loading_picture.setVisibility(View.VISIBLE);
        new GetProductImage(img_product_picture).execute(mProduct.getImageLocation());
    }


    /**
     * From increment / decrement / resetQuantity
     *
     * Called when the current quantity is changed upon SeekBar or + - changes.
     * Sets the SeekBar to the current quantity.
     *
     * @param current_quantity the updated quantity
     */
    private void setSeekBar(int current_quantity) {

        final Integer balance = current_quantity;


        // BUG FIX
        // http://stackoverflow.com/questions/17313197/using-seekbar-and-setprogress-doesnt-change-seekbar-position
        seekBar_product_quantity.post(new Runnable() {
            @Override
            public void run() {
                seekBar_product_quantity.setProgress(balance);
            }
        });
        seekBar_product_quantity.setMax(MAX_QUANTITY);
        seekBar_product_quantity.setProgress(balance);
    }


    /**
     * From Cancel Inventory
     *
     * Called when the quantity should be decremented.
     * Sets the quantity to the old quantity by getting it from the Product-object.
     */
    private void resetBalance() {
        mCurrentQuantity = mProduct.getQuantity();
        output_product_quantity.setText(Integer.toString(mProduct.getQuantity()));
        setSeekBar(mCurrentQuantity);
    }


    /**
     * From RptUpdater or ( - OnTouch)
     *
     * Called when the quantity should be decremented.
     * Decrements the current quantity by -1.
     */
    private void decrement(){
        mCurrentQuantity--;
        output_product_quantity.setText(String.valueOf(mCurrentQuantity));
        seekBar_product_quantity.setProgress(mCurrentQuantity);
        setSeekBar(mCurrentQuantity);
    }


    /**
     * From RptUpdater or ( - OnTouch)
     *
     * Called when the quantity should be incremented.
     * Increments the current quantity by +1.
     */
    private void increment(){
        mCurrentQuantity++;
        output_product_quantity.setText(String.valueOf(mCurrentQuantity));
        setSeekBar(mCurrentQuantity);
    }


    /**
     * Runnable class which increase or decrease the current quantity each 50ms on LongClick.
     */
    private class RptUpdater implements Runnable {
        public void run() {
            if (mAutoIncrement) {
                if (mCurrentQuantity < MAX_QUANTITY) {
                    increment();
                    mRepeatUpdateHandler.postDelayed(new RptUpdater(), 50);
                }
            } else if (mAutoDecrement) {
                if (mCurrentQuantity > MIN_QUANTITY) {
                    decrement();
                    mRepeatUpdateHandler.postDelayed(new RptUpdater(), 50);
                }
            }
        }
    }



    /**
     * AsyncTask which will run in the background and fetch a Product Image.
     */
    private class GetProductImage extends AsyncTask<String, Void, Bitmap> {

        ImageView bmImage;

        public GetProductImage(ImageView bmImage) {
            this.bmImage = bmImage;
        }


        @Override
        protected Bitmap doInBackground(String... urls) {
            String url = urls[0];
            Bitmap mImage = null;

            try {

                // Establish a Socket-Connection.
                Socket socket = new Socket();
                socket.connect(new InetSocketAddress(InetAddress.getByName(mServerIp), Integer.parseInt(mServerPort)), mUpdateFreq);

                // If socket has established a connection to the server.
                if (socket.isConnected()) {

                    // Create a PrintWriter to write to the Server with a GET.
                    PrintWriter out = new PrintWriter(socket.getOutputStream());

                    // Write a GET-method to the Server.
                    out.println("GET/products/image=" + url + "/pw=" + mServerPw);
                    out.flush();

                    // Decode ByteArray from InputStream to Bitmap.
                    mImage = BitmapFactory.decodeStream(socket.getInputStream());

                    // Close the connection.
                    socket.close();
                }
                return mImage;

            } catch (IOException e) {
                return null;
            }
        }

        @Override
        protected void onPostExecute(Bitmap result) {
            super.onPostExecute(result);
            if(result != null) {
                progress_loading_picture.setVisibility(View.INVISIBLE);
                mView.findViewById(R.id.img_product_picture);
                bmImage.setImageBitmap(result);
            }
        }
    }


    /**
     * AsyncTask which will run in the background and PUT inventory on a Product to the Servers Database.
     */
    private class PutInventory extends AsyncTask<Void, Void, Boolean> {

        @Override
        protected Boolean doInBackground(Void... arg0) {

            try {

                // Establish a Socket-Connection.
                Socket socket = new Socket();
                socket.connect(new InetSocketAddress(InetAddress.getByName(mServerIp), Integer.parseInt(mServerPort)), mUpdateFreq);

                // If socket has established a connection to the server.
                if (socket.isConnected()) {


                    // Create a PrintWriter to PUT (inventory) product.
                    PrintWriter out = new PrintWriter(socket.getOutputStream());

                    // Write a PUT-method to the Server.
                    out.println("PUT/products/inventory/json=" + mJSONProduct + "/pw=" + mServerPw);
                    out.flush();

                    // Close the connection.
                    socket.close();
                }
                return true;

            } catch (IOException e) {
                return false;
            }
        }

        @Override
        protected void onPostExecute(Boolean finished) {
            super.onPostExecute(finished);

            if(finished) {
                // Call the Activity through the interface that an Inventory have been made.
                mCallback.onPutInventory();
            }
        }
    }
}

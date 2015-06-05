package com.aware_client.Storage.Products;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.aware_client.Storage.Product;
import com.aware_client.Storage.ProductListener;
import com.aware_client.Helpers.FragmentIntentIntegrator;
import com.google.zxing.integration.android.IntentIntegrator;
import com.aware_client.R;
import com.google.gson.Gson;
import com.google.zxing.integration.android.IntentResult;
import java.io.IOException;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.util.ArrayList;



/**
 * Created by andreaslengqvist on 15-04-15.
 *
 * ProductViewFragment handles displaying and updating of a Product.
 *
 */
public class ProductViewFragment extends Fragment {

    // Static Name variables.
    private static final String PARCELABLE_PRODUCT_LIST_TAG = "PARCELABLE_PRODUCT_LIST_TAG";
    private static final String PARCELABLE_PRODUCT_TAG = "PARCELABLE_PRODUCT_TAG";

    // Shared Preference Static variables.
    public static final String APP_PREFERENCES = "APP_PREFERENCES" ;
    public static final String SERVER_IP = "SERVER_IP";
    public static final String SERVER_PORT = "SERVER_PORT";
    public static final String SERVER_PW = "SERVER_PW";
    public static final String UPDATE_FREQ = "UPDATE_FREQ";

    // Layout variables.
    private RelativeLayout btn_product_view_show_edit_menu;
    private Button btn_product_view_create_EAN;
    private TextView output_product_position;
    private TextView output_product_name;
    private TextView output_product_number;
    private TextView output_product_quantity;
    private ImageView icon_product_view_show_edit_menu;
    private ImageView img_product_picture;
    private RelativeLayout progress_loading_picture;

    // Member variables.
    private ProductListener mCallback;
    private View mView;
    private ArrayList<Product> mProducts;
    private Product mProduct;
    private boolean mInsideEditMenu;
    private String mJSONProduct;

    // Shared Preference variables.
    private String mServerIp;
    private String mServerPort;
    private String mServerPw;
    private Integer mUpdateFreq;


    /**
     * From onCreate
     *
     * Basically initialize all elements from the XML-layout (res/layout/fragment_product_view.xml).
     */
    private void initializeVariables() {

        // Get saved preferences.
        SharedPreferences sharedpreferences = getActivity().getSharedPreferences(APP_PREFERENCES, Context.MODE_PRIVATE);
        mServerIp = sharedpreferences.getString(SERVER_IP, "");
        mServerPort = sharedpreferences.getString(SERVER_PORT, "");
        mServerPw = sharedpreferences.getString(SERVER_PW, "");
        mUpdateFreq = sharedpreferences.getInt(UPDATE_FREQ, 10000);

        // Set GUI-components.
        output_product_position = (TextView) mView.findViewById(R.id.output_product_position);
        output_product_name = (TextView) mView.findViewById(R.id.output_product_name);
        output_product_number = (TextView) mView.findViewById(R.id.output_product_number);
        img_product_picture = (ImageView) mView.findViewById(R.id.img_product_picture);
        output_product_quantity = (TextView) mView.findViewById(R.id.output_product_quantity);

        btn_product_view_show_edit_menu = (RelativeLayout) mView.findViewById(R.id.btn_product_view_show_edit_menu);
        icon_product_view_show_edit_menu = (ImageView) mView.findViewById(R.id.icon_product_view_show_edit_menu);
        btn_product_view_create_EAN = (Button) mView.findViewById(R.id.btn_product_view_create_EAN);
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
        mView = inflater.inflate(R.layout.fragment_product_view, container, false);
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


        // Get the bundled Product.
        Bundle bundle = getArguments();
        mProducts = bundle.getParcelableArrayList(PARCELABLE_PRODUCT_LIST_TAG);
        mProduct = bundle.getParcelable(PARCELABLE_PRODUCT_TAG);


        // Set Product to View.
        setProduct();


        // When user clicks Edit-symbol (pencil).
        btn_product_view_show_edit_menu.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (!mInsideEditMenu) {
                    mInsideEditMenu = true;
                    btn_product_view_create_EAN.setVisibility(View.VISIBLE);
                    icon_product_view_show_edit_menu.setBackgroundResource(R.drawable.ic_action_edit_inside);
                } else {
                    mInsideEditMenu = false;
                    btn_product_view_create_EAN.setVisibility(View.GONE);
                    icon_product_view_show_edit_menu.setBackgroundResource(R.drawable.ic_action_edit);
                }
            }
        });


        btn_product_view_create_EAN.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                FragmentIntentIntegrator integrator = new FragmentIntentIntegrator(ProductViewFragment.this);
                // Create an Integrator which is used to initiate the scanner.
                if(getResources().getConfiguration().orientation == Configuration.ORIENTATION_PORTRAIT) {
                    integrator.setOrientation(90);
                }
                integrator.initiateScan();
            }
        });
    }


    @Override
    /**
     * Called when Activity gets back a response from a Intent in this case the Barcode scanner.
     * Handles the scanned EAN.
     *
     * @param requestCode allowing you to identify who this result came from
     * @param resultCode result code returned by the child activity through its setResult()
     * @param intent which can return result data to the caller
     */
    public void onActivityResult(int requestCode, int resultCode, Intent intent) {

        // Receive the scanned EAN, bundles it and starts the InventoryFastActivity.
        IntentResult scanResult = IntentIntegrator.parseActivityResult(requestCode, resultCode, intent);
        String ean = scanResult.getContents();

        if (ean != null) {

            boolean eanExists = false;

            // If EAN already exist in the database.
            for(Product product : mProducts) {
                if (product.getEAN().equals(ean)) {
                    eanExists = true;
                    Toast toast = Toast.makeText(getActivity().getApplicationContext(), R.string.toast_ean_already_exists, Toast.LENGTH_LONG);
                    toast.setGravity(Gravity.TOP | Gravity.CENTER_HORIZONTAL, 0, 20);
                    toast.show();
                }
            }

            // If EAN doesn't exist. Call PutProduct in background.
            if(!eanExists) {
                mProduct.setEAN(ean);
                mJSONProduct = new Gson().toJson(mProduct);
                new PutProduct().execute();
            }
        }
    }


    /**
     * From onActivityCreated
     *
     * Called when Fragment is being created.
     * Sets the enclosed Product to the layout elements.
     */
    private void setProduct() {

        // Storage Space.
        output_product_position.setText(mProduct.getStorageSpace());

        // Name.
        output_product_name.setText(mProduct.getName());

        // SKU (Number).
        output_product_number.setText(mProduct.getSKU());

        // Quantity.
        output_product_quantity.setText(Integer.toString(mProduct.getQuantity()));

        // Image.
        progress_loading_picture.setVisibility(View.VISIBLE);
        new GetProductImage(img_product_picture).execute(mProduct.getImageLocation());

        // If Product has EAN. Change "Add EAN"-Button to "Edit EAN"-Button.
        if(!mProduct.getEAN().equals("0")) {
            btn_product_view_create_EAN.setText(R.string.btn_product_view_change_EAN);
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
     * AsyncTask which will run in the background and PUT a updated Product to the Servers Database.
     */
    private class PutProduct extends AsyncTask<Void, String, Boolean> {

        @Override
        protected Boolean doInBackground(Void... arg0) {

            try {

                // Establish a Socket-Connection.
                Socket socket = new Socket();
                socket.connect(new InetSocketAddress(InetAddress.getByName(mServerIp), Integer.parseInt(mServerPort)), mUpdateFreq);

                // If socket has established a connection to the server.
                if (socket.isConnected()) {

                    // Create a PrintWriter to PUT updated product.
                    PrintWriter out = new PrintWriter(socket.getOutputStream());

                    // Write a PUT-method to the Server.
                    out.println("PUT/products/json=" + mJSONProduct + "/pw=" + mServerPw);
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
                // Call the Activity through the Interface that an Update have been made.
                mCallback.onPutProduct();
            }
        }
    }
}

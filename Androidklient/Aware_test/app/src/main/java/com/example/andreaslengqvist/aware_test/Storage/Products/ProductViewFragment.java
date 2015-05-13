package com.example.andreaslengqvist.aware_test.Storage.Products;

import android.app.Activity;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.example.andreaslengqvist.aware_test.Storage.ProductListener;
import com.google.zxing.integration.android.IntentIntegrator;


import com.example.andreaslengqvist.aware_test.Connection.Connection;
import com.example.andreaslengqvist.aware_test.R;
import com.example.andreaslengqvist.aware_test.Storage.ProductListListener;
import com.example.andreaslengqvist.aware_test.Storage.Product;
import com.google.gson.Gson;
import com.google.zxing.integration.android.IntentResult;

import java.io.IOException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.ArrayList;


/**
 * Created by andreaslengqvist on 15-04-15.
 *
 */
public class ProductViewFragment extends Fragment {

    private static final String PARCELABLE_PRODUCT_LIST_TAG = "PARCELABLE_PRODUCT_LIST_TAG";
    private static final String PARCELABLE_PRODUCT_TAG = "PARCELABLE_PRODUCT_TAG";

    private ProductListener mCallback;
    private View mView;
    private boolean mInsideEditMenu;

    private RelativeLayout btn_product_view_show_edit_menu;
    private Button btn_product_view_create_EAN;
    private TextView output_product_position;
    private TextView output_product_name;
    private TextView output_product_number;
    private TextView output_product_balance;
    private ImageView icon_product_view_show_edit_menu;
    private ImageView img_product_picture;

    private ArrayList<Product> mProducts;
    private Product mProduct;
    private String jsonProduct;

    private void initializeVariables() {

        output_product_position = (TextView) mView.findViewById(R.id.output_product_position);
        output_product_name = (TextView) mView.findViewById(R.id.output_product_name);
        output_product_number = (TextView) mView.findViewById(R.id.output_product_number);
        img_product_picture = (ImageView) mView.findViewById(R.id.img_product_picture);
        output_product_balance = (TextView) mView.findViewById(R.id.output_product_balance);

        btn_product_view_show_edit_menu = (RelativeLayout) mView.findViewById(R.id.btn_product_view_show_edit_menu);
        icon_product_view_show_edit_menu = (ImageView) mView.findViewById(R.id.icon_product_view_show_edit_menu);
        btn_product_view_create_EAN = (Button) mView.findViewById(R.id.btn_product_view_create_EAN);
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
        mView = inflater.inflate(R.layout.fragment_product_view, container, false);
        return mView;
    }

    @Override
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
                integrator.setDesiredBarcodeFormats(IntentIntegrator.PRODUCT_CODE_TYPES);
                integrator.setPrompt("Scanna streckkod");
                integrator.setResultDisplayDuration(0);
                integrator.setWide();
                integrator.setOrientation(1);
                integrator.initiateScan();
            }
        });
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent intent) {

        // Receive the scanned EAN, bundles it and starts the InventoryFastActivity.
        IntentResult scanResult = IntentIntegrator.parseActivityResult(requestCode, resultCode, intent);
        String ean = scanResult.getContents();

        if (ean != null) {

            boolean eanExists = false;

            for(Product product : mProducts) {
                if (product.getEAN().equals(ean)) {
                    eanExists = true;
                    Toast toast = Toast.makeText(getActivity().getApplicationContext(), R.string.toast_ean_already_exists, Toast.LENGTH_LONG);
                    toast.setGravity(Gravity.TOP | Gravity.CENTER_HORIZONTAL, 0, 20);
                    toast.show();
                }
            }

            if(!eanExists) {
                mProduct.setEAN(ean);
                jsonProduct = new Gson().toJson(mProduct);
                new PutProduct().execute();
            }
        }
    }

    private void setProduct() {

        output_product_position.setText(mProduct.getStorageSpace());
        output_product_name.setText(mProduct.getName());
        output_product_number.setText(mProduct.getSKU());
        output_product_balance.setText(Integer.toString(mProduct.getQuantity()));
        new ImageDownloader(img_product_picture).execute("https://psmedia.playstation.com/is/image/psmedia/the-last-of-us-remastered-two-column-01-ps4-us-28jul14?$TwoColumn_Image$");

        if(!mProduct.getEAN().equals("0")) {
            btn_product_view_create_EAN.setText(R.string.btn_product_view_change_EAN);
        }
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


    /**
     *
     * AsyncTask which will run in the background and PUT a updated Product to the Server.
     *
     */
    private class PutProduct extends AsyncTask<Void, String, Boolean> {

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
                out.println("PUT/products/json=" + jsonProduct);
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
            mCallback.onPutProduct();
        }
    }
}

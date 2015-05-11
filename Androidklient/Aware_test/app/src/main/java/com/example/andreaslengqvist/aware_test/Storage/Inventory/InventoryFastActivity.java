package com.example.andreaslengqvist.aware_test.Storage.Inventory;

import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.FragmentActivity;
import android.util.Log;

import com.example.andreaslengqvist.aware_test.Connection.Connection;
import com.example.andreaslengqvist.aware_test.R;
import com.example.andreaslengqvist.aware_test.Storage.Product;
import com.example.andreaslengqvist.aware_test.Storage.Products.ProductViewFragment;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;


/**
 * Created by andreaslengqvist on 15-04-07.
 */
public class InventoryFastActivity extends FragmentActivity {

    private static final String EAN_TAG = "EAN_TAG";

    private Handler mHandler;
    private String mScannedEAN;
    private String mOldJSON;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.fragment_inventory_view);

        // Get which type (Products / Inventory).
        Intent i = getIntent();
        if (i.hasExtra(EAN_TAG)) {
            mScannedEAN = i.getStringExtra(EAN_TAG);
        }
        Log.d("asdasd", mScannedEAN);


        // If coming from new state. (e.g NOT screen rotation)
        if (savedInstanceState == null) {

            // If user is using a tablet or a handheld.
            if (!getResources().getBoolean(R.bool.isTablet)) {
                setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
            }

            startPeriodically();
        }
    }

    @Override
    public void onBackPressed() {
        super.onBackPressed();
        stopPeriodically();
    }

    public void startPeriodically() {
        mHandler = new Handler();
        mHandler.post(runPeriodically);
    }

    public void stopPeriodically() {
        mHandler.removeCallbacks(runPeriodically);
    }

    private Runnable runPeriodically = new Runnable() {

        @Override
        public void run() {
            new GetProductByEAN().execute();
            mHandler.postDelayed(runPeriodically, 1000);
        }
    };


    /**
     *
     * AsyncTask which will run in the background and fetch a ArrayList of Products.
     * If the new ArrayList is the same as the old one no changes have been made and NO need to replace the old one.
     *
     */
    private class GetProductByEAN extends AsyncTask<Void, Void, Product> {

        private Socket socket;
        private String newJSON;


        @Override
        protected Product doInBackground(Void... arg0) {

            try {

                // Establish a Socket-Connection.
                Connection OC = new Connection();
                socket = OC.establish();

                // Create a PrintWriter to write to the Server with a GET.
                PrintWriter out = new PrintWriter(socket.getOutputStream());

                // Write a GET-method to the Server.
                out.println("GET/products/barcodenumber=" + mScannedEAN);
                out.flush();

                // Get Products by using a InputStreamReader wrapped in a BufferedReader to read JSON as a String.
                newJSON = new BufferedReader(new InputStreamReader(socket.getInputStream())).readLine();

            } catch (UnknownHostException e) {

                e.printStackTrace();
            } catch (IOException e) {

                Log.d("Error: ", e.toString());
            } finally {
                try {

                    socket.close();
                } catch (IOException e) {

                    e.printStackTrace();
                }
            }

            // If and old JSON-object exists. (e.g not the first GET)
            if(mOldJSON != null) {

                // And the new JSON-object NOT equals to the old, return the new one.
                if (!newJSON.equals(mOldJSON)) {
                    mOldJSON = newJSON;
                    return new Gson().fromJson(newJSON, new TypeToken<Product>() {}.getType());
                }
                // Else, there has not been any changes in the database. Retain the old one.
                else { return null; }
            }

            // Else, this is the first GET.
            else {
                mOldJSON = newJSON;
                Log.d("asdasd", newJSON);

                return new Gson().fromJson(newJSON, new TypeToken<Product>() {}.getType());
            }
        }

        @Override
        protected void onPostExecute(Product result) {
            super.onPostExecute(result);
            Log.d("asdasd", result.getName());
        }
    }
}

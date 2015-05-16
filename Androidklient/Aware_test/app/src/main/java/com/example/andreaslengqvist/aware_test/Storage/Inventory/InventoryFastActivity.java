package com.example.andreaslengqvist.aware_test.Storage.Inventory;

import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.support.v7.app.ActionBarActivity;
import android.util.Log;
import android.view.Gravity;
import android.widget.Toast;
import com.example.andreaslengqvist.aware_test.Connection.Connection;
import com.example.andreaslengqvist.aware_test.R;
import com.example.andreaslengqvist.aware_test.Storage.Product;
import com.example.andreaslengqvist.aware_test.Storage.ProductListener;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.lang.ref.WeakReference;
import java.net.Socket;
import java.net.UnknownHostException;



/**
 * Created by andreaslengqvist on 15-04-07.
 *
 * InventoryFastActivity handles the scanned EAN (barcode) and searches the database for the represented Product linked
 * to that EAN. Also handles the Inventory for that Product.
 *
 */
public class InventoryFastActivity extends ActionBarActivity implements ProductListener {

    // Static Name variables.
    private static final String EAN_TAG = "EAN_TAG";

    private static final String INVENTORY_VIEW_FRAGMENT_TAG = "INVENTORY_VIEW_FRAGMENT_TAG";
    private static final String PARCELABLE_PRODUCT_TAG = "PARCELABLE_PRODUCT_TAG";
    private static final String INVENTORY_LAYOUT_TAG = "INVENTORY_LAYOUT_TAG";

    // WeakReference variable.
    private static WeakReference<InventoryFastActivity> wrActivity = null;

    // Member variables.
    private Handler mHandler;
    private String mScannedEAN;
    private String mOldJSON;
    private boolean mEANNotFound;
    private boolean mWaitingForInventoryUpdate;


    @Override
    /**
     * Called when this Activity is being created.
     *
     * Basically just do thing that needs to be done upon creation.
     *
     * @param savedInstanceState saved data from a Configuration change
     */
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_inventory_fast);


        // If user is NOT using a tablet set the orientation to only allow Portrait-mode.
        if (!getResources().getBoolean(R.bool.isTablet)) {
            setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        }


        // Create a WeakReference to remember this Activity upon a orientation change.
        wrActivity = new WeakReference<>(this);


        // Get the scanned EAN from the bundle.
        Intent i = getIntent();
        if (i.hasExtra(EAN_TAG)) {
            mScannedEAN = i.getStringExtra(EAN_TAG);
        }


        // Create and start a Handler to run periodically.
        mHandler = new Handler();
        startPeriodically();
    }


    @Override
    /**
     * Called when pressing Back button.
     * Works like an regular back button and goes back to the previous Activity (MainActivity).
     */
    public void onBackPressed() {
        super.onBackPressed();
        overridePendingTransition(R.anim.pull_in_bottom, R.anim.push_out_top);
    }


    @Override
    /**
     * Called when Activity destroyed.
     *
     * Stops the periodically background thread GetProductByEAN.
     */
    protected void onDestroy() {
        super.onDestroy();
        stopPeriodically();
    }


    @Override
    /**
     * Called when Activity paused. (e.g sleep-mode or user heads to another app)
     *
     * Stops the periodically background thread GetProductByEAN.
     */
    protected void onPause() {
        super.onPause();
        stopPeriodically();
    }


    @Override
    /**
     * Called when Activity resumed. (e.g from sleep-mode or user in again from another app)
     *
     * Stops the old periodically background thread GetProductByEAN and starts
     * a new one.
     */
    protected void onResume() {
        super.onResume();
        mHandler.removeCallbacks(runPeriodically);
        startPeriodically();
    }


    @Override
    /**
     * This Interface-function is never used.
     */
    public void onPutProduct() {
        // TODO: NOTHING.
    }


    @Override
    /**
     * This Interface-function is never used.
     */
    public void onInsideInventory(boolean inside) {
        // TODO: NOTHING.
    }


    @Override
    /**
     * From InventoryViewFragment - OnCLickListener (Save Inventory)
     *
     * Called when user clicked Save in InventoryView.
     * Sets a waiting variable to use in the AsyncTask.
     */
    public void onPutInventory() {
        mWaitingForInventoryUpdate = true;
    }


    /**
     * From onCreate / onResume
     *
     * Called when activity is created or resumed.
     * Posts runPeriodically to the Handler.
     */
    public void startPeriodically() {
        mHandler.post(runPeriodically);
    }


    /**
     * From onPaused / onDestroy
     *
     * Called when activity is paused or destroyed.
     * Removes runPeriodically from the callbacks.
     */
    public void stopPeriodically() {
        mHandler.removeCallbacks(runPeriodically);
    }


    /**
     * From startPeriodically
     *
     * Called when the Handler wants to run the periodically AsyncTask GetProductByEAN.
     * Runs a GetProductByEAN each 1000ms (delayed).
     */
    private Runnable runPeriodically = new Runnable() {

        @Override
        public void run() {

            // Create a new AsyncTask each second.
            new GetProductByEAN().execute();
            mHandler.postDelayed(runPeriodically, 1000);
        }
    };


    /**
     * AsyncTask which will run in the background and fetch a new version of the Scanned Product.
     * If the new Product is the same as the old Product no changes have been made and NO need to replace the old Product.
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
                out.println("GET/products/ean=" + mScannedEAN);
                out.flush();

                // Get Product by using a InputStreamReader wrapped in a BufferedReader to read JSON as a String.
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

            // If the returned JSON is not "null".
            if (!newJSON.equals("null")) {

                    // If the new JSON-object NOT equals to the old one, return the new one.
                    if (!newJSON.equals(mOldJSON)) {
                        mOldJSON = newJSON;
                        return new Gson().fromJson(newJSON, new TypeToken<Product>() {
                        }.getType());
                    }
                    // Else, there has not been any changes in the database. Keep the old one.
                    else { return null; }
            }

            // Else the EAN cannot be found in the database.
            else {
                mEANNotFound = true;
                return null;
            }
        }

        @Override
        protected void onPostExecute(Product result) {
            super.onPostExecute(result);

            // Weird solution to solve the bug with Activity lost upon screen rotation.
            if ((wrActivity.get() != null) && (!wrActivity.get().isFinishing())) {

                // If EAN couldn't be found Toast and destroy Activity and return to the previous one.
                if(mEANNotFound){
                    Toast toast = Toast.makeText(getApplicationContext(), R.string.toast_ean_doesnt_exists, Toast.LENGTH_LONG);
                    toast.setGravity(Gravity.TOP | Gravity.CENTER_HORIZONTAL, 0, 20);
                    toast.show();
                    onBackPressed();
                }

                // If EAN was found and a Product was returned.
                if (result != null) {

                    Bundle bundle = new Bundle();
                    bundle.putParcelable(PARCELABLE_PRODUCT_TAG, result);
                    bundle.putBoolean(INVENTORY_LAYOUT_TAG, true);

                    // Create an instance of InventoryViewFragment and add the bundle.
                    InventoryViewFragment mInventoryViewFragment = new InventoryViewFragment();
                    mInventoryViewFragment.setArguments(bundle);

                    // Get FragmentManager,replace whatever is in the container with a InventoryViewFragment.
                    wrActivity.get().getSupportFragmentManager()
                            .beginTransaction()
                            .replace(R.id.fragment_inventory_view_container, mInventoryViewFragment, INVENTORY_VIEW_FRAGMENT_TAG)
                            .commit();

                    // If an Inventory has been made.
                    if (mWaitingForInventoryUpdate) {
                        mWaitingForInventoryUpdate = false;
                        Toast toast = Toast.makeText(getApplicationContext(), R.string.toast_inventory_finished, Toast.LENGTH_LONG);
                        toast.setGravity(Gravity.TOP | Gravity.CENTER_HORIZONTAL, 0, 20);
                        toast.show();
                    }
                }
            }
        }
    }
}

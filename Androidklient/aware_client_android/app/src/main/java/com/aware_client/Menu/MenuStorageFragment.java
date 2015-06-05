package com.aware_client.Menu;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;
import com.aware_client.R;
import com.aware_client.Settings.SettingsActivity;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.Socket;



/**
 * Created by andreaslengqvist on 15-04-07.
 *
 * MenuStorageFragment handles menu choices in the StorageMenu.
 *
 */
public class MenuStorageFragment extends Fragment {

    // Static Name variables.
    public static final String SERVER_ERROR = "SERVER_ERROR";

    // Shared Preference Static variables.
    public static final String APP_PREFERENCES = "APP_PREFERENCES" ;
    public static final String SERVER_IP = "SERVER_IP";
    public static final String SERVER_PORT = "SERVER_PORT";
    public static final String SERVER_PW = "SERVER_PW";
    public static final String UPDATE_FREQ = "UPDATE_FREQ";

    // Layout variables.
    private TextView output_total_products;
    private TextView output_total_quantity;
    private Button btn_storage_menu_products;
    private Button btn_storage_menu_inventory;
    private Button btn_inventory_menu_fast;
    private Button btn_inventory_menu_full;
    private Button btn_inventory_menu_cancel;

    // Member variables.
    private View mView;
    private MenuListener mCallback;
    public GetTotalProducts gtp;

    // Shared Preference variables.
    private String mServerIp;
    private String mServerPort;
    private String mServerPw;
    private Integer mUpdateFreq;


    /**
     * From onCreate
     *
     * Basically initialize all elements from the XML-layout (res/layout/fragment_menu_storage.xml).
     */
    private void initializeVariables() {

        // Set saved preferences.
        SharedPreferences sharedpreferences = getActivity().getSharedPreferences(APP_PREFERENCES, Context.MODE_PRIVATE);
        mServerIp = sharedpreferences.getString(SERVER_IP, "");
        mServerPort = sharedpreferences.getString(SERVER_PORT, "");
        mServerPw = sharedpreferences.getString(SERVER_PW, "");
        mUpdateFreq = sharedpreferences.getInt(UPDATE_FREQ, 10000);

        // Set GUI-components.
        output_total_products = (TextView) mView.findViewById(R.id.output_total_products);
        output_total_quantity = (TextView) mView.findViewById(R.id.output_total_quantity);

        btn_storage_menu_products = (Button) mView.findViewById(R.id.btn_storage_menu_products);
        btn_storage_menu_inventory = (Button) mView.findViewById(R.id.btn_storage_menu_inventory);
        btn_inventory_menu_fast = (Button) mView.findViewById(R.id.btn_inventory_menu_fast);
        btn_inventory_menu_full = (Button) mView.findViewById(R.id.btn_inventory_menu_full);
        btn_inventory_menu_cancel = (Button) mView.findViewById(R.id.btn_inventory_menu_cancel);
    }


    /**
     * Called when this Fragment is being created.
     *
     * This makes sure that the container activity has implemented
     * the callback interface. If not, it throws an exception
     */
    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);
        try {
            mCallback = (MenuListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString()
                    + " must implement OnProductSelectedListener");
        }
    }


    @Override
    /**
     * Called when this Fragment is being created.
     *
     * Basically just do thing that needs to be done upon creation.
     * In this case, sets the Fragment to retain its instance upon Configuration changes.
     *
     * @param savedInstanceState saved data from a Configuration change
     */
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setRetainInstance(true);
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
        mView = inflater.inflate(R.layout.fragment_menu_storage, container, false);
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

        gtp = new GetTotalProducts();
        gtp.execute();

        // Menu choice - "Products".
        btn_storage_menu_products.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                mCallback.onMenuProducts();
            }
        });


        // Menu choice - "Inventory".
        btn_storage_menu_inventory.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                btn_inventory_menu_fast.setVisibility(View.VISIBLE);
                btn_inventory_menu_cancel.setVisibility(View.VISIBLE);
                btn_inventory_menu_full.setVisibility(View.VISIBLE);
                btn_storage_menu_inventory.setVisibility(View.GONE);
            }
        });


        // Menu choice - "Cancel" in "Inventory".
        btn_inventory_menu_cancel.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                btn_storage_menu_inventory.setVisibility(View.VISIBLE);
                btn_inventory_menu_fast.setVisibility(View.GONE);
                btn_inventory_menu_cancel.setVisibility(View.GONE);
                btn_inventory_menu_full.setVisibility(View.GONE);
            }
        });


        // Menu choice - "Fast" in "Inventory".
        btn_inventory_menu_fast.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                mCallback.onMenuInventoryFast();
            }
        });


        // Menu choice - "Full" in "Inventory".
        btn_inventory_menu_full.setOnClickListener(new View.OnClickListener() {

            @Override
            public void onClick(View v) {
                mCallback.onMenuInventoryFull();
            }
        });
    }


    @Override
    /**
     * Called when Activity paused. (e.g sleep-mode, user heads to another app, etc.)
     *
     * Cancels all running AsyncTasks.
     */
    public void onPause() {
        super.onPause();
        if(gtp != null && gtp.getStatus() == AsyncTask.Status.RUNNING)
            gtp.cancel(true);
    }


    /**
     * AsyncTask which will run in the background and fetch the total number of Products.
     */
    private class GetTotalProducts extends AsyncTask<Void, Void, Boolean> {

        private Boolean connectionLost = false;

        @Override
        protected Boolean doInBackground(Void... arg0) {

            try {

                // Establish a Socket-Connection.
                Socket socket = new Socket();
                socket.connect( new InetSocketAddress(InetAddress.getByName(mServerIp), Integer.parseInt(mServerPort)), mUpdateFreq);

                // If socket has established a connection to the server.
                if (socket.isConnected()) {

                    connectionLost = false;

                    // Create a PrintWriter to write to the Server with a GET.
                    PrintWriter out = new PrintWriter(socket.getOutputStream());

                    // Write a GET-method to the Server.
                    out.println("GET/products/count/pw=" + mServerPw);
                    out.flush();

                    // Get Products by using a InputStreamReader wrapped in a BufferedReader to read JSON as a String.
                    String fetchedData = new BufferedReader(new InputStreamReader(socket.getInputStream())).readLine();

                    if(fetchedData.equals("WRONG PASSWORD")){
                        Intent settingsActivity = new Intent(getActivity().getApplicationContext(), SettingsActivity.class);
                        settingsActivity.putExtra(SERVER_ERROR, fetchedData);
                        startActivity(settingsActivity);
                        return false;
                    }

                    // Get Number of Products by using a InputStreamReader wrapped in a BufferedReader to read JSON as a String.
                    output_total_products.setText(fetchedData);

                    // Close the connection.
                    socket.close();
                }
                return true;

            } catch (IOException e) {
                connectionLost = true;
                return false;
            }
        }

        @Override
        protected void onPostExecute(Boolean completed) {
            super.onPostExecute(completed);

            if(connectionLost){
                Intent settingsActivity = new Intent(getActivity().getApplicationContext(), SettingsActivity.class);
                settingsActivity.putExtra(SERVER_ERROR, "NO SERVER WAS FOUND");
                startActivity(settingsActivity);
            } else {
                if (completed) {
                    new GetTotalQuantity().execute();
                }
            }
        }
    }


    /**
     * AsyncTask which will run in the background and fetch the total number of Products.
     */
    private class GetTotalQuantity extends AsyncTask<Void, Void, Boolean> {

        @Override
        protected Boolean doInBackground(Void... arg0) {

            try {

                // Establish a Socket-Connection.
                Socket socket = new Socket();
                socket.connect( new InetSocketAddress(InetAddress.getByName(mServerIp), Integer.parseInt(mServerPort)), mUpdateFreq);

                // If socket has established a connection to the server.
                if (socket.isConnected()) {

                    // Create a PrintWriter to write to the Server with a GET.
                    PrintWriter out = new PrintWriter(socket.getOutputStream());

                    // Write a GET-method to the Server.
                    out.println("GET/products/quantitysum/pw=" + mServerPw);
                    out.flush();

                    // Get Total quantity by using a InputStreamReader wrapped in a BufferedReader to read JSON as a String.
                    output_total_quantity.setText(new BufferedReader(new InputStreamReader(socket.getInputStream())).readLine());

                    // Close the connection.
                    socket.close();
                }
                return true;

            } catch (IOException e) {
                return false;
            }
        }

        @Override
        protected void onPostExecute(Boolean completed) {
            super.onPostExecute(completed);
            if(completed) {
                mCallback.onServerConnected();
                btn_storage_menu_inventory.setEnabled(true);
                btn_storage_menu_products.setEnabled(true);
                mView.findViewById(R.id.layout_status_bar).setVisibility(View.GONE);
                mView.findViewById(R.id.layout_storage_info).setVisibility(View.VISIBLE);
            }
        }
    }
}
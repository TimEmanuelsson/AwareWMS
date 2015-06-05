package com.aware_client.Storage;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.res.Configuration;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.RelativeLayout;

import com.aware_client.Helpers.FragmentIntentIntegrator;
import com.aware_client.Settings.SettingsActivity;
import com.aware_client.R;
import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;
import com.google.zxing.integration.android.IntentIntegrator;
import com.google.zxing.integration.android.IntentResult;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.InetAddress;
import java.net.InetSocketAddress;
import java.net.Socket;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;



/**
 *
 * Created by andreaslengqvist on 15-04-14.
 *
 * ProductListFragment which loads and processes a List of Products.
 *
 */
public class ProductListFragment extends Fragment {

    // Static Name variables.
    private static final String INTENT_KEY = "INTENT_KEY";
    private static final String ACTIVITY_INVENTORY_FULL = "ACTIVITY_INVENTORY_FULL";

    // Shared Preference Static variables.
    public static final String APP_PREFERENCES = "APP_PREFERENCES" ;
    public static final String SERVER_IP = "SERVER_IP";
    public static final String SERVER_PORT = "SERVER_PORT";
    public static final String SERVER_PW = "SERVER_PW";
    public static final String DAYS_UNDER = "DAYS_UNDER";
    public static final String DAYS_OVER = "DAYS_OVER";
    public static final String UPDATE_FREQ = "UPDATE_FREQ";

    // Layout variables.
    private ListView list_products;
    private RelativeLayout sort_storage_space;
    private RelativeLayout sort_name;
    private RelativeLayout sort_last_inventory;
    private RelativeLayout sort_sku;
    private RelativeLayout sort_quantity;
    private ImageView icon_sort_storage_space;
    private ImageView icon_sort_name;
    private ImageView icon_sort_last_inventory;
    private ImageView icon_sort_sku;
    private ImageView icon_sort_quantity;
    private RelativeLayout layout_dimmer;
    private RelativeLayout progress_loading_list;

    // Member variables.
    private View mView;
    private Handler mHandler;
    private ProductListAdapter mAdapter;
    private ProductListListener mCallback;
    private ArrayList<Product> mProducts;
    private Product mSelectedProduct;
    public boolean doServerConnectionLostOnce = true;
    public boolean mWaitingForProductUpdate;
    public boolean mWaitingForInventoryUpdate;
    public boolean mInsideSearch;
    private GetProducts gp;

    // Sorting variables.
    private Comparator<Product> currentSort;
    private boolean sortASC = false;

    // Current JSON.
    private String oldJSON;

    // Shared Preference variables.
    private SharedPreferences sharedpreferences;
    private String mServerIp;
    private String mServerPort;
    private String mServerPw;
    private Integer mUpdateFreq;


    /**
     * From onCreate
     *
     * Basically initialize all elements from the XML-layout (res/layout/activity_products.xml).
     */
    private void initializeVariables() {

        // Set saved preferences.
        mServerIp = sharedpreferences.getString(SERVER_IP, "");
        mServerPort = sharedpreferences.getString(SERVER_PORT, "");
        mServerPw = sharedpreferences.getString(SERVER_PW, "");
        mUpdateFreq = sharedpreferences.getInt(UPDATE_FREQ, 10000);

        // Set GUI-components.
        list_products = (ListView) mView.findViewById(R.id.list_products);
        list_products.setEmptyView(mView.findViewById(R.id.empty_list));

        sort_name = (RelativeLayout) mView.findViewById(R.id.sort_name);
        sort_storage_space = (RelativeLayout) mView.findViewById(R.id.sort_storage_space);
        sort_last_inventory = (RelativeLayout) mView.findViewById(R.id.sort_last_inventory);
        sort_sku = (RelativeLayout) mView.findViewById(R.id.sort_sku);
        sort_quantity = (RelativeLayout) mView.findViewById(R.id.sort_quantity);

        icon_sort_name = (ImageView) mView.findViewById(R.id.icon_sort_name);
        icon_sort_storage_space = (ImageView) mView.findViewById(R.id.icon_sort_storage_space);
        icon_sort_last_inventory = (ImageView) mView.findViewById(R.id.icon_sort_last_inventory);
        icon_sort_sku = (ImageView) mView.findViewById(R.id.icon_sort_sku);
        icon_sort_quantity = (ImageView) mView.findViewById(R.id.icon_sort_quantity);

        layout_dimmer = (RelativeLayout) mView.findViewById(R.id.layout_dimmer);
        progress_loading_list = (RelativeLayout) mView.findViewById(R.id.progress_loading_list);
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
            mCallback = (ProductListListener) activity;
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
        mView = inflater.inflate(R.layout.fragment_products_list, container, false);
        sharedpreferences = getActivity().getSharedPreferences(APP_PREFERENCES, Context.MODE_PRIVATE);
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



        // If coming from new state. (e.g NOT screen rotation)
        // Start Handler to get Products periodically on background thread (AsyncTask).
        if (savedInstanceState == null) {

            progress_loading_list.setVisibility(View.VISIBLE);

            Bundle bundle = getArguments();
            String mTypeOfActivity = bundle.getString(INTENT_KEY);

            mProducts = new ArrayList<>();

            if(mTypeOfActivity.equals(ACTIVITY_INVENTORY_FULL)) {
                mAdapter = new ProductListAdapter(
                        getActivity(),
                        mTypeOfActivity,
                        sharedpreferences.getInt(DAYS_UNDER, 5),
                        sharedpreferences.getInt(DAYS_OVER, 365));
            } else {
                mAdapter = new ProductListAdapter(
                        getActivity(),
                        mTypeOfActivity,
                        0,
                        0);
            }

            mHandler = new Handler();
            startPeriodically();
        }


        // Else coming from already saved state.
        // setList from RetainInstanceState.
        else {
            setList(mProducts);
        }


        list_products.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View item, int position, long id) {

                // Deselect item in list.
                if (mAdapter.getSelectedPosition() == position) {
                    deselectList();
                    mCallback.onProductDeSelected();
                }

                // Select item in list.
                else {
                    mAdapter.setSelectedPosition(-2);
                    selectList(position);
                    mCallback.onProductSelected(position);
                }
            }
        });


        // Sort items - LastInventory.
        sort_last_inventory.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setSort(
                        icon_sort_last_inventory,
                        Product.ProductLastInventoryComparatorASC,
                        Product.ProductLastInventoryComparatorDESC
                );
            }
        });


        // Sort items - StorageSpace.
        sort_storage_space.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setSort(
                        icon_sort_storage_space,
                        Product.ProductStorageSpaceComparatorASC,
                        Product.ProductStorageSpaceComparatorDESC
                );
            }
        });


        // Sort items - Name.
        sort_name.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setSort(
                        icon_sort_name,
                        Product.ProductNameComparatorASC,
                        Product.ProductNameComparatorDESC
                );
            }
        });


        // Sort items - SKU.
        sort_sku.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setSort(
                        icon_sort_sku,
                        Product.ProductSKUComparatorASC,
                        Product.ProductSKUComparatorDESC
                );
            }
        });


        // Sort items - Quantity.
        sort_quantity.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setSort(
                        icon_sort_quantity,
                        Product.ProductQuantityComparatorASC,
                        Product.ProductQuantityComparatorDESC
                );
            }
        });


        // Background dimmer when inside SearchBar.
        layout_dimmer.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mCallback.onDoneSearching();
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

        // Receive the scanned EAN and search to find the Product.
        IntentResult scanResult = IntentIntegrator.parseActivityResult(requestCode, resultCode, intent);
        if (scanResult.getContents() != null) {
            mCallback.onScannedForEAN(scanResult.getContents());
        }
    }


    /**
     * From ProductListActivity - onOptionsItemSelected
     *
     * Called when user clicked Search on EAN (Barcode).
     * Starts the Barcode scanner.
     */
    public void searchOnEAN() {
        FragmentIntentIntegrator integrator = new FragmentIntentIntegrator(ProductListFragment.this);
        // Create an Integrator which is used to initiate the scanner.
        if(getResources().getConfiguration().orientation == Configuration.ORIENTATION_PORTRAIT) {
            integrator.setOrientation(90);
        }
        integrator.initiateScan();
    }


    /**
     * From ProductListActivity - SearchWatcher / onProductsUpdated
     *
     * Called when a search query have changed upon a Search.
     * Replaces the Adapters list with a new filtered version.
     */
    public void filterAdapter(ArrayList<Product> products) {

        // If a sorting is made. Sort it.
        if (currentSort != null) {
            Collections.sort(products, currentSort);
        }
        setList(products);
    }


    /**
     * From ProductListActivity - openSearchBar (onFocus)
     *
     * Called when the SearchBar gets focus.
     * Deselects the Product in the ListView and alerts the Fragment that the SearchBar has focus.
     */
    public void openSearch() {
        mCallback.onProductDeSelected();
        deselectList();
        mInsideSearch = true;
    }


    /**
     * From ProductListActivity - cancelSearchBar (noFocus)
     *
     * Called when the SearchBar loses its focus.
     * Remembers the selected searched Products position.
     */
    public void setSearchedProduct() {

        int position = mAdapter.getSelectedPosition();
        if(position != -1) {
            mSelectedProduct = mAdapter.getItem(mAdapter.getSelectedPosition());
        }
    }


    /**
     * From ProductListActivity - cancelSearchBar (noFocus)
     *
     * Called when the SearchBar loses its focus and a unfiltered list has been deployed to the Adapter.
     *
     * @return mSelectedProduct that were selected before the search was cancelled.
     */
    public int getSearchedProduct() {
        return mAdapter.getPosition(mSelectedProduct);
    }


    /**
     * From onCreate / filterAdapter / sortList / onPostExecute (AsyncTask - GetProducts)
     *
     * Called when the list is ready to be added to the Adapter.
     *
     * @param result list of products
     */
    private void setList(ArrayList<Product> result){

        // When a new ArrayList of Products is fetched.
        if(result != null) {
            mProducts = result;
        }

        // If there is NO Adapter set to the list. Set it.
        if(list_products.getAdapter() == null) {
            list_products.setAdapter(mAdapter);
        }

        // If there is a change, don't destroy the scroll position.
        mAdapter.setNotifyOnChange(false);

        // Clear the Adapter from items.
        mAdapter.clear();

        // Add all Products to the Adapter.
        mAdapter.addAll(mProducts);

        // Notify the Adapter that data has changed.
        mAdapter.notifyDataSetChanged();

        // Notify the Activity that the Adapter has Products.
        mCallback.onProductsAddedToAdapter(mProducts, mAdapter.getSelectedPosition());
    }


    /**
     * From openSearch / Deselect item (OnListItemClickListener) / onBackPressed (insideProduct)
     *
     * Called when the ListView needs to be deselected.
     * Sets the selected position to -1 which in this case is no item selected.
     * And notifies the Adapter.
     */
    public void deselectList() {
        mAdapter.setSelectedPosition(-1);
        mAdapter.notifyDataSetChanged();
    }


    /**
     * From Select item (OnListItemClickListener) / onPageSelected (ViewPager)
     *
     * Called when the ListView needs to be selected.
     * Sets the selected position to the correct position and notifies the Adapter.
     * Also scrolls to the correct position in the ListView.
     *
     * @param position selected position
     */
    public void selectList(int position) {
        if(mAdapter.getSelectedPosition() != -1) {
            mAdapter.setSelectedPosition(position);
            mAdapter.notifyDataSetChanged();
            list_products.smoothScrollToPosition(position);
        }
    }


    /**
     * From setSort
     *
     * Called when the list is ready to sort.
     * Sorts the list with the correct sorting and sets the new sorted list to the Adapter.
     *
     * @param sort type of sorting
     */
    private void sortList(Comparator<Product> sort) {
        Collections.sort(mProducts, sort);
        setList(mProducts);
    }


    /**
     * From LastInventory / StorageSpace / Name / SKU / Quantity - OnClickListener
     *
     * Called when users clicked one of the sorting functions.
     * Setups the sorting.
     *
     * @param column which column to sort
     * @param asc sort ascending
     * @param desc sort descending
     */
    private void setSort(ImageView column, Comparator<Product> asc, Comparator<Product> desc) {

        if (!sortASC) {
            sortASC = true;
            currentSort = asc;
            sortList(asc);
            column.setImageDrawable(getResources().getDrawable(R.drawable.ic_asc));
        } else {
            sortASC = false;
            currentSort = desc;
            sortList(desc);
            column.setImageDrawable(getResources().getDrawable(R.drawable.ic_desc));
        }

        resetSortVisibility();
        column.setVisibility(View.VISIBLE);
    }


    /**
     * From setSort
     *
     * Called before the sorted column is set to Visible.
     * Resets all the columns to Invisible.
     */
    private void resetSortVisibility(){
        icon_sort_name.setVisibility(View.INVISIBLE);
        icon_sort_storage_space.setVisibility(View.INVISIBLE);
        icon_sort_last_inventory.setVisibility(View.INVISIBLE);
        icon_sort_sku.setVisibility(View.INVISIBLE);
        icon_sort_quantity.setVisibility(View.INVISIBLE);
    }


    /**
     * From ProductListActivity - onInsideInventory / openSearchBar (SearchBar onFocus)
     *
     * Called before the sorted column is set to Visible.
     * Resets all the columns to Invisible.
     */
    public void lockList(boolean clickable) {
        layout_dimmer.setEnabled(clickable);
        layout_dimmer.setVisibility(View.VISIBLE);
        sort_storage_space.setEnabled(false);
        sort_name.setEnabled(false);
        sort_sku.setEnabled(false);
        sort_quantity.setEnabled(false);
        list_products.setEnabled(false);

    }


    /**
     * From ProductListActivity - onInsideInventory / onBackPressed / openSearchBar (SearchBar No Focus)
     *
     * Called before the sorted column is set to Visible.
     * Resets all the columns to Invisible.
     */
    public void unlockList() {
        layout_dimmer.setVisibility(View.GONE);
        sort_storage_space.setEnabled(true);
        sort_name.setEnabled(true);
        sort_sku.setEnabled(true);
        sort_quantity.setEnabled(true);
        list_products.setEnabled(true);
    }


    /**
     * From onCreate and ProductListActivity - onResume
     *
     * Called when activity is created or resumed.
     * Posts runPeriodically to the Handler.
     */
    public void startPeriodically() {
        mHandler.post(runPeriodically);
    }


    /**
     * From ProductListActivity - onPaused / onDestroy
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
     * Called when the Handler wants to run the periodically AsyncTask GetProducts.
     * Runs a instance of GetProducts each 2000ms (delayed).
     */
    private Runnable runPeriodically = new Runnable() {

        @Override
        public void run() {
            gp = new GetProducts();
            gp.execute();
            mHandler.postDelayed(runPeriodically, 1000);
        }
    };


    @Override
    /**
     * Called when Activity paused. (e.g sleep-mode, user heads to another app, etc.)
     *
     * Cancels all running AsyncTasks.
     */
    public void onPause() {
        super.onPause();
        if(gp != null && gp.getStatus() == AsyncTask.Status.RUNNING)
            gp.cancel(true);
    }


    /**
     * AsyncTask which will run in the background and fetch a JSON of Products.
     * If the new Products is the same as the old Products no changes have been made and NO need to replace the old Products.
     */
    private class GetProducts extends AsyncTask<Void, Void, ArrayList<Product>> {

        private String newJSON;
        private Boolean connectionLost;


        @Override
        protected ArrayList<Product> doInBackground(Void... arg0) {

            try {
                connectionLost = false;

                // Establish a Socket-Connection.
                Socket socket = new Socket();
                socket.connect( new InetSocketAddress(InetAddress.getByName(mServerIp), Integer.parseInt(mServerPort)), mUpdateFreq);

                // If socket has established a connection to the server.
                if (socket.isConnected()) {


                    // Create a PrintWriter to write to the Server with a GET.
                    PrintWriter out = new PrintWriter(socket.getOutputStream());

                    // Write a GET-method to the Server.
                    out.println("GET/products/pw=" + mServerPw);
                    out.flush();

                    // Get Products by using a InputStreamReader wrapped in a BufferedReader to read JSON as a String.
                    newJSON = new BufferedReader(new InputStreamReader(socket.getInputStream())).readLine();

                    // Close the connection.
                    socket.close();
                }

                // If the new JSON-object NOT equals to the old, return the new one.
                if (!newJSON.equals(oldJSON)) {
                    oldJSON = newJSON;
                    return new Gson().fromJson(newJSON, new TypeToken<List<Product>>() {}.getType());
                }
                // Else, there has not been any changes in the database. Retain the old one.
                else { return null; }

            } catch (IOException e) {
                connectionLost = true;
                return null;
            }
        }

        @Override
        protected void onPostExecute(ArrayList<Product> result) {
            super.onPostExecute(result);


            if(connectionLost){
                if(doServerConnectionLostOnce) {
                    doServerConnectionLostOnce = false;
                    Intent settingsActivity = new Intent(getActivity().getApplicationContext(), SettingsActivity.class);
                    settingsActivity.putExtra("SERVER_ERROR", "NO SERVER FOUND");
                    startActivity(settingsActivity);
                }
            } else {

                // If there is an result. (e.g JSON-object has changed.)
                // Replace the old list with the new list.
                if (result != null) {

                    // If a sorting is made. Sort it.
                    if (currentSort != null) {
                        Collections.sort(result, currentSort);
                    }

                    // If inside the SearchBar. Update the searched list.
                    if (mInsideSearch) {
                        mCallback.onProductsUpdated(result);
                    }

                    // Else unlock the ListView and set the new list of products.
                    else {
                        unlockList();
                        setList(result);
                    }

                    progress_loading_list.setVisibility(View.GONE);
                }

                // If an ProductUpdate has been made.
                if (mWaitingForProductUpdate) {
                    mWaitingForProductUpdate = false;
                    mCallback.onProductUpdateFinished(false);
                }

                // If an Inventory has been made.
                if (mWaitingForInventoryUpdate) {
                    mWaitingForInventoryUpdate = false;
                    mCallback.onProductUpdateFinished(true);
                }
            }
        }
    }
}
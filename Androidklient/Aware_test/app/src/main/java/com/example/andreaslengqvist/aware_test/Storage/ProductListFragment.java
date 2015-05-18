package com.example.andreaslengqvist.aware_test.Storage;

import android.app.Activity;
import android.os.AsyncTask;
import android.os.Bundle;
import android.os.Handler;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.RelativeLayout;
import com.example.andreaslengqvist.aware_test.Connection.Connection;
import com.example.andreaslengqvist.aware_test.R;
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

    private static final String INTENT_KEY = "INTENT_KEY";

    private ListView list_products;

    private RelativeLayout sort_storagespace;
    private RelativeLayout sort_name;
    private RelativeLayout sort_last_inventory;
    private RelativeLayout sort_sku;
    private RelativeLayout sort_balance;

    private ImageView icon_sort_storagespace;
    private ImageView icon_sort_name;
    private ImageView icon_sort_last_inventory;
    private ImageView icon_sort_sku;
    private ImageView icon_sort_balance;

    private RelativeLayout layout_dimmer;


    private View mView;
    private Handler mHandler;
    private ProductListAdapter mAdapter;
    private ProductListListener mCallback;
    private ArrayList<Product> mProducts;

    private Comparator<Product> currentSort;

    private boolean sortASC = false;

    private String oldJSON;

    public boolean mWaitingOnUpdate;
    public boolean insideSearch;
    private Product selectedProduct;


    private void initializeVariables() {
        list_products = (ListView) mView.findViewById(R.id.list_products);
        list_products.setEmptyView(mView.findViewById(R.id.progress_loading_list));

        sort_name = (RelativeLayout) mView.findViewById(R.id.sort_name);
        sort_storagespace = (RelativeLayout) mView.findViewById(R.id.sort_storagespace);
        sort_last_inventory = (RelativeLayout) mView.findViewById(R.id.sort_last_inventory);
        sort_sku = (RelativeLayout) mView.findViewById(R.id.sort_sku);
        sort_balance = (RelativeLayout) mView.findViewById(R.id.sort_balance);

        icon_sort_name = (ImageView) mView.findViewById(R.id.icon_sort_name);
        icon_sort_storagespace = (ImageView) mView.findViewById(R.id.icon_sort_storagespace);
        icon_sort_last_inventory = (ImageView) mView.findViewById(R.id.icon_sort_last_inventory);
        icon_sort_sku = (ImageView) mView.findViewById(R.id.icon_sort_sku);
        icon_sort_balance = (ImageView) mView.findViewById(R.id.icon_sort_balance);

        layout_dimmer = (RelativeLayout) mView.findViewById(R.id.layout_dimmer);
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);

        // This makes sure that the container activity has implemented
        // the callback interface. If not, it throws an exception
        try {
            mCallback = (ProductListListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString()
                    + " must implement OnProductSelectedListener");
        }
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setRetainInstance(true);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        mView = inflater.inflate(R.layout.fragment_products_list, container, false);
        return mView;
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        initializeVariables();


        // If coming from new state. (e.g NOT screen rotation)
        // Start Handler to get Products periodically on background thread (AsyncTask).
        if (savedInstanceState == null) {

            Bundle bundle = getArguments();
            String mTypeOfActivity = bundle.getString(INTENT_KEY);

            mProducts = new ArrayList<>();
            mAdapter = new ProductListAdapter(getActivity(), mTypeOfActivity);

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


        // Sort items - StorageSpace.
        sort_storagespace.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setSort(
                        icon_sort_storagespace,
                        Product.ProductStorageSpaceComparatorASC,
                        Product.ProductStorageSpaceComparatorDESC
                );
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


        // Sort items - Balance.
        sort_balance.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setSort(
                        icon_sort_balance,
                        Product.ProductBalanceComparatorASC,
                        Product.ProductBalanceComparatorDESC
                );
            }
        });

        layout_dimmer.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                mCallback.onCloseSearch();
            }
        });
    }

    public void filterAdapter(ArrayList<Product> products) {
        if (currentSort != null) {
            Collections.sort(products, currentSort);
        }
        setList(products);
    }

    public void openSearch() {
        mCallback.onProductDeSelected();
        deselectList();
        insideSearch = true;
    }

    public void setSearchedProduct() {

        int position = mAdapter.getSelectedPosition();
        Log.d("asd", Integer.toString(position));
        if(position != -1) {
            selectedProduct = mAdapter.getItem(mAdapter.getSelectedPosition());
        }
    }

    public int getSearchedProduct() {
        return mAdapter.getPosition(selectedProduct);
    }

    private void setList(ArrayList<Product> result){

        // When a new ArrayList of Products is fetched.
        if(result != null) {
            mProducts = result;
        }

        list_products.setAdapter(mAdapter);
        mAdapter.setNotifyOnChange(false);
        mAdapter.clear();
        mAdapter.addAll(mProducts);
        mAdapter.notifyDataSetChanged();

        mCallback.onProductListLoaded(mProducts, mAdapter.getSelectedPosition());
    }

    private void refreshList(){
        mAdapter.setNotifyOnChange(false);
        mAdapter.clear();
        mAdapter.addAll(mProducts);
        mAdapter.notifyDataSetChanged();
    }

    public void deselectList() {
        mAdapter.setSelectedPosition(-1);
        mAdapter.notifyDataSetChanged();
    }

    public void selectList(int position) {
        if(mAdapter.getSelectedPosition() != -1) {
            mAdapter.setSelectedPosition(position);
            mAdapter.notifyDataSetChanged();
            list_products.smoothScrollToPosition(position);
        }
    }

    private void sortList(Comparator<Product> sort) {
        Collections.sort(mProducts, sort);
        refreshList();
        mCallback.onProductListLoaded(mProducts, mAdapter.getSelectedPosition());
    }

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

    private void resetSortVisibility(){
        icon_sort_name.setVisibility(View.INVISIBLE);
        icon_sort_storagespace.setVisibility(View.INVISIBLE);
        icon_sort_last_inventory.setVisibility(View.INVISIBLE);
        icon_sort_sku.setVisibility(View.INVISIBLE);
        icon_sort_balance.setVisibility(View.INVISIBLE);
    }

    public void lockList(boolean clickable) {
        layout_dimmer.setEnabled(clickable);
        layout_dimmer.setVisibility(View.VISIBLE);
        sort_storagespace.setEnabled(false);
        sort_name.setEnabled(false);
        sort_sku.setEnabled(false);
        sort_balance.setEnabled(false);
        list_products.setEnabled(false);

    }

    public void unlockList() {
        layout_dimmer.setVisibility(View.GONE);
        sort_storagespace.setEnabled(true);
        sort_name.setEnabled(true);
        sort_sku.setEnabled(true);
        sort_balance.setEnabled(true);
        list_products.setEnabled(true);
    }

    public void startPeriodically() {
        mHandler.post(runPeriodically);
    }

    public void stopPeriodically() {
        mHandler.removeCallbacks(runPeriodically);
    }

    private Runnable runPeriodically = new Runnable() {

        @Override
        public void run() {
            new GetProducts().execute();
        mHandler.postDelayed(runPeriodically, 1000);
        }
    };


    /**
     *
     * AsyncTask which will run in the background and fetch a ArrayList of Products.
     * If the new ArrayList is the same as the old one no changes have been made and NO need to replace the old one.
     *
     */
    private class GetProducts extends AsyncTask<Void, Void, ArrayList<Product>> {

        private Socket socket;
        private String newJSON;


        @Override
        protected ArrayList<Product> doInBackground(Void... arg0) {

            try {

                // Establish a Socket-Connection.
                Connection OC = new Connection();
                socket = OC.establish();

                // Create a PrintWriter to write to the Server with a GET.
                PrintWriter out = new PrintWriter(socket.getOutputStream());

                // Write a GET-method to the Server.
                out.println("GET/products");
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
            if(oldJSON != null) {

                // And the new JSON-object NOT equals to the old, return the new one.
                if (!newJSON.equals(oldJSON)) {
                    oldJSON = newJSON;
                    return new Gson().fromJson(newJSON, new TypeToken<List<Product>>() {}.getType());
                }
                // Else, there has not been any changes in the database. Retain the old one.
                else { return null; }
            }

            // Else, this is the first GET.
            else {
                oldJSON = newJSON;
                return new Gson().fromJson(newJSON, new TypeToken<List<Product>>() {}.getType());
            }
        }

        @Override
        protected void onPostExecute(ArrayList<Product> result) {
            super.onPostExecute(result);

            // If there is an result. (e.g JSON-object has changed.)
            // Replace the old list with the new list.
            if(result != null) {

                if (currentSort != null) {
                    Collections.sort(result, currentSort);
                }

                if(insideSearch) {
                    mCallback.onListUpdatedInsideSearch(result);
                }
                else {
                    unlockList();
                    setList(result);
                }
            }

            if(mWaitingOnUpdate) {
                mWaitingOnUpdate = false;
                mCallback.onInventoryFinished();
            }
        }
    }
}
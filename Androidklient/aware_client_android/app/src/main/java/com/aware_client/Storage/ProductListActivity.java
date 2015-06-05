package com.aware_client.Storage;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.ActivityInfo;
import android.support.v4.app.Fragment;
import android.os.Bundle;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentStatePagerAdapter;
import android.support.v4.view.PagerAdapter;
import android.support.v4.view.ViewPager;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarActivity;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.Gravity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.Toast;
import com.aware_client.Helpers.ProductPager;
import com.aware_client.Helpers.ZoomOutPageTransformer;
import com.aware_client.R;
import com.aware_client.Storage.Inventory.InventoryViewFragment;
import com.aware_client.Storage.Products.ProductViewFragment;
import java.util.ArrayList;



/**
 * Created by andreaslengqvist on 15-04-13.
 *
 * ProductListActivity handles ProductListFragment, ProductPagerFragment, ProductViewFragment / InventoryViewFragment
 * and all communications between them (ProductListListener/ProductListener).
 *
 */
public class ProductListActivity extends ActionBarActivity implements ProductListListener, ProductListener {

    // Static Name variables.
    private static final String INTENT_KEY = "INTENT_KEY";

    private static final String ACTIVITY_INVENTORY_FULL = "ACTIVITY_INVENTORY_FULL";
    private static final String ACTIVITY_PRODUCTS = "ACTIVITY_PRODUCTS";

    private static final String PRODUCT_LIST_FRAGMENT_TAG = "PRODUCT_LIST_FRAGMENT_TAG";
    private static final String PARCELABLE_PRODUCT_LIST_TAG = "PARCELABLE_PRODUCT_LIST_TAG";
    private static final String PARCELABLE_PRODUCT_TAG = "PARCELABLE_PRODUCT_TAG";

    private static final String UNFILTERED_PRODUCTS = "UNFILTERED_PRODUCTS";
    private static final String SEARCH_OPENED = "SEARCH_OPENED";
    private static final String SEARCH_QUERY = "SEARCH_QUERY";

    public static final String APP_PREFERENCES = "APP_PREFERENCES" ;
    public static final String TABLET_HANDEDNESS = "TABLET_HANDEDNESS";

    // Layout variables.
    private ProductListFragment mProductListFragment;
    private FrameLayout mListContainer;

    // Pager variables.
    private ProductPager mPager;
    private int mPagerPosition;

    // ActivityType variable.
    private String mTypeOfActivity;

    // List variables.
    private ArrayList<Product> mFilteredProducts;
    private ArrayList<Product> mUnFilteredProducts;

    // Handheld device variable.
    private boolean mInsideProduct;

    // Search variables.
    private MenuItem mSearchAction;
    private MenuItem mSearchEANAction;
    private EditText mSearchEt;
    private boolean mSearchOpened;
    private String mSearchQuery;
    SharedPreferences sharedpreferences;


    /**
     * From onCreate
     *
     * Basically initialize all elements from the XML-layout (res/layout/activity_products.xml).
     */
    private void initializeVariables() {
        mListContainer = (FrameLayout) findViewById(R.id.fragment_product_list_container);
        mPager = (ProductPager) findViewById(R.id.pager);
    }


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


        // Set Home in ActionBar.
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setHomeAsUpIndicator(R.drawable.aware_logo_tiny);


        // Get Shared Preferences.
        sharedpreferences = getSharedPreferences(APP_PREFERENCES, Context.MODE_PRIVATE);


        // Check Handedness.
        if(sharedpreferences.getBoolean(TABLET_HANDEDNESS, true)) {
            setContentView(R.layout.activity_products_right);
        } else {
            setContentView(R.layout.activity_products_left);
        }


        initializeVariables();


        // Get which Activity to handle (Products / InventoryFull).
        Intent i = getIntent();
        if (i.hasExtra(INTENT_KEY)) {
            mTypeOfActivity = i.getStringExtra(INTENT_KEY);
        }


        // If coming from new state.
        if (savedInstanceState == null) {

            // If user is using a tablet or a handheld.
            if (getResources().getBoolean(R.bool.isTablet)) {
                addListFragment();
            } else {
                setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
                addListFragment();
            }
        }


        // Else coming from already created state. (e.g screen orientation)
        // Get saved data.
        else {
            mProductListFragment = (ProductListFragment) getSupportFragmentManager()
                    .findFragmentById(R.id.fragment_product_list_container);
            mUnFilteredProducts = savedInstanceState.getParcelableArrayList(UNFILTERED_PRODUCTS);
            mSearchOpened = savedInstanceState.getBoolean(SEARCH_OPENED);
            mSearchQuery = savedInstanceState.getString(SEARCH_QUERY);
        }
    }


    @Override
    /**
     * Called when this Activity is destroyed upon Configuration change (e.g Screen orientation)
     *
     * Saves the necessary data which is needed when coming back from an Configuration change.
     *
     * @param outState the Bundle in which the saved data will be stored
     */
    protected void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putParcelableArrayList(UNFILTERED_PRODUCTS, mUnFilteredProducts);
        outState.putBoolean(SEARCH_OPENED, mSearchOpened);
        outState.putString(SEARCH_QUERY, mSearchQuery);
    }


    @Override
    /**
     * Called when this Activity is being created.
     *
     * Basically just inflates the Menu.
     *
     * @param menu Menu
     *
     * @return boolean true
     */
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.search_menu, menu);
        return true;
    }


    @Override
    /**
     * Called when this Activity is being created.
     *
     * Finds the two ActionBar elements Search and Search for EAN.
     * Also checks if an SearchBar should already be opened.
     *
     * @param menu Menu
     *
     * @return boolean true
     */
    public boolean onPrepareOptionsMenu(Menu menu) {
        mSearchAction = menu.findItem(R.id.action_search);
        mSearchEANAction = menu.findItem(R.id.action_search_ean);
        mSearchEANAction.setVisible(false);

        // If inside a search.
        if (mSearchOpened) {
            openSearchBar(mSearchQuery);
        }
        return super.onPrepareOptionsMenu(menu);
    }


    @Override
    /**
     * Called when a item in the ActionBar is clicked.
     *
     * Checks which item the users clicked and either returns home / Show / Hide SearchBar or Searches on EAN.
     *
     * @param item MenuItem
     *
     * @return boolean true
     */
    public boolean onOptionsItemSelected(MenuItem item) {

        switch (item.getItemId()) {

            case android.R.id.home:
                mInsideProduct = false;
                mSearchOpened = false;
                onBackPressed();
                return true;

            case R.id.action_search:
                if (mSearchOpened) {
                    cancelSearchBar(true);
                } else {
                    openSearchBar(mSearchQuery);
                }
                return true;

            case R.id.action_search_ean:
                mProductListFragment.searchOnEAN();

            default:
                return super.onOptionsItemSelected(item);
        }
    }


    @Override
    /**
     * Called when pressing Back button.
     *
     * When using a handheld device and when inside ProductView / InventoryView the back
     * button works like an Show-function for the hidden ProductListFragment.
     * Same thing if the SearchBar is opened.
     *
     * Other, it works like an regular back button and goes back to the previous Activity (MainActivity).
     */
    public void onBackPressed() {

        if(mInsideProduct) {
            mInsideProduct = false;
            mSearchAction.setVisible(true);
            mListContainer.setVisibility(View.VISIBLE);
            mProductListFragment.unlockList();
            if(mSearchOpened) {
                mSearchEANAction.setVisible(true);
            }
            getSupportActionBar().setDisplayShowCustomEnabled(true);
            mPager.setVisibility(View.GONE);
            mProductListFragment.deselectList();
        }
        else if (mSearchOpened) {
            cancelSearchBar(true);
        }
        else {
            super.onBackPressed();
            overridePendingTransition(R.anim.pull_in_bottom, R.anim.push_out_top);
        }
    }


    @Override
    /**
     * Called when Activity destroyed.
     *
     * Stops the periodically background thread inside ProductListFragment (GetProducts).
     */
    protected void onDestroy() {
        super.onDestroy();
        mProductListFragment.stopPeriodically();
    }


    @Override
    /**
     * Called when Activity paused. (e.g sleep-mode or user heads to another app)
     *
     * Stops the periodically background thread inside ProductListFragment (GetProducts).
     */
    protected void onPause() {
        super.onPause();
        mProductListFragment.stopPeriodically();
    }


    @Override
    /**
     * Called when Activity resumed. (e.g from sleep-mode or user in again from another app)
     *
     * Stops the old periodically background thread inside ProductListFragment (GetProducts) and starts
     * a new one.
     */
    protected void onResume() {
        super.onResume();
        mProductListFragment.stopPeriodically();
        mProductListFragment.doServerConnectionLostOnce = true;
        mProductListFragment.startPeriodically();
    }


    @Override
    /**
     * From ProductsListFragment - AsyncTask (GetProducts)
     *
     * Called when inside SearchBar and a updated list is ready.
     * Replaces the old list and again, filters it with the search query.
     *
     * @param products with updated ArrayList of Products
     */
    public void onProductsUpdated(ArrayList<Product> products) {
        mUnFilteredProducts = products;
        mProductListFragment.filterAdapter(performSearch(mUnFilteredProducts, mSearchQuery));
    }


    @Override
    /**
     * From ProductsListFragments - setList()
     *
     * Called when products were added to the ProductListAdapter.
     * Replaces the filtered products and restarts the ViewPager with the replaced products.
     *
     * @param products with added ArrayList of Products
     * @param position of the selected item in ListView
     */
    public void onProductsAddedToAdapter(ArrayList<Product> products, int position) {
        mFilteredProducts = products;
        if(mPager != null){
            setPager(position);
        }
    }


    @Override
    /**
     * From ProductsListFragment - OnItemClickListener (Select)
     *
     * Called when a selection has been made in the ListView.
     * Replaces the old position and restarts the ViewPager.
     *
     * @param position of the selected item in ListView
     */
    public void onProductSelected(int position) {
        mPagerPosition = position;
        setPager(position);
    }


    @Override
    /**
     * From ProductsListFragment - OnItemClickListener (Deselect)
     *
     * Called when a deselection has been made in the ListView.
     * Basically just hides the ViewPager.
     */
    public void onProductDeSelected() {
        mPager.setVisibility(View.GONE);
    }


    @Override
    /**
     * From InventoryViewFragment / ProductViewFragment - AsyncTask (GetProducts) onPostExecute
     *
     * Called after an "Inventory" / "Update" was made in a Product and the list was updated.
     * Toasts a success message. (different message for each type of update)
     *
     * @param updatedInventory boolean if update is because of an Inventory or an regular Update
     */
    public void onProductUpdateFinished(boolean updatedInventory) {

        if(updatedInventory) {
            setPager(mPagerPosition + 1);

            Toast toast = Toast.makeText(getApplicationContext(), R.string.toast_inventory_finished, Toast.LENGTH_LONG);
            toast.setGravity(Gravity.TOP | Gravity.CENTER_HORIZONTAL, 0, 20);
            toast.show();
        } else {
            Toast toast = Toast.makeText(getApplicationContext(), R.string.toast_product_updated, Toast.LENGTH_LONG);
            toast.setGravity(Gravity.TOP | Gravity.CENTER_HORIZONTAL, 0, 20);
            toast.show();
        }
    }


    @Override
    /**
     * From InventoryViewFragment - OnCLickListener (Save Inventory)
     *
     * Called when user clicked Save in InventoryView.
     * Sets a waiting variable in the ProductListFragment.
     */
    public void onPutProduct() {
        mProductListFragment.mWaitingForProductUpdate = true;
    }


    @Override
    /**
     * From ProductViewFragment - AsyncTask (PutProduct) onPostExecute
     *
     * Called when user finished update a Product in ProductView.
     * Sets a waiting variable in the ProductListFragment.
     */
    public void onPutInventory() {
        mProductListFragment.mWaitingForInventoryUpdate = true;
    }


    @Override
    /**
     * From InventoryViewFragment - OnItemClickListener (Show/Hide/Save)
     *
     * Called when inside a Product and user clicked "Do Inventory" / "Cancel" / "Save".
     * Enables / Disables swipe in the ViewPager and locks / unlocks the ListView.
     *
     * @param inside boolean if user clicked "Do Inventory" / "Cancel" / "Save"
     */
    public void onInsideInventory(boolean inside) {

        if(inside) {
            mPager.setPagingEnabled(false);
            mProductListFragment.lockList(false);
        } else {
            mPager.setPagingEnabled(true);
            mProductListFragment.unlockList();
        }
    }


    /**
     * From onCreate
     *
     * Called when this Activity is being created.
     * Creates a ProductListFragment.
     */
    public void addListFragment(){

        Bundle bundle = new Bundle();
        bundle.putString(INTENT_KEY, mTypeOfActivity);

        // Create an instance of ProductListFragment and add the bundle.
        mProductListFragment = new ProductListFragment();
        mProductListFragment.setArguments(bundle);

        // Get FragmentManager,replace whatever is in the container with a ProductListFragment.
        getSupportFragmentManager()
                .beginTransaction()
                .replace(R.id.fragment_product_list_container, mProductListFragment, PRODUCT_LIST_FRAGMENT_TAG)
                .commit();
    }


    @Override
    /**
     * From ProductListFragment - OnCLickListener (dimmer)
     *
     * Called when user clicked on the dimmer-background.
     * Loses the focus from the SearchBar.
     */
    public void onDoneSearching() {
        cancelSearchBar(false);
    }


    @Override
    /**
     * From ProductListFragment - onActivityResult
     *
     * Called when scanner returned an EAN (barcode).
     * Reopens the SearchBar and adds the search query with the EAN.
     *
     * @param ean scanned barcode to search for Product.
     */
    public void onScannedForEAN(String ean) {
        openSearchBar(ean);
    }


    /**
     * From ActionBar or onScannedForEAN
     *
     * Called when user clicked on the Search icon.
     * Setups and displays the SearchBar.
     *
     * @param queryText search query
     */
    private void openSearchBar(String queryText) {

        mSearchEANAction.setVisible(true);

        // If this is the first time searching. Save the unfiltered list.
        if(mUnFilteredProducts == null) {
            mUnFilteredProducts = mFilteredProducts;
        }

        // Set custom view on action bar.
        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayShowCustomEnabled(true);
        actionBar.setCustomView(R.layout.search_bar);

        // SearchBar setup.
        mSearchEt = (EditText) actionBar.getCustomView().findViewById(R.id.etSearch);

        // OnFocusListener to Lock / Unlock ListView and also Show / Hide keyboard.
        mSearchEt.setOnFocusChangeListener(new View.OnFocusChangeListener() {
            @Override
            public void onFocusChange(View v, boolean hasFocus) {
                if (hasFocus) {
                    mProductListFragment.openSearch();
                    mProductListFragment.lockList(true);
                    InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
                    imm.showSoftInput(mSearchEt, 0);
                } else {
                    mProductListFragment.unlockList();
                    InputMethodManager imm = (InputMethodManager) getSystemService(Context.INPUT_METHOD_SERVICE);
                    imm.hideSoftInputFromWindow(mSearchEt.getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
                }
            }
        });

        // Add TextWatcher.
        mSearchEt.addTextChangedListener(new SearchWatcher());

        // Set focus to the search field and set the search query.
        mSearchEt.setText(queryText);
        mSearchEt.requestFocus();

        // Lock the ListView.
        mProductListFragment.lockList(true);

        // Change the Search icon into a Cancel icon.
        mSearchAction.setIcon(R.drawable.ic_action_cancel);

        // SearchBar is open.
        mSearchOpened = true;
    }


    /**
     * From ActionBar or onDoneSearching
     *
     * Called when user clicked on the Search icon or on the dimmer background in ProductListFragment.
     * Sets the SearchBar to either "Close" or "Unfocus".
     *
     * @param close boolean to check if the SearchBar should close or not
     */
    private void cancelSearchBar(boolean close) {

        // Loses the focus from the SearchBar.
        mSearchEt.clearFocus();


        // If the SearchBar should be closed.
        if(close) {

            mProductListFragment.setSearchedProduct();
            mSearchEt.setText(null);
            mPager.setCurrentItem(mProductListFragment.getSearchedProduct());
        }

        // If the search query is empty. Close it.
        if (mSearchEt.getText().length() == 0) {
            mSearchEANAction.setVisible(false);
            getSupportActionBar().setDisplayShowCustomEnabled(false);
            mSearchAction.setIcon(R.drawable.ic_action_search);

            // The SearchBar is closed.
            mSearchOpened = false;
        }
    }


    /**
     * From SearchWatcher (AfterTextChanged) or onProductsUpdated
     *
     * Called when query has been changed or the list have been updated.
     * Goes through the given list and filters it according to the given query.
     *
     * @param products list to search in
     * @param query to be searched for
     *
     * @return productsFiltered filtered list of products
     */
    private ArrayList<Product> performSearch(ArrayList<Product> products, String query) {

        // First we split the query so that we're able
        // to search word by word (in lower case).
        String[] queryByWords = query.toLowerCase().split("\\s+");

        // Empty list to fill with matches.
        ArrayList<Product> productsFiltered = new ArrayList<>();

        // Go through initial releases and perform search.
        for (Product product : products) {

            // Content to search through (in lower case).
            String content = (
                    product.getName() + " " +
                            product.getSKU() + " " +
                            String.valueOf(product.getStorageSpace() + " " +
                            String.valueOf(product.getEAN()))
            ).toLowerCase();

            // There is a match only if all of the words are contained.
            int numberOfMatches = queryByWords.length;

            for (String word : queryByWords) {

                // All query words have to be contained,
                // otherwise the release is filtered out.
                if (content.contains(word)) {
                    numberOfMatches--;
                } else {
                    break;
                }

                // They all match.
                if (numberOfMatches == 0) {
                    productsFiltered.add(product);
                }
            }
        }
        return productsFiltered;
    }


    /**
     * Inner Class TextWatcher which is responsible for handling changes in the SearchBar.
     */
    private class SearchWatcher implements TextWatcher {

        @Override
        public void beforeTextChanged(CharSequence c, int i, int i2, int i3) {
        }

        @Override
        public void onTextChanged(CharSequence c, int i, int i2, int i3) {

        }

        @Override
        public void afterTextChanged(Editable editable) {
            mSearchQuery = mSearchEt.getText().toString();
            mProductListFragment.filterAdapter(performSearch(mUnFilteredProducts, mSearchQuery));
        }
    }


    /**
     * From onProductSelected or onProductsAddedToAdapter
     *
     * Called when the ViewPager should be set or reset.
     * Setups the ViewPager and the ProductSlidePagerAdapter and adds the Products to it.
     *
     * @param position to set the Pager to the selected Product
     */
    public void setPager(int position){

        // Instantiate a ProductPagerAdapter.
        final PagerAdapter mPagerAdapter = new ProductSlidePagerAdapter(getSupportFragmentManager());
        mPager.setPageTransformer(true, new ZoomOutPageTransformer());

        // Set SwipeListener when user swipe on products.
        mPager.setOnPageChangeListener(new ViewPager.OnPageChangeListener() {

            @Override
            public void onPageScrolled(int position, float positionOffset, int positionOffsetPixels) {
            }
            @Override
            public void onPageScrollStateChanged(int state) {
            }

            @Override
            public void onPageSelected(int position) {
                if (mPagerPosition != position) {

                    mPagerPosition = position;

                    // On swipe in the ProductPager, findFragmentById and updatePositionFromPager with new position.
                    mProductListFragment.selectList(position);
                }
            }
        });

        // Set Adapter to the Pager and display selected Product from the ListView.
        mPager.setAdapter(mPagerAdapter);
        mPager.setCurrentItem(position);

        // If there is an selected Product in the list.
        if (position != -1) {
            mPager.setVisibility(View.VISIBLE);

            // Check if user is NOT using a tablet. Then go inside the selected Product.
            if (!getResources().getBoolean(R.bool.isTablet)) {
                mInsideProduct = true;
                getSupportActionBar().setDisplayShowCustomEnabled(false);
                mSearchAction.setVisible(false);
                mSearchEANAction.setVisible(false);
                mListContainer.setVisibility(View.GONE);
            }
        }
    }


    /**
     * Inner Class ProductSlidePagerAdapter to swipe between products in the ProductList -> ProductView.
     */
    private class ProductSlidePagerAdapter extends FragmentStatePagerAdapter {

        public ProductSlidePagerAdapter(FragmentManager fm) {
            super(fm);
        }


        @Override
        /**
         * Called when item gets added to the ProductSlidePagerAdapter.
         * Appends a ProductViewFragment / InventoryViewFragment as an item in the Adapter.
         *
         * @param position of the item within the adapters data set
         *
         * @return Fragment to start
         */
        public Fragment getItem(int position) {

            Bundle bundle = new Bundle();
            bundle.putParcelableArrayList(PARCELABLE_PRODUCT_LIST_TAG, mFilteredProducts);
            bundle.putParcelable(PARCELABLE_PRODUCT_TAG, mFilteredProducts.get(position));

            // If Activity is of type Products.
            if(mTypeOfActivity.equals(ACTIVITY_INVENTORY_FULL)){
                InventoryViewFragment f = new InventoryViewFragment();
                f.setArguments(bundle);
                return f;
            }

            // If Activity is of type Inventory.
            if(mTypeOfActivity.equals(ACTIVITY_PRODUCTS)){
                ProductViewFragment f = new ProductViewFragment();
                f.setArguments(bundle);
                return f;
            }
            return null;
        }


        @Override
        /**
         * Called when adapter get created and gets the number of pages the pager should consist of.
         *
         * @return size of the list of products
         */
        public int getCount() {
            return mFilteredProducts.size();
        }
    }
}

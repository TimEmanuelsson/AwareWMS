package com.example.andreaslengqvist.aware_test.Storage;

import android.content.Context;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.graphics.drawable.Drawable;
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
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.Toast;

import com.example.andreaslengqvist.aware_test.Helpers.ProductPager;
import com.example.andreaslengqvist.aware_test.Helpers.ZoomOutPageTransformer;
import com.example.andreaslengqvist.aware_test.R;
import com.example.andreaslengqvist.aware_test.Storage.Inventory.InventoryListener;
import com.example.andreaslengqvist.aware_test.Storage.Inventory.InventoryViewFragment;
import com.example.andreaslengqvist.aware_test.Storage.Products.ProductViewFragment;
import java.util.ArrayList;



/**
 * Created by andreaslengqvist on 15-04-13.
 *
 * ProductListActivity handles ProductListFragment, ProductPagerFragment, ProductViewFragment / InventoryViewFragment
 * and all communications between them (ProductListListener/InventoryListener).
 *
 */
public class ProductListActivity extends ActionBarActivity implements ProductListListener, InventoryListener {

    private static final String INTENT_KEY = "Intent_Key";

    private static final String ACTIVITY_INVENTORY_FULL = "Inventory_Full_Actvity";
    private static final String ACTIVITY_PRODUCTS = "Products_Actvity";

    private static final String PRODUCT_LIST_FRAGMENT_TAG = "Product_List_Fragment";
    private static final String PARCELABLE_PRODUCT_TAG = "Product";

    private static final String UNFILTRED_PRODUCTS = "UNFILTRED_PRODUCTS";
    private static final String SEARCH_OPENED = "SEARCH_OPENED";
    private static final String SEARCH_QUERY = "SEARCH_QUERY";


    private ProductListFragment mProductListFragment;
    private ArrayList<Product> mProducts;
    private FrameLayout mListContainer;
    private ProductPager mPager;

    private int mPagerPosition;
    private boolean insideProduct;
    private String mTypeOfActivity;

    private EditText mSearchEt;
    private String mSearchQuery;
    private boolean mSearchOpened;
    private ArrayList<Product> mUnFilteredProducts;

    private MenuItem mSearchAction;
    private Drawable mIconOpenSearch;
    private Drawable mIconCloseSearch;

    private void initializeVariables() {
        mListContainer = (FrameLayout) findViewById(R.id.fragment_product_list_container);
        mPager = (ProductPager) findViewById(R.id.pager);

        // Getting the icons.
        mIconOpenSearch = getResources().getDrawable(R.drawable.ic_action_search);
        mIconCloseSearch = getResources().getDrawable(R.drawable.ic_action_cancel);
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_products);
        initializeVariables();



        // Get which type (Products / Inventory).
        Intent i = getIntent();
        if (i.hasExtra(INTENT_KEY)) {
            mTypeOfActivity = i.getStringExtra(INTENT_KEY);
        }

        // If coming from new state. (e.g NOT screen rotation)
        if (savedInstanceState == null) {

            // If user is using a tablet or a handheld.
            if (getResources().getBoolean(R.bool.isTablet)) {
                addListFragment();
            } else {
                setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
                addListFragment();
            }
        } else {
            mProductListFragment = (ProductListFragment) getSupportFragmentManager()
                    .findFragmentById(R.id.fragment_product_list_container);

            mUnFilteredProducts = savedInstanceState.getParcelableArrayList(UNFILTRED_PRODUCTS);
            mSearchOpened = savedInstanceState.getBoolean(SEARCH_OPENED);
            mSearchQuery = savedInstanceState.getString(SEARCH_QUERY);

        }
    }

    @Override
    protected void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putParcelableArrayList(UNFILTRED_PRODUCTS, mUnFilteredProducts);
        outState.putBoolean(SEARCH_OPENED, mSearchOpened);
        outState.putString(SEARCH_QUERY, mSearchQuery);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.search_menu, menu);
        return true;
    }

    @Override
    public boolean onPrepareOptionsMenu(Menu menu) {
        mSearchAction = menu.findItem(R.id.action_search);
        if (mSearchOpened) {
            openSearchBar(mSearchQuery);
        }
        return super.onPrepareOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();
        if (id == R.id.action_search) {
            if (mSearchOpened) {
                cancelSearchBar(true);
            } else {
                openSearchBar(mSearchQuery);
            }
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    private void openSearchBar(String queryText) {

        if(mUnFilteredProducts == null) {
            mUnFilteredProducts = mProducts;
        }

        // Set custom view on action bar.
        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayShowCustomEnabled(true);
        actionBar.setCustomView(R.layout.search_bar);


        // Search edit text field setup.
        mSearchEt = (EditText) actionBar.getCustomView().findViewById(R.id.etSearch);
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

        mSearchEt.addTextChangedListener(new SearchWatcher());
        mSearchEt.setText(queryText);
        mSearchEt.requestFocus();

        mProductListFragment.lockList(true);

        mSearchAction.setIcon(mIconCloseSearch);

        mSearchOpened = true;
    }

    private void cancelSearchBar(boolean close) {
        mSearchEt.clearFocus();

        if(close) {

            mProductListFragment.setSearchedProduct();
            mSearchEt.setText(null);
            mPager.setCurrentItem(mProductListFragment.getSearchedProduct());
        }

        if (mSearchEt.getText().length() == 0) {
            getSupportActionBar().setDisplayShowCustomEnabled(false);
            mSearchAction.setIcon(mIconOpenSearch);
            mSearchOpened = false;
        }
    }

    /**
     * Responsible for handling changes in search edit text.
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
     * Goes through the given list and filters it according to the given query.
     *
     * @param products list given as search sample
     * @param query to be searched
     * @return new filtered list
     */
    private ArrayList<Product> performSearch(ArrayList<Product> products, String query) {

        // First we split the query so that we're able
        // to search word by word (in lower case).
        String[] queryByWords = query.toLowerCase().split("\\s+");

        // Empty list to fill with matches.
        ArrayList<Product> moviesFiltered = new ArrayList<>();

        // Go through initial releases and perform search.
        for (Product movie : products) {

            // Content to search through (in lower case).
            String content = (
                    movie.getName() + " " +
                            movie.getSKU() + " " +
                            String.valueOf(movie.getStorageSpace())
            ).toLowerCase();

            for (String word : queryByWords) {

                // There is a match only if all of the words are contained.
                int numberOfMatches = queryByWords.length;

                // All query words have to be contained,
                // otherwise the release is filtered out.
                if (content.contains(word)) {
                    numberOfMatches--;
                } else {
                    break;
                }

                // They all match.
                if (numberOfMatches == 0) {
                    moviesFiltered.add(movie);
                }

            }

        }

        return moviesFiltered;
    }


    @Override
    public void onBackPressed() {

        if(insideProduct) {
            insideProduct = false;
            mSearchAction.setVisible(true);
            mPager.setVisibility(View.GONE);
            mListContainer.setVisibility(View.VISIBLE);
            mProductListFragment.deselectList();

            if(mSearchOpened) {
                getSupportActionBar().setDisplayShowCustomEnabled(true);
            }
        }
        else {
            super.onBackPressed();
            mProductListFragment.stopPeriodically();
            overridePendingTransition(R.anim.pull_in_bottom, R.anim.push_out_top);
        }
    }

    @Override
    public void onListUpdatedInsideSearch(ArrayList<Product> products) {
        mUnFilteredProducts = products;
        mProductListFragment.filterAdapter(performSearch(mUnFilteredProducts, mSearchQuery));
    }

    @Override
    public void onCloseSearch() {
        cancelSearchBar(false);
    }

    @Override
    public void onProductListLoaded(ArrayList<Product> products, int position) {
        mProducts = products;
        if(mPager != null){
            setPager(position);
        }
    }

    @Override
    public void onProductSelected(int position) {
        mPagerPosition = position;
        setPager(position);
    }

    @Override
    public void onProductDeSelected() {
        mPager.setVisibility(View.GONE);
    }

    @Override
    public void onInsideInventory(boolean inside) {

        if(inside) {
            mPager.setPagingEnabled(false);
            mProductListFragment.lockList(false);
        } else {
            mPager.setPagingEnabled(true);
            mProductListFragment.unlockList();
        }
    }

    @Override
    public void onUpdatedFinished() {
        mProductListFragment.selectList(mPagerPosition+1);
        mPager.setCurrentItem(mPagerPosition+1);

        Toast.makeText(getApplicationContext(), R.string.inventory_finished, Toast.LENGTH_LONG).show();
    }

    @Override
    public void onDoInventory() {
        mProductListFragment.waitingOnUpdate = true;
    }

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
        mPager.setAdapter(mPagerAdapter);
        mPager.setCurrentItem(position);

        if (position != -1) {
            mPager.setVisibility(View.VISIBLE);

            if (!getResources().getBoolean(R.bool.isTablet)) {
                insideProduct = true;
                getSupportActionBar().setDisplayShowCustomEnabled(false);
                mSearchAction.setVisible(false);
                mListContainer.setVisibility(View.GONE);
            }
        }
    }


/**
 * PagerAdapter to swipe between products in the ProductList -> ProductView.
 */
    private class ProductSlidePagerAdapter extends FragmentStatePagerAdapter {

        public ProductSlidePagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {

            Bundle bundle = new Bundle();
            bundle.putParcelable(PARCELABLE_PRODUCT_TAG, mProducts.get(position));

            if(mTypeOfActivity.equals(ACTIVITY_INVENTORY_FULL)){
                final InventoryViewFragment f = new InventoryViewFragment();
                f.setArguments(bundle);
                return f;
            }
            if(mTypeOfActivity.equals(ACTIVITY_PRODUCTS)){
                final ProductViewFragment f = new ProductViewFragment();
                f.setArguments(bundle);
                return f;
            }
            return null;
        }

        @Override
        public int getCount() {
            return mProducts.size();
        }
    }
}

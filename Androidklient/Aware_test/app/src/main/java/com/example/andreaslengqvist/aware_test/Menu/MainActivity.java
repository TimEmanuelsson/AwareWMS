package com.example.andreaslengqvist.aware_test.Menu;

import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.content.res.Configuration;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentStatePagerAdapter;
import android.support.v4.view.PagerAdapter;
import android.support.v4.view.ViewPager;
import com.example.andreaslengqvist.aware_test.Storage.Inventory.InventoryFastActivity;
import com.example.andreaslengqvist.aware_test.Storage.ProductListActivity;
import com.example.andreaslengqvist.aware_test.R;
import com.google.zxing.integration.android.IntentIntegrator;
import com.google.zxing.integration.android.IntentResult;



/**
 * Created by andreaslengqvist on 15-04-07.
 *
 * MainActivity handles MenuOrderFragment, MenuStorageFragment and all communications between them.
 *
 */
public class MainActivity extends FragmentActivity implements MenuListener {

    // Number of pages in the ViewPager.
    private static final int NUM_PAGES = 2;

    // Static Name variables.
    private static final String INTENT_KEY = "INTENT_KEY";
    private static final String ACTIVITY_INVENTORY_FULL = "ACTIVITY_INVENTORY_FULL";
    private static final String ACTIVITY_PRODUCTS = "ACTIVITY_PRODUCTS";
    private static final String EAN_TAG = "EAN_TAG";


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
        setContentView(R.layout.activity_main);


        // If user is NOT using a tablet set the orientation to only allow Portrait-mode.
        if (!getResources().getBoolean(R.bool.isTablet)) {
            setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        }


        // Instantiate a ViewPager and a PagerAdapter.
        ViewPager mPager = (ViewPager) findViewById(R.id.menu_pager);
        PagerAdapter mPagerAdapter = new MenuPagerAdapter(getSupportFragmentManager());
        mPager.setAdapter(mPagerAdapter);
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

        // Receive the scanned EAN, bundle it and start the InventoryFastActivity.
        IntentResult scanResult = IntentIntegrator.parseActivityResult(requestCode, resultCode, intent);
        if (scanResult.getContents() != null) {
            Intent fastInventory = new Intent(this, InventoryFastActivity.class);
            fastInventory.putExtra(EAN_TAG, scanResult.getContents());
            startActivity(fastInventory);

            // Animation slide in / out.
            overridePendingTransition(R.anim.pull_in_top, R.anim.push_out_bottom);
        }
    }

    @Override
    /**
     * From MenuStorageFragment - OnClickListener (Products)
     *
     * Called when user clicked Products in the menu.
     * Starts a new ProductListActivity with ActivityType set to Products.
     */
    public void onMenuProducts() {

        // Start ProductActivity and also tell the ListFragment to handle a "Products"-activity.
        Intent intent = new Intent(this, ProductListActivity.class);
        intent.putExtra(INTENT_KEY, ACTIVITY_PRODUCTS);
        startActivity(intent);

        // Animation slide in / out.
        overridePendingTransition(R.anim.pull_in_top, R.anim.push_out_bottom);
    }


    @Override
    /**
     * From MenuStorageFragment - OnClickListener (Inventory Fast)
     *
     * Called when user clicked Inventory Fast in the menu.
     * Starts the Barcode scanner.
     */
    public void onMenuInventoryFast() {

        // Create an Integrator which is used to initiate the scanner.
        IntentIntegrator integrator = new IntentIntegrator(this);

        // Check which orientation to set.
        if(getResources().getConfiguration().orientation == Configuration.ORIENTATION_PORTRAIT) {
            integrator.setOrientation(90);
        }
        integrator.initiateScan();
    }


    @Override
    /**
     * From MenuStorageFragment - OnClickListener (Inventory Full)
     *
     * Called when user clicked Inventory Full in the menu.
     * Starts a new ProductListActivity with ActivityType set to Inventory.
     */
    public void onMenuInventoryFull() {

        // Start InventoryFullActivity and also tell the ListFragment to handle a "Inventory"-activity.
        Intent intent = new Intent(this, ProductListActivity.class);
        intent.putExtra(INTENT_KEY, ACTIVITY_INVENTORY_FULL);
        startActivity(intent);

        // Animation slide in / out.
        overridePendingTransition(R.anim.pull_in_top, R.anim.push_out_bottom);
    }


    /**
     * A simple FragmentStatePagerAdapter that represents 2 MenuFragments, in sequence to swipe between.
     */
    private class MenuPagerAdapter extends FragmentStatePagerAdapter {
        public MenuPagerAdapter(FragmentManager fm) {
            super(fm);
        }


        @Override
        /**
         * Called when item gets added to the ProductSlidePagerAdapter.
         * Checks which Fragment to start.
         *
         * @param position of the item within the adapters data set
         *
         * @return Fragment to start
         */
        public Fragment getItem(int position) {

            switch (position) {
                case 0: return new MenuStorageFragment();
                case 1: return new MenuOrderFragment();
                default: return new MenuStorageFragment();
            }
        }


        @Override
        /**
         * Called when adapter get created and gets the number of pages the pager should consist of.
         *
         * @return size of the number of pages
         */
        public int getCount() {
            return NUM_PAGES;
        }
    }
}

package com.example.andreaslengqvist.aware_test.Menu;

import android.content.Intent;
import android.content.pm.ActivityInfo;
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

    /**
     * The number of pages (two MenuFragments) to show in this demo.
     */
    private static final int NUM_PAGES = 2;

    private static final String INTENT_KEY = "INTENT_KEY";
    private static final String ACTIVITY_INVENTORY_FULL = "ACTIVITY_INVENTORY_FULL";
    private static final String ACTIVITY_PRODUCTS = "ACTIVITY_PRODUCTS";

    private static final String EAN_TAG = "EAN_TAG";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        // If user is NOT using a tablet.
        if (!getResources().getBoolean(R.bool.isTablet)) {
            setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        }


        // Instantiate a ViewPager and a PagerAdapter.
        ViewPager mPager = (ViewPager) findViewById(R.id.menu_pager);
        PagerAdapter mPagerAdapter = new MenuPagerAdapter(getSupportFragmentManager());
        mPager.setAdapter(mPagerAdapter);
    }

    public void onActivityResult(int requestCode, int resultCode, Intent intent) {

        // Receive the scanned EAN, bundles it and starts the InventoryFastActivity.
        IntentResult scanResult = IntentIntegrator.parseActivityResult(requestCode, resultCode, intent);
        if (scanResult.getContents() != null) {
            Intent fastInventory = new Intent(this, InventoryFastActivity.class);
            fastInventory.putExtra(EAN_TAG, scanResult.getContents());
            startActivity(fastInventory);
            overridePendingTransition(R.anim.pull_in_top, R.anim.push_out_bottom);
        }
    }

    @Override
    public void onMenuProducts() {

        // Start ProductActivity.
        Intent intent = new Intent(this, ProductListActivity.class);
        intent.putExtra(INTENT_KEY, ACTIVITY_PRODUCTS);
        startActivity(intent);
        overridePendingTransition(R.anim.pull_in_top, R.anim.push_out_bottom);
    }

    @Override
    public void onMenuInventoryFast() {

        // Create an Integrator which is used to initiate the scanner.
        IntentIntegrator integrator = new IntentIntegrator(this);
        integrator.setDesiredBarcodeFormats(IntentIntegrator.PRODUCT_CODE_TYPES);
        integrator.setPrompt("Scanna streckkod");
        integrator.setResultDisplayDuration(0);
        integrator.setWide();
        integrator.setOrientation(1);
        integrator.initiateScan();
    }

    @Override
    public void onMenuInventoryFull() {

        // Start InventoryFullActivity.
        Intent intent = new Intent(this, ProductListActivity.class);
        intent.putExtra(INTENT_KEY, ACTIVITY_INVENTORY_FULL);
        startActivity(intent);
        overridePendingTransition(R.anim.pull_in_top, R.anim.push_out_bottom);
    }


/**
 * A simple pager adapter that represents 2 MenuFragments, in sequence.
 *
 */
    private class MenuPagerAdapter extends FragmentStatePagerAdapter {
        public MenuPagerAdapter(FragmentManager fm) {
            super(fm);
        }

        @Override
        public Fragment getItem(int position) {

            switch (position) {
                case 0: return new MenuStorageFragment();
                case 1: return new MenuOrderFragment();
                default: return new MenuStorageFragment();
            }
        }

        @Override
        public int getCount() {
            return NUM_PAGES;
        }
    }
}

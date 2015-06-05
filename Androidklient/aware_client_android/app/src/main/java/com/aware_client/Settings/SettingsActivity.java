package com.aware_client.Settings;

import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentStatePagerAdapter;
import android.support.v4.view.PagerAdapter;
import android.support.v4.view.ViewPager;
import android.support.v7.app.ActionBar;
import android.support.v7.app.ActionBarActivity;
import android.view.MenuItem;
import com.aware_client.Helpers.SlidingTabLayout;
import com.aware_client.R;



/**
 * Created by andreaslengqvist on 15-04-19.
 *
 * SettingsActivity handles SettingsGeneralFragment and SettingsServerFragment.
 *
 */
public class SettingsActivity extends ActionBarActivity {

    // Number of pages in the ViewPager.
    private static final int NUM_PAGES = 2;

    // Static Name variables.
    private static final String SETTINGS_TAG = "SETTINGS_TAG";
    private static final String SERVER_ERROR = "SERVER_ERROR";

    // Member variable.
    private String mServerError;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_settings);


        // Set Home in ActionBar.
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setHomeAsUpIndicator(R.drawable.aware_logo_tiny);


        // Get which Activity to handle (Products / InventoryFull).
        Intent i = getIntent();
        if (i.hasExtra(SERVER_ERROR)) {
            mServerError = i.getStringExtra(SERVER_ERROR);
        }


        // Set custom view on action bar.
        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayShowCustomEnabled(true);
        actionBar.setCustomView(R.layout.settings_bar);


        // If user is NOT using a tablet set the orientation to only allow Portrait-mode.
        if (!getResources().getBoolean(R.bool.isTablet)) {
            setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
        }


        // Instantiate a ViewPager and a PagerAdapter.
        ViewPager mPager = (ViewPager) findViewById(R.id.menu_pager);
        PagerAdapter mPagerAdapter = new SettingsPagerAdapter(getSupportFragmentManager());
        mPager.setAdapter(mPagerAdapter);


        // Set SlideIndicator.
        SlidingTabLayout mSlidingTabLayout = (SlidingTabLayout) findViewById(R.id.sliding_tabs);


        // Fill whole width of screen.
        mSlidingTabLayout.setDistributeEvenly(true);
        mSlidingTabLayout.setViewPager(mPager, SETTINGS_TAG);


        // Set color of tab.
        mSlidingTabLayout.setCustomTabColorizer(new SlidingTabLayout.TabColorizer() {

            @Override
            public int getIndicatorColor(int position) {
                return getResources().getColor(R.color.gray);
            }
        });
    }


    @Override
    /**
     * Called when a item in the ActionBar is clicked.
     *
     * Checks which item the users clicked and either returns to Home
     *
     * @param item MenuItem
     *
     * @return boolean true
     */
    public boolean onOptionsItemSelected(MenuItem item) {

        switch (item.getItemId()) {

            case android.R.id.home:
                onBackPressed();
                return true;
            default:
                return super.onOptionsItemSelected(item);
        }
    }


    @Override
    /**
     * Called when pressing Back button.
     * Works like an regular back button and goes back to the previous Activity (MainActivity).
     */
    public void onBackPressed() {
        super.onBackPressed();
        overridePendingTransition(R.anim.pull_in_left, R.anim.push_out_right);
    }


    /**
     * A simple FragmentStatePagerAdapter that represents 2 MenuFragments, in sequence to swipe between.
     */
    private class SettingsPagerAdapter extends FragmentStatePagerAdapter {

        public SettingsPagerAdapter(FragmentManager fm) {
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
                case 0:
                    Bundle bundle = new Bundle();
                    bundle.putString(SERVER_ERROR, mServerError);
                    SettingsServerFragment f = new SettingsServerFragment();
                    f.setArguments(bundle);
                    return f;
                case 1:
                    return new SettingsGeneralFragment();
                default:
                    return new SettingsGeneralFragment();
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

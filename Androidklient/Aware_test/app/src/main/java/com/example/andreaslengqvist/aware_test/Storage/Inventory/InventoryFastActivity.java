package com.example.andreaslengqvist.aware_test.Storage.Inventory;

import android.content.pm.ActivityInfo;
import android.os.Bundle;
import android.support.v4.app.FragmentActivity;
import com.example.andreaslengqvist.aware_test.R;
import com.example.andreaslengqvist.aware_test.Storage.Products.ProductViewFragment;



/**
 * Created by andreaslengqvist on 15-04-07.
 */
public class InventoryFastActivity extends FragmentActivity {


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.fragment_inventory_view);

        // If coming from new state. (e.g NOT screen rotation)
        if (savedInstanceState == null) {

            // If user is using a tablet or a handheld.
            if (getResources().getBoolean(R.bool.isTablet)) {
                addInventoryViewFragment();
            }
            else {
                setRequestedOrientation(ActivityInfo.SCREEN_ORIENTATION_PORTRAIT);
                addInventoryViewFragment();
            }
        }
    }

    public void addInventoryViewFragment(){

        // Create an instance of ProductListFragment and add the bundle.
        ProductViewFragment productListFragment = new ProductViewFragment();

        // Get FragmentManager,replace whatever is in the container with a ProductListFragment.
        getSupportFragmentManager()
                .beginTransaction()
                .replace(R.id.fragment_product_list_container, productListFragment)
                .commit();
    }
}

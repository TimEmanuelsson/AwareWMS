package com.example.andreaslengqvist.aware_test.Menu;

import android.app.Activity;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import com.example.andreaslengqvist.aware_test.R;



/**
 * Created by andreaslengqvist on 15-04-07.
 *
 */
public class MenuStorageFragment extends Fragment {

    private View mView;
    private MenuListener mCallback;
    private Button btn_storage_menu_products;
    private Button btn_storage_menu_inventory;
    private Button btn_inventory_menu_fast;
    private Button btn_inventory_menu_full;
    private Button btn_inventory_menu_cancel;

    private void initializeVariables() {
        btn_storage_menu_products = (Button) mView.findViewById(R.id.btn_storage_menu_products);
        btn_storage_menu_inventory = (Button) mView.findViewById(R.id.btn_storage_menu_inventory);
        btn_inventory_menu_fast = (Button) mView.findViewById(R.id.btn_inventory_menu_fast);
        btn_inventory_menu_full = (Button) mView.findViewById(R.id.btn_inventory_menu_full);
        btn_inventory_menu_cancel = (Button) mView.findViewById(R.id.btn_inventory_menu_cancel);
    }

    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);

        // This makes sure that the container activity has implemented
        // the callback interface. If not, it throws an exception
        try {
            mCallback = (MenuListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString()
                    + " must implement OnProductSelectedListener");
        }
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        mView = inflater.inflate(R.layout.fragment_menu_storage, container, false);
        return mView;
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        initializeVariables();


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
}
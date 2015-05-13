package com.example.andreaslengqvist.aware_test.Storage;

import java.util.ArrayList;



/**
 * Created by andreaslengqvist on 15-04-27.
 *
 * Interface listening for changes in ProductList and ProductView.
 */
public interface ProductListListener {
    void onProductListLoaded(ArrayList<Product> products, int position);
    void onProductSelected(int position);
    void onProductDeSelected();
    void onCloseSearch();
    void onInventoryFinished();
    void onListUpdatedInsideSearch(ArrayList<Product> products);
}

package com.aware_client.Storage;

import java.util.ArrayList;



/**
 * Created by andreaslengqvist on 15-04-27.
 *
 * Interface listening for changes in ProductListFragment.
 */
public interface ProductListListener {
    void onProductsAddedToAdapter(ArrayList<Product> products, int position);
    void onProductsUpdated(ArrayList<Product> products);
    void onProductSelected(int position);
    void onProductDeSelected();
    void onProductUpdateFinished(boolean updatedInventory);
    void onDoneSearching();
    void onScannedForEAN(String ean);
}

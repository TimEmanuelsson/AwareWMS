package com.aware_client.Storage;

/**
 * Created by andreaslengqvist on 15-05-13.
 *
 * Interface listening for changes in ProductView and InventoryView.
 */
public interface ProductListener {
    void onPutProduct();
    void onInsideInventory(boolean inside);
    void onPutInventory();
}

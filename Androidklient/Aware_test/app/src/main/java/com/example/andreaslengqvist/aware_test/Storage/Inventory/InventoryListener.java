package com.example.andreaslengqvist.aware_test.Storage.Inventory;

/**
 * Created by andreaslengqvist on 15-05-02.
 *
 */
public interface InventoryListener {
    void onInsideInventory(boolean inside);
    void onDoInventory();
}

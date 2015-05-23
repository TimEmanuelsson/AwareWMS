package com.example.andreaslengqvist.aware_client_android.Menu;



/**
 * Created by andreaslengqvist on 15-04-29.
 *
 * Interface for handling changes in the MenuFragments.
 */
public interface MenuListener {
    void onMenuProducts();
    void onMenuInventoryFast();
    void onMenuInventoryFull();
    void onServerConnected();
}
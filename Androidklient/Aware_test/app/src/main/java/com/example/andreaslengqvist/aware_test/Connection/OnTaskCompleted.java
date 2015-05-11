package com.example.andreaslengqvist.aware_test.Connection;


import com.example.andreaslengqvist.aware_test.Storage.Product;

import java.util.ArrayList;

/**
 * Created by Jocke on 2015-04-15.
 */
public interface OnTaskCompleted{
    void onTaskCompleted(ArrayList<Product> output);
}

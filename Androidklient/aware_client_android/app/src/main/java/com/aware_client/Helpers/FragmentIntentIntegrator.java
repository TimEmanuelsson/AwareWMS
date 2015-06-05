package com.aware_client.Helpers;

import android.content.Intent;
import android.support.v4.app.Fragment;
import com.google.zxing.integration.android.IntentIntegrator;



/**
 * Created by andreaslengqvist on 15-05-13.
 *
 * Overridden IntentIntegrator to use Barcode-scanner inside a fragment.
 *
 */
public final class FragmentIntentIntegrator extends IntentIntegrator {

    private final Fragment fragment;


    public FragmentIntentIntegrator(Fragment fragment) {
        super(fragment.getActivity());
        this.fragment = fragment;
    }

    @Override
    protected void startActivityForResult(Intent intent, int code) {
        fragment.startActivityForResult(intent, code);
    }
}
package com.aware_client.Settings;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.EditText;
import android.widget.RelativeLayout;
import android.widget.TextView;
import com.aware_client.R;



/**
 * Created by andreaslengqvist on 15-05-20.
 *
 * Settings for General settings like Inventory and Handedness.
 *
 */
public class SettingsGeneralFragment extends Fragment {

    // Shared Preference Static variables.
    public static final String APP_PREFERENCES = "APP_PREFERENCES" ;
    public static final String UPDATE_FREQ = "UPDATE_FREQ";
    public static final String TABLET_HANDEDNESS = "TABLET_HANDEDNESS";
    public static final String DAYS_UNDER = "DAYS_UNDER";
    public static final String DAYS_OVER = "DAYS_OVER";
    public static final String MAX_QTY = "MAX_QTY";

    // Layout variables.
    private RelativeLayout btn_tablet_handedness_left;
    private TextView label_tablet_handedness_left;
    private RelativeLayout btn_tablet_handedness_right;
    private TextView label_tablet_handedness_right;
    private EditText output_general_update_freq;
    private EditText output_general_last_inventory_indicators_under;
    private EditText output_general_last_inventory_indicators_over;
    private EditText output_general_max_inventory_quantity;

    // Member variables.
    private View mView;

    // Shared Preference variables.
    private SharedPreferences sharedpreferences;


    /**
     * From onCreate
     *
     * Basically initialize all elements from the XML-layout (res/layout/activity_products.xml).
     */
    private void initializeVariables() {

        // Set GUI-components.
        btn_tablet_handedness_left = (RelativeLayout) mView.findViewById(R.id.btn_tablet_handedness_left);
        label_tablet_handedness_left = (TextView) mView.findViewById(R.id.label_tablet_handedness_left);
        btn_tablet_handedness_right = (RelativeLayout) mView.findViewById(R.id.btn_tablet_handedness_right);
        label_tablet_handedness_right = (TextView) mView.findViewById(R.id.label_tablet_handedness_right);
        output_general_update_freq = (EditText) mView.findViewById(R.id.output_general_update_freq);
        output_general_last_inventory_indicators_under = (EditText) mView.findViewById(R.id.output_general_last_inventory_indicators_under);
        output_general_last_inventory_indicators_over = (EditText) mView.findViewById(R.id.output_general_last_inventory_indicators_over);
        output_general_max_inventory_quantity = (EditText) mView.findViewById(R.id.output_general_max_inventory_quantity);
    }


    @Override
    /**
     * Called when this Fragments View is being created.
     *
     * Basically just do thing that needs to be done upon creation of the View.
     *
     * @param inflater that can be used to inflate any views in the fragment
     * @param container this can be used to generate the LayoutParams of the view
     * @param savedInstanceState saved data from a Configuration change
     *
     * @return mView inflated with the correct layout
     */
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        mView = inflater.inflate(R.layout.fragment_settings_general, container, false);
        return mView;
    }


    @Override
    /**
     * Called when this Fragments Activity finished its creation.
     *
     * Basically just do thing that needs to be done upon creation of the Fragment.
     *
     * @param savedInstanceState saved data from a Configuration change
     */
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        initializeVariables();

        // Get shared preferences.
        sharedpreferences = getActivity().getSharedPreferences(APP_PREFERENCES, Context.MODE_PRIVATE);


        // Check which handedness.
        if(sharedpreferences.getBoolean(TABLET_HANDEDNESS, true)){
            label_tablet_handedness_right.setTextColor(getResources().getColor(R.color.menu_green));
            label_tablet_handedness_left.setTextColor(getResources().getColor(R.color.graytrans));
        } else {
            label_tablet_handedness_left.setTextColor(getResources().getColor(R.color.menu_green));
            label_tablet_handedness_right.setTextColor(getResources().getColor(R.color.graytrans));
        }


        // Set TextWatchers.
        output_general_update_freq.addTextChangedListener(new UpdateFreqWatcher());
        output_general_last_inventory_indicators_under.addTextChangedListener(new DaysUnderWatcher());
        output_general_last_inventory_indicators_over.addTextChangedListener(new DaysOverWatcher());
        output_general_max_inventory_quantity.addTextChangedListener(new MaxQtyWatcher());


        // Set EditText fields.
        output_general_update_freq.setText(Integer.toString(sharedpreferences.getInt(UPDATE_FREQ, 10000)));
        output_general_last_inventory_indicators_under.setText(Integer.toString(sharedpreferences.getInt(DAYS_UNDER, 5)));
        output_general_last_inventory_indicators_over.setText(Integer.toString(sharedpreferences.getInt(DAYS_OVER, 365)));
        output_general_max_inventory_quantity.setText(Integer.toString(sharedpreferences.getInt(MAX_QTY, 1000)));


        // When clicking outside a EditText.
        mView.findViewById(R.id.layout_general).setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                InputMethodManager imm = (InputMethodManager) getActivity().getSystemService(Context.INPUT_METHOD_SERVICE);
                imm.hideSoftInputFromWindow(output_general_update_freq.getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
                imm.hideSoftInputFromWindow(output_general_last_inventory_indicators_under.getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
                imm.hideSoftInputFromWindow(output_general_last_inventory_indicators_over.getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
                imm.hideSoftInputFromWindow(output_general_max_inventory_quantity.getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
                output_general_update_freq.clearFocus();
                output_general_last_inventory_indicators_under.clearFocus();
                output_general_last_inventory_indicators_over.clearFocus();
                output_general_max_inventory_quantity.clearFocus();
                return false;
            }
        });


        // Select Left-handed.
        btn_tablet_handedness_left.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                label_tablet_handedness_left.setTextColor(getResources().getColor(R.color.menu_green));
                label_tablet_handedness_right.setTextColor(getResources().getColor(R.color.graytrans));
                SharedPreferences.Editor mEditor = sharedpreferences.edit();
                mEditor.putBoolean(TABLET_HANDEDNESS, false);
                mEditor.apply();
            }
        });


        // Select Right-handed.
        btn_tablet_handedness_right.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                label_tablet_handedness_right.setTextColor(getResources().getColor(R.color.menu_green));
                label_tablet_handedness_left.setTextColor(getResources().getColor(R.color.graytrans));
                SharedPreferences.Editor mEditor = sharedpreferences.edit();
                mEditor.putBoolean(TABLET_HANDEDNESS, true);
                mEditor.apply();
            }
        });


        // Focus to EditText - UPDATE FREQ.
        output_general_update_freq.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                view.setFocusableInTouchMode(true);
                return false;
            }
        });


        // Focus to EditText - DAYS UNDER.
        output_general_last_inventory_indicators_under.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                Log.d("asds", "INNE");
                view.setFocusableInTouchMode(true);
                return false;
            }
        });


        // Focus to EditText - DAYS OVER.
        output_general_last_inventory_indicators_over.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                view.setFocusableInTouchMode(true);
                return false;
            }
        });


        // Focus to EditText - MAX QTY.
        output_general_max_inventory_quantity.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                view.setFocusableInTouchMode(true);
                return false;
            }
        });
    }


    /**
     * Inner Class TextWatcher which is responsible for handling changes in the EditText for Update Freq.
     * Saves the typed in Update Freq to Shared Preferences.
     */
    private class UpdateFreqWatcher implements TextWatcher {

        @Override
        public void beforeTextChanged(CharSequence c, int i, int i2, int i3) {
        }

        @Override
        public void onTextChanged(CharSequence c, int i, int i2, int i3) {

        }

        @Override
        public void afterTextChanged(Editable editable) {
            String freq = editable.toString();

            if(freq.length() >= 1) {
                SharedPreferences.Editor mEditor = sharedpreferences.edit();
                mEditor.putInt(UPDATE_FREQ, Integer.parseInt(freq));
                mEditor.apply();
            }
        }
    }


    /**
     * Inner Class TextWatcher which is responsible for handling changes in the EditText for DAYS UNDER.
     * Saves the typed in DAYS UNDER to Shared Preferences.
     */
    private class DaysUnderWatcher implements TextWatcher {

        @Override
        public void beforeTextChanged(CharSequence c, int i, int i2, int i3) {
        }

        @Override
        public void onTextChanged(CharSequence c, int i, int i2, int i3) {

        }

        @Override
        public void afterTextChanged(Editable editable) {
            String daysU = editable.toString();

            if(daysU.length() >= 1) {
                SharedPreferences.Editor mEditor = sharedpreferences.edit();
                mEditor.putInt(DAYS_UNDER, Integer.parseInt(daysU));
                mEditor.apply();
            }
        }
    }


    /**
     * Inner Class TextWatcher which is responsible for handling changes in the EditText for DAYS OVER.
     * Saves the typed in DAYS OVER to Shared Preferences.
     */
    private class DaysOverWatcher implements TextWatcher {

        @Override
        public void beforeTextChanged(CharSequence c, int i, int i2, int i3) {
        }

        @Override
        public void onTextChanged(CharSequence c, int i, int i2, int i3) {

        }

        @Override
        public void afterTextChanged(Editable editable) {
            String daysO = editable.toString();

            if(daysO.length() >= 1) {
                SharedPreferences.Editor mEditor = sharedpreferences.edit();
                mEditor.putInt(DAYS_OVER, Integer.parseInt(daysO));
                mEditor.apply();
            }
        }
    }


    /**
     * Inner Class TextWatcher which is responsible for handling changes in the EditText for MAX QTY.
     * Saves the typed in MAX QTY to Shared Preferences.
     */
    private class MaxQtyWatcher implements TextWatcher {

        @Override
        public void beforeTextChanged(CharSequence c, int i, int i2, int i3) {
        }

        @Override
        public void onTextChanged(CharSequence c, int i, int i2, int i3) {

        }

        @Override
        public void afterTextChanged(Editable editable) {
            String max = editable.toString();

            if(max.length() > 1) {
                SharedPreferences.Editor mEditor = sharedpreferences.edit();
                mEditor.putInt(MAX_QTY, Integer.parseInt(max));
                mEditor.apply();
            }
        }
    }
}

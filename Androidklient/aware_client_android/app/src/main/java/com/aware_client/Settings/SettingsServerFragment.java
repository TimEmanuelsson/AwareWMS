package com.aware_client.Settings;

import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import com.aware_client.R;
import java.io.UnsupportedEncodingException;



/**
 * Created by andreaslengqvist on 15-05-20.
 *
 * Settings for connection to the Server.
 *
 */
public class SettingsServerFragment extends Fragment {

    // Static Name variables.
    private static final String SERVER_ERROR = "SERVER_ERROR";

    // Shared Preference Static variables.
    private SharedPreferences sharedpreferences;
    public static final String APP_PREFERENCES = "APP_PREFERENCES" ;
    public static final String SERVER_IP = "SERVER_IP";
    public static final String SERVER_PORT = "SERVER_PORT";
    public static final String SERVER_PW = "SERVER_PW";

    // Layout variables.
    private TextView output_server_status;
    private EditText output_server_ip;
    private EditText output_server_port;
    private EditText output_server_password;
    private Button btn_reconnect;

    // Member variables.
    private View mView;


    /**
     * From onCreate
     *
     * Basically initialize all elements from the XML-layout (res/layout/activity_products.xml).
     */
    private void initializeVariables() {

        // Set GUI-components.
        output_server_status = (TextView) mView.findViewById(R.id.output_server_status);
        output_server_ip = (EditText) mView.findViewById(R.id.output_server_ip);
        output_server_port = (EditText) mView.findViewById(R.id.output_server_port);
        output_server_password = (EditText) mView.findViewById(R.id.output_server_password);
        btn_reconnect = (Button) mView.findViewById(R.id.btn_reconnect);
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
        mView = inflater.inflate(R.layout.fragment_settings_server, container, false);
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


        // Set TextWatchers.
        output_server_ip.addTextChangedListener(new IPWatcher());
        output_server_port.addTextChangedListener(new PortWatcher());


        // Set EditText fields.
        output_server_ip.setText(sharedpreferences.getString(SERVER_IP, ""));
        output_server_port.setText(sharedpreferences.getString(SERVER_PORT, ""));
        output_server_password.setText(sharedpreferences.getString(SERVER_PW, ""));


        // Set Server status.
        Bundle bundle = getArguments();
        if(bundle != null) {
            String mServerError = bundle.getString(SERVER_ERROR);
            if(mServerError.equals("CONNECTED")) {
                output_server_status.setTextColor(getResources().getColor(R.color.menu_green));
                output_server_status.setText(mServerError);
            } else {
                output_server_status.setTextColor(getResources().getColor(R.color.menu_red));
                output_server_status.setText(mServerError);
            }
        }


        // When clicking outside a EditText.
        mView.findViewById(R.id.layout_server).setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                InputMethodManager imm = (InputMethodManager) getActivity().getSystemService(Context.INPUT_METHOD_SERVICE);
                imm.hideSoftInputFromWindow(output_server_ip.getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
                imm.hideSoftInputFromWindow(output_server_port.getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
                imm.hideSoftInputFromWindow(output_server_password.getWindowToken(), InputMethodManager.HIDE_NOT_ALWAYS);
                output_server_ip.clearFocus();
                output_server_port.clearFocus();
                output_server_password.clearFocus();
                return false;
            }
        });


        // Focus to EditText - Server IP.
        output_server_ip.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                view.setFocusableInTouchMode(true);
                return false;
            }
        });


        // Focus to EditText - Server Port.
        output_server_port.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                view.setFocusableInTouchMode(true);
                return false;
            }
        });


        // Focus to EditText - Server Password.
        output_server_password.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View view, MotionEvent motionEvent) {
                output_server_password.addTextChangedListener(new PasswordWatcher());
                view.setFocusableInTouchMode(true);
                return false;
            }
        });


        // Reconnect - Restarts the app and checks for connection.
        btn_reconnect.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                getActivity().finish();
            }
        });
    }


    /**
     * Inner Class TextWatcher which is responsible for handling changes in the Server IP.
     * Saves the typed in IP to Shared Preferences.
     */
    private class IPWatcher implements TextWatcher {

        @Override
        public void beforeTextChanged(CharSequence c, int i, int i2, int i3) {
        }

        @Override
        public void onTextChanged(CharSequence c, int i, int i2, int i3) {

        }

        @Override
        public void afterTextChanged(Editable editable) {
            String ip = editable.toString();

            if(ip.equals("")) {
                btn_reconnect.setEnabled(false);
                output_server_status.setTextColor(getResources().getColor(R.color.menu_red));
                output_server_status.setText("NO IP ENTERED");
            } else {
                btn_reconnect.setEnabled(true);
            }

            SharedPreferences.Editor mEditor = sharedpreferences.edit();
            mEditor.putString(SERVER_IP, ip);
            mEditor.apply();
        }
    }


    /**
     * Inner Class TextWatcher which is responsible for handling changes in the Server Port.
     * Saves the typed in Port to Shared Preferences.
     */
    private class PortWatcher implements TextWatcher {

        @Override
        public void beforeTextChanged(CharSequence c, int i, int i2, int i3) {
            output_server_password.setText("");
        }

        @Override
        public void onTextChanged(CharSequence c, int i, int i2, int i3) {

        }

        @Override
        public void afterTextChanged(Editable editable) {

            String port = editable.toString();

            if(port.equals("")) {
                btn_reconnect.setEnabled(false);
                output_server_status.setTextColor(getResources().getColor(R.color.menu_red));
                output_server_status.setText("NO PORT ENTERED");
            } else {
                btn_reconnect.setEnabled(true);
            }

            SharedPreferences.Editor mEditor = sharedpreferences.edit();
            mEditor.putString(SERVER_PORT, output_server_port.getText().toString());
            mEditor.apply();
        }
    }


    /**
     * Inner Class TextWatcher which is responsible for handling changes in the Server Password.
     * Saves the typed in Hashed Password to Shared Preferences.
     */
    private class PasswordWatcher implements TextWatcher {

        @Override
        public void beforeTextChanged(CharSequence c, int i, int i2, int i3) {
        }

        @Override
        public void onTextChanged(CharSequence c, int i, int i2, int i3) {

        }

        @Override
        public void afterTextChanged(Editable editable) {
            SharedPreferences.Editor mEditor = sharedpreferences.edit();

            String pw = editable.toString();

            try {
                java.security.MessageDigest md = java.security.MessageDigest.getInstance("MD5");
                byte[] array = md.digest(pw.getBytes("UTF-8"));
                StringBuffer sb = new StringBuffer();

                for (int i = 0; i < array.length; ++i) {
                    sb.append(Integer.toHexString((array[i] & 0xFF) | 0x100).substring(1,3));
                }

                pw = sb.toString();
            } catch (java.security.NoSuchAlgorithmException e) {
            } catch (UnsupportedEncodingException e) {
            }
            mEditor.putString(SERVER_PW, pw);
            mEditor.apply();
        }
    }
}

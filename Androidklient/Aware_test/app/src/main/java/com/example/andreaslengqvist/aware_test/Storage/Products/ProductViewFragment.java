package com.example.andreaslengqvist.aware_test.Storage.Products;

import android.app.Activity;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import com.example.andreaslengqvist.aware_test.R;
import com.example.andreaslengqvist.aware_test.Storage.ProductListListener;
import com.example.andreaslengqvist.aware_test.Storage.Product;
import java.io.InputStream;



/**
 * Created by andreaslengqvist on 15-04-15.
 *
 */
public class ProductViewFragment extends Fragment {

    private static final String PARCELABLE_PRODUCT_TAG = "Product";

    private ProductListListener mCallback;
    private View mView;
    private boolean mInsideEditMenu;

    private RelativeLayout btn_product_view_show_edit_menu;
    private Button btn_product_view_create_EAN;
    private Button btn_product_view_create_picture;
    private TextView output_product_position;
    private TextView output_product_name;
    private TextView output_product_number;
    private TextView output_product_balance;
    private ImageView icon_product_view_show_edit_menu;
    private ImageView img_product_picture;


    @Override
    public void onAttach(Activity activity) {
        super.onAttach(activity);

        // This makes sure that the container activity has implemented
        // the callback interface. If not, it throws an exception
        try {
            mCallback = (ProductListListener) activity;
        } catch (ClassCastException e) {
            throw new ClassCastException(activity.toString()
                    + " must implement OnProductSelectedListener");
        }
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        mView = inflater.inflate(R.layout.fragment_product_view, container, false);
        return mView;
    }

    @Override
    public void onActivityCreated(Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        initializeVariables();

        // Get the bundled Product.
        Bundle bundle = getArguments();
        setProduct((Product)bundle.getParcelable(PARCELABLE_PRODUCT_TAG));


        // When user clicks Edit-symbol (pencil).
        btn_product_view_show_edit_menu.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (!mInsideEditMenu) {
                    mInsideEditMenu = true;
                    btn_product_view_create_EAN.setVisibility(View.VISIBLE);
                    btn_product_view_create_picture.setVisibility(View.VISIBLE);
                    icon_product_view_show_edit_menu.setBackgroundResource(R.drawable.ic_action_edit_inside);
                } else {
                    mInsideEditMenu = false;
                    btn_product_view_create_EAN.setVisibility(View.GONE);
                    btn_product_view_create_picture.setVisibility(View.GONE);
                    icon_product_view_show_edit_menu.setBackgroundResource(R.drawable.ic_action_edit);
                }
            }
        });
    }

    private void initializeVariables() {

        output_product_position = (TextView) mView.findViewById(R.id.output_product_position);
        output_product_name = (TextView) mView.findViewById(R.id.output_product_name);
        output_product_number = (TextView) mView.findViewById(R.id.output_product_number);
        img_product_picture = (ImageView) mView.findViewById(R.id.img_product_picture);
        output_product_balance = (TextView) mView.findViewById(R.id.output_product_balance);

        btn_product_view_show_edit_menu = (RelativeLayout) mView.findViewById(R.id.btn_product_view_show_edit_menu);
        icon_product_view_show_edit_menu = (ImageView) mView.findViewById(R.id.icon_product_view_show_edit_menu);
        btn_product_view_create_EAN = (Button) mView.findViewById(R.id.btn_product_view_create_EAN);
        btn_product_view_create_picture = (Button) mView.findViewById(R.id.btn_product_view_create_picture);
    }

    private void setProduct(Product product) {
        output_product_position.setText(product.getStorageSpace());
        output_product_name.setText(product.getName());
        output_product_number.setText(product.getSKU());
        output_product_balance.setText(Integer.toString(product.getQuantity()));
        new ImageDownloader(img_product_picture).execute("https://psmedia.playstation.com/is/image/psmedia/the-last-of-us-remastered-two-column-01-ps4-us-28jul14?$TwoColumn_Image$");
    }


    /**
     * Helper class which loads the ProductPicture into the ImageView in background thread.
     */
    class ImageDownloader extends AsyncTask<String, Void, Bitmap> {
        ImageView bmImage;

        public ImageDownloader(ImageView bmImage) {
            this.bmImage = bmImage;
        }

        protected Bitmap doInBackground(String... urls) {
            String url = urls[0];
            Bitmap mIcon = null;
            try {
                InputStream in = new java.net.URL(url).openStream();
                mIcon = BitmapFactory.decodeStream(in);
            } catch (Exception e) {
                Log.e("Error", e.getMessage());
            }
            return mIcon;
        }

        protected void onPostExecute(Bitmap result) {
            mView.findViewById(R.id.img_product_picture);
            bmImage.setImageBitmap(result);
        }
    }
}

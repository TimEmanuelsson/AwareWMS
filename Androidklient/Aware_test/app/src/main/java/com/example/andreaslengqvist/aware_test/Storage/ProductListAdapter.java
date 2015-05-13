package com.example.andreaslengqvist.aware_test.Storage;

import android.content.Context;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.LinearLayout;
import android.widget.TextView;
import com.example.andreaslengqvist.aware_test.R;


/**
 * ProductListAdapter which loads a ArrayList of Products into a ListView.
 */
public class ProductListAdapter extends ArrayAdapter<Product> {

    private static final String ACTIVITY_INVENTORY_FULL = "ACTIVITY_INVENTORY_FULL";

    private int mSelectedPosition = -1;
    private String mTypeOfActivity;

    // Using a ViewHolder to speed things up. Basically just stores all the objects to solve the DRY-issue.
    private static class ViewHolder {
        LinearLayout rowLayout;
        TextView productPosition;
        TextView productLastInventory;
        TextView productName;
        TextView productSKU;
        TextView productBalance;
    }

    public ProductListAdapter(Context context, String mTypeOfActivity) {
        super(context, 0);
        this.mTypeOfActivity = mTypeOfActivity;
    }


    @Override
    public View getView(int position, View convertView, ViewGroup parent) {

        final ViewHolder viewHolder;

        if (convertView == null) {

            viewHolder = new ViewHolder();

            LayoutInflater inflater = LayoutInflater.from(getContext());
            convertView = inflater.inflate(R.layout.fragment_products_list_item, parent, false);
            viewHolder.rowLayout = (LinearLayout) convertView.findViewById(R.id.layout_product_row);
            viewHolder.productPosition = (TextView) convertView.findViewById(R.id.output_product_position);
            viewHolder.productLastInventory = (TextView) convertView.findViewById(R.id.output_product_last_inventory);
            viewHolder.productName = (TextView) convertView.findViewById(R.id.output_product_name);
            viewHolder.productSKU = (TextView) convertView.findViewById(R.id.output_product_sku);
            viewHolder.productBalance = (TextView) convertView.findViewById(R.id.output_product_balance);
            convertView.setTag(viewHolder);
        } else {
            viewHolder = (ViewHolder) convertView.getTag();
        }

        if(mSelectedPosition == position) {
            convertView.setBackgroundResource(R.drawable.selected_key);
        }
        else {
            convertView.setBackgroundResource(R.drawable.deselected_key);
        }


        Product product = getItem(position);


        if(mTypeOfActivity.equals(ACTIVITY_INVENTORY_FULL)) {

            int lastInventory = product.getLastInventory();

            if (lastInventory < 5) {
                convertView.setBackgroundResource(R.drawable.inventory_green_key);
            }
            if (lastInventory > 100) {
                convertView.setBackgroundResource(R.drawable.inventory_red_key);
            }
            if(mSelectedPosition == position) {
                convertView.setBackgroundResource(R.drawable.selected_key);
            }
        }

        viewHolder.productPosition.setText(product.getStorageSpace());
        viewHolder.productLastInventory.setText(Integer.toString(product.getLastInventory()));
        viewHolder.productName.setText(product.getName());
        viewHolder.productSKU.setText(product.getSKU());
        viewHolder.productBalance.setText(Integer.toString(product.getQuantity()));

        return convertView;
    }

    public int getSelectedPosition() {
        return mSelectedPosition;
    }

    public void setSelectedPosition(int selectedPosition) {
        this.mSelectedPosition = selectedPosition;
    }
}
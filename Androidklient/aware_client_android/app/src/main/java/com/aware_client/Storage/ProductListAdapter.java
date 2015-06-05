package com.aware_client.Storage;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.LinearLayout;
import android.widget.TextView;
import com.aware_client.R;



/**
 * ProductListAdapter which loads a ArrayList of Products into a ListView.
 */
public class ProductListAdapter extends ArrayAdapter<Product> {

    // Static Name variables.
    private static final String ACTIVITY_INVENTORY_FULL = "ACTIVITY_INVENTORY_FULL";

    // Member variables.
    private int mSelectedPosition = -1;
    private String mTypeOfActivity;
    private int DAYS_UNDER;
    private int DAYS_OVER;


    /**
     * Using a ViewHolder to speed things up. Basically just stores all the objects to solve the DRY-issue.
     */
    private static class ViewHolder {
        LinearLayout rowLayout;
        TextView productLastInventory;
        TextView productPosition;
        TextView productName;
        TextView productSKU;
        TextView productQuantity;
    }

    /**
     * Constructor
     *
     * @param context context from the Activity
     * @param mTypeOfActivity which type of Activity is started
     */
    public ProductListAdapter(Context context, String mTypeOfActivity, Integer daysUnder, Integer daysOver) {
        super(context, 0);
        this.mTypeOfActivity = mTypeOfActivity;
        this.DAYS_UNDER = daysUnder;
        this.DAYS_OVER = daysOver;
    }


    @Override
    /**
     * Called on every element added to the Adapter.
     * Gets and sets the Product to the list item layout.
     *
     * @param position of the item within the adapters data set
     * @param convertView old view to reuse
     * @param parent that this view will eventually be attached to
     *
     * @return convertView new view
     */
    public View getView(int position, View convertView, ViewGroup parent) {

        final ViewHolder viewHolder;

        //Using a ViewHolder to speed things up. Basically just stores all the objects to solve the DRY-issue.
        if (convertView == null) {

            viewHolder = new ViewHolder();

            LayoutInflater inflater = LayoutInflater.from(getContext());
            convertView = inflater.inflate(R.layout.fragment_products_list_item, parent, false);
            viewHolder.rowLayout = (LinearLayout) convertView.findViewById(R.id.layout_product_row);
            viewHolder.productLastInventory = (TextView) convertView.findViewById(R.id.output_product_last_inventory);
            viewHolder.productPosition = (TextView) convertView.findViewById(R.id.output_product_position);
            viewHolder.productName = (TextView) convertView.findViewById(R.id.output_product_name);
            viewHolder.productSKU = (TextView) convertView.findViewById(R.id.output_product_sku);
            viewHolder.productQuantity = (TextView) convertView.findViewById(R.id.output_product_quantity);
            convertView.setTag(viewHolder);
        } else { viewHolder = (ViewHolder) convertView.getTag(); }

        // If position is the same as the selected position. Select it.
        if(mSelectedPosition == position) {
            convertView.setBackgroundResource(R.drawable.selected_key);
        } else { convertView.setBackgroundResource(R.drawable.deselected_key); }

        // Get the Product.
        Product product = getItem(position);

        // If its a InventoryActivity.
        if(mTypeOfActivity.equals(ACTIVITY_INVENTORY_FULL)) {

            int lastInventory = product.getLastInventory();

            // If Last Inventory was done under MIN days ago.
            if (lastInventory < DAYS_UNDER) {
                convertView.setBackgroundResource(R.drawable.inventory_green_key);
            }

            // If Last Inventory was done over MAX days ago.
            if (lastInventory > DAYS_OVER) {
                convertView.setBackgroundResource(R.drawable.inventory_red_key);
            }

            // If Last Inventory is selected. Select it.
            if(mSelectedPosition == position) {
                convertView.setBackgroundResource(R.drawable.selected_key);
            }
        }

        // Set Position to the Layout.
        viewHolder.productPosition.setText(product.getStorageSpace());

        // Set Last Inventory to the Layout.
        viewHolder.productLastInventory.setText(Integer.toString(product.getLastInventory()));

        // Set Name to the Layout.
        String name = product.getName();

            // If Name is longer than 20 characters. Cut it and add "..." on the end.
            if(name.length() > 20) {
                name = name.substring(0, 20);
                viewHolder.productName.setText(name + "...");
            } else {
                viewHolder.productName.setText(name);
            }

        // Set SKU (number) to the Layout.
        viewHolder.productSKU.setText(product.getSKU());

        // Set Quantity to the Layout.
        viewHolder.productQuantity.setText(Integer.toString(product.getQuantity()));

        // Return the View.
        return convertView;
    }


    /**
     * Called when application needs to know the selected position.
     * Returns the selected position.
     *
     * @return mSelectedPosition of which item selected
     */
    public int getSelectedPosition() {
        return mSelectedPosition;
    }


    /**
     * Called when selecting a item in the ListView.
     * Sets the selected position to the new position.
     *
     * @param selectedPosition position of the selection
     */
    public void setSelectedPosition(int selectedPosition) {
        this.mSelectedPosition = selectedPosition;
    }
}
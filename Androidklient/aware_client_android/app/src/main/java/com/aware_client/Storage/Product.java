package com.aware_client.Storage;

import android.os.Parcel;
import android.os.Parcelable;
import java.util.Comparator;



/**
 * Created by andreaslengqvist on 15-04-07.
 *
 * Product Class which handles sorting and stores each Product.
 * Implements Parcelable for easily send between activities and fragments.
 *
 */
public class Product implements Parcelable {

    private Integer ProductId;
    private String Name;
    private String SKU;
    private Integer Quantity;
    private double Weight;
    private String StorageSpace;
    private String EAN;
    private String ImageLocation;
    private Integer LastInventory;


    public void setProductId(Integer productId) {
        this.ProductId = productId;
    }
    public Integer getProductId() {
        return ProductId;
    }

    public void setName(String name) {
        this.Name = name;
    }
    public String getName() {
        return Name;
    }

    public void setSKU(String sku) {
        this.SKU = sku;
    }
    public String getSKU() {
        return SKU;
    }

    public void setQuantity(Integer quantity) {
        this.Quantity = quantity;
    }
    public Integer getQuantity() {
        return Quantity;
    }

    public double getWeight() {
        return Weight;
    }
    public void setWeight(double weight) {
        this.Weight = weight;
    }

    public void setEAN(String ean) {
        this.EAN = ean;
    }
    public String getEAN() {
        return EAN;
    }

    public String getStorageSpace() {
        return StorageSpace;
    }
    public void setStorageSpace(String storageSpace) {
        this.StorageSpace = storageSpace;
    }

    public void setImageLocation(String imageLocation) {
        this.ImageLocation = imageLocation;
    }
    public String getImageLocation() {
        return ImageLocation;
    }

    public void setLastInventory(Integer lastInventory) {
        this.LastInventory = lastInventory;
    }
    public Integer getLastInventory() {
        return LastInventory;
    }


    /**
     * Sort StorageSpace ascending.
     */
    public static Comparator<Product> ProductStorageSpaceComparatorASC = new Comparator<Product>() {

        public int compare(Product s1, Product s2) {
            return s1.getStorageSpace().toUpperCase().compareTo(s2.getStorageSpace().toUpperCase());
        }
    };


    /**
     * Sort StorageSpace descending.
     */
    public static Comparator<Product> ProductStorageSpaceComparatorDESC = new Comparator<Product>() {

        public int compare(Product s1, Product s2) {
            return s2.getStorageSpace().toUpperCase().compareTo(s1.getStorageSpace().toUpperCase());
        }
    };


    /**
     * Sort LastInventory ascending.
     */
    public static Comparator<Product> ProductLastInventoryComparatorASC = new Comparator<Product>() {

        public int compare(Product s1, Product s2) {
            return ((Integer)s1.getLastInventory()).compareTo(s2.getLastInventory());
        }
    };


    /**
     * Sort LastInventory descending.
     */
    public static Comparator<Product> ProductLastInventoryComparatorDESC = new Comparator<Product>() {

        public int compare(Product s1, Product s2) {
            return ((Integer)s2.getLastInventory()).compareTo(s1.getLastInventory());
        }
    };


    /**
     * Sort Name ascending.
     */
    public static Comparator<Product> ProductNameComparatorASC = new Comparator<Product>() {

        public int compare(Product s1, Product s2) {
            return s1.getName().toUpperCase().compareTo(s2.getName().toUpperCase());
        }
    };


    /**
     * Sort Name descending.
     */
    public static Comparator<Product> ProductNameComparatorDESC = new Comparator<Product>() {

        public int compare(Product s1, Product s2) {
            return s2.getName().toUpperCase().compareTo(s1.getName().toUpperCase());
        }
    };


    /**
     * Sort SKU ascending.
     */
    public static Comparator<Product> ProductSKUComparatorASC = new Comparator<Product>() {

        public int compare(Product s1, Product s2) {
            return s1.getSKU().toUpperCase().compareTo(s2.getSKU().toUpperCase());
        }
    };


    /**
     * Sort SKU descending.
     */
    public static Comparator<Product> ProductSKUComparatorDESC = new Comparator<Product>() {

        public int compare(Product s1, Product s2) {
            return s2.getSKU().toUpperCase().compareTo(s1.getSKU().toUpperCase());
        }
    };


    /**
     * Sort Quantity ascending.
     */
    public static Comparator<Product> ProductQuantityComparatorASC = new Comparator<Product>() {

        public int compare(Product s1, Product s2) {
            return ((Integer)s1.getQuantity()).compareTo(s2.getQuantity());
        }
    };


    /**
     * Sort Quantity descending.
     */
    public static Comparator<Product> ProductQuantityComparatorDESC = new Comparator<Product>() {

        public int compare(Product s1, Product s2) {
            return ((Integer)s2.getQuantity()).compareTo(s1.getQuantity());
        }
    };


    /**
     * Parcelable settings
     */
    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel out, int flags) {
        out.writeString(SKU);
        out.writeString(Name);
        out.writeString(StorageSpace);
        out.writeInt(Quantity);
    }

    public static final Parcelable.Creator<Product> CREATOR = new Parcelable.Creator<Product>() {
        @Override
        public Product createFromParcel(Parcel in) {
            return new Product(in);
        }

        @Override
        public Product[] newArray(int size) {
            return new Product[size];
        }
    };

    private Product(Parcel in) {
        SKU = in.readString();
        Name = in.readString();
        StorageSpace = in.readString();
        Quantity = in.readInt();
    }
}
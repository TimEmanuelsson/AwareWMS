package model.product;

/**
 * Model class for a Person.
 *
 * @author Marco Jakob
 */
public class Product {

	/**
	 * Default constructor.
	 */
	private int ProductId;
	private String Name;
	private String SKU;
	private int Quantity;
	private double Weight;
	private String StorageSpace;
	private String BarcodeNumber;
	private String ImageLocation;

	public void setProductId(int productId) {
		this.ProductId = productId;
	}

	public int getProductId() {
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

	public void setQuantity(int quantity) {
		this.Quantity = quantity;
	}

	public int getQuantity() {
		return Quantity;
	}

	public double getWeight() {
		return Weight;
	}

	public void setWeight(double weight) {
		this.Weight = weight;
	}

	public String getStorageSpace() {
		return StorageSpace;
	}

	public void setStorageSpace(String storageSpace) {
		this.StorageSpace = storageSpace;
	}

	public String getBarcodeNumber() {
		return BarcodeNumber;
	}

	public void setImageLocation(String imageLocation) {
		this.ImageLocation = imageLocation;
	}

	public String getImageLocation() {
		return ImageLocation;
	}
}
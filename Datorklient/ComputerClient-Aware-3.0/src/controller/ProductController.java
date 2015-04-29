package controller;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import application.Main;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.SplitPane;
import javafx.scene.control.Tab;
import javafx.scene.control.TabPane;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.layout.AnchorPane;
import javafx.stage.Stage;
import model.Context;
import model.product.Product;
import model.product.ProductModel;

public class ProductController {
	@FXML
	private TableView<Product> productTable;
	@FXML
	private TableColumn<Product, Integer> productId;
	@FXML
	private TableColumn<Product, String> nameColumn;
	@FXML
	private TableColumn<Product, String> skuColumn;
	@FXML
	private TableColumn<Product, Integer> quantityColumn;
	@FXML
	private TableColumn<Product, Double> weightColumn;
	@FXML
	private TableColumn<Product, String> storageSpaceColumn;
	@FXML
	private TableColumn<Product, String> barcodeNumberColumn;
	@FXML
	private TableColumn<Product, String> imageLocationColumn;


	
	
	private AnchorPane root;
	private SplitPane splitpane;
	private Scene scene;
	private TabPane tabpane;
	private Tab tab = new Tab();
	private ProductController controller;
	public void doControll(Scene scene) {
		try {

			   
			   
				//root = FXMLLoader.load(ProductController.class.getResource("../view/ProductView.fxml")); 
				 FXMLLoader loader = new FXMLLoader(Main.class.getResource("../view/ProductView.fxml"));
				 root = (AnchorPane) loader.load();
				 controller = loader.getController();
				 controller.setScene(scene);
				
				
				splitpane = (SplitPane) scene.lookup("#MainSplit");
				tabpane = (TabPane) scene.lookup("#Tab");
				splitpane.getItems().set(1 , tabpane);
				tab.setText("Product");
				tabpane.getTabs().add(tab);
				tabpane.getSelectionModel().select(tab);
				tab.setContent(root);
			
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	/**
	 * Initializes the controller class. This method is automatically called
	 * after the fxml file has been loaded.
	 * @param scene 
	 */
	@FXML
	public void initialize() {
		//scene = Context.getInstance().currentMain().getScene();
		//System.out.println(scene);
		// Initialize the person table with the two columns.
		productId.setCellValueFactory(new PropertyValueFactory<Product, Integer>("productId"));
		nameColumn.setCellValueFactory(new PropertyValueFactory<Product, String>("name"));
		skuColumn.setCellValueFactory(new PropertyValueFactory<Product, String>("SKU"));
		quantityColumn.setCellValueFactory(new PropertyValueFactory<Product, Integer>("quantity"));
		weightColumn.setCellValueFactory(new PropertyValueFactory<Product, Double>("weight"));
		storageSpaceColumn.setCellValueFactory(new PropertyValueFactory<Product, String>("storageSpace"));
		barcodeNumberColumn.setCellValueFactory(new PropertyValueFactory<Product, String>("barcodeNumber"));
		imageLocationColumn.setCellValueFactory(new PropertyValueFactory<Product, String>("imageLocation"));
		readAllProducts();
		// clear person
		//showProductDetails(null);
		
		// Listen for selection changes
					productTable.getSelectionModel().selectedItemProperty().addListener(new ChangeListener<Product>() {
						@Override
						public void changed(ObservableValue<? extends Product> observable,
								Product oldValue, Product newValue) {
							
							showProductDetails(newValue);
						}
					});
	}

	/**
	 * Is called by the main application to give a reference back to itself.
	 * 
	 * @param main
	 */
	public void setMainApp(Main main) {
		// Add observable list data to the table
		// personTable.setItems(mainApp.getPersonData());
		readAllProducts();
	}

	private String stringToGetProducts = "GET/products";
	private String jsonString = null;
	private ObservableList<Product> productData = FXCollections.observableArrayList();

	private void readAllProducts() {
		ArrayList<Product> jsonProducts = null;
		ProductModel getProducts = new ProductModel();
		getProducts.storeConnectionString(stringToGetProducts);
		jsonString = getProducts.getAllProducts();

		// Break down the json-string into separate objects in a temp list
		jsonProducts = new Gson().fromJson(jsonString, new TypeToken<List<Product>>() {}.getType());
		for (Product product : jsonProducts) {
			productData.add(product);
		}
		productTable.setItems(productData);
	}	
	
	private void editProducts() {
		Product selectedProduct = productTable.getSelectionModel().getSelectedItem();
		if (selectedProduct != null) {
			/*boolean okClicked = main.showPersonEditDialog(selectedProduct);
			if (okClicked) {
				refreshProductTable();
				showProductDetails(selectedProduct);
			}*/
			
		}
	}

	// fx:controller="controller.ProductController" <- KALLA PÅ DEN I SAMMA CLASS SOM DEN PEKAR PÅ GÅR EJ.
	private void showProductDetails(Product product) {
		try {
			
			//root =  FXMLLoader.load(ProductController.class.getResource("../view/ProductDetailsView.fxml"));
			AnchorPane pane = (AnchorPane) FXMLLoader.load(ProductController.class.getResource("../view/ProductDetailsView.fxml"));
			splitpane = (SplitPane) scene.lookup("#ShowProductsPane");
			
			splitpane.getItems().set(1 , pane);
			
		
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} 
		
		if (product != null) {
			Label productIdLabel = (Label) scene.lookup("#productIdLabel");
			Label nameLabel = (Label) scene.lookup("#nameLabel");
			Label skuLabel = (Label) scene.lookup("#skuLabel");
			Label quantityLabel = (Label) scene.lookup("#quantityLabel");
			Label weightLabel = (Label) scene.lookup("#weightLabel");
			Label storageSpaceLabel = (Label) scene.lookup("#storageSpaceLabel");
			Label barcodeNumberLabel = (Label) scene.lookup("#barcodeNumberLabel");
			Label imageLocationLabel = (Label) scene.lookup("#imageLocationLabel");
			
			productIdLabel.setText(Integer.toString(product.getProductId())); 
			nameLabel.setText(product.getName());
			skuLabel.setText(product.getSKU());
			quantityLabel.setText(Integer.toString(product.getQuantity()));
			weightLabel.setText(Double.toString(product.getWeight()));
			storageSpaceLabel.setText(product.getStorageSpace());
			barcodeNumberLabel.setText(product.getBarcodeNumber());
			imageLocationLabel.setText(product.getImageLocation());
		} 
		/*	else {
			firstNameLabel.setText("");
			lastNameLabel.setText("");
			streetLabel.setText("");
			postalCodeLabel.setText("");
			cityLabel.setText("");
			birthdayLabel.setText("");
			
		}*/
	}

	private void refreshProductTable() {
		// TODO Auto-generated method stub
		
	}
	public void setScene(Scene mainScene) {
		this.scene = mainScene;
	}
}

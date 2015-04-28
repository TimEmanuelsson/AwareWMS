package controller;


import java.util.ArrayList;
import java.util.List;

import application.Main;

import com.google.gson.Gson;
import com.google.gson.reflect.TypeToken;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.cell.PropertyValueFactory;
import model.product.Product;
import model.product.ProductModel;

public class ProductController {
	@FXML
	private TableView<Product> personTable;
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
	@FXML
	private Button s;

	

	/**
	 * Initializes the controller class. This method is automatically called
	 * after the fxml file has been loaded.
	 */
	@FXML
	private void initialize() {
		// Initialize the person table with the two columns.
		productId.setCellValueFactory(new PropertyValueFactory<Product, Integer>("productId"));
		nameColumn.setCellValueFactory(new PropertyValueFactory<Product, String>("name"));
		skuColumn.setCellValueFactory(new PropertyValueFactory<Product, String>("SKU"));
		quantityColumn.setCellValueFactory(new PropertyValueFactory<Product, Integer>("quantity"));
		weightColumn.setCellValueFactory(new PropertyValueFactory<Product, Double>("weight"));
		storageSpaceColumn.setCellValueFactory(new PropertyValueFactory<Product, String>("storageSpace"));
		barcodeNumberColumn.setCellValueFactory(new PropertyValueFactory<Product, String>("barcodeNumber"));
		imageLocationColumn.setCellValueFactory(new PropertyValueFactory<Product, String>("imageLocation"));
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
			System.out.println(product);
			productData.add(product);
		}
		personTable.setItems(productData);
	}


	
}

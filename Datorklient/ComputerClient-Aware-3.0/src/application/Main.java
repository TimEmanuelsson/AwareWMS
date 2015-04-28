package application;
	


import java.io.File;
import java.io.IOException;



import controller.MainController;
import controller.ProductController;
import javafx.application.Application;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.stage.Stage;
import javafx.scene.Scene;
import javafx.scene.image.Image;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.BorderPane;


public class Main extends Application {
	private BorderPane root;
	@Override
	public void start(Stage primaryStage) {		
		try {
			// Load the root layout from the fxml file
			FXMLLoader loader = new FXMLLoader(Main.class.getResource("../view/RootLayout.fxml"));
			root = (BorderPane) loader.load();
			Scene scene = new Scene(root);
			primaryStage.setScene(scene);
			primaryStage.show();
			ssd();
		} catch (IOException e) {
			// Exception gets thrown if the fxml file could not be loaded
			e.printStackTrace();
		}
		
	}
	
	private void ssd() {
		try {
			// Load the fxml file and set into the center of the main layout
			FXMLLoader loader = new FXMLLoader(Main.class.getResource("../view/MainView.fxml"));
			AnchorPane overviewPage = (AnchorPane) loader.load();
			root.setCenter(overviewPage);
			
			
			
			MainController controller = loader.getController();
			controller.setMainApp(this);
			
		} catch (IOException e) {
			// Exception gets thrown if the fxml file could not be loaded
			e.printStackTrace();
		}
	}

	
	public static void main(String[] args) {
		launch(args);
		
	}
}

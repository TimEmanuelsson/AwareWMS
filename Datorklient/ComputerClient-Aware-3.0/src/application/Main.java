package application;
	


import java.io.IOException;

import controller.MainController;
import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.stage.Stage;
import javafx.scene.Scene;
import javafx.scene.control.SplitPane;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.BorderPane;


public class Main extends Application {
	private BorderPane root;
	private Scene scene;
	@Override
	public void start(Stage primaryStage) {		
		try {
			// Load the root layout from the fxml file
			FXMLLoader loader = new FXMLLoader(Main.class.getResource("../view/RootLayout.fxml"));
			root = (BorderPane) loader.load();
			scene = new Scene(root);
			primaryStage.setScene(scene);
			primaryStage.show();
			LoadMenu();
		} catch (IOException e) {
			// Exception gets thrown if the fxml file could not be loaded
			e.printStackTrace();
		}
		
	}
	
	private void LoadMenu() {
		try {
			// Load the fxml file and set into the center of the main layout
			FXMLLoader loader = new FXMLLoader(Main.class.getResource("../view/MainView.fxml"));
			AnchorPane overviewPage = (AnchorPane) loader.load();
			root.setCenter(overviewPage);
			
			//Behöver vi detta?
			MainController controller = loader.getController();
		//	controller.setMainApp(this);
			controller.setScene(scene);
		} catch (IOException e) {
			// Exception gets thrown if the fxml file could not be loaded
			e.printStackTrace();
		}
	}

	
	public static void main(String[] args) {
		launch(args);
		
	}
}

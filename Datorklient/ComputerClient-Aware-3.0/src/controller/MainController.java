package controller;

import java.io.IOException;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.control.Button;
import javafx.scene.control.SplitPane;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.BorderPane;
import application.Main;



public class MainController {
	
	private Main main;
	private AnchorPane root;
	private SplitPane splitpane;
	public void setMainApp(Main mainApp) {
		this.main = mainApp;
	}
	
	@FXML
	public void onMenuClick(ActionEvent getButtonId) {
		//Get the id from the corresponding clicked button
		Object source = getButtonId.getSource();
		Button clickedBtn = (Button) source;

		 switch (clickedBtn.getId()) {
		case "1":
			AnchorPane pane;
			try {
			//	MainController controller = loader.getController();
			//	controller.setMainApp(this);
				pane = (AnchorPane) FXMLLoader.load(MainController.class.getResource("../view/ProductView.fxml"));
				splitpane.getItems().set(1, pane);
			} catch (IOException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}
			

			System.out.println("Product");
			break;
		case "2":
			System.out.println("Ordrar");	
			break;
		case "3":
			System.out.println("Returer");
			break;
		case "4":
			System.out.println("Inventering");
			break;
		case "5":
			System.out.println("Inleverans");
			break;
		default:
			break;
		}
	}
		
}

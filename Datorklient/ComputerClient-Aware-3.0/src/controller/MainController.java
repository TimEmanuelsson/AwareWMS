package controller;
import java.io.IOException;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.FXMLLoader;
import javafx.scene.Scene;
import javafx.scene.control.Button;
import javafx.scene.control.SplitPane;
import javafx.scene.layout.AnchorPane;
import application.Main;

public class MainController {
	
	//private Main main;
	private AnchorPane root;
	@FXML
	private SplitPane splitpane;
	private Scene scene;
	
	//Behöver vi detta?
	
	public void setScene(Scene mainScene) {
		this.scene = mainScene;
	}
	
	@FXML
	public void onMenuClick(ActionEvent getButtonId) {
		//Get the id from the corresponding clicked button
		Object source = getButtonId.getSource();
		Button clickedBtn = (Button) source;

		 switch (clickedBtn.getId()) {
		case "1":
			//TODO FIXA METODER TILL SWITCH
			
			try {
				root = FXMLLoader.load(MainController.class.getResource("../view/ProductView.fxml")); 
				
				splitpane = (SplitPane) scene.lookup("#MainSplit");

				splitpane.getItems().set(1 , root);
			} catch (IOException e) {
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

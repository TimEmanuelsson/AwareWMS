package model.DAL;

import java.io.IOException;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;

/**
 * Created by Jocke on 2015-04-16.
 */
public class OpenConnection {

	public Socket establish() {
		Socket socket = null;
		InetAddress serverAddr = null;
		int dstPort = 0;

		try {
			serverAddr = InetAddress.getByName("78.73.137.154");
			dstPort = 11000;

		} catch (UnknownHostException e1) {
			// TODO Auto-generated catch block
			e1.printStackTrace();
		}

		try {
			socket = new Socket(serverAddr, dstPort);
			return socket;
		} catch (IOException e) {
			e.printStackTrace();
		}
		return null;
	}
}
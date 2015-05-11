package com.example.andreaslengqvist.aware_test.Connection;

import java.io.IOException;
import java.net.InetAddress;
import java.net.Socket;
import java.net.UnknownHostException;



public class Connection {

    Socket socket = null;
    InetAddress serverAddr = null;


    public Socket establish(){

        int dstPort = 0;
        try {
            serverAddr = InetAddress.getByName("78.73.137.154");
            dstPort = 11000;

        } catch (UnknownHostException e1) {
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

package model;

import application.Main;


public class Context {
	private final static Context instance = new Context();

    public static Context getInstance() {
        return instance;
    }

    private Main main = new Main();

    public Main currentMain() {
        return main;
    }

}

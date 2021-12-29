package com.quantum.utils;

import jline.Terminal;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import java.util.Date;

public class Utils {

    private static Terminal terminal = Terminal.getTerminal();

    public static void exec(String command) {
        try {
            new ProcessBuilder(command).inheritIO().start().waitFor();
        } catch (IOException | InterruptedException e) {
            e.printStackTrace();
        }
    }

    public static String getDate() {
        return new Date().toString();
    }

    public static void clear() {
        System.out.print("\033[H\033[2J");
        System.out.flush();
    }

    public static boolean netIsAvailable() {
        try {
            final URL url = new URL("http://www.google.com");
            final URLConnection conn = url.openConnection();
            conn.connect();
            conn.getInputStream().close();
            return true;
        } catch (MalformedURLException e) {
            throw new RuntimeException(e);
        } catch (IOException e) {
            return false;
        }
    }

    public static String getSpace(int count) {
        StringBuilder acc = new StringBuilder();
        for (int i = 0; i < terminal.getTerminalWidth() / (count * 10); i++) {
            acc.append(" ");
        }
        return acc.toString();
    }

    public static void setColor(String color) {
        System.out.print(color);
    }

    public static int getWidth() {
        return terminal.getTerminalWidth();
    }

    public static int getHeight() {
        return terminal.getTerminalHeight();
    }

    public static int getCharacter() {
        try {
            return terminal.readVirtualKey(System.in);
        } catch (IOException e) {
            e.printStackTrace();
        }
        return -1;
    }
}

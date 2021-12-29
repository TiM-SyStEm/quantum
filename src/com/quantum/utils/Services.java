package com.quantum.utils;

import com.quantum.logger.LogTimer;

public class Services {
    public static Thread timer = new Thread(new LogTimer());
    public static Thread wifi = new Thread(new WIFIChecker());
    public static Thread keyboard = new Thread(new KeyboardChecker());

    public static void start() {
        wifi.start(); keyboard.start(); timer.start();
    }

    public static void stop() {
        wifi.stop(); keyboard.stop(); timer.stop();
    }
}

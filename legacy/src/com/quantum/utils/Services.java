package com.quantum.utils;

import com.quantum.logger.LogTimer;

public class Services {

    public static Thread timer = new Thread(new LogTimer());
    public static Thread wifi = new Thread(new WIFIChecker());
    public static Thread keyboard = new Thread(new KeyboardChecker());
    public static Thread screenSaver = new Thread(new ScreenSaverTimeout());

    public static void start() {
        timer = new Thread(new LogTimer());
        wifi = new Thread(new WIFIChecker());
        keyboard = new Thread(new KeyboardChecker());
        screenSaver = new Thread(new ScreenSaverTimeout());
        wifi.start(); keyboard.start(); timer.start(); screenSaver.start();
    }

    public static void stop() {
        wifi.stop(); keyboard.stop(); timer.stop(); screenSaver.stop();
    }
}

package com.quantum.utils;

import com.quantum.Quantum;

public class ScreenSaverTimeout implements Runnable {

    private static int timeout = 120;
    private static int def = 120;

    @Override
    public void run() {
        try {
            while (true) {
                Thread.sleep(1000);
                timeout--;
                if (timeout == 0) {
                    Quantum.screenSave = true;
                }
            }
        } catch (InterruptedException ignored) {

        }
    }

    public static void resetTimeout() {
        timeout = def;
    }

    public static void setDef(int def) {
        ScreenSaverTimeout.def = def;
    }
}

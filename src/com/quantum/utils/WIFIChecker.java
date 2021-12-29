package com.quantum.utils;

import com.quantum.Quantum;

public class WIFIChecker implements Runnable {
    @Override
    public void run() {
        while (true) {
            Quantum.gotWifi = Utils.netIsAvailable();
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        }
    }
}

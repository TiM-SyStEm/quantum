package com.quantum.logger;

public class LogTimer implements Runnable {

    private static int time = 0;

    @Override
    public void run() {
        while (true) {
            time++;
            try {
                Thread.sleep(1000);
            } catch (InterruptedException e) {
                break;
            }
        }
    }

    public static int getTime() {
        return time;
    }
}

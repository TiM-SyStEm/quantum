package com.quantum.ui;

import com.quantum.utils.Utils;

public class LoggerBar {

    private static final LoggerBar runtime = new LoggerBar();
    private String currentLog = "";

    private LoggerBar() {}

    public static LoggerBar getRuntime() {
        return runtime;
    }

    public void change(String text) {
        this.currentLog = text;
    }

    public void show() {
        System.out.print("   |   " + currentLog);
    }
}

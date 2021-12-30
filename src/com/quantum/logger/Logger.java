package com.quantum.logger;

import com.quantum.ui.LoggerBar;

import java.io.FileWriter;
import java.io.IOException;

public class Logger {

    private static int priority = 0;

    public static void prepare() {
        try (FileWriter writer = new FileWriter("quantum/quantum.log", false)) {
            writer.write("");
        } catch (IOException ex) {

        }
    }

    public static void ok(String text, int pr) {
        if (pr >= priority) {
            LoggerBar.getRuntime().change(String.format("[%s] %s.\n", LogTimer.getTime(), text));
            try (FileWriter writer = new FileWriter("quantum/quantum.log", true)) {
                writer.write(String.format("[OK] [%s] %s.\n", LogTimer.getTime(), text));
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    public static void err(String text, int pr) {
        if (pr >= priority) {
            LoggerBar.getRuntime().change(String.format("[%s] %s.\n", LogTimer.getTime(), text));
            try (FileWriter writer = new FileWriter("quantum/quantum.log", true)) {
                writer.write(String.format("[ERR] [%s] %s.\n", LogTimer.getTime(), text));
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    public static int getPriority() {
        return priority;
    }

    public static void setPriority(int priority) {
        Logger.priority = priority;
    }
}

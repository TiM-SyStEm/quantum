package com.quantum.kernel;

import com.quantum.elements.QElement;
import com.quantum.ui.Viewport;

import java.io.File;

public class Parser {
    public static void parse(String command, QElement caller, Viewport viewport) {
        String[] parts = command.split(" ");
        if (parts[0].equalsIgnoreCase("cdr")) {
            cdr("quantum/" + Console.dir, viewport);
        }
    }

    public static void cdr(String path, Viewport v) {
        final File folder = new File(path);
        for (final File fileEntry : folder.listFiles()) {
            if (fileEntry.isDirectory()) {
                cdr(fileEntry.getAbsolutePath(), v);
            } else {
                Console.printText(null, v, "\n" + fileEntry.getName() + "\n");
            }
        }
    }
}

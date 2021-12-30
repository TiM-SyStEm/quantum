package com.quantum.kernel;

import com.quantum.elements.QElement;
import com.quantum.elements.QTextElement;
import com.quantum.ui.Viewport;
import com.quantum.utils.Utils;

import java.io.File;

public class Parser {
    public static void parse(String command, QElement caller, Viewport viewport) {
        String[] parts = command.split(" ");
        if (parts[0].equalsIgnoreCase("cdr")) {
            cdr("quantum/" + Console.dir, viewport);
        } else if (parts[0].equalsIgnoreCase("clear")) {
            clear(caller, viewport);
        } else {
            unknown(command, caller, viewport);
        }
    }

    private static void unknown(String command, QElement caller, Viewport viewport) {
        Console.printText(caller, viewport, "\n" + "Can't find command '" + command + "'\n");
    }

    private static void clear(QElement caller, Viewport viewport) {
        Console.printText(caller, viewport, Utils.strDup("\n", Utils.getHeight() - 2));
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

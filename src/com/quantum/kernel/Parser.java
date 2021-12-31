package com.quantum.kernel;

import com.quantum.Quantum;
import com.quantum.elements.QElement;
import com.quantum.elements.QTextElement;
import com.quantum.ui.Viewport;
import com.quantum.utils.Utils;

import java.io.File;
import java.util.Objects;

public class Parser {
    public static void parse(String command, QElement caller, Viewport viewport) {
        String[] parts = command.split(" ");
        if (parts[0].equalsIgnoreCase("cdr")) {
            cdr("quantum/" + Console.dir, viewport);
        } else if (parts[0].equalsIgnoreCase("clear")) {
            clear(caller, viewport);
        } else if (parts[0].equalsIgnoreCase("echo")) {
            Console.printText(caller, viewport, "\n%s\n".formatted(parts[1]));
        } else {
            unknown(command, caller, viewport);
        }
    }

    private static void unknown(String command, QElement caller, Viewport viewport) {
        Console.printText(caller, viewport, "\n" + "Can't find command '" + command + "'\n");
    }

    private static void clear(QElement caller, Viewport viewport) {
        viewport.clear(() -> Console.setConsolePointer(1));
        Quantum.pointer = 1;
    }

    public static void cdr(String path, Viewport v) {
        final File folder = new File(path);
        Console.printText(null, v, "\n");
        for (final File fileEntry : Objects.requireNonNull(folder.listFiles())) {
            if (fileEntry.isDirectory()) {
                Console.printText(null, v, fileEntry.getName() + "/\n");
            } else Console.printText(null, v, fileEntry.getName() + "\n");
        }
    }
}

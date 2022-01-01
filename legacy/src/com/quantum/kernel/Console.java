package com.quantum.kernel;

import com.quantum.Quantum;
import com.quantum.elements.QElement;
import com.quantum.elements.QRudeInputField;
import com.quantum.elements.QTextElement;
import com.quantum.logger.Logger;
import com.quantum.ui.Compositor;
import com.quantum.ui.QWindow;
import com.quantum.ui.Viewport;
import com.quantum.utils.Utils;

import java.util.ArrayList;

public class Console {

    private static int consolePointer = 1;
    private static ArrayList<QElement> consoleElements = new ArrayList<>();
    public static String dir = "root";
    private static final Object lock = new Object();

    public static void start() throws InterruptedException {
        Utils.setDirectory("quantum/root");
        Viewport viewport = genConsole();
        Logger.ok("Kernel's viewport created success", 5);
        QWindow window = new QWindow(viewport, 0, ">> ", "@Kernel@Console@");
        Logger.ok("Abstract window packing was success", 5);
        Compositor compositor = new Compositor(window);
        Logger.ok("Compositor class was created success", 5);
        compositor.compose();
        Logger.ok("Tick", 0);
        synchronized (lock) {
             while (true) {
                 compositor.compose();
                 Logger.ok("Tick", 0);
             }
        }
    }

    private static Viewport genConsole() {
        Viewport viewport = new Viewport(new ArrayList<>());
        printText(null, viewport, "Welcome to Quantum Kernel's Console!\n");
        genPrompt(null, viewport);
        return viewport;
    }

    public static void printText(QElement c, Viewport v, String text) {
        consolePointer += text.chars().filter(ch -> ch == '\n').count();
        v.add(new QTextElement(text));
    }

    public static void genPrompt(QElement c, Viewport v) {
        v.add(new QRudeInputField(dir + "~", Console::nextPrompt, consolePointer));
    }

    private static void nextPrompt(QElement caller, Viewport viewport) {
        Parser.parse(caller.toString(), caller, viewport);
        genPrompt(caller, viewport);
    }

    public static void setConsolePointer(int consolePointer) {
        Console.consolePointer = consolePointer;
    }
}

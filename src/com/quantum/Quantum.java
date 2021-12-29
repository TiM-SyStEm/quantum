package com.quantum;

import com.quantum.elements.QElement;
import com.quantum.elements.QTextElement;
import com.quantum.ui.Compositor;
import com.quantum.ui.QWindow;
import com.quantum.ui.TaskBar;
import com.quantum.ui.Viewport;
import com.quantum.utils.Utils;
import jline.Terminal;

import java.awt.*;
import java.io.IOException;
import java.util.ArrayList;

public class Quantum {

    public static boolean gotWifi = false;
    public static int interfaceLength = 0xff; // Placeholder
    public static int keyboardCode = 0xff;

    public static void main(String[] args) throws InterruptedException {
        Viewport viewport = new Viewport(genHelloWorld());
        QWindow window = new QWindow(viewport, 0, ">>");
        Compositor globalCompositor = new Compositor(window);
        while (true) {
            globalCompositor.compose();
        }
    }

    private static ArrayList<QElement> genHelloWorld() {
        ArrayList<QElement> elements = new ArrayList<>();
        elements.add(new QTextElement("Hello, World!"));
        elements.add(new QTextElement("\nAnd Hello, Dog!"));
        return elements;
    }
}

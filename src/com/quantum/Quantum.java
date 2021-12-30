package com.quantum;

import com.quantum.elements.QElement;
import com.quantum.elements.QEvent;
import com.quantum.elements.QRudeInputField;
import com.quantum.elements.QTextElement;
import com.quantum.logger.Logger;
import com.quantum.rescue.Rescue;
import com.quantum.ui.Compositor;
import com.quantum.ui.QWindow;
import com.quantum.ui.TaskBar;
import com.quantum.ui.Viewport;
import com.quantum.utils.*;

import java.util.ArrayList;

public class Quantum {

    public static boolean gotWifi = false;
    public static int interfaceLength = 0xff; // Placeholder
    public static int keyboardCode = Keys.NKBH; // Placeholder
    public static int pointer = 1;
    public static int sleepConstant = 300;

    public static void main(String[] args) throws InterruptedException {
        Logger.prepare();
        Services.start();
        Logger.ok("Services activated success", 2);
        try {
            Viewport viewport = new Viewport(genHelloWorld());
            Logger.ok("Viewport creation success", 5);
            QWindow window = new QWindow(viewport, 0, ">>", "@Kernel@Console@");
            Logger.ok("Abstract window creation success", 5);
            Compositor globalCompositor = new Compositor(window);
            Logger.ok("Compositor creation success", 5);
            while (true) {
                globalCompositor.compose();
                Logger.ok("Tick", 0);
            }
        } catch (Exception ex) {
            Logger.err("Caught exception. Entering rescue mode", 5);
            Utils.setColor(Colors.ANSI_BLACK_BACKGROUND);
            Utils.clear();
            Logger.ok("Changing terminal mode", 1);
            Services.stop();
            Logger.ok("Stopping all services", 5);
            Rescue.main(ex);
        }
    }


    private static ArrayList<QElement> genHelloWorld() {
        ArrayList<QElement> elements = new ArrayList<>();
        elements.add(new QTextElement("Write text, please!\n"));
        elements.add(new QRudeInputField(">> ", Quantum::genFields, 2));
        return elements;
    }

    private static void genFields(QElement caller, Viewport viewport) {
        viewport.add(new QTextElement("\n" + caller.toString() + "\n"));
        viewport.add(new QRudeInputField(">> ", Quantum::genFields, 4));
    }
}

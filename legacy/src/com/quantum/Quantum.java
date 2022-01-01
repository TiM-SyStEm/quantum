package com.quantum;

import com.quantum.elements.QElement;
import com.quantum.elements.QEvent;
import com.quantum.elements.QRudeInputField;
import com.quantum.elements.QTextElement;
import com.quantum.kernel.Console;
import com.quantum.kernel.ImageRender;
import com.quantum.logger.Logger;
import com.quantum.rescue.Rescue;
import com.quantum.ui.Compositor;
import com.quantum.ui.QWindow;
import com.quantum.ui.TaskBar;
import com.quantum.ui.Viewport;
import com.quantum.utils.*;

import java.awt.image.Kernel;
import java.io.IOException;
import java.util.ArrayList;

public class Quantum {

    public static boolean gotWifi = false;
    public static int interfaceLength = 0xff; // Placeholder
    public static int keyboardCode = Keys.NKBH; // Placeholder
    public static int pointer = 1;
    public static String screenSaverPicture = "quantum/root/.savers/screensaver.png";
    public static boolean screenSave = false;

    public static void main(String[] args) throws InterruptedException, IOException {
        Utils.clear();
        Quantum.pointer = 1;
        Logger.ok("Pointer settings was restored successfully", 1);
        Logger.prepare();
        Services.start();
        Logger.ok("Services activated success", 2);
        try {
            Console.start();
        } catch (Exception ex) {
            Logger.err("Caught exception. Entering rescue mode", 5);
            Utils.setColor(Colors.ANSI_BLACK_BACKGROUND);
            Utils.exec("clear");
            Logger.ok("Changing terminal mode", 1);
            Services.stop();
            Logger.ok("Stopping all services", 5);
            Rescue.main(ex);
        }
    }
}

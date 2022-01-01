package com.quantum.kernel;

import com.quantum.Quantum;
import com.quantum.utils.Keys;
import com.quantum.utils.ScreenSaverTimeout;
import com.quantum.utils.Utils;

import java.io.IOException;

public class ScreenSaver {

    public static void save() {
        try {
            if (Quantum.keyboardCode != Keys.NKBH) {
                ScreenSaverTimeout.resetTimeout();
                Quantum.screenSave = false;
                Utils.breakKeyboard();
                return;
            }
            ImageRender.main(new String[] {"-w", String.valueOf(Utils.getWidth()), "-h", String.valueOf(Utils.getHeight()), Quantum.screenSaverPicture});
            Thread.sleep(50);
            Utils.clear();
        } catch (IOException | InterruptedException e) {
            e.printStackTrace();
        }
    }

}

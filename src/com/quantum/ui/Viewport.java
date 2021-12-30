package com.quantum.ui;

import com.quantum.Quantum;
import com.quantum.elements.QElement;
import com.quantum.logger.Logger;
import com.quantum.utils.Colors;
import com.quantum.utils.IOBuffer;
import com.quantum.utils.Keys;
import com.quantum.utils.Utils;
import jline.Terminal;

import java.io.IOException;
import java.util.ArrayList;

public class Viewport {

    private IOBuffer stringAcc;
    private ArrayList<QElement> elements;

    public Viewport(ArrayList<QElement> elements) {
        this.elements = elements;
        this.stringAcc = new IOBuffer();
        this.stringAcc.setLength(Quantum.interfaceLength * (Utils.getHeight() - 5));
    }

    public void render() {
        Utils.setColor(Colors.ANSI_PURPLE_BACKGROUND);
        for (int i = 0; i < elements.size(); i++) {
            pointerKeys();
            elements.get(i).render(stringAcc, this);
        }
        Logger.ok("Viewport rendering almost finished", 1);
        getStringAcc().patch();
        Logger.ok("Viewport buffer was patched from rubbish", 1);
        getStringAcc().render();
        Logger.ok("Rendering viewport", 5);
        clearBuffer();
        Logger.ok("Buffer was cleaned", 1);
        Utils.setColor(Colors.ANSI_RESET);
    }

    private void pointerKeys() {
        if (Quantum.keyboardCode == Keys.PTR_UP) {
            Logger.ok("Got pointer up", 1);
            Quantum.pointer--;
            if (Quantum.pointer == Utils.getHeight() - 3) {
                stringAcc.pushBuffer();
            }
            Utils.breakKeyboard();
        } else if (Quantum.keyboardCode == Keys.PTR_DOWN) {
            Logger.ok("Got pointer down", 1);
            Quantum.pointer++;
            Utils.breakKeyboard();
        }
    }

    public void clearBuffer() {
        this.stringAcc.setLength(0);
        this.stringAcc.setCursor(0);
        this.stringAcc.setNextCursor(Utils.getWidth());
        this.stringAcc.setLine(1);
        this.stringAcc.setLength(Quantum.interfaceLength * (Utils.getHeight() - 6));
    }

    public QElement get(int index) {
        return elements.get(index);
    }

    public boolean add(QElement qElement) {
        return elements.add(qElement);
    }
    //    public void adjust() {
//        IOBuffer acc = new IOBuffer();
//        acc.setLength(Quantum.interfaceLength * (Utils.getHeight() - 3));
//        int line = 0;
//        int pos = 0;
//        int strPos = 0;
//        while (true) {
//            if (line == Utils.getHeight() - 3) break;
//            if (pos == acc.length()) break;
//            if (strPos == Quantum.interfaceLength) {
//                acc.setCharAt(pos, '\n');
//                pos++;
//                strPos = 0;
//                line++;
//                continue;
//            }
//            acc.setCharAt(pos, ' ');
//            pos++; strPos++;
//        }
//        this.setStringAcc(acc);
//    }

    public IOBuffer getStringAcc() {
        return stringAcc;
    }

    public void setStringAcc(IOBuffer stringAcc) {
        this.stringAcc = stringAcc;
    }
}

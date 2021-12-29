package com.quantum.ui;

import com.quantum.Quantum;
import com.quantum.elements.QElement;
import com.quantum.utils.Colors;
import com.quantum.utils.IOBuffer;
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
        for (QElement element : elements) {
            element.render(stringAcc);
        }
        getStringAcc().patch();
        getStringAcc().render();
        clearBuffer();
        Utils.setColor(Colors.ANSI_RESET);
    }

    public void clearBuffer() {
        this.stringAcc.setLength(0);
        this.stringAcc.setCursor(0);
        this.stringAcc.setNextCursor(Utils.getWidth());
        this.stringAcc.setLine(1);
        this.stringAcc.setLength(Quantum.interfaceLength * (Utils.getHeight() - 6));
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

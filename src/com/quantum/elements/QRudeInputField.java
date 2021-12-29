package com.quantum.elements;

import com.quantum.Quantum;
import com.quantum.utils.IOBuffer;
import com.quantum.utils.Keys;
import com.quantum.utils.Utils;

public class QRudeInputField implements QElement {

    private String prompt, acc;

    public QRudeInputField(String prompt) {
        this.prompt = prompt;
        this.acc = "";
    }

    @Override
    public void render(IOBuffer stringAcc) {
        stringAcc.addString(prompt);
        if (Quantum.keyboardCode != Keys.NKBH) {
            if (Quantum.keyboardCode == Keys.BACKSPACE) {
                this.acc = acc.substring(acc.length() - 1, 1);
            } else {
                this.acc += (char) Quantum.keyboardCode;
            }
            Utils.breakKeyboard();
        }
        stringAcc.addString(acc);
    }
}

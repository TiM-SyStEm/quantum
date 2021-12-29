package com.quantum.elements;

import com.quantum.Quantum;
import com.quantum.ui.Viewport;
import com.quantum.utils.IOBuffer;
import com.quantum.utils.Keys;
import com.quantum.utils.Utils;

public class QRudeInputField implements QElement {

    private String prompt;
    private StringBuilder buffer;
    private QEvent event;
    private int requiredPointer;

    public QRudeInputField(String prompt, QEvent event, int ptr) {
        this.prompt = prompt;
        this.buffer = new StringBuilder();
        this.event = event;
        this.requiredPointer = ptr;
    }

    @Override
    public void render(IOBuffer stringAcc, Viewport viewport) {
        stringAcc.addString(prompt);
        if (this.requiredPointer != Quantum.pointer) {
            stringAcc.addString(buffer.toString());
            return;
        }
        if (Quantum.keyboardCode != Keys.NKBH) {
            if (Quantum.keyboardCode == Keys.BACKSPACE) {
                try {
                    this.buffer.deleteCharAt(buffer.length() - 1);
                } catch (IndexOutOfBoundsException ex) {

                }
            } else if (Quantum.keyboardCode == Keys.ENTER) {
                event.execute(this, viewport);
            } else {
                this.buffer.append((char) Quantum.keyboardCode);
            }
            Utils.breakKeyboard();
        }
        stringAcc.addString(buffer.toString());
    }

    public String getPrompt() {
        return prompt;
    }

    public StringBuilder getBuffer() {
        return buffer;
    }

    public QEvent getEvent() {
        return event;
    }

    @Override
    public String toString() {
        return buffer.toString();
    }
}

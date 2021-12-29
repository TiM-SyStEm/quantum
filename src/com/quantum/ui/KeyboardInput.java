package com.quantum.ui;

import com.quantum.Quantum;
import com.quantum.utils.Colors;
import com.quantum.utils.KeyboardChecker;
import com.quantum.utils.Utils;

public class KeyboardInput {

    private String prompt;

    public KeyboardInput(String prompt) {
        this.prompt = prompt;
        new Thread(new KeyboardChecker()).start();
    }

    public void show() {
        Utils.setColor(Colors.ANSI_RESET);
        System.out.print(this.getPrompt());
        System.out.println(Quantum.keyboardCode);
    }

    public String getPrompt() {
        return prompt;
    }

    public void setPrompt(String prompt) {
        this.prompt = prompt;
    }
}

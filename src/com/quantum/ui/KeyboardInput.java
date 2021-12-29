package com.quantum.ui;

import com.quantum.Quantum;
import com.quantum.logger.Logger;
import com.quantum.utils.Colors;
import com.quantum.utils.KeyboardChecker;
import com.quantum.utils.Utils;

public class KeyboardInput {

    private String prompt;

    public KeyboardInput(String prompt) {
        this.prompt = prompt;
    }

    public void show() {
        Utils.setColor(Colors.ANSI_RESET);
        System.out.print(this.getPrompt());
        System.out.println(Quantum.keyboardCode);
        Logger.ok("Input was received", 1);
    }

    public String getPrompt() {
        return prompt;
    }

    public void setPrompt(String prompt) {
        this.prompt = prompt;
    }
}

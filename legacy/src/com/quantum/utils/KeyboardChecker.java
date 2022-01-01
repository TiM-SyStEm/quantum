package com.quantum.utils;

import com.quantum.Quantum;

public class KeyboardChecker implements Runnable {
    @Override
    public void run() {
        while (true) {
            Quantum.keyboardCode = Utils.getCharacter();
        }
    }
}

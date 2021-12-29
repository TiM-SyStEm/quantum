package com.quantum.elements;

import com.quantum.ui.Viewport;

import java.util.ArrayList;

public interface QEvent {
    void execute(QElement caller, Viewport viewport);
}

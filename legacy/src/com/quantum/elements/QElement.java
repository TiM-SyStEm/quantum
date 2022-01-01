package com.quantum.elements;

import com.quantum.ui.Viewport;
import com.quantum.utils.IOBuffer;


public interface QElement {
    void render(IOBuffer stringAcc, Viewport viewport);
}

package com.quantum.elements;

import com.quantum.ui.Viewport;
import com.quantum.utils.IOBuffer;

import java.util.ArrayList;

public class QTextElement implements QElement {

    private String text;

    public QTextElement(String text) {
        this.text = text;
    }

    @Override
    public void render(IOBuffer acc, Viewport viewport) {
        acc.addString(text);
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }
}

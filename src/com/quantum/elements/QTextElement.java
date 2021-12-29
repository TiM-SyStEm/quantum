package com.quantum.elements;

import com.quantum.utils.IOBuffer;

public class QTextElement implements QElement {
    private String text;

    public QTextElement(String text) {
        this.text = text;
    }

    @Override
    public void render(IOBuffer acc) {
        char[] chars = text.toCharArray();
        for (int i = 0; i < text.length(); i++) {
            char ch = chars[i];
            acc.addChar(ch);
        }
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }
}

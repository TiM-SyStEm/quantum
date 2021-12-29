package com.quantum.ui;

import com.quantum.utils.Colors;
import com.quantum.utils.Utils;

import javax.xml.namespace.QName;

public class BName {

    private String text;

    public BName(String text) {
        this.text = text;
    }

    public void show() {
        Utils.setColor(Colors.ANSI_GREEN_BACKGROUND);
        int size = Utils.getWidth();
        int left = size/2 - text.length();
        int right = size - left;
        String format = "%" + left + "s%-" + right + "s";
        System.out.printf(String.format(format, " ", text).replaceAll(" ", "="));
        Utils.setColor(Colors.ANSI_RESET);
    }

    public String getText() {
        return text;
    }

    public void setText(String text) {
        this.text = text;
    }
}

package com.quantum.utils;

import com.quantum.Quantum;
import com.quantum.logger.Logger;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.stream.IntStream;

public class IOBuffer {

    private int nextCursor;
    private int line;
    private int cursor;
    private int pagesCount;
    private HashMap<Integer, Integer> lines;
    private StringBuilder builder;
    private ArrayList<IOBuffer> pages;

    public IOBuffer() {
        this.builder = new StringBuilder();
        this.lines = new HashMap<>();
        lines.put(0, 1);
        this.cursor = 0;
        this.nextCursor = Utils.getWidth();
        this.line = 1;
        this.pages = new ArrayList<>();
        this.pages.add(this);
        this.pagesCount = 0;
    }

    public IOBuffer last() {
        return pages.get(pagesCount);
    }

    public void pushBuffer() {
        pages.add(new IOBuffer());
        pagesCount++;
    }

    public void popBuffer() {
        pages.remove(pagesCount);
        pagesCount--;
    }

    public StringBuilder currentBuffer() {
        return pages.get(pagesCount).getBuilder();
    }

    public StringBuilder deleteCharAt(int index) {
        return currentBuffer().deleteCharAt(index);
    }

    public int length() {
        return builder.length();
    }

    public void setLength(int newLength) {
        builder.setLength(newLength);
    }

    public char charAt(int index) {
        return builder.charAt(index);
    }

    public void setCharAt(int index, char ch) {
        if (index == this.length()) {
            this.clear();
            builder.setCharAt(0, ch);
        } else
        builder.setCharAt(index, ch);
    }

    public void clear() {
        this.setLength(0);
        this.setCursor(0);
        this.setNextCursor(Utils.getWidth());
        this.setLine(1);
        this.setLength(Quantum.interfaceLength * (Utils.getHeight() - 6));
    }

    public String substring(int start) {
        return builder.substring(start);
    }

    public CharSequence subSequence(int start, int end) {
        return builder.subSequence(start, end);
    }

    public String substring(int start, int end) {
        return builder.substring(start, end);
    }

    public IntStream chars() {
        return builder.chars();
    }

    public IntStream codePoints() {
        return builder.codePoints();
    }

    public boolean isEmpty() {
        return builder.isEmpty();
    }

    // Cursor methods

    public int getCursor() {
        return cursor;
    }

    public void setCursor(int cursor) {
        this.cursor = cursor;
    }

    public int getNextCursor() {
        return nextCursor;
    }

    public void setNextCursor(int nextCursor) {
        this.nextCursor = nextCursor;
    }

    public int getLine() {
        return line;
    }

    public void setLine(int line) {
        this.line = line;
    }

    public void patch() {
        int length = this.length();
        this.deleteCharAt(length - 2); this.deleteCharAt(length - 2);
    }

    public void addString(String str) {
        for (char ch : str.toCharArray()) {
            addChar(ch);
        }
    }

    public void addChar(char ch) {
        if (ch == '\n') {
            this.line++;
            this.setCursor(this.nextCursor);
            lines.put(this.cursor, this.line);
            this.setNextCursor(Utils.getWidth() * this.line);
        } else {
            this.setCharAt(this.cursor, ch);
            this.cursor++;
        }
    }

    public void render() {
        // I'm stuck at this for 1-2 hours =(
        char[] chars = builder.toString().toCharArray();
        int targetLine = Quantum.pointer;
        int currentLine = 1;
        for (int i = 0; i < chars.length; i++) {
            char ch = chars[i];
            if (lines.containsKey(i) && lines.get(i) == targetLine) {
                Utils.setColor(Colors.ANSI_RESET);
                Utils.setColor(Colors.ANSI_YELLOW_BACKGROUND);
                System.out.print(ch == '\0' ? ' ' : ch);
                Utils.setColor(Colors.ANSI_RESET);
                Utils.setColor(Colors.ANSI_PURPLE_BACKGROUND);
            } else
                System.out.print(ch == '\0' ? ' ' : ch);
        }
    }

    public StringBuilder getBuilder() {
        return builder;
    }

    public void setBuilder(StringBuilder builder) {
        this.builder = builder;
    }

    @Override
    public String toString() {
        return builder.toString();
    }
}

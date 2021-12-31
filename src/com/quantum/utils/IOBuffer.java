package com.quantum.utils;

import com.quantum.Quantum;
import com.quantum.kernel.Console;
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
        last().clear();
    }

    public void popBuffer() {
        pages.remove(pagesCount);
        pagesCount--;
    }

    public StringBuilder currentBuffer() {
        return last().builder;
    }

    public StringBuilder deleteCharAt(int index) {
        return currentBuffer().deleteCharAt(index);
    }

    public int length() {
        return currentBuffer().length();
    }

    public void setLength(int newLength) {
        currentBuffer().setLength(newLength);
    }

    public void setCharAt(int index, char ch) {
        currentBuffer().setCharAt(index, ch);
    }

    public void clear() {
        last().setLength(0);
        last().setCursor(0);
        last().setNextCursor(Utils.getWidth());
        last().setLine(1);
        last().setLength(Quantum.interfaceLength * (Utils.getHeight() - 6));
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

    public void setNextCursor(int nextCursor) {
        this.nextCursor = nextCursor;
    }

    public void setLine(int line) {
        this.line = line;
    }

    public void patch() {
        int length = last().length();
        currentBuffer().deleteCharAt(length - 1); currentBuffer().deleteCharAt(length - 2);
    }

    public void addString(String str) {
        for (char ch : str.toCharArray()) {
            addChar(ch);
        }
    }

    public void addChar(char ch) {
        if (last().cursor + 1 >= last().length()) {
            this.pushBuffer();
            this.clear();
            Console.setConsolePointer(1);
            Logger.ok("Generated new page of IOBuffer", 1);
        }
        if (ch == '\n') {
            last().line++;
            last().setCursor(last().nextCursor);
            last().lines.put(last().cursor, last().line);
            last().setNextCursor(Utils.getWidth() * last().line);
        } else {
            last().setCharAt(last().cursor, ch);
            last().cursor++;
        }
    }

    public void render() {
        // I'm stuck at this for 1-2 hours =(
        char[] chars = last().toString().toCharArray();
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
        return currentBuffer();
    }

    public void setBuilder(StringBuilder builder) {
        last().builder = builder;
    }

    @Override
    public String toString() {
        return currentBuffer().toString();
    }
}

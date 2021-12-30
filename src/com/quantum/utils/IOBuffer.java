package com.quantum.utils;

import com.quantum.Quantum;
import com.quantum.logger.Logger;

import java.util.HashMap;
import java.util.stream.IntStream;

public class IOBuffer {

    private int nextCursor;
    private int line;
    private int cursor;
    private HashMap<Integer, Integer> lines;
    private StringBuilder builder;

    public IOBuffer() {
        this.builder = new StringBuilder();
        this.lines = new HashMap<>();
        lines.put(0, 1);
        this.cursor = 0;
        this.nextCursor = Utils.getWidth();
        this.line = 1;
    }

    public int compareTo(StringBuilder another) {
        return builder.compareTo(another);
    }

    public StringBuilder append(Object obj) {
        return builder.append(obj);
    }

    public StringBuilder append(String str) {
        return builder.append(str);
    }

    public StringBuilder append(StringBuffer sb) {
        return builder.append(sb);
    }

    public StringBuilder append(CharSequence s) {
        return builder.append(s);
    }

    public StringBuilder append(CharSequence s, int start, int end) {
        return builder.append(s, start, end);
    }

    public StringBuilder append(char[] str) {
        return builder.append(str);
    }

    public StringBuilder append(char[] str, int offset, int len) {
        return builder.append(str, offset, len);
    }

    public StringBuilder append(boolean b) {
        return builder.append(b);
    }

    public StringBuilder append(char c) {
        return builder.append(c);
    }

    public StringBuilder append(int i) {
        return builder.append(i);
    }

    public StringBuilder append(long lng) {
        return builder.append(lng);
    }

    public StringBuilder append(float f) {
        return builder.append(f);
    }

    public StringBuilder append(double d) {
        return builder.append(d);
    }

    public StringBuilder appendCodePoint(int codePoint) {
        return builder.appendCodePoint(codePoint);
    }

    public StringBuilder delete(int start, int end) {
        return builder.delete(start, end);
    }

    public StringBuilder deleteCharAt(int index) {
        return builder.deleteCharAt(index);
    }

    public StringBuilder replace(int start, int end, String str) {
        return builder.replace(start, end, str);
    }

    public StringBuilder insert(int index, char[] str, int offset, int len) {
        return builder.insert(index, str, offset, len);
    }

    public StringBuilder insert(int offset, Object obj) {
        return builder.insert(offset, obj);
    }

    public StringBuilder insert(int offset, String str) {
        return builder.insert(offset, str);
    }

    public StringBuilder insert(int offset, char[] str) {
        return builder.insert(offset, str);
    }

    public StringBuilder insert(int dstOffset, CharSequence s) {
        return builder.insert(dstOffset, s);
    }

    public StringBuilder insert(int dstOffset, CharSequence s, int start, int end) {
        return builder.insert(dstOffset, s, start, end);
    }

    public StringBuilder insert(int offset, boolean b) {
        return builder.insert(offset, b);
    }

    public StringBuilder insert(int offset, char c) {
        return builder.insert(offset, c);
    }

    public StringBuilder insert(int offset, int i) {
        return builder.insert(offset, i);
    }

    public StringBuilder insert(int offset, long l) {
        return builder.insert(offset, l);
    }

    public StringBuilder insert(int offset, float f) {
        return builder.insert(offset, f);
    }

    public StringBuilder insert(int offset, double d) {
        return builder.insert(offset, d);
    }

    public int indexOf(String str) {
        return builder.indexOf(str);
    }

    public int indexOf(String str, int fromIndex) {
        return builder.indexOf(str, fromIndex);
    }

    public int lastIndexOf(String str) {
        return builder.lastIndexOf(str);
    }

    public int lastIndexOf(String str, int fromIndex) {
        return builder.lastIndexOf(str, fromIndex);
    }

    public StringBuilder reverse() {
        return builder.reverse();
    }

    public int length() {
        return builder.length();
    }

    public int capacity() {
        return builder.capacity();
    }

    public void ensureCapacity(int minimumCapacity) {
        builder.ensureCapacity(minimumCapacity);
    }

    public void trimToSize() {
        builder.trimToSize();
    }

    public void setLength(int newLength) {
        builder.setLength(newLength);
    }

    public char charAt(int index) {
        return builder.charAt(index);
    }

    public int codePointAt(int index) {
        return builder.codePointAt(index);
    }

    public int codePointBefore(int index) {
        return builder.codePointBefore(index);
    }

    public int codePointCount(int beginIndex, int endIndex) {
        return builder.codePointCount(beginIndex, endIndex);
    }

    public int offsetByCodePoints(int index, int codePointOffset) {
        return builder.offsetByCodePoints(index, codePointOffset);
    }

    public void getChars(int srcBegin, int srcEnd, char[] dst, int dstBegin) {
        builder.getChars(srcBegin, srcEnd, dst, dstBegin);
    }

    public void setCharAt(int index, char ch) {
        if (index == this.length()) {
            this.clear();
            builder.setCharAt(0, ch);
        } else
        builder.setCharAt(index, ch);
    }

    private void clear() {
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

    @Override
    public String toString() {
        return builder.toString();
    }
}

package com.quantum.ui;

public class QWindow {

    private TaskBar taskbar;
    private Viewport viewport;
    private KeyboardInput input;
    private BName name;

    public QWindow(Viewport viewport, int notifications, String prompt, String name) {
        this.taskbar = new TaskBar(notifications);
        this.viewport = viewport;
        this.input = new KeyboardInput(prompt);
        this.name = new BName(name);
    }

    public TaskBar getTaskbar() {
        return taskbar;
    }

    public void setTaskbar(TaskBar taskbar) {
        this.taskbar = taskbar;
    }

    public Viewport getViewport() {
        return viewport;
    }

    public void setViewport(Viewport viewport) {
        this.viewport = viewport;
    }

    public KeyboardInput getInput() {
        return input;
    }

    public void setInput(KeyboardInput input) {
        this.input = input;
    }

    public BName getName() {
        return name;
    }

    public void setName(BName name) {
        this.name = name;
    }
}

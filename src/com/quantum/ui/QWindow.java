package com.quantum.ui;

public class QWindow {

    private TaskBar taskbar;
    private Viewport viewport;

    public QWindow(Viewport viewport, int notifications) {
        this.taskbar = new TaskBar(notifications);
        this.viewport = viewport;
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
}

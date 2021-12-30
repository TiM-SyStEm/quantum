package com.quantum.ui;

import com.quantum.Quantum;
import com.quantum.logger.Logger;
import com.quantum.utils.Colors;
import com.quantum.utils.Utils;

public class Compositor {

    private QWindow qWindow;

    public Compositor(QWindow qWindow) {
        this.qWindow = qWindow;
    }

    public void compose(TaskBar taskbar) {
        taskbar.show();
    }

    public void compose(Viewport viewport) {
        viewport.render();
    }

    public void compose(KeyboardInput input) {
        input.show();
    }

    public void compose(BName name) {
        name.show();
    }

    public void compose(LoggerBar bar) {
        bar.show();
    }

    public void compose() throws InterruptedException {
        this.compose(qWindow.getTaskbar());
        this.compose(qWindow.getViewport());
        this.compose(qWindow.getName());
        this.compose(qWindow.getInput());
        this.compose(LoggerBar.getRuntime());
        Utils.clear();
        Logger.ok("Render was success", 5);
    }
}

package com.quantum.ui;

import com.quantum.Quantum;
import com.quantum.utils.Colors;
import com.quantum.utils.Utils;
import com.quantum.utils.WIFIChecker;

public class TaskBar {

    private int notifications;

    public TaskBar(int nots) {
        this.notifications = nots;
    }

    public void show() {
        // 0 Notes | Date | WIFI? | SHUTDOWN (CTRL+C)
        String date = Utils.getDate();
        String space = Utils.getSpace(3);
        StringBuilder acc = new StringBuilder();
        acc.append(Colors.ANSI_CYAN_BACKGROUND);
        acc.append(this.notifications).append(" Notifications").append(space).append("|").append(space); // Placeholder for notifications
        acc.append(date);
        acc.append(space).append("|").append(space);
        acc.append(Quantum.gotWifi ? "^" : "X");
        acc.append(space).append("|").append(space);
        acc.append("SHUTDOWN (CTRL-C) ").append(space).append(Quantum.pointer);
        acc.append(Colors.ANSI_RESET);
        Quantum.interfaceLength = acc.toString().length();
        System.out.println(acc.toString());
    }
}

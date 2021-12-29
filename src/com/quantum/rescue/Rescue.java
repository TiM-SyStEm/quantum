package com.quantum.rescue;

import com.quantum.Quantum;
import com.quantum.logger.Logger;
import com.quantum.utils.Colors;
import com.quantum.utils.Keys;
import com.quantum.utils.Services;
import com.quantum.utils.Utils;

import java.util.Queue;
import java.util.Scanner;

public class Rescue {

    static Scanner sc = new Scanner(System.in);

    public static void main(Exception ex) throws InterruptedException {
        System.out.println("Caught exception: " + ex.getMessage());
        rescue(ex);
    }

    public static void rescue(Exception ex) throws InterruptedException {
        while (true) {
            String input = prompt();
            if (input.equalsIgnoreCase("type")) {
                System.out.println(ex.getCause());
            } else if (input.equalsIgnoreCase("msg")) {
                System.out.println(ex.getMessage());
            } else if (input.equalsIgnoreCase("repair")) {
                Logger.ok("Rebooting kernel..", 5);
                System.out.println("Rebooting kernel...");
                Quantum.main(new String[0]);
            } else if (input.equalsIgnoreCase("exit")) {
                System.out.println("Killing kernel parts...");
                Logger.ok("Killing kernel parts..", 5);
                Utils.setColor(Colors.ANSI_RESET);
                System.exit(1);
            } else {
                System.out.println("Command not found: '" + input + "'");
            }
        }
    }

    public static String prompt() {
        System.out.print("quantum rescue>");
        String result = sc.nextLine();
        System.out.print("\n");
        return result;
    }
}

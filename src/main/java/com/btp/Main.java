package com.btp;

import javax.swing.*;
import java.awt.*;

public class Main{


    private static void createWindow(){
        JFrame frame = new JFrame("simpleGUI");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        JLabel textLabel = new JLabel("I'm a label",SwingConstants.CENTER);
        textLabel.setPreferredSize(new Dimension(300,100));
        frame.getContentPane().add(textLabel, BorderLayout.CENTER);

        frame.setLocationRelativeTo(null);
        frame.pack();
        frame.setVisible(true);
    }


    public static void main(String[] args) {
        createWindow();
        System.out.println("window running");
    }
}

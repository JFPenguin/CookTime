package com.btp.gui;

import javax.swing.*;
import java.awt.*;

public class ServerGUI extends JFrame{
    private JPanel mainPanel;
    private JPanel rightPanel;
    private JPanel leftPanel;
    private JPanel middlePanel;
    private JTextPane serverConsoleMonitor;
    private JTable chefRequestTable;
    private JButton acceptButton;
    private JButton refuseButton;

    public ServerGUI() {
        setTitle("CookTime Server Manager");
        setSize(800,600);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        add(mainPanel);
//        mainPanel.add(rightPanel);
//        mainPanel.add(middlePanel);
//        mainPanel.add(leftPanel);
    }
}


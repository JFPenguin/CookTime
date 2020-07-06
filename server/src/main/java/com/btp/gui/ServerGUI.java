package com.btp.gui;

import javax.swing.*;
import java.awt.*;

public class ServerGUI extends JFrame{
    private JPanel mainPanel;
    private JPanel rightPanel;
    private JPanel leftPanel;
    private JPanel middlePanel;
    private JTextArea serverConsoleMonitor;
    private JTable chefRequestTable;
    private JButton acceptButton;
    private JButton refuseButton;
    private JTable table1;
    private JTextArea userSearchBoxTextArea;
    private JTable table2;
    private JTextArea textArea1;
    private JButton sendButton;

    public ServerGUI() {
        setTitle("CookTime Server Manager");
        setSize(1200,650);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        add(mainPanel);
        serverConsoleMonitor.setEditable(false);
    }

    public void printLn(String txt) {
        serverConsoleMonitor.setEditable(true);
        serverConsoleMonitor.append(txt+"\n");
        serverConsoleMonitor.setEditable(false);
    }
}


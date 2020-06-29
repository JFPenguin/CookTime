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
        setSize(1000,600);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        add(mainPanel);


    }

}


package com.btp;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.swing.*;
import java.awt.*;


public class Main extends HttpServlet {

    public void init() {
        createWindow();
    }

    private static void createWindow(){
        JFrame frame = new JFrame("simpleGUI");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        JLabel textLabel = new JLabel("Hola soy su server pa",SwingConstants.CENTER);
        textLabel.setPreferredSize(new Dimension(300,100));
        frame.getContentPane().add(textLabel, BorderLayout.CENTER);

        frame.setLocationRelativeTo(null);
        frame.pack();
        frame.setVisible(true);
    }
}

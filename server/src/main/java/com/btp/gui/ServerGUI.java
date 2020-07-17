package com.btp.gui;

import com.btp.serverData.clientObjects.User;
import com.btp.serverData.repos.UserRepo;
import com.btp.utils.Notifier;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

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
    private JTextArea messageTextArea;
    private JButton sendButton;
    private JButton searchButton;
    private JCheckBox notifyAllCheckBox;
    private User tmpUser;

    public ServerGUI() {
        setTitle("CookTime Server Manager");
        setSize(1200,650);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        add(mainPanel);
        serverConsoleMonitor.setEditable(false);
        searchButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                searchUser();
            }
        });
        sendButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                if(notifyAllCheckBox.isSelected()){
                    Notifier.notifyAll(messageTextArea.getText());
                    printLn("sent message to all users");
                    printLn("message:\n"+messageTextArea.getText());
                    printLn("updating dataBase");

                }
                else if(tmpUser!=null){
                    printLn("send message to ;"+tmpUser.getEmail()+";");
                    tmpUser.sendMessage(messageTextArea.getText());
                    printLn("message:\n"+messageTextArea.getText());
                    printLn("updating dataBase");
                }
            }
        });
    }

    public void printLn(String txt) {
        serverConsoleMonitor.setEditable(true);
        serverConsoleMonitor.append(txt+"\n");
        serverConsoleMonitor.setEditable(false);
    }

    public void searchUser(){
        if(userSearchBoxTextArea.getText().isEmpty()){
            searchUser();
            printLn("ingrese un valor de busqueda valido");
            tmpUser=null;
        }
        else{
            if(UserRepo.getUser(userSearchBoxTextArea.getText())==null) {
                printLn("user ;" + userSearchBoxTextArea.getText() + " not found.");
                tmpUser = null;
            }
            else{
                tmpUser = UserRepo.getUser(userSearchBoxTextArea.getText());
                printLn("user:"+tmpUser.getEmail()+" loaded");
                populateFields(tmpUser);
            }
        }
    }

    private void populateFields(User user) {
        printLn("loading user data...");
    }


}


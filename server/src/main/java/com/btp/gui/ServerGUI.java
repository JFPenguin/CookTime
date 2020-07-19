package com.btp.gui;

import com.btp.serverData.clientObjects.User;
import com.btp.serverData.repos.UserRepo;
import com.btp.utils.Notifier;

import javax.swing.*;
import javax.swing.table.DefaultTableModel;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

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
    private JButton refreshButton;
    private User tmpUser;
    DefaultTableModel chefRequestModel = new DefaultTableModel(){
        @Override
        public boolean isCellEditable(int row, int column){
            return false;
        }
    };



    public ServerGUI() {
        setTitle("CookTime Server Manager");
        setSize(1200,650);
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        add(mainPanel);
        serverConsoleMonitor.setEditable(false);
        chefRequestTable.setModel(chefRequestModel);
        chefRequestModel.addColumn("userName");
        chefRequestModel.addColumn("email");
        chefRequestModel.addColumn("recipes");
        loadActiveChefRequests();
        searchButton.addActionListener(e -> searchUser());
        sendButton.addActionListener(e -> sendNotification());
        refreshButton.addActionListener(e -> loadActiveChefRequests());
        acceptButton.addActionListener(e -> acceptChef());
        refuseButton.addActionListener(e -> rejectChef());
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

    private void loadActiveChefRequests(){
        chefRequestModel.setRowCount(0);
        ArrayList<String> chefRequests = UserRepo.getChefRequests();
        for (String chefRequest : chefRequests) {
            String name = UserRepo.getUser(chefRequest).fullName();
            String email = UserRepo.getUser(chefRequest).getEmail();
            int recipes = UserRepo.getUser(chefRequest).userCreatedRecipes().size();
            chefRequestModel.addRow(new Object[]{name, email, recipes});
        }
    }

    private void sendNotification(){
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

    private void acceptChef() {
        int row = chefRequestTable.getSelectedRow();
        String userID = (String) chefRequestModel.getValueAt(row,1);
        UserRepo.getUser(userID).setChef(true);
        UserRepo.removeChefRequest(userID);
        loadActiveChefRequests();
        printLn(UserRepo.getUser(userID).fullName()+"'s chef request accepted");
        Notifier.notify(userID,"Congratulations, we have approved your chef request");
    }

    private void rejectChef() {
        int row = chefRequestTable.getSelectedRow();
        String userID = (String) chefRequestModel.getValueAt(row,1);
        UserRepo.getUser(userID).setChef(false);
        UserRepo.removeChefRequest(userID);
        loadActiveChefRequests();
        printLn(UserRepo.getUser(userID).fullName()+"'s chef request rejected");
        Notifier.notify(userID,"We have decided at this time not to approve your chef request, please wait 2 weeks, before sending another request");

    }


}


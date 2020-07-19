package com.btp.gui;

import com.btp.serverData.clientObjects.DishTime;
import com.btp.serverData.clientObjects.DishType;
import com.btp.serverData.clientObjects.User;
import com.btp.serverData.repos.RecipeRepo;
import com.btp.serverData.repos.UserRepo;
import com.btp.utils.Notifier;

import javax.swing.*;
import javax.swing.table.DefaultTableModel;
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
    private JTable userData;
    private JTextArea userSearchBoxTextArea;
    private JTable userRecipes;
    private JTextArea messageTextArea;
    private JButton sendButton;
    private JButton searchButton;
    private JButton refreshButton;
    private JRadioButton allUsersRadioButton;
    private JRadioButton thisUserRadioButton;
    private ButtonGroup radioButtonGroup = new ButtonGroup();
    private User tmpUser;
    private final DefaultTableModel chefRequestModel = new DefaultTableModel(){
        @Override
        public boolean isCellEditable(int row, int column){
            return false;
        }
    };
    private final DefaultTableModel userDataModel = new DefaultTableModel(){
        @Override
        public boolean isCellEditable(int row, int column){
            return false;
        }
    };
    private final DefaultTableModel userRecipesModel = new DefaultTableModel(){
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
        chefRequestModel.addColumn("Name");
        chefRequestModel.addColumn("Email");
        chefRequestModel.addColumn("Recipes");
        userData.setModel(userDataModel);
        userDataModel.addColumn("Name");
        userDataModel.addColumn("Age");
        userDataModel.addColumn("Recipes");
        userDataModel.addColumn("Chef Status");
        userDataModel.addColumn("Followers");
        userDataModel.addColumn("Following");
        userRecipes.setModel(userRecipesModel);
        userRecipesModel.addColumn("Name");
        userRecipesModel.addColumn("Number id");
        userRecipesModel.addColumn("Type");
        userRecipesModel.addColumn("Time");
        userRecipesModel.addColumn("Rating");
        userRecipesModel.addColumn("# comments");
        radioButtonGroup.add(allUsersRadioButton);
        radioButtonGroup.add(thisUserRadioButton);

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
            printLn("enter an user's email");
            tmpUser=null;
            userDataModel.setRowCount(0);
            userRecipesModel.setRowCount(0);
        }
        else{
            if(UserRepo.getUser(userSearchBoxTextArea.getText())==null) {
                printLn("user ;" + userSearchBoxTextArea.getText() + " not found.");
                userDataModel.setRowCount(0);
                userRecipesModel.setRowCount(0);
                tmpUser = null;
            }
            else{
                tmpUser = UserRepo.getUser(userSearchBoxTextArea.getText());
                populateFields(tmpUser);
            }
        }
    }

    private void populateFields(User user) {
        userSearchBoxTextArea.setText("");
        printLn("loading user data...");
        loadUserDetails(user);
        loadUserRecipes(user);
        printLn("user:"+user.getEmail()+" loaded");
        thisUserRadioButton.setSelected(true);

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

    private void loadUserDetails(User user){
        userDataModel.setRowCount(0);
        String name = user.fullName();
        int age = user.getAge();
        int recipes = user.userCreatedRecipes().size();
        boolean chefStatus = user.isChef();
        int followers = user.getFollowerEmails().size();
        int following = user.getFollowingEmails().size();
        userDataModel.addRow(new Object[]{name, age, recipes,chefStatus,followers,following});
    }

    private void loadUserRecipes(User user){
        userRecipesModel.setRowCount(0);
        ArrayList<Integer> userRecipes = user.getRecipeList();
        for (int userRecipe : userRecipes) {
            String name = RecipeRepo.getRecipe(userRecipe).getName();
            int id = RecipeRepo.getRecipe(userRecipe).getId();
            DishType type = RecipeRepo.getRecipe(userRecipe).getDishType();
            DishTime time = RecipeRepo.getRecipe(userRecipe).getDishTime();
            float rating = RecipeRepo.getRecipe(userRecipe).getScore();
            int comments = RecipeRepo.getRecipe(userRecipe).getComments().size();
            userRecipesModel.addRow(new Object[]{name, id, type,time,rating,comments});
        }
    }

    private void sendNotification(){
        if(allUsersRadioButton.isSelected()){
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


package com.btp.serverData.repos;

import com.btp.Initializer;
import com.btp.dataStructures.trees.UserBST;
import com.btp.utils.DataWriter;
import com.btp.serverData.clientObjects.User;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

public class UserRepo {
    private static UserBST userTree = new UserBST();
    private static final DataWriter<UserBST> dataWriter = new DataWriter<>();
    private static final String path =System.getProperty("project.folder")+"/dataBase/userDataBase.json";


    public static void addUser(User user) {
        userTree.insert(user);
        System.out.println("user added");
        if(Initializer.isGUIOnline()){
            Initializer.getServerGUI().printLn("user added");
        }
        dataWriter.writeData(userTree, path);

    }

    public static void updateTree(){
        dataWriter.writeData(userTree, path);
    }

    public static User getUser(String email){
        return userTree.getElementByEmail(email);
    }

    public static boolean checkByID(String email) {
        return userTree.checkByEmail(email);
    }

    public static ArrayList<String> searchUsers(String data){
        return userTree.searchPreOrder(data);
    }

    public static void loadTree() throws IOException {
        System.out.println("loading user data base...");
        ObjectMapper objectMapper = new ObjectMapper();
        FileReader file = new FileReader(path);
        userTree = objectMapper.readValue(file, userTree.getClass());
    }

}

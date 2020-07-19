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
    private static ArrayList<String> chefRequests = new ArrayList<>();
    private static final DataWriter<UserBST> dataWriter = new DataWriter<>();
    private static final DataWriter<ArrayList<String>> dataWriter2 = new DataWriter<>();
    private static final String pathUserDataBase =System.getProperty("project.folder")+"/dataBase/userDataBase.json";
    private static final String pathChefRequestDataBase =System.getProperty("project.folder")+"/dataBase/chefRequestsDataBase.json";


    public static void addUser(User user) {
        userTree.insert(user);
        System.out.println("user added");
        if(Initializer.isGUIOnline()){
            Initializer.getServerGUI().printLn("user added");
        }
        dataWriter.writeData(userTree, pathUserDataBase);
    }

    public static void deleteRecipe(int id){
        userTree.deleteRecipe(id);
    }

    public static void updateTree(){
        dataWriter.writeData(userTree, pathUserDataBase);
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
        FileReader file = new FileReader(pathUserDataBase);
        userTree = objectMapper.readValue(file, userTree.getClass());
//        System.out.println("loading chef requests...");
//        FileReader file2 = new FileReader(pathChefRequestDataBase);
//        chefRequests = objectMapper.readValue(file2, chefRequests.getClass());
    }

    public static void notifyAll(String notification) {
        userTree.messageAll(notification);
    }

    public static void addChefRequest(String id) {
        chefRequests.add(id);
        dataWriter2.writeData(chefRequests, pathChefRequestDataBase);
    }

    public static ArrayList<String> recommend(String data){
        return userTree.recommend(data);
    }

    public static void removeChefRequest(String id){
        chefRequests.remove(id);
        dataWriter2.writeData(chefRequests, pathChefRequestDataBase);
    }

    public static boolean isActiveRequest(String id) {
        return chefRequests.contains(id);
    }

}

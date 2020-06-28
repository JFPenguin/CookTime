package com.btp.serverData.repos;

import com.btp.dataStructures.trees.UserBST;
import com.btp.utils.DataWriter;
import com.btp.serverData.clientObjects.User;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.FileReader;
import java.io.IOException;

public class UserRepo {
    private static UserBST userTree = new UserBST();
    private static final DataWriter<UserBST> dataWriter = new DataWriter<>();
    private static final String path ="C:/Users/Joe_A/OneDrive - Estudiantes ITCR/Universidad/2020-1/Algoritmos y estructuras de datos 1/CookTime/dataBase/userDataBase.json";


    public static void addUser(User user) {
        userTree.insert(user);
        System.out.println("user added");
        dataWriter.writeData(userTree, path);

    }

    public static User getUser(int id){
        return userTree.getElementById(id);
    }

    public static boolean checkByID(int id) {
        return userTree.checkById(id);
    }


    public static void loadTree() throws IOException {
        System.out.println("loading user data base...");
        ObjectMapper objectMapper = new ObjectMapper();
        FileReader file = new FileReader(path);
        userTree = objectMapper.readValue(file, userTree.getClass());
    }

}

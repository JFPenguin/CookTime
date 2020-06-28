package com.btp.serverData;

import com.btp.dataStructures.trees.BinarySearchTree;

import java.util.Random;

public class UserRepo {
    private static final Random random = new Random();
    private static final BinarySearchTree<User> userTree = new BinarySearchTree<>();
    private static final DataWriter<BinarySearchTree<User>> dataWriter = new DataWriter<>();


    public static void addUser(User user) {
        int i = random.nextInt(999) + 1;
        System.out.println("generating id...");
        System.out.println("userID: "+i);
        while (userTree.checkById(i)){
            i = random.nextInt(999) + 1;
            System.out.println("new Number!");
        }
        user.setId(i);
        userTree.insert(user);
        System.out.println("user Added");
        dataWriter.writeData(userTree, "C:/Users/Joe_A/OneDrive - Estudiantes ITCR/Universidad/2020-1/Algoritmos y estructuras de datos 1/CookTime/dataBase/userDataBase.json");

    }

    public static User getUser(int id){
        return userTree.getElementById(id);
    }

    private static BinarySearchTree<User> loadTree(String path){
        BinarySearchTree<User> loadedTree = new BinarySearchTree<>();
        DataReader<BinarySearchTree<User>> dataReader = new DataReader();
        loadedTree = dataReader.readData(path);
        return loadedTree;

    }

}

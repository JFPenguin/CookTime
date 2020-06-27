package com.btp.serverData;

import com.btp.dataStructures.lists.SinglyList;
import com.btp.dataStructures.trees.BinarySearchTree;

import java.util.Random;

//TODO change data structure from list to the appropriate tree

public class UserRepo {
    private static Random random = new Random();


    private static final BinarySearchTree<User> userTree = new BinarySearchTree<>();


    public static void addUser(User user){
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
    }

    public static User getUser(int id){
        return userTree.getElementById(id);
    }

}

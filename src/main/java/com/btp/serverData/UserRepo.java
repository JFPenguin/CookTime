package com.btp.serverData;

import com.btp.dataStructures.lists.SinglyList;
import com.btp.dataStructures.trees.BinarySearchTree;

//TODO change data structure from list to the appropriate tree

public class UserRepo {

    private static final BinarySearchTree<User> userTree = new BinarySearchTree<>();


    public static void addUser(User user){
        userTree.insert(user);
        System.out.println("user Added");
    }

    public static User getUser(int id){
        return userTree.getElementById(id);
    }

}

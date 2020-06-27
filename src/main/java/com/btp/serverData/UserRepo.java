package com.btp.serverData;

import com.btp.dataStructures.lists.SinglyList;

//TODO change data structure from list to the appropriate tree

public class UserRepo {

    private static final SinglyList<User> userList = new SinglyList<>();


    public static void addUser(User user){
        userList.add(user);
        System.out.println("user Added");
    }

    public static User getUser(int id){
        return userList.get(id).getData();
    }

}

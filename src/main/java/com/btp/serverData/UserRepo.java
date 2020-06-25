package com.btp.serverData;

import com.btp.dataStructures.lists.SinglyList;

//TODO change data structure from list to the appropriate tree

public class UserRepo {

    private static SinglyList<User> userList;

    public UserRepo() {
         userList = new SinglyList<>();
    }

    public void addUser(User user){
        userList.add(user);
    }

    public User getUser(int id){
        return userList.get(id).getData();
    }

}

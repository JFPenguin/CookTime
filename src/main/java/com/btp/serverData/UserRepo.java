package com.btp.serverData;

import com.btp.dataStructures.lists.SinglyList;

//TODO change data structure from list to the appropriate tree

public class UserRepo {

    private static SinglyList<User> userList;

    public UserRepo() {
         userList = new SinglyList<>();
         createUser("Pedrito Johnson",13,"xxxxXXXXpedritoSexyGamer420XXXxxx@yahoo.com","password");
         createUser("Ojo Noda",18,"OjoNoda@gmail.com","password");
         createUser("Fus RoDah",14,"dragonborn69@bugtesda.com","password");
         createUser("Michael Jayson Toshiba",65,"theRealMichaelJayson@gmail.com","password");
         createUser("Kenny Velasquez",22,"kennyBellius@gmail.com","password");
    }

    public void addUser(User user){
        userList.add(user);
    }

    public User getUser(int id){
        return userList.get(id).getData();
    }

    public void createUser(String name, int age, String email, String password){
        User newUser = new User();
        newUser.setName(name);
        newUser.setAge(age);
        newUser.setEmail(email);
        newUser.setPassword(password);
        userList.add(newUser);
    }

}

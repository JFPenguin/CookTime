package com.btp.serverData.clientObjects;

import com.btp.dataStructures.lists.SinglyList;

/**
 * Created business class, this holds data for all employees and the recipes
 *
 */
public class Business extends User{
    private int id;
    private SinglyList<User> employeeList;
    //TODO image logo

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}

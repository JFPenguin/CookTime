package com.btp.serverData.clientObjects;

import com.btp.dataStructures.lists.SinglyList;

import java.util.ArrayList;

/**
 * Created business class, this holds data for all employees and the recipes
 *
 */
public class Business{
    private int id;
    private String name;
    //TODO location
    private ArrayList<User> employeeList;
    //TODO image logo

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public ArrayList<User> getEmployeeList() {
        return employeeList;
    }

    public void setEmployeeList(ArrayList<User> employeeList) {
        this.employeeList = employeeList;
    }
}

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
    private float rating = 0.0f;
    private ArrayList<String> raters;
    private ArrayList<Integer> privateList;
    private ArrayList<Integer> publicList;
    //TODO location
    private ArrayList<String> employeeList;
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

    public ArrayList<String> getEmployeeList() {
        return employeeList;
    }

    public void setEmployeeList(ArrayList<String> employeeList) {
        this.employeeList = employeeList;
    }

    public float getRating() {
        return rating;
    }

    public ArrayList<String> getRaters() {
        return raters;
    }

    public ArrayList<Integer> getPrivateList() {
        return privateList;
    }

    public ArrayList<Integer> getPublicList() {
        return publicList;
    }
}

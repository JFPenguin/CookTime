package com.btp.serverData.clientObjects;

import com.btp.serverData.repos.UserRepo;

import java.util.ArrayList;

/**
 * Created business class, this holds data for all employees and the recipes
 *
 */
public class Business{
    private int id;
    private String name;
    private float rating = 0.0f;
    private int scoreTimes;
    private ArrayList<String> raters = new ArrayList<>();
    private ArrayList<Integer> privateList = new ArrayList<>();
    private ArrayList<Integer> publicList = new ArrayList<>();
    private String location;
    private ArrayList<String> employeeList;
    private String contact;
    private String photo;
    private String businessHours;

    /**
     * Getter of the id attribute
     * @return int the id of the business
     */
    public int getId() {
        return id;
    }

    /**
     * Setter of the id attribute
     * @param id int the id of the business
     */
    public void setId(int id) {
        this.id = id;
    }

    /**
     * Getter of the name attribute
     * @return String the name of the business
     */
    public String getName() {
        return name;
    }

    /**
     * Setter of the name attribute
     * @param name String the name of the business
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * Adds a new rating
     * @param score
     */
    public void addRating(int score){
        float tmp = this.rating*this.scoreTimes;
        this.scoreTimes ++;
        if (score < 0.0){
            score = 0;
        } else if (score > 5.0){
            score = 5;
        }
        this.rating = (tmp + score)/this.scoreTimes;
    }

    public ArrayList<String> getEmployeeList() {
        return employeeList;
    }

    public void addEmployee(String email){
        User user = UserRepo.getUser(email);
        employeeList.add(email+";"+user.fullName());

    }

    public void deleteEmployee(String email){
        User user = UserRepo.getUser(email);
        employeeList.remove(email+";"+user.fullName());
    }

    public void setEmployeeList(ArrayList<String> employeeList) {
        this.employeeList = employeeList;
    }

    public void addRater(String email){
        this.raters.add(email);
    }

    public void setRating(float rating) {
        this.rating = rating;
    }

    public int getScoreTimes() {
        return scoreTimes;
    }

    public void setScoreTimes(int scoreTimes) {
        this.scoreTimes = scoreTimes;
    }

    public void setRaters(ArrayList<String> raters) {
        this.raters = raters;
    }

    public void setPrivateList(ArrayList<Integer> privateList) {
        this.privateList = privateList;
    }

    public void setPublicList(ArrayList<Integer> publicList) {
        this.publicList = publicList;
    }

    public String getLocation() {
        return location;
    }

    public void setLocation(String location) {
        this.location = location;
    }

    public String getContact() {
        return contact;
    }

    public void setContact(String contact) {
        this.contact = contact;
    }

    public String getPhoto() {
        return photo;
    }

    public void setPhoto(String photo) {
        this.photo = photo;
    }

    public String getBusinessHours() {
        return businessHours;
    }

    public void setBusinessHours(String businessHours) {
        this.businessHours = businessHours;
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

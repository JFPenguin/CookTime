package com.btp.serverData.clientObjects;

import com.btp.dataStructures.lists.SinglyList;
import java.util.ArrayList;

/**
 * This is the Class for the User obj, it holds the user data
 */
public class User implements Comparable<User> {
    private String firstName;
    private String lastName;
    private String email;
    private String password;
    private boolean isChef;
    private ArrayList<String> followerEmails = new ArrayList<>();
    private ArrayList<String> followingEmails = new ArrayList<>();
    private ArrayList<Integer> recipeList = new ArrayList<>();
    private NewsFeed newsFeed = new NewsFeed();

    private int age;

    /**
     * Getter of the id attribute
     * @return int of the id attribute
     */
    public String getFirstName() {
        return this.firstName;
    }

    /**
     * Setter of the id attribute
     * @param firstName int the id to be set
     */
    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    /**
     * Getter of the name attribute
     * @return String of the name attribute
     */
    public String getLastName() {
        return this.lastName;
    }

    /**
     * Setter of the id attribute
     * @param lastName String the name to be set
     */
    public void setLastName(String lastName) {
        this.lastName = lastName;
    }

    /**
     * Getter of the email attribute
     * @return String of the email attribute
     */
    public String getEmail() {
        return email;
    }

    /**
     * Setter of the email attribute
     * @param email String the email to be set
     */
    public void setEmail(String email) {
        this.email = email;
    }

    /**
     * Getter of the password attribute
     * @return String of the password attribute
     */
    public String getPassword() {
        return password;
    }

    /**
     * Setter of the password attribute
     * @param password String the password to be set
     */
    public void setPassword(String password) {
        this.password = password;
    }

    /**
     * Getter of the age attribute
     * @return int of the age attribute
     */
    public int getAge() {
        return age;
    }

    /**
     * Setter of the age attribute
     * @param age int the age to be set
     */
    public void setAge(int age) {
        this.age = age;
    }

    public void addRecipe(int id) {
        this.recipeList.add(id);
    }

    public ArrayList<Integer> getRecipeList(){
        return this.recipeList;
    }

    public void addFollower(String email){
        this.followerEmails.add(email);
    }

    public void addFollowing(String email){
        this.followingEmails.add(email);
    }

    public ArrayList<String> getFollowerEmails(){
        return this.followerEmails;
    }

    public ArrayList<String> getFollowingEmails(){
        return this.followingEmails;
    }

    /**
     * Compares this User to the User in the parameter using their id attribute
     * @param user the User to be compared
     * @return int the result of the comparison
     */
    @Override
    public int compareTo(User user) {
        return this.getEmail().compareToIgnoreCase(user.getEmail());
    }

    public String fullName() {
        return getFirstName()+" "+getLastName();
    }
}


package com.btp.serverData.clientObjects;

import com.btp.dataStructures.lists.SinglyList;
import java.util.ArrayList;

/**
 * This is the Class for the User obj, it holds the user data
 */
public class User implements Comparable<User> {
    private String FirstName;
    private String LastName;
    private String Email;
    private String Password;
    private boolean IsChef;
    private final ArrayList<String> FollowerEmails = new ArrayList<>();
    private final ArrayList<String> FollowingEmails = new ArrayList<>();
    private final ArrayList<Integer> RecipeList = new ArrayList<>();
    private int Age;

    /**
     * Getter of the id attribute
     * @return int of the id attribute
     */
    public String getFirstName() {
        return this.FirstName;
    }

    /**
     * Setter of the id attribute
     * @param firstName int the id to be set
     */
    public void setFirstName(String firstName) {
        this.FirstName = firstName;
    }

    /**
     * Getter of the name attribute
     * @return String of the name attribute
     */
    public String getLastName() {
        return this.LastName;
    }

    /**
     * Setter of the id attribute
     * @param lastName String the name to be set
     */
    public void setLastName(String lastName) {
        this.LastName = lastName;
    }

    /**
     * Getter of the email attribute
     * @return String of the email attribute
     */
    public String getEmail() {
        return Email;
    }

    /**
     * Setter of the email attribute
     * @param email String the email to be set
     */
    public void setEmail(String email) {
        this.Email = email;
    }

    /**
     * Getter of the password attribute
     * @return String of the password attribute
     */
    public String getPassword() {
        return Password;
    }

    /**
     * Setter of the password attribute
     * @param password String the password to be set
     */
    public void setPassword(String password) {
        this.Password = password;
    }

    /**
     * Getter of the age attribute
     * @return int of the age attribute
     */
    public int getAge() {
        return Age;
    }

    /**
     * Setter of the age attribute
     * @param age int the age to be set
     */
    public void setAge(int age) {
        this.Age = age;
    }

    public void addRecipe(int id) {
        this.RecipeList.add(id);
    }

    public ArrayList<Integer> getRecipeList(){
        return this.RecipeList;
    }

    public void addFollower(String email){
        this.FollowerEmails.add(email);
    }

    public void addFollowing(String email){
        this.FollowingEmails.add(email);
    }

    public ArrayList<String> getFollowerEmails(){
        return this.FollowerEmails;
    }

    public ArrayList<String> getFollowingEmails(){
        return this.FollowingEmails;
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


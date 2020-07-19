package com.btp.serverData.clientObjects;

import com.btp.serverData.repos.RecipeRepo;
import com.btp.serverData.repos.UserRepo;
import jakarta.ws.rs.GET;
import jakarta.ws.rs.Path;
import jakarta.ws.rs.Produces;
import jakarta.ws.rs.QueryParam;
import jakarta.ws.rs.core.MediaType;

import java.util.ArrayList;

/**
 * This is the Class for the User obj, it holds the user data
 */
public class User implements Comparable<User> {
    private String firstName;
    private String lastName;
    private String email;
    private String password;
    private boolean isChef = false;
    private boolean isChefRequest = false;
    private ArrayList<String> followerEmails = new ArrayList<>();
    private ArrayList<String> followingEmails = new ArrayList<>();
    private ArrayList<Integer> recipeList = new ArrayList<>();
    private ArrayList<String> businessList = new ArrayList<>();
    private ArrayList<String> userPhotos = new ArrayList<>();
    private ArrayList<Integer> newsFeed = new ArrayList<>();
    private ArrayList<String> ratedBy = new ArrayList<>();
    private float chefScore = 0;
    private int chefScoreTimes = 0;
    private int age;

    private ArrayList<String> notifications = new ArrayList<>();
    public ArrayList<String> getUserPhotos() {
        return userPhotos;
    }

    public void addPhoto(String photo) {
        this.userPhotos.add(photo);
    }

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

    /**
     * Adds a recipe id to the author personal recipe list
     * @param id the id of the recipe
     */
    public void addRecipe(int id) {
        this.recipeList.add(id);
    }

    /**
     * Getter of the recipeList
     * @return ArrayList<Integer> the recipe list
     */
    public ArrayList<Integer> getRecipeList(){
        return this.recipeList;
    }

    public void addFollower(String email){
        this.followerEmails.add(email);
    }

    public void unFollower(String email) {
        this.followerEmails.remove(email);
    }

    public void addFollowing(String email){
        this.followingEmails.add(email);
    }

    public void unFollowing(String email){
        this.followingEmails.remove(email);
    }

    public ArrayList<String> getFollowerEmails(){
        return this.followerEmails;
    }

    public ArrayList<String> getFollowingEmails(){
        return this.followingEmails;
    }

    public boolean isChef() {
        return this.isChef;
    }

    public void setChef(boolean Chef) {
        this.isChef = Chef;
    }

    public boolean isChefRequest() {
        return this.isChefRequest;
    }

    public void setChefRequest(boolean chefRequest) {
        this.isChefRequest = chefRequest;
    }

    public float getChefScore() {
        return this.chefScore;
    }

    public ArrayList<String> getRatedBy() {
        return ratedBy;
    }

    public void addRated(String email){
        ratedBy.add(email);
    }

    public void addChefScore(float score){
        float tmp = this.chefScore * this.chefScoreTimes;
        this.chefScoreTimes++;
        if (score < 0.0){
            score = 0;
        } else if (score > 5.0){
            score = 5;
        }
        this.chefScore = (tmp + score)/chefScoreTimes;
    }
    
    public int getChefScoreTimes() {
        return this.chefScoreTimes;
    }

    public ArrayList<String> getBusinessList() {
        return businessList;
    }

    public ArrayList<Integer> getNewsFeed() {
        return newsFeed;
    }

    public void addNewsFeed(int id){
        newsFeed.add(id);
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

    public void sendMessage(String message) {
        notifications.add(message);
    }

    public ArrayList<String> getNotifications(){
        return this.notifications;
    }

    public void deleteMessage(int index){
        notifications.remove(index);
    }

    public ArrayList<Integer> userCreatedRecipes(){
        ArrayList<Integer> userRecipes = new ArrayList<>();
        for (Integer integer : recipeList) {
            if (RecipeRepo.getRecipe(integer).getAuthorEmail().equals(this.email)) {
                userRecipes.add(integer);
            }
        }
        return userRecipes;
    }

    public void clearNotifications() {
        notifications.clear();
    }
}


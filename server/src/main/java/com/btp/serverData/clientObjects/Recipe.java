package com.btp.serverData.clientObjects;

import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.ArrayList;

/**
 * This is the class of the recipe obj, it holds the recipe information
 */
public class Recipe implements Comparable<Recipe> {
    private String name;
    private String authorEmail;
    private String dishTime;
    private int portions;
    private int duration; //in minutes
    private String dishType;
    private int difficulty;
    private ArrayList<String> dishTags;
    //TODO picture
    private ArrayList<String> ingredientsList;
    private ArrayList<String> instructions;
    private float price;
    private int id;
    private LocalDateTime postTime;
    private float score;
    private int scoreTimes;

    /**
     * Getter of the id attribute
     * @return int the unique id associated to a recipe
     */
    public int getId() {
        return id;
    }

    /**
     * Setter of the id attribute
     * @param id int the unique id to be set
     */
    public void setId(int id) {
        this.id = id;
    }

    /**
     * Getter of the name attribute
     * @return String the name of the recipe
     */
    public String getName() {
        return name;
    }

    /**
     * Setter of the name attribute
     * @param name String the name of the recipe to be set
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * Getter of the author attribute
     * @return User the author attribute
     */
    public String getAuthorEmail() {
        return authorEmail;
    }

    public void setAuthorEmail(String authorEmail) {
        this.authorEmail = authorEmail;
    }

    /**
     * Getter of the dishTime attribute
     * @return ENUM the dishTime attribute
     */
    public String getDishTime() {
        return dishTime;
    }

    /**
     * Setter of the dishTime attribute
     * @param dishTime ENUM the dishTime to be set
     */
    public void setDishTime(String dishTime) {
        this.dishTime = dishTime;
    }

    /**
     * Getter of the portions attribute
     * @return int the portions attribute
     */
    public int getPortions() {
        return portions;
    }

    /**
     * Setter of the portions attribute
     * @param portions int the portions to be set
     */
    public void setPortions(int portions) {
        this.portions = portions;
    }

    /**
     * Getter of the duration attribute
     * @return int the duration attribute
     */
    public int getDuration() {
        return duration;
    }

    /**
     * Setter of the duration attribute
     * @param duration int the duration to be set
     */
    public void setDuration(int duration) {
        this.duration = duration;
    }

    /**
     * Getter of the dishType attribute
     * @return ENUM of the dishType attribute
     */
    public String getDishType() {
        return dishType;
    }

    /**
     * Setter of the dishType attribute
     * @param dishType ENUM of the DishType to be set
     */
    public void setDishType(String dishType) {
        this.dishType = dishType;
    }

    /**
     * Getter of the difficulty attribute
     * @return int the difficulty attribute
     */
    public int getDifficulty() {
        return difficulty;
    }

    /**
     * Setter of the  difficulty attribute
     * @param difficulty int difficulty to be set
     */
    public void setDifficulty(int difficulty) {
        if(difficulty<1){
            this.difficulty = 1;
        }
        else if(difficulty>5){
            this.difficulty = 5;
        } else {
            this.difficulty = difficulty;
        }
    }

    /**
     * Getter of the dishTags attribute
     * @return ArrayList<DishTag> of the dishTags attribute
     */
    public ArrayList<String> getDishTags() {
        return dishTags;
    }

    /**
     * Setter of the dishTags attribute
     * @param dishTags ArrayList<DishTag> of the dishTags to be set
     */
    public void setDishTags(ArrayList<String> dishTags) {
        this.dishTags = dishTags;
    }

    /**
     * Getter of the ingredientsList attribute
     * @return ArrayList<Ingredient> of the ingredientsList attribute
     */
    public ArrayList<String> getIngredientsList() {
        return ingredientsList;
    }

    /**
     * Setter of the ingredientsList attribute
     * @param ingredientsList ArrayList<Ingredient> of the ingredientList to be set
     */
    public void setIngredientsList(ArrayList<String> ingredientsList) {
        this.ingredientsList = ingredientsList;
    }

    /**
     * Getter of the instructions attribute
     * @return ArrayList<String> of the instructions attribute
     */
    public ArrayList<String> getInstructions() {
        return instructions;
    }

    /**
     * Setter of the instructions attribute
     * @param instructions ArrayList<String> of the instructions to be set
     */
    public void setInstructions(ArrayList<String> instructions) {
        this.instructions = instructions;
    }

    /**
     * Getter of the price attribute
     * @return int of the price attribute
     */
    public float getPrice() {
        return price;
    }

    /**
     * Setter of the price attribute
     * @param price int of the price to be set
     */
    public void setPrice(float price) {
        this.price = price;
    }

    /**
     * Getter of the postTime attribute
     * @return LocalDateTime of the postTime attribute
     */
    public LocalDateTime getPostTime() {
        return postTime;
    }

    /**
     * Gets the postTime attribute but in String
     * @return String of the postTime attribute using YYYY/MM/dd HH:mm:ss
     */
    public String getPostTimeString() {
        DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyy/MM/dd HH:mm:ss");
        return dtf.format(this.postTime);
    }

    /**
     * Setter of the postTime attribute
     * @param postTime LocalDateTime of the postTime to be set
     */
    public void setPostTime(LocalDateTime postTime) {
        this.postTime = postTime;
    }

    /**
     * Getter of the score attribute
     * @return float of the score attribute
     */
    public float getScore() {
        return score;
    }

    /**
     * Setter of the score attribute
     * @param score score by one of the other users
     */
    public void addScore(float score) {
        float tmp = this.score*this.scoreTimes;
        this.scoreTimes ++;
        if (score < 0.0){
            score = 0;
        } else if (score > 5.0){
            score = 5;
        }
        this.score = (tmp + score)/this.scoreTimes;
    }

    /**
     * Getter of the scoreTimes attribute
     * @return int of the scoreTimes attribute
     */
    public int getScoreTimes() {
        return scoreTimes;
    }

    /**
     * Compares this Recipe to the Recipe in the parameter using their id attribute
     * @param recipe the Recipe to be compared
     * @return int the result of the comparison
     */
    @Override
    public int compareTo(Recipe recipe) {
        return this.getId() - recipe.getId();
    }
}

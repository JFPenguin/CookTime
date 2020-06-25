package com.btp.serverData;

import com.btp.dataStructures.lists.SinglyList;

/**
 * This is the class of the recipe obj, it holds the recipe information
 */
public class Recipe {

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public User getAuthor() {
        return author;
    }

    public DishTime getDishTime() {
        return dishTime;
    }

    public void setDishTime(DishTime dishTime) {
        this.dishTime = dishTime;
    }

    public int getPortions() {
        return portions;
    }

    public void setPortions(int portions) {
        this.portions = portions;
    }

    public int getDuration() {
        return duration;
    }

    public void setDuration(int duration) {
        this.duration = duration;
    }

    public DishType getDishType() {
        return dishType;
    }

    public void setDishType(DishType dishType) {
        this.dishType = dishType;
    }

    public float getDifficulty() {
        return difficulty;
    }

    public void setDifficulty(float difficulty) {
        this.difficulty = difficulty;
    }

    public SinglyList<DishTag> getDishTags() {
        return dishTags;
    }

    public void setDishTags(SinglyList<DishTag> dishTags) {
        this.dishTags = dishTags;
    }

    public SinglyList<String> getIngredientsList() {
        return ingredientsList;
    }

    public void setIngredientsList(SinglyList<String> ingredientsList) {
        this.ingredientsList = ingredientsList;
    }

    public SinglyList<String> getInstructions() {
        return instructions;
    }

    public void setInstructions(SinglyList<String> instructions) {
        this.instructions = instructions;
    }

    public float getPrice() {
        return price;
    }

    public void setPrice(float price) {
        this.price = price;
    }

    private String name;
    private final User author;
    private DishTime dishTime;
    private int portions;
    private int duration; //in minutes
    private DishType dishType;
    private float difficulty;
    private SinglyList<DishTag> dishTags;
    //TODO picture
    private SinglyList<String> ingredientsList;
    private SinglyList<String> instructions;
    private float price;

    /**
     * Constructor for the Recipe Class
     * @param name A String representing the name of the dish
     * @param author An user obj, determined by the creator of the recipe
     * @param dishTime Enum DishTime to determine the time of day intended for this dish ej: BREAKFAST, LUNCH, etc
     * @param portions int value of the number of portions from the recipe
     * @param duration int value of the minutes it takes to make the dish
     * @param dishType Enum of the type of dish this is, ej: MAIN_DISH, DESSERT, etc
     * @param difficulty a float value that determines the difficulty of this dish from a minimal value of 1, to a maximum of 5
     * @param dishTags list of tags associated with this dish
     * @param ingredientsList list of Strings of the ingredients used in the recipe
     * @param instructions list of Strings of the instructions that need to be followed
     */
    public Recipe(String name, User author, DishTime dishTime, int portions, int duration, DishType dishType,
                  float difficulty, SinglyList<DishTag> dishTags, SinglyList<String> ingredientsList, SinglyList<String> instructions) {

        this.name = name;
        this.author = author;
        this.dishTime = dishTime;
        this.portions = portions;
        this.duration = duration;
        this.dishType = dishType;
        if(difficulty<1){
            this.difficulty = 1;
        }
        else if(difficulty>5){
            this.difficulty = 5;
        }
        else {
            this.difficulty = difficulty;
        }
        this.dishTags = dishTags;
        this.ingredientsList = ingredientsList;
        this.instructions = instructions;

    }
}

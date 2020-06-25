package com.btp.serverData;

import com.btp.dataStructures.lists.SinglyList;

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

    public void setAuthor(User author) {
        this.author = author;
    }

    public DishType getDishType() {
        return dishType;
    }

    public void setDishType(DishType dishType) {
        this.dishType = dishType;
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

    public DishTime getDishTime() {
        return dishTime;
    }

    public void setDishTime(DishTime dishTime) {
        this.dishTime = dishTime;
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
    private User author;
    private DishType dishType;
    private int portions;
    private int duration; //in minutes
    private DishTime dishTime;
    private float difficulty;
    private SinglyList<DishTag> dishTags;
    //TODO picture
    private SinglyList<String> ingredientsList;
    private SinglyList<String> instructions;
    private float price;

    public Recipe(String name, User author, DishType dishType, int portions, int duration, DishTime dishTime,
                  float difficulty, SinglyList<DishTag> dishTags, SinglyList<String> ingredientsList, SinglyList<String> instructions) {

        this.name = name;
        this.author = author;
        this.dishType = dishType;
        this.portions = portions;
        this.duration = duration;
        this.dishTime = dishTime;
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

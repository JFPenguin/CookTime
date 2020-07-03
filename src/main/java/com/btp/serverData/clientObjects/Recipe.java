package com.btp.serverData.clientObjects;

import com.btp.dataStructures.lists.SinglyList;

/**
 * This is the class of the recipe obj, it holds the recipe information
 */
public class Recipe implements Comparable<Recipe> {

    private String name;
    private final User author;
    private DishTime dishTime;
    private int portions;
    private int duration; //in minutes
    private DishType dishType;
    private float difficulty;
    private SinglyList<DishTag> dishTags;
    //TODO picture
    private SinglyList<Ingredient> ingredientsList;
    private SinglyList<String> instructions;
    private float price;
    private int id;

    /**
     * Getter of the id attribute
     * @return int the id attribute
     */
    public int getId() {
        return id;
    }

    /**
     * Setter of the id attribute
     * @param id int the id to be set
     */
    public void setId(int id) {
        this.id = id;
    }

    /**
     * Getter of the name attribute
     * @return String the name attribute
     */
    public String getName() {
        return name;
    }

    /**
     * Setter of the name attribute
     * @param name String the name to be set
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * Getter of the author attribute
     * @return User the author attribute
     */
    public User getAuthor() {
        return author;
    }

    /**
     * Getter of the dishTime attribute
     * @return ENUM the dishTime attribute
     */
    public DishTime getDishTime() {
        return dishTime;
    }

    /**
     * Setter of the dishTime attribute
     * @param dishTime ENUM the dishTime to be set
     */
    public void setDishTime(DishTime dishTime) {
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
    public DishType getDishType() {
        return dishType;
    }

    /**
     * Setter of the dishType attribute
     * @param dishType ENUM of the DishType to be set
     */
    public void setDishType(DishType dishType) {
        this.dishType = dishType;
    }

    /**
     * Getter of the difficulty attribute
     * @return int the difficulty attribute
     */
    public float getDifficulty() {
        return difficulty;
    }

    /**
     * Setter of the  difficulty attribute
     * @param difficulty int difficulty to be set
     */
    public void setDifficulty(float difficulty) {
        if(difficulty<1){
            this.difficulty = 1;
        }
        else if(difficulty>5){
            this.difficulty = 5;
        }
    }

    /**
     * Getter of the dishTags attribute
     * @return SinglyList<DishTag> of the dishTags attribute
     */
    public SinglyList<DishTag> getDishTags() {
        return dishTags;
    }

    /**
     * Setter of the dishTags attribute
     * @param dishTags SinglyList<DishTag> of the dishTags to be set
     */
    public void setDishTags(SinglyList<DishTag> dishTags) {
        this.dishTags = dishTags;
    }

    /**
     * Getter of the ingredientsList attribute
     * @return SinglyList<Ingredient> of the ingredientsList attribute
     */
    public SinglyList<Ingredient> getIngredientsList() {
        return ingredientsList;
    }

    /**
     * Setter of the ingredientsList attribute
     * @param ingredientsList SinglyList<Ingredient> of the ingredientList to be set
     */
    public void setIngredientsList(SinglyList<Ingredient> ingredientsList) {
        this.ingredientsList = ingredientsList;
    }

    /**
     * Getter of the instructions attribute
     * @return SinglyList<String> of the instructions attribute
     */
    public SinglyList<String> getInstructions() {
        return instructions;
    }

    /**
     * Setter of the instructions attribute
     * @param instructions SinglyList<String> of the instructions to be set
     */
    public void setInstructions(SinglyList<String> instructions) {
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
                  float difficulty, SinglyList<DishTag> dishTags, SinglyList<Ingredient> ingredientsList, SinglyList<String> instructions) {

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

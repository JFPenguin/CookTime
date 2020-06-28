package com.btp.serverData.clientObjects;

/**
 * Ingredient obj that represents ingredients, it has 3 values, the ingredient String name, the quantity and the enum of the measurement type
 */
public class Ingredient {
    private String ingredient;
    private float quantity;
    private MeasurementUnit measurementUnit;

    /**
     * Getter for ingredients String name
     * @return the string name of the ingredient
     */
    public String getIngredient() {
        return ingredient;
    }

    /**
     * Setter for the ingredient String name
     * @param ingredient String name of the ingredients
     */
    public void setIngredient(String ingredient) {
        this.ingredient = ingredient;
    }

    /**
     * Getter for the int ingredient quantity
     * @return int value for the qty
     */
    public float getQuantity() {
        return quantity;
    }

    /**
     * setter for the int qty of an ingredient
     * @param quantity int value for the ingredient
     */
    public void setQuantity(float quantity) {
        this.quantity = quantity;
    }

    /**
     * getter for the ENUM value of the unit of measurement
     * @return ENUM value of the unit of measurement
     */
    public MeasurementUnit getMeasurementUnit() {
        return measurementUnit;
    }

    /**
     * setter for the unit of measurement for the ingredient
     * @param measurementUnit enum value of the unit of measurement
     */
    public void setMeasurementUnit(MeasurementUnit measurementUnit) {
        this.measurementUnit = measurementUnit;
    }

    /**
     * Main constructor for the ingredient class
     * @param ingredient String for the name of the ingredient
     * @param quantity int value for the qty of the ingredient
     * @param measurementUnit ENUM of the unit of measurement
     */
    public Ingredient(String ingredient, float quantity, MeasurementUnit measurementUnit) {
        this.ingredient = ingredient;
        this.quantity = quantity;
        this.measurementUnit = measurementUnit;
    }
}

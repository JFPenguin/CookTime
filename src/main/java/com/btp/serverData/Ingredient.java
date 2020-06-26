package com.btp.serverData;

public class Ingredient {
    private String ingredient;
    private float quantity;
    private MeasurementUnit measurementUnit;

    public String getIngredient() {
        return ingredient;
    }

    public void setIngredient(String ingredient) {
        this.ingredient = ingredient;
    }

    public float getQuantity() {
        return quantity;
    }

    public void setQuantity(float quantity) {
        this.quantity = quantity;
    }

    public MeasurementUnit getMeasurementUnit() {
        return measurementUnit;
    }

    public void setMeasurementUnit(MeasurementUnit measurementUnit) {
        this.measurementUnit = measurementUnit;
    }

    public Ingredient(String ingredient, float quantity, MeasurementUnit measurementUnit) {
        this.ingredient = ingredient;
        this.quantity = quantity;
        this.measurementUnit = measurementUnit;
    }
}

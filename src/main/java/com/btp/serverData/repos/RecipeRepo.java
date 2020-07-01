package com.btp.serverData.repos;

import com.btp.dataStructures.lists.SinglyList;
import com.btp.serverData.clientObjects.Recipe;

/**
 * This class represents the main repository for recipes
 */
public class RecipeRepo {

    private static final SinglyList<Recipe> recipeList = new SinglyList<>();

    /**
     * Adds a recipe to the RecipeRepo
     * @param recipe Recipe to be added
     */
    public static void addRecipe(Recipe recipe){
        recipeList.add(recipe);
    }

    /**
     * Gets a recipe from the RecipeRepo using its id
     * @param id int id of the Recipe to get
     * @return the Recipe of that specific id
     */
    public static Recipe getRecipe(int id){
        return recipeList.get(id).getData();
    }

}

package com.btp.serverData;

import com.btp.dataStructures.lists.SinglyList;

public class RecipeRepo {
    private static final SinglyList<Recipe> recipeList = new SinglyList<>();

    public static void addRecipe(Recipe recipe){
        recipeList.add(recipe);
    }

    public static Recipe getRecipe(int id){
        return recipeList.get(id).getData();
    }
}

package com.btp.serverData;

import com.btp.dataStructures.lists.SinglyList;

public class RecipeRepo {
    private static SinglyList<Recipe> recipeList;

    public RecipeRepo() {
        recipeList = new SinglyList<>();
    }

    public void addRecipe(Recipe recipe){
        recipeList.add(recipe);
    }

    public Recipe getRecipe(int id){
        return recipeList.get(id).getData();
    }
}

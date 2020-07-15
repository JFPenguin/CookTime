package com.btp.serverData.repos;

import com.btp.Initializer;
import com.btp.dataStructures.trees.RecipeTree;
import com.btp.serverData.clientObjects.Recipe;
import com.btp.utils.DataWriter;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;

/**
 * This class represents the main repository for recipes
 */
public class RecipeRepo {

    private static RecipeTree recipeTree = new RecipeTree();
    private static final DataWriter<RecipeTree> dataWriter = new DataWriter<>();
    private static final String path =System.getProperty("project.folder")+"/dataBase/recipeDataBase.json";

    /**
     * Adds a recipe to the RecipeRepo
     * @param recipe Recipe to be added
     */
    public static void addRecipe(Recipe recipe){
        recipeTree.insert(recipe);
        System.out.println("Recipe added");
        if(Initializer.isGUIOnline()){
            Initializer.getServerGUI().printLn("Recipe added");
        }
        dataWriter.writeData(recipeTree, path);

    }

    /**
     * Gets a recipe from the RecipeRepo using its id
     * @param id int id of the Recipe to get
     * @return the Recipe of that specific id
     */
    public static Recipe getRecipe(int id){
        return recipeTree.getElementById(id);
    }

    public static ArrayList<String> searchByName(String data){
        return recipeTree.searchByName(data);
    }

    public static void loadTree() throws IOException {
        System.out.println("loading user data base...");
        ObjectMapper objectMapper = new ObjectMapper();
        FileReader file = new FileReader(path);
        recipeTree = objectMapper.readValue(file, recipeTree.getClass());
    }

    public static boolean checkId(int id){
        return true;
    }
}

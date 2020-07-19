package com.btp;

import com.btp.gui.ServerGUI;
import com.btp.serverData.clientObjects.DishTag;
import com.btp.serverData.clientObjects.DishTime;
import com.btp.serverData.clientObjects.DishType;
import com.btp.serverData.clientObjects.Recipe;
import com.btp.serverData.repos.RecipeRepo;
import com.btp.serverData.repos.UserRepo;
import javax.servlet.http.HttpServlet;
import javax.swing.*;
import java.io.IOException;
import java.util.ArrayList;

public class Initializer extends HttpServlet {

    public static boolean isGUIOnline() {
        return guiStatus;
    }

    public static boolean guiStatus = false;

    public static ServerGUI getServerGUI() {
        return serverGUI;
    }

    public static ServerGUI serverGUI;

    public void init() {
        System.out.println("running initialization...");
        System.out.println("loading resources...");
        try {
            UserRepo.loadTree();
            RecipeRepo.loadTree();
            //testResources();

        } catch (IOException e) {
            e.printStackTrace();
        }
        System.out.println("opening GUI...");
        try {
            createWindow();

        } catch (ClassNotFoundException | UnsupportedLookAndFeelException | InstantiationException | IllegalAccessException e) {
            e.printStackTrace();
        }
    }

    public static void testResources() {
        System.out.println("loading test resources");
        //Test Recipes
        //Gallo Pinto
        Recipe galloPinto = new Recipe();
        ArrayList<DishTag> galloPintoTags = new ArrayList<>();
        galloPintoTags.add(DishTag.VEGAN);

        ArrayList<String> galloPintoIngredients = new ArrayList<>();
        galloPintoIngredients.add("Arroz;1;taza");
        galloPintoIngredients.add("Frijoles;1;taza");
        galloPintoIngredients.add("Especies Mixtas;1/2;taza");
        galloPintoIngredients.add("Lizano, obvio;1;cucharada");

        ArrayList<String> galloPintoInstructions = new ArrayList<>();
        galloPintoInstructions.add("Revolver las varas");
        galloPintoInstructions.add("Cocinar");


        galloPinto.setId(500);
        galloPinto.setIngredientsList(galloPintoIngredients);
        galloPinto.setInstructions(galloPintoInstructions);
        galloPinto.setName("Gallo Pinto");
        System.out.println("set recipe name");
        galloPinto.setAuthorEmail("m@gmail.com");
        System.out.println("set authorEmail");
        galloPinto.setDishTime(DishTime.BREAKFAST);
        System.out.println("");
        galloPinto.setPortions(5);
        galloPinto.setDuration(15);
        galloPinto.setDishType(DishType.MAIN_DISH);
        galloPinto.setDishTags(galloPintoTags);
        galloPinto.addPhotos("galloPinto-picture");
        galloPinto.setPostTime(System.currentTimeMillis());
        RecipeRepo.addRecipe(galloPinto);


        //CheeseCake
        Recipe cheeseCake = new Recipe();
        ArrayList<DishTag> cheeseCakeTags = new ArrayList<>();
        cheeseCakeTags.add(DishTag.VEGETARIAN);

        cheeseCake.setId(250);
        ArrayList<String> cheeseCakeIngredients = new ArrayList<>();
        cheeseCakeIngredients.add("Queso Crema;300; gramos");
        cheeseCakeIngredients.add("Azucar; 1 ; taza");
        cheeseCakeIngredients.add("Galleta Maria;500;gramos");
        cheeseCakeIngredients.add("Mantequilla; 150; grams");
        cheeseCakeIngredients.add("Huevos; 3; unidades");
        cheeseCakeIngredients.add("Natilla; 1; taza");
        cheeseCakeIngredients.add("Jalea de Fresa; 1/2; tazas");

        ArrayList<String> cheeseCakeInstructions = new ArrayList<>();
        cheeseCakeInstructions.add("Precalentar horno a 350°F ");
        cheeseCakeInstructions.add("Pulverize la galleta maria y mézclela con la mantequilla" +
                " y recubra el interior de un molde para queques");
        cheeseCakeInstructions.add("Mezcle el queso crema y la azucar hasta que tenga una consistencia suave, " +
                "agregue los huevos uno a uno, mezclando despues de cada uno");
        cheeseCakeInstructions.add("Hornee en el horno precalentado por 50 minutos, espere a que se enfrie por completo antes de remover del molde");


        cheeseCake.setIngredientsList(cheeseCakeIngredients);
        cheeseCake.setInstructions(cheeseCakeInstructions);
        cheeseCake.setName("CheeseCake");
        cheeseCake.setAuthorEmail("m@gmail.com");
        cheeseCake.setDishTime(DishTime.BREAKFAST);
        cheeseCake.setPortions(5);
        cheeseCake.setDuration(15);
        cheeseCake.setDishType(DishType.MAIN_DISH);
        cheeseCake.setDishTags(cheeseCakeTags);
        cheeseCake.addPhotos("cheeseCake-picture");
        cheeseCake.setPostTime(System.currentTimeMillis());
        RecipeRepo.addRecipe(cheeseCake);
        System.out.println("loaded test recipe");

    }

    private void createWindow() throws ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        serverGUI = new ServerGUI();
        serverGUI.setVisible(true);
        guiStatus = true;


    }

}

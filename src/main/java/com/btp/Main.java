package com.btp;

import com.btp.dataStructures.lists.SinglyList;
import com.btp.serverData.*;

import javax.servlet.http.HttpServlet;
import javax.swing.*;
import java.awt.*;


public class Main extends HttpServlet {

    public void init() {
        testResources();
        createWindow();
    }

    private static void createWindow() {
        JFrame frame = new JFrame("simpleGUI");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        JLabel textLabel = new JLabel("Hola soy su server pa",SwingConstants.CENTER);
        textLabel.setPreferredSize(new Dimension(300,100));
        frame.getContentPane().add(textLabel, BorderLayout.CENTER);

        frame.setLocationRelativeTo(null);
        frame.pack();
        frame.setVisible(true);

    }

    public void testResources() {

        //Test Users
        User u1 = new User();
        u1.setName("Pedrito Johnson"); u1.setEmail("xxxxXXXXpedritoSexyGamer420XXXxxx@yahoo.com");u1.setPassword("password");u1.setAge(13);

        User u2 = new User();
        u2.setName("Ojo Noda"); u2.setEmail("OjoNoda@gmail.com");u2.setPassword("password");u2.setAge(18);

        User u3 = new User();
        u3.setName("Fus RoDah"); u3.setEmail("dragonborn69@bugtesda.com");u3.setPassword("password");u3.setAge(18);

        User u4 = new User();
        u4.setName("Michael Jayson Toshiba"); u4.setEmail("theRealMichaelJayson@gmail.com");u4.setPassword("password");u4.setAge(65);

        User u5 = new User();
        u5.setName("Kenny Velasquez"); u5.setEmail("kennyBellius@gmail.com");u5.setPassword("password");u5.setAge(22);


        UserRepo.addUser(u1);
        UserRepo.addUser(u2);
        UserRepo.addUser(u3);
        UserRepo.addUser(u4);
        UserRepo.addUser(u5);


        //Test Recipes
        //Gallo Pinto
        SinglyList<DishTag> galloPintoTags = new SinglyList<>();
        galloPintoTags.add(DishTag.VEGAN);

        SinglyList<Ingredient> galloPintoIngredients = new SinglyList<>();
        galloPintoIngredients.add(new Ingredient("Arroz",1, MeasurementUnit.CUP));
        galloPintoIngredients.add(new Ingredient("Frijoles",1,MeasurementUnit.CUP));
        galloPintoIngredients.add(new Ingredient("Especies Mixtas",(float)1/2,MeasurementUnit.CUP));
        galloPintoIngredients.add(new Ingredient("Lizano, obvio",(float) 1/2, MeasurementUnit.CUP));

        SinglyList<String> galloPintoInstructions = new SinglyList<>();
        galloPintoInstructions.add("Revolver las varas");
        galloPintoInstructions.add("Cocinar");

        Recipe galloPinto = new Recipe("Gallo Pinto",UserRepo.getUser(0), DishTime.BREAKFAST,
                5,15, DishType.MAIN_DISH,3,galloPintoTags,galloPintoIngredients,galloPintoInstructions);

        //CheeseCake
        SinglyList<DishTag> CheeseCakeTags = new SinglyList<>();
        CheeseCakeTags.add(DishTag.VEGETARIAN);

        SinglyList<Ingredient> cheeseCakeIngredients = new SinglyList<>();
        cheeseCakeIngredients.add(new Ingredient("Queso Crema",300,MeasurementUnit.GRAM));
        cheeseCakeIngredients.add(new Ingredient("Azucar",1,MeasurementUnit.CUP));
        cheeseCakeIngredients.add(new Ingredient("Galleta Maria",500,MeasurementUnit.GRAM));
        cheeseCakeIngredients.add(new Ingredient("Mantequilla",150,MeasurementUnit.GRAM));
        cheeseCakeIngredients.add(new Ingredient("Huevos", 3,MeasurementUnit.UNIT));
        cheeseCakeIngredients.add(new Ingredient("Natilla",1,MeasurementUnit.CUP));
        cheeseCakeIngredients.add(new Ingredient("Jalea de Fresa",(float)1/2,MeasurementUnit.CUP));

        SinglyList<String> cheeseCakeInstructions = new SinglyList<>();
        cheeseCakeInstructions.add("Precalentar horno a 350°F ");
        cheeseCakeInstructions.add("Pulverize la galleta maria y mézclela con la mantequilla" +
                " y recubra el interior de un molde para queques");
        cheeseCakeInstructions.add("Mezcle el queso crema y la azucar hasta que tenga una consistencia suave, " +
                "agregue los huevos uno a uno, mezclando despues de cada uno");
        cheeseCakeInstructions.add("Hornee en el horno precalentado por 50 minutos, espere a que se enfrie por completo antes de remover del molde");

        Recipe cheeseCake = new Recipe("Cheese Cake",UserRepo.getUser(1), DishTime.BREAKFAST,
                5,15, DishType.MAIN_DISH,3,CheeseCakeTags,cheeseCakeIngredients,cheeseCakeInstructions);

        RecipeRepo.addRecipe(cheeseCake);
        RecipeRepo.addRecipe(galloPinto);


    }
}

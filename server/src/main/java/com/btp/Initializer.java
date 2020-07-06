package com.btp;

import com.btp.dataStructures.lists.SinglyList;
import com.btp.gui.ServerGUI;
import com.btp.serverData.clientObjects.*;
import com.btp.serverData.repos.RecipeRepo;
import com.btp.serverData.repos.UserRepo;

import javax.servlet.http.HttpServlet;
import javax.swing.*;
import java.io.IOException;


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

    private void createWindow() throws ClassNotFoundException, UnsupportedLookAndFeelException, InstantiationException, IllegalAccessException {
        UIManager.setLookAndFeel(UIManager.getSystemLookAndFeelClassName());
        serverGUI = new ServerGUI();
        serverGUI.setVisible(true);
        guiStatus = true;


        //serverGUI.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

//        JFrame frame = new JFrame("simpleGUI");
//        //frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
//        JLabel textLabel = new JLabel("Hola soy su server pa",SwingConstants.CENTER);
//        //textLabel.setPreferredSize(new Dimension(800,600));
//        //frame = new JFrame(title);
//        frame.setPreferredSize(new Dimension(800,600));
//        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
//        frame.setResizable(true);
//        frame.setLocationRelativeTo(null);
//        frame.setVisible(true);
//        frame.pack();
//        //Display display = new Display("CookTime Server Manager", 800, 600);
//        //Graphics graphics;
//        frame.getContentPane().add(textLabel, BorderLayout.CENTER);
//       // display.getFrame().setLocationRelativeTo(null);
//       // display.getFrame().pack();
//        //display.getFrame().setVisible(true);

    }

}

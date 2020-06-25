package com.btp;

import com.btp.dataStructures.lists.SinglyList;
import com.btp.serverData.*;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;

/**
 * Root resource (exposed at "myresource" path)
 */
@Path("resources")
public class Resources {

    UserRepo userRepo = new UserRepo();
    RecipeRepo recipeRepo = new RecipeRepo();

    public Resources() {

        //Test Users
        createUser("Pedrito Johnson",13,"xxxxXXXXpedritoSexyGamer420XXXxxx@yahoo.com","password");
        createUser("Ojo Noda",18,"OjoNoda@gmail.com","password");
        createUser("Fus RoDah",14,"dragonborn69@bugtesda.com","password");
        createUser("Michael Jayson Toshiba",65,"theRealMichaelJayson@gmail.com","password");
        createUser("Kenny Velasquez",22,"kennyBellius@gmail.com","password");

        //Test Recipes
        //Gallo Pinto
        SinglyList<DishTag> galloPintoTags = new SinglyList<>();
        galloPintoTags.add(DishTag.VEGAN);

        SinglyList<String> galloPintoIngredients = new SinglyList<>();
        galloPintoIngredients.add("Arroz");
        galloPintoIngredients.add("Frijoles");
        galloPintoIngredients.add("Especies Mixtas");
        galloPintoIngredients.add("Lizano, obvio");

        SinglyList<String> galloPintoInstructions = new SinglyList<>();
        galloPintoInstructions.add("Revolver las varas");
        galloPintoInstructions.add("Cocinar");

        Recipe galloPinto = new Recipe("Gallo Pinto",userRepo.getUser(0), DishTime.BREAKFAST,
                5,15, DishType.MAIN_DISH,3,galloPintoTags,galloPintoIngredients,galloPintoInstructions);

        recipeRepo.addRecipe(galloPinto);

    }


    /**
     * Method handling HTTP GET requests. The returned object will be sent
     * to the client as "text/plain" media type.
     * @return String that will be returned as a text/plain response.
     */
    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public String getResources() {
        return "Resources Main page, \n\nIf you want to check user's JSONs do the following:" +
                "\n\non your browser address bar add the following to the url: /getUser?id= followed by the int value of the id" +
                "\n\nif you want to check the recipe's JSONs do the following:\n\n" +
                "on your browser address bar add the following to the url: /getRecipe?id= followed by the int value of the id";
    }

    /**
     * API getter for the user obj
     * @param id int value of the id of the user
     * @return User obj
     */
    @Path("getUser")
    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public User getUser(@QueryParam("id") int id){
        return userRepo.getUser(id);
    }

    /**
     * API getter for the recipe obj
     * @param id int value of the id of the recipe
     * @return recipe obj
     */
    @Path("getRecipe")
    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public Recipe getRecipe(@QueryParam("id") int id){
        return recipeRepo.getRecipe(id);
    }

    public void createUser(String name, int age, String email, String password){
        User newUser = new User();
        newUser.setName(name);
        newUser.setAge(age);
        newUser.setEmail(email);
        newUser.setPassword(password);
        userRepo.addUser(newUser);
    }

}

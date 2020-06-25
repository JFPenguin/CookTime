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

        createUser("Pedrito Johnson",13,"xxxxXXXXpedritoSexyGamer420XXXxxx@yahoo.com","password");
        createUser("Ojo Noda",18,"OjoNoda@gmail.com","password");
        createUser("Fus RoDah",14,"dragonborn69@bugtesda.com","password");
        createUser("Michael Jayson Toshiba",65,"theRealMichaelJayson@gmail.com","password");
        createUser("Kenny Velasquez",22,"kennyBellius@gmail.com","password");

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

        Recipe galloPinto = new Recipe("Gallo Pinto",userRepo.getUser(0),DishType.BREAKFAST,
                5,15,DishTime.MAIN_DISH,3,galloPintoTags,galloPintoIngredients,galloPintoInstructions);

        recipeRepo.addRecipe(galloPinto);

    }


    /**
     * Method handling HTTP GET requests. The returned object will be sent
     * to the client as "text/plain" media type.
     * @return String that will be returned as a text/plain response.
     */
    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public String getIt() {
        return "Resources Main page, \n\nnot much to see here";
    }

    @Path("getUser")
    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public User getUser(@QueryParam("id") int id){
        return userRepo.getUser(id);
    }

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

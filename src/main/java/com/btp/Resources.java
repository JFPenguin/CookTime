package com.btp;

import com.btp.serverData.Recipe;
import com.btp.serverData.RecipeRepo;
import com.btp.serverData.User;
import com.btp.serverData.UserRepo;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;

/**
 * Root resource (exposed at "myresource" path)
 */
@Path("resources")
public class Resources {


//    public Resources() {
//
//        //Test Users
//        User u1 = new User();
//        u1.setName("Pedrito Johnson"); u1.setEmail("xxxxXXXXpedritoSexyGamer420XXXxxx@yahoo.com");u1.setPassword("password");u1.setAge(13);
//
//        User u2 = new User();
//        u2.setName("Ojo Noda"); u2.setEmail("OjoNoda@gmail.com");u2.setPassword("password");u2.setAge(18);
//
//        User u3 = new User();
//        u3.setName("Fus RoDah"); u3.setEmail("dragonborn69@bugtesda.com");u3.setPassword("password");u3.setAge(18);
//
//        User u4 = new User();
//        u4.setName("Michael Jayson Toshiba"); u4.setEmail("theRealMichaelJayson@gmail.com");u4.setPassword("password");u4.setAge(65);
//
//        User u5 = new User();
//        u5.setName("Kenny Velasquez"); u5.setEmail("kennyBellius@gmail.com");u5.setPassword("password");u5.setAge(22);
//
//
//        userRepo.addUser(u1);
//        userRepo.addUser(u2);
//        userRepo.addUser(u3);
//        userRepo.addUser(u4);
//        userRepo.addUser(u5);
//
//
//        //Test Recipes
//        //Gallo Pinto
//        SinglyList<DishTag> galloPintoTags = new SinglyList<>();
//        galloPintoTags.add(DishTag.VEGAN);
//
//        SinglyList<Ingredient> galloPintoIngredients = new SinglyList<>();
//        galloPintoIngredients.add(new Ingredient("Arroz",1,MeasurementUnit.CUP));
//        galloPintoIngredients.add(new Ingredient("Frijoles",1,MeasurementUnit.CUP));
//        galloPintoIngredients.add(new Ingredient("Especies Mixtas",1/2,MeasurementUnit.CUP));
//        galloPintoIngredients.add(new Ingredient("Lizano, obvio",1/2,MeasurementUnit.CUP));
//
//        SinglyList<String> galloPintoInstructions = new SinglyList<>();
//        galloPintoInstructions.add("Revolver las varas");
//        galloPintoInstructions.add("Cocinar");
//
//        Recipe galloPinto = new Recipe("Gallo Pinto",userRepo.getUser(0), DishTime.BREAKFAST,
//                5,15, DishType.MAIN_DISH,3,galloPintoTags,galloPintoIngredients,galloPintoInstructions);
//
//        //CheeseCake
//        SinglyList<DishTag> CheeseCakeTags = new SinglyList<>();
//        CheeseCakeTags.add(DishTag.VEGETARIAN);
//
//        SinglyList<Ingredient> cheeseCakeIngredients = new SinglyList<>();
//        cheeseCakeIngredients.add(new Ingredient("Queso Crema",300,MeasurementUnit.GRAM));
//        cheeseCakeIngredients.add(new Ingredient("Azucar",1,MeasurementUnit.CUP));
//        cheeseCakeIngredients.add(new Ingredient("Galleta Maria",500,MeasurementUnit.GRAM));
//        cheeseCakeIngredients.add(new Ingredient("Mantequilla",150,MeasurementUnit.GRAM));
//        cheeseCakeIngredients.add(new Ingredient("Huevos", 3,MeasurementUnit.UNIT));
//        cheeseCakeIngredients.add(new Ingredient("Natilla",1,MeasurementUnit.CUP));
//        cheeseCakeIngredients.add(new Ingredient("Jalea de Fresa",(float)1/2,MeasurementUnit.CUP));
//
//        SinglyList<String> cheeseCakeInstructions = new SinglyList<>();
//        cheeseCakeInstructions.add("Precalentar horno a 350°F ");
//        cheeseCakeInstructions.add("Pulverize la galleta maria y mézclela con la mantequilla" +
//                " y recubra el interior de un molde para queques");
//        cheeseCakeInstructions.add("Mezcle el queso crema y la azucar hasta que tenga una consistencia suave, " +
//                "agregue los huevos uno a uno, mezclando despues de cada uno");
//        cheeseCakeInstructions.add("Hornee en el horno precalentado por 50 minutos, espere a que se enfrie por completo antes de remover del molde");
//
//        Recipe cheeseCake = new Recipe("Cheese Cake",userRepo.getUser(1), DishTime.BREAKFAST,
//                5,15, DishType.MAIN_DISH,3,CheeseCakeTags,cheeseCakeIngredients,cheeseCakeInstructions);
//
//        recipeRepo.addRecipe(cheeseCake);
//        recipeRepo.addRecipe(galloPinto);
//
//
//    }


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
        return UserRepo.getUser(id);
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
        return RecipeRepo.getRecipe(id);
    }


    @POST
    @Path("createUser")
    public void createUser(User user){
        System.out.println(user.getName()+ user.getEmail()+ user.getPassword()+ user.getAge());
        UserRepo.addUser(user);
    }

//    @Path("createRecipe")
//    @POST
//    public void createRecipe(String name,User author,DishTime dishTime,int portions, int duration,DishType dishType,
//                             float difficulty, SinglyList<DishTag> dishTags, SinglyList<Ingredient> ingredients,
//                             SinglyList<String> instructions){
//
//        Recipe recipe = new Recipe(name,author,dishTime,portions,duration,dishType,
//                difficulty,dishTags,ingredients,instructions);
//        recipeRepo.addRecipe(recipe);
//    }

//    @Path("getIngredient")
//    @GET
//    public Ingredient getIngredient(String ingredient, float qty, MeasurementUnit measurementUnit) {
//        return new Ingredient(ingredient, qty, measurementUnit);
//    }

}

package com.btp;

import com.btp.gui.ServerGUI;
import com.btp.serverData.clientObjects.Recipe;
import com.btp.serverData.clientObjects.User;
import com.btp.serverData.repos.RecipeRepo;
import com.btp.serverData.repos.UserRepo;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;

import javax.persistence.criteria.CriteriaBuilder;
import java.security.NoSuchAlgorithmException;
import java.util.Random;

import static com.btp.security.HashPassword.hashPassword;

/**
 * Root resource (exposed at "myresource" path)
 */
@Path("resources")
public class Resources {

    private static final Random random = new Random();


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
     * @param email String value of the email of the user
     * @return User obj
     */
    @Path("getUser")
    @GET
    @Produces(MediaType.APPLICATION_JSON)
        public User getUser(@QueryParam("id") String email){
        return UserRepo.getUser(email);
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
    public void createUser(User user, @QueryParam("uniqueID") boolean uniqueID) throws NoSuchAlgorithmException {
        System.out.println("new user!");
        System.out.println("name: "+ user.fullName()+" email: "+user.getEmail()+ " password: "+user.getPassword()+ " age: "+user.getAge());
        if(Initializer.isGUIOnline()){
            Initializer.getServerGUI().printLn("new user!");
            Initializer.getServerGUI().printLn("name: "+ user.fullName()+"\nemail: "+user.getEmail()+ "\npassword: "+user.getPassword()+ "\nage: "+user.getAge());
        }
//        int i = random.nextInt(999) + 1;
//        System.out.println("generating id...");
//        System.out.println("userID: "+i);
//        if(Initializer.isGUIOnline()){
//            Initializer.getServerGUI().printLn("generating id...");
//            Initializer.getServerGUI().printLn("userID: "+i);
//        }
//        while (UserRepo.checkByID(i)){
//            i = random.nextInt(999) + 1;
//            System.out.println("id in use, generating new id...");
//            if(Initializer.isGUIOnline()){
//                Initializer.getServerGUI().printLn("id in use, generating new id...");
//            }
//        }
        user.setPassword(hashPassword(user.getPassword()));
        UserRepo.addUser(user);
    }

    @GET
    @Path("isEmailNew")
    @Produces(MediaType.APPLICATION_JSON)
    public String isEmailNew(@QueryParam("email") String email){
        String value;
        if (UserRepo.checkByID(email)) {
            value = "1";
        }
        else {
            value = "0";
        }
        return value;
    }

    @GET
    @Path("auth")
    @Produces(MediaType.APPLICATION_JSON)
    public String authUserAndPassword(@QueryParam("email") String email, @QueryParam("password") String password) throws NoSuchAlgorithmException {
        if(UserRepo.checkByID(email)){
            User user = getUser(email);
            String userPassword = user.getPassword();
            String hashPassword = hashPassword(password);
            if(hashPassword.equals(userPassword)){
                return "1";
            }
            else {
                return "0";
            }
        }
        else {
            return "2";
        }

    }

    @PUT
    @Path("rateRecipe")
    public void rateRecipe(@QueryParam("id") int id,@QueryParam("rating") float rating){
        RecipeRepo.getRecipe(id).addScore(rating);
    }

    @PUT
    @Path("updateUserData")
    public void updateUserData(String email, String dataType, String data){
        User user = UserRepo.getUser(email);
        switch (dataType){
            case "firstName":
                user.setFirstName(data);
                break;
            case "lastName":
                user.setLastName(data);
                break;
            case "email":
                user.setEmail(data);
                break;
            case "password":
                user.setPassword(data);
                break;
            case "age":
                user.setAge(Integer.parseInt(data));
                break;
            default:
                System.out.println("incorrect update request type: "+dataType);
                if(Initializer.isGUIOnline()){
                    Initializer.serverGUI.printLn("incorrect update request type: "+dataType);
                }
        }
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

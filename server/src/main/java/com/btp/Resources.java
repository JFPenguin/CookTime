package com.btp;

import com.btp.serverData.clientObjects.Recipe;
import com.btp.serverData.clientObjects.User;
import com.btp.serverData.repos.BusinessRepo;
import com.btp.serverData.repos.UserRepo;
import com.btp.serverData.repos.RecipeRepo;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;
import org.glassfish.jersey.media.multipart.FormDataContentDisposition;

import java.io.*;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.Random;

import static com.btp.utils.security.HashPassword.hashPassword;

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
                "on your browser address bar add the following to the url: /getRecipe?id= followed by the int value of the id"+
                "\n\nto se the cheeseCake, put /getPicture?id=cheesecake-picture0";
    }

    /**
     * API getter for the user obj
     *
     * @param email String value of the email of the user
     * @return User obj
     */
    @Path("getUser")
    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public User getUser(@QueryParam("id") String email) {
        return UserRepo.getUser(email);
    }

    /**
     * API getter for the recipe obj
     *
     * @param id int value of the id of the recipe
     * @return recipe obj
     */
    @Path("getRecipe")
    @GET
    @Produces(MediaType.APPLICATION_JSON)
    public Recipe getRecipe(@QueryParam("id") int id) {
        return RecipeRepo.getRecipe(id);
    }


    @POST
    @Path("createUser")
    public void createUser(User user, @QueryParam("uniqueID") boolean uniqueID) throws NoSuchAlgorithmException {
        System.out.println("new user!");
        System.out.println("name: " + user.fullName() + " email: " + user.getEmail() + " password: " + user.getPassword() + " age: " + user.getAge());
        if (Initializer.isGUIOnline()) {
            Initializer.getServerGUI().printLn("new user!");
            Initializer.getServerGUI().printLn("name: " + user.fullName() + "\nemail: " + user.getEmail() + "\npassword: " + user.getPassword() + "\nage: " + user.getAge());
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
    public String isEmailNew(@QueryParam("email") String email) {
        String value;
        if (UserRepo.checkByID(email)) {
            value = "1";
        } else {
            value = "0";
        }
        return value;
    }

    @GET
    @Path("auth")
    @Produces(MediaType.APPLICATION_JSON)
    public String authUserAndPassword(@QueryParam("email") String email, @QueryParam("password") String password) throws NoSuchAlgorithmException {
        if (UserRepo.checkByID(email)) {
            User user = UserRepo.getUser(email);
            String userPassword = user.getPassword();
            String hashPassword = hashPassword(password);
            if (hashPassword.equals(userPassword)) {
                return "1";
            } else {
                return "0";
            }
        } else {
            return "2";
        }

    }

    @GET
    @Path("followUser")
    @Produces(MediaType.APPLICATION_JSON)
    public String followUser(@QueryParam("ownEmail") String ownEmail, @QueryParam("followingEmail") String followingEmail) {
        String response;
        User ownUser = UserRepo.getUser(ownEmail);
        User followingUser = UserRepo.getUser(followingEmail);

        boolean alreadyFollows = false;

        for (String email : ownUser.getFollowingEmails()) {
            if (email.equals(followingEmail)) {
                alreadyFollows = true;
                break;
            }
        }

        if (alreadyFollows) {
            response = "0";
        }else{
            ownUser.addFollowing(followingEmail+";"+followingUser.fullName());
            followingUser.addFollower(ownEmail+";"+ownUser.fullName());
            response = "1";
        }

        //TODO notify followingUser
        UserRepo.updateTree();
        return response;
    }

    @GET
    @Path("editUserPassword")
    @Produces(MediaType.APPLICATION_JSON)
    public String editUser(@QueryParam("email") String email, @QueryParam("newPassword") String newPassword,
                           @QueryParam("password") String password) throws NoSuchAlgorithmException {
        String response;
        System.out.println(email);
        User user = UserRepo.getUser(email);
        System.out.println(password);
        System.out.println(newPassword);
        if (user.getPassword().equals(hashPassword(password))) {
            user.setPassword(hashPassword(newPassword));
            response = "1";
        } else {
            response = "0";
        }
        System.out.println(response);
        UserRepo.updateTree();
        return response;
    }

    @GET
    @Path("rateUser")
    public void rateChef(@QueryParam("email") String email, @QueryParam("rating") float rating) {
        System.out.println("Rating user...");
        User user = UserRepo.getUser(email);

        if (user.isChef()) {
            user.addChefScore(rating);
            System.out.println("Chef rated");
        }
    }

    @GET
    @Path("search")
    @Produces(MediaType.APPLICATION_JSON)
    public ArrayList<ArrayList> search(@QueryParam("search") String search, @QueryParam("filter") String filter) {
        System.out.println("Searching recipes");
        ArrayList<ArrayList> profilesList = new ArrayList<>();
        ArrayList<String> userList = new ArrayList<>();
        ArrayList<String> recipeList = new ArrayList<>();
        ArrayList<String> businessList = new ArrayList<>();

        System.out.println(search);

        switch (filter) {
            case "filter1":
                System.out.println("Filter 1");
                break;
            case "filter2":
                System.out.println("Filter 2");
                break;
            case "filter3":
                System.out.println("Filter 3");
                break;
            default:
                System.out.println("Default Filter");
                userList = UserRepo.searchUsers(search);
                recipeList = RecipeRepo.searchByName(search);
                businessList = BusinessRepo.search(search);
        }
        System.out.println(profilesList);
        userList = prioChef(userList);
        recipeList = prioChef(recipeList);
        profilesList.add(userList);
        profilesList.add(recipeList);
        profilesList.add(businessList);
        return profilesList;
    }

    /**
    *
     */
    @GET
    @Path("getPicture")
    @Produces("image/png")
    public Response getPicture(@QueryParam("id") String id) {
        File file = new File(System.getProperty("project.folder") + "/dataBase/photos/" + id + ".png");
        Response.ResponseBuilder response = Response.ok(file);
        response.header("Photo", "attachment:filename=DisplayName-" + id + ".png");
        return response.build();
    }

//    /**
//     *
//     * @param id
//     * @param uploadedInputStream
//     * @param fileDetail
//     */
//    @POST
//    @Path("addRecipePicture")
//    @Consumes(MediaType.MULTIPART_FORM_DATA)
//    public static void addRecipePicture(@QueryParam("id") int id, @FormDataParam("file") InputStream uploadedInputStream, @FormDataParam("file") FormDataContentDisposition fileDetail) {
//        String location = System.getProperty("project.folder") + "/dataBase/photos/";
//        RecipeRepo.getRecipe(id).addPhotos(saveToDisk(uploadedInputStream, fileDetail, (String.valueOf(id)), location));
//    }

//    @POST
//    @Path("addUserPicture")
//    @Consumes(MediaType.MULTIPART_FORM_DATA)
//    public static void addUserPicture(@QueryParam("id") String id, InputStream uploadedInputStream, FormDataContentDisposition fileDetail) {
//        String location = System.getProperty("project.folder") + "/dataBase/photos";
//        UserRepo.getUser(id).addPhoto(saveToDisk(uploadedInputStream, fileDetail, id, location));
//
//    }

    private static String saveToDisk(InputStream uploadedInputStream, FormDataContentDisposition fileDetail, String id, String location) {
        try {
            OutputStream out = new FileOutputStream(new File(location + id + "-" + fileDetail.getName()));
            int read = 0;
            byte[] bytes = new byte[1024];

            out = new FileOutputStream(new File(location + id + "-" + fileDetail.getName()));
            while ((read = uploadedInputStream.read(bytes)) != -1) {
                out.write(bytes, 0, read);
            }
            out.flush();
            out.close();
        } catch (IOException e) {
            e.printStackTrace();
        }
        return id + "-" + fileDetail.getName();
    }


//    @PUT
//    @Path("rateRecipe")
//    public void rateRecipe(@QueryParam("id") int id,@QueryParam("rating") float rating){
//        RecipeRepo.getRecipe(id).addScore(rating);
//    }

//    @PUT
//    @Path("updateUserData")
//    public void updateUserData(String email, String dataType, String data){
//        User user = UserRepo.getUser(email);
//        switch (dataType){
//            case "firstName":
//                user.setFirstName(data);
//                break;
//            case "lastName":
//                user.setLastName(data);
//                break;
//            case "email":
//                user.setEmail(data);
//                break;
//            case "password":
//                user.setPassword(data);
//                break;
//            case "age":
//                user.setAge(Integer.parseInt(data));
//                break;
//            default:
//                System.out.println("incorrect update request type: "+dataType);
//                if(Initializer.isGUIOnline()){
//                    Initializer.serverGUI.printLn("incorrect update request type    : "+dataType);
//                }
//        }
//    }

//    @Path("createRecipe")
//    @POST
//    public void createRecipe(Recipe recipe){
//        int i = random.nextInt(999) + 1;
//        System.out.println("generating id...");
//        System.out.println("userID: "+i);
//        if(Initializer.isGUIOnline()){
//            Initializer.getServerGUI().printLn("generating id...");
//            Initializer.getServerGUI().printLn("userID: "+i);
//        }
//        while (RecipeRepo.checkByID(i)){
//            i = random.nextInt(999) + 1;
//            System.out.println("id in use, generating new id...");
//            if(Initializer.isGUIOnline()){
//                Initializer.getServerGUI().printLn("id in use, generating new id...");
//            }
//        }
//        String authorEmail = recipe.getAuthorEmail();
//        User author = UserRepo.getUser(authorEmail);
//        RecipeRepo.addRecipe(recipe);
//    }

//    @Path("getIngredient")
//    @GET
//    public Ingredient getIngredient(String ingredient, float qty, MeasurementUnit measurementUnit) {
//        return new Ingredient(ingredient, qty, measurementUnit);
//    }

    private ArrayList<String> prioChef(ArrayList<String> list){
        ArrayList<String> prioList= new ArrayList<>();
        int i = 0;
        for (String prioString:list) {
            if (prioString.split(";")[0].equals("true") && i < 3){
                prioList.add(i, prioString);
                i++;
            } else{
                prioList.add(prioString);
            }
        }
        return prioList;
    }
}

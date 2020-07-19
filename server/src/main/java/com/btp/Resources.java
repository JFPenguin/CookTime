package com.btp;

import com.btp.dataStructures.lists.SinglyList;
import com.btp.dataStructures.nodes.SinglyNode;
import com.btp.dataStructures.sorters.Sorter;
import com.btp.dataStructures.trees.UserBST;
import com.btp.serverData.clientObjects.Recipe;
import com.btp.serverData.clientObjects.User;
import com.btp.serverData.repos.BusinessRepo;
import com.btp.serverData.repos.UserRepo;
import com.btp.serverData.repos.RecipeRepo;
import com.btp.utils.Notifier;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;
import jakarta.ws.rs.core.Response;
import org.glassfish.jersey.media.multipart.FormDataContentDisposition;

import javax.persistence.criteria.CriteriaBuilder;
import java.io.*;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.Collections;
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
        user.sendMessage("Welcome to CookTime!");
        UserRepo.addUser(user);
    }

    @POST
    @Path("createRecipe")
    public void createRecipe(Recipe recipe){
        System.out.println("Starting");
        int i = random.nextInt(999) + 1;
        while (RecipeRepo.checkId(i)){
            System.out.println(i);
            i = random.nextInt(999) +1;
        }
        recipe.setId(i);
        recipe.setPostTime(System.currentTimeMillis());
        RecipeRepo.addRecipe(recipe);
        User user = UserRepo.getUser(recipe.getAuthorEmail());
        user.addRecipe(i);
        user.addNewsFeed(i);
        SinglyList<Recipe> recipeList = new SinglyList<>();
        for (int j:user.getRecipeList()){
            Recipe recipeTmp = RecipeRepo.getRecipe(j);
            recipeList.add(recipeTmp);
        }

        Sorter.insertionSort(recipeList);

        System.out.println(user.getRecipeList().toString());
        recipeList.print();
        SinglyNode tmp = recipeList.getHead();
        user.getRecipeList().clear();
        System.out.println(user.getRecipeList().toString());
        while (tmp!=null){
            System.out.println("While 1");
            Recipe recipeTmp = (Recipe) tmp.getData();
            user.addRecipe(recipeTmp.getId());
            tmp =(SinglyNode) tmp.getNext();
        }

        for(String data:user.getFollowerEmails()){
            String[] email = data.split(";");
            System.out.println(data + ";" + email + ";" + email[0]);
            User follower = UserRepo.getUser(email[0]);
            follower.addNewsFeed(i);
        }
        UserRepo.updateTree();
    }

    @GET
    @Path("shareRecipe")
    @Produces(MediaType.APPLICATION_JSON)
    public String shareRecipe(@QueryParam("id") int id, @QueryParam("email") String email){
        User user = UserRepo.getUser(email);
        SinglyList<Recipe> recipeList = new SinglyList<>();

        if (!RecipeRepo.checkId(id)){
            return "0";
        }

        for (int data:user.getRecipeList()) {
            if (data == id){
                return "0";
            }
        }

        user.addRecipe(id);

        for (int j:user.getRecipeList()){
            Recipe recipeTmp = RecipeRepo.getRecipe(j);
            recipeList.add(recipeTmp);
        }

        SinglyNode tmp = recipeList.getHead();
        user.getRecipeList().clear();
        while (tmp!=null){
            Recipe recipeTmp = (Recipe) tmp.getData();
            user.addRecipe(recipeTmp.getId());
            tmp =(SinglyNode) tmp.getNext();
        }
        UserRepo.updateTree();

        return "1";
    }

    @GET
    @Path("commentRecipe")
    @Produces(MediaType.APPLICATION_JSON)
    public String commentRecipe(@QueryParam("id") int id, @QueryParam("comment") String comment, @QueryParam("email") String email){
        Recipe recipe = RecipeRepo.getRecipe(id);
        recipe.addComment(comment+";"+UserRepo.getUser(email).fullName());

        User user = UserRepo.getUser(recipe.getAuthorEmail());

        Notifier.notify(recipe.getAuthorEmail(), "Your recipe: " + recipe.getName() + "has a new commented");
        RecipeRepo.updateTree();

        return "1";
    }

    @GET
    @Path("deleteRecipe")
    @Produces(MediaType.APPLICATION_JSON)
    public String deleteRecipe(@QueryParam("email") String email, @QueryParam("id") int id){
        boolean response;
        Recipe recipe = RecipeRepo.getRecipe(id);

        User user = UserRepo.getUser(email);

        if (recipe == null){
            response = false;
        }
        else if (!recipe.getAuthorEmail().equals(email)) {
            response = user.getRecipeList().remove(Integer.valueOf(id));

        } else {
            UserRepo.deleteRecipe(id);

            RecipeRepo.deleteRecipe(id);
            response = true;
        }

        RecipeRepo.updateTree();
        UserRepo.updateTree();
        if (response){
            return "1";
        } else {
            return "0";
        }
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
    @Path("myMenu")
    @Produces(MediaType.APPLICATION_JSON)
    public ArrayList<String> myMenu(@QueryParam("email") String email, @QueryParam("filter") String filter){
        ArrayList<String> myMenuList = new ArrayList<>();
        User user = UserRepo.getUser(email);
        SinglyList<Recipe> sortList = new SinglyList<>();

        for (int i:user.getRecipeList()) {
            Recipe recipe = RecipeRepo.getRecipe(i);
            sortList.add(recipe);
        }

        switch (filter){
            case "date":
                Sorter.bubbleSort(sortList);
                break;
            case "score":
                Sorter.quickSort(sortList);
                break;
            case "difficulty":
                Sorter.radixSort(sortList);
                break;
        }
        SinglyNode tmp = sortList.getHead();
        while (tmp!=null){
            Recipe recipe = (Recipe) tmp.getData();
            myMenuList.add(recipe.getId()+";"+recipe.getName()+
                    ";"+UserRepo.getUser(recipe.getAuthorEmail()).fullName()+";"+recipe.getAuthorEmail());
            tmp =(SinglyNode) tmp.getNext();
        }
        return myMenuList;
    }

    @GET
    @Path("newsfeed")
    @Produces(MediaType.APPLICATION_JSON)
    public ArrayList<String> newsfeed(@QueryParam("email") String email){
        ArrayList<String> newsfeed = new ArrayList<>();
        User user = UserRepo.getUser(email);
        SinglyList<Recipe> sortList = new SinglyList<>();

        for (int i:user.getNewsFeed()) {
            Recipe recipe = RecipeRepo.getRecipe(i);
            sortList.add(recipe);
        }
        Sorter.bubbleSort(sortList);

        SinglyNode tmp = sortList.getHead();
        while (tmp!=null){
            Recipe recipe = (Recipe) tmp.getData();
            newsfeed.add(recipe.getId()+";"+recipe.getName()+
                    ";"+UserRepo.getUser(recipe.getAuthorEmail()).fullName()+";"+recipe.getAuthorEmail());
            tmp =(SinglyNode) tmp.getNext();
        }
        return newsfeed;
    }

    @GET
    @Path("isFollowing")
    @Produces(MediaType.APPLICATION_JSON)
    public String isFollowing(@QueryParam("ownEmail") String ownEmail, @QueryParam("followingEmail") String followingEmail){
        User ownUser = UserRepo.getUser(ownEmail);
        User followingUser = UserRepo.getUser(followingEmail);

        String response = "0";

        for (String email : ownUser.getFollowingEmails()) {
            if (email.equals(followingEmail+";"+followingUser.fullName())) {
                response = "1";
                break;
            }
        }
        return response;
    }

    @GET
    @Path("isChef")
    @Produces(MediaType.APPLICATION_JSON)
    public String isChef(@QueryParam("email") String email){
        User user = UserRepo.getUser(email);
        if (user.isChef()){
            return "1";
        } else {
            return "0";
        }
    }

    @GET
    @Path("isChefRated")
    @Produces(MediaType.APPLICATION_JSON)
    public String isChefRated(@QueryParam("ownEmail") String ownEmail, @QueryParam("chefEmail") String chefEmail){
        return checkChefRating(ownEmail, chefEmail);
    }

    @GET
    @Path("rateChef")
    @Produces(MediaType.APPLICATION_JSON)
    public String rateChef(@QueryParam("ownEmail") String ownEmail, @QueryParam("chefEmail") String chefEmail, @QueryParam("rating") int score){
        String response = checkChefRating(ownEmail, chefEmail);
        if (response.equals("0")){
            User chef = UserRepo.getUser(chefEmail);
            chef.addRated(ownEmail);
            chef.addChefScore(score);
            chef.sendMessage("new rating by "+ UserRepo.getUser(ownEmail).fullName());
        }
        UserRepo.updateTree();
        return response;
    }

    private String checkChefRating(String ownEmail, String chefEmail) {
        User chef = UserRepo.getUser(chefEmail);
        String response = "0";
        if (ownEmail.equals(chefEmail) || chef.isChef()){
            response = "1";
        }
        else {
            for(String userEmail:chef.getRatedBy()){
                if (userEmail.equals(ownEmail)){
                    response = "1";
                    break;
                }
            }
        }
        return response;
    }

    @GET
    @Path("isRated")
    @Produces(MediaType.APPLICATION_JSON)
    public String isRated(@QueryParam("id") int id, @QueryParam("email") String email){
        return checkRating(id, email);
    }

    @GET
    @Path("rateRecipe")
    @Produces(MediaType.APPLICATION_JSON)
    public String rateRecipe(@QueryParam("id") int id, @QueryParam("email") String email, @QueryParam("rating") int score){
        String response = checkRating(id, email);
        if (response.equals("0")){
            Recipe recipe = RecipeRepo.getRecipe(id);
            recipe.addRating(email);
            recipe.addScore(score);
            UserRepo.getUser(recipe.getAuthorEmail()).sendMessage("your recipe: "+recipe.getName()+" has been rated!");
        }
        RecipeRepo.updateTree();
        return response;
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
            if (email.equals(followingEmail+";"+followingUser.fullName())) {
                alreadyFollows = true;
                break;
            }
        }

        if (alreadyFollows) {
            ownUser.unFollowing(followingEmail+";"+followingUser.fullName());
            followingUser.unFollower(ownEmail+";"+ownUser.fullName());
            response = "0";
        }else{
            ownUser.addFollowing(followingEmail+";"+followingUser.fullName());
            followingUser.addFollower(ownEmail+";"+ownUser.fullName());
            followingUser.sendMessage(ownUser.fullName()+" is now following you");
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
    @Path("recommend")
    @Produces(MediaType.APPLICATION_JSON)
    public ArrayList<String> recommended(@QueryParam("email") String email){
        ArrayList<String> userList = UserRepo.recommend(email);
        ArrayList<String> recipeList = RecipeRepo.recommend(email);
        ArrayList<String> businessList = BusinessRepo.recommend(email);

        ArrayList<String> profilesList = createSearchList(userList, recipeList, businessList);

        Collections.shuffle(profilesList);

        ArrayList<String> returnList = new ArrayList<>();

        int i = 0;
        int maxSize = profilesList.size();

        if (maxSize > 15){
            maxSize = 15;
        }

        while (i < maxSize){
            returnList.add(profilesList.get(i));
            i++;
        }
        return returnList;
    }

   @GET
   @Path("ratings")
   @Produces(MediaType.APPLICATION_JSON)
   public ArrayList<String> ratings(){
        ArrayList<String> ratingList = new ArrayList<>();



        return ratingList;
   }

    @GET
    @Path("searchByFilter")
    @Produces(MediaType.APPLICATION_JSON)
    public ArrayList<String> filteredSearch(@QueryParam("search") String search, @QueryParam("filter") String filter){
        ArrayList<String> recipeList = new ArrayList<>();

        switch (filter){
            case "tag":

            case "time":

            case "type":
        }

        return recipeList;
    }

    @GET
    @Path("searchByName")
    @Produces(MediaType.APPLICATION_JSON)
    public ArrayList<String> search(@QueryParam("search") String search) {
        ArrayList<String> userList = UserRepo.searchUsers(search);
        ArrayList<String> recipeList = RecipeRepo.searchByName(search);
        ArrayList<String> businessList = BusinessRepo.search(search);

        ArrayList<String> profilesList = createSearchList(userList, recipeList, businessList);

        return profilesList;
    }

    private ArrayList<String> createSearchList(ArrayList<String> userList, ArrayList<String> recipeList,
                                               ArrayList<String> businessList){
        ArrayList<String> profilesList = new ArrayList<>();

        int i = 0;

        for (String data: userList){
            String[] stringList = data.split(";");
            if (stringList[2].equals("chef") && i < 3){
                profilesList.add(i, data);
                i++;
            } else {
                profilesList.add(data);
            }
        }

        for (String data: recipeList){
            String[] stringList = data.split(";");
            if (stringList[3].equals("chef") && i < 3){
                profilesList.add(i, stringList[0]+";"+stringList[1]+";"+stringList[2]);
            } else {
                profilesList.add(stringList[0]+";"+stringList[1]+";"+stringList[2]);
            }
        }

        for (String data: businessList){
            profilesList.add(data);
        }

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

    @GET
    @Path("chefRequest")
    @Produces(MediaType.APPLICATION_JSON)
    public String chefRequest(@QueryParam("id") String id){
        if(!UserRepo.getUser(id).isChef()) {
            if (!UserRepo.isActiveRequest(id)) {
                UserRepo.addChefRequest(id);
                UserRepo.getUser(id).sendMessage("We have received your request to become a chef!, please wait from 5 to 7 business days for your request to be processed, thank you!");
                return "2";
            }
            else{
                UserRepo.getUser(id).sendMessage("You already have an active request!, please wait until you receive a response");
                return "0";
            }
        }
        return "1";
    }

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

    private String checkRating(int id, String email){
        Recipe recipe = RecipeRepo.getRecipe(id);
        String response = "0";
        if (email.equals(recipe.getAuthorEmail())){
            response = "1";
        }
        else {
            for(String userEmail:recipe.getRatedBy()){
                if (userEmail.equals(email)){
                    response = "1";
                    break;
                }
            }
        }
        return response;
    }
}

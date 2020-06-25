package com.btp;

import com.btp.serverData.User;
import com.btp.serverData.UserRepo;
import jakarta.ws.rs.*;
import jakarta.ws.rs.core.MediaType;

/**
 * Root resource (exposed at "myresource" path)
 */
@Path("resources")
public class Resources {

    User pedrito = new User();
    UserRepo userRepo = new UserRepo();


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


}

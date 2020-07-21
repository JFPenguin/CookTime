package com.btp.serverData.repos;

import com.btp.Initializer;
import com.btp.dataStructures.trees.BusinessTree;
import com.btp.serverData.clientObjects.Business;
import com.btp.utils.DataWriter;

import java.util.ArrayList;

/**
 * This class represents the repository for businesses
 */
public class BusinessRepo {

    private static BusinessTree businessTree = new BusinessTree();
    private static final DataWriter<BusinessTree> dataWriter = new DataWriter<>();
    private static final String path =System.getProperty("project.folder")+"/dataBase/businessDataBase.json";

    /**
     * This method is used to add a business to the repo
     * @param business Business obj
     */
    public static void addBusiness(Business business){
        businessTree.insert(business);
        System.out.println("Business added");
        if(Initializer.isGUIOnline()){
            Initializer.getServerGUI().printLn("Business added");
        }
        dataWriter.writeData(businessTree, path);
    }

    public static boolean checkBusinessById(int id){
        return businessTree.checkById(id);
    }

    /**
     * This method is used to retrieve a business using the id
     * @param id int id to identify the business
     * @return Business obj
     */
    public static Business getBusiness(int id){
        return businessTree.getElementById(id);
    }

    /**
     * this method is used to search businesses using a string
     * @param data String to be used on the search
     * @return ArrayList of strings of the results
     */
    public static ArrayList<String> search(String data){
        return businessTree.search(data);
    }

    /**
     * this method is used to recommend businesses
     * @param data String to be used on the search
     * @return ArrayList of strings of the results
     */
    public static ArrayList<String> recommend(String data){
        return businessTree.recommend(data);
    }

    /**
     * this method is used to retrieve the businesses with the highest rating
     * @return ArrayList of the 5 highest rated businesses
     */
    public static ArrayList<String> rating(){
        return businessTree.rating();
    }
}

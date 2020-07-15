package com.btp.serverData.repos;

import com.btp.Initializer;
import com.btp.dataStructures.trees.BusinessTree;
import com.btp.serverData.clientObjects.Business;
import com.btp.utils.DataWriter;

import java.util.ArrayList;

public class BusinessRepo {

    private static BusinessTree businessTree = new BusinessTree();
    private static final DataWriter<BusinessTree> dataWriter = new DataWriter<>();
    private static final String path =System.getProperty("project.folder")+"/dataBase/businessDataBase.json";

    public static void addBusiness(Business business){
        businessTree.insert(business);
        System.out.println("Business added");
        if(Initializer.isGUIOnline()){
            Initializer.getServerGUI().printLn("Business added");
        }
        dataWriter.writeData(businessTree, path);
    }

    public static Business getBusiness(int id){
        return businessTree.getElementById(id);
    }

    public static ArrayList<String> search(String data){
        return businessTree.search(data);
    }
}

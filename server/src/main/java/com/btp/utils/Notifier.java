package com.btp.utils;

import com.btp.serverData.repos.UserRepo;

import java.util.ArrayList;

public class Notifier {

    public static void notify(  String id, String notification){
        UserRepo.getUser(id).sendMessage(notification);
        UserRepo.updateTree();
    }

    public static void notifyMult(ArrayList<String> idList, String notification){
        for (String s : idList) {
            UserRepo.getUser(s).sendMessage(notification);
            UserRepo.updateTree();
        }
    }

    public static void notifyAll(String notification){
        UserRepo.notifyAll(notification);
        UserRepo.updateTree();
    }
}

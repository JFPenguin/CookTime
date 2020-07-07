package com.btp.security;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class HashPassword {

    public static String hashPassword(String password) throws NoSuchAlgorithmException {
        MessageDigest md = MessageDigest.getInstance("MD5");
        md.update(password.getBytes());
        byte[] b = md.digest();
        StringBuilder sb = new StringBuilder();
        for(byte b1 : b){
            sb.append(Integer.toHexString(b1 & 0xff));
        }
        return sb.toString();
    }
}
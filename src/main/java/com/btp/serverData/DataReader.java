package com.btp.serverData;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.StringWriter;

/**
 * This class is used to create a reader object, that can user .readData() to return an object based on a .json file
 * @param <T> Generic value to return value to the required java object
 */
public class DataReader<T> {

    /**
     * this method takes a string parameter that refers to a path to a .json file, and returns a
     * @param path String value of a file on disc
     * @return Generic obj of the type that was requested
     */
    public T readData(String path){
        ObjectMapper objectMapper = new ObjectMapper();
        StringWriter stringWriter = new StringWriter();
        JSONParser jsonParser = new JSONParser();
        T data;

        try(FileReader reader = new FileReader(path)){
            System.out.println("reading file");
            Object obj = jsonParser.parse(reader);
            data = ((T) obj);
            return data;
        } catch (IOException | ParseException e) {
            e.printStackTrace();
        }
        return null;
    }
}

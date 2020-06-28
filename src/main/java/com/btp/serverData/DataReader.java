package com.btp.serverData;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.io.StringWriter;

public class DataReader<T> {

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

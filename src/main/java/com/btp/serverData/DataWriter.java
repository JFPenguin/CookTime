package com.btp.serverData;

import com.fasterxml.jackson.databind.ObjectMapper;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

import java.io.*;

public class DataWriter<T>{

    public void writeData(T data, String path) {
        ObjectMapper objectMapper = new ObjectMapper();
        StringWriter stringWriter = new StringWriter();
        JSONParser jsonParser = new JSONParser();

        String dataString;

        try {
            dataString = objectMapper.writeValueAsString(data);
            System.out.println("\nJSON Object: " + dataString);
            String absolutePath = new File(path).getAbsolutePath();
            FileWriter file = new FileWriter(absolutePath);
            file.write(dataString);
            file.flush();
            System.out.println("Successfully Copied JSON Object to File...");

        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}

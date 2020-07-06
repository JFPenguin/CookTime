package com.btp.serverData.clientObjects;

/**
 * This is the Class for the User obj, it holds the user data
 */
public class User implements Comparable<User> {
    private int id;
    private String name;
    private String email;
    private String password;
    private int age;

    /**
     * Getter of the id attribute
     * @return int of the id attribute
     */
    public int getId() {
        return id;
    }

    /**
     * Setter of the id attribute
     * @param id int the id to be set
     */
    public void setId(int id) {
        this.id = id;
    }

    /**
     * Getter of the name attribute
     * @return String of the name attribute
     */
    public String getName() {
        return name;
    }

    /**
     * Setter of the id attribute
     * @param name String the name to be set
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * Getter of the email attribute
     * @return String of the email attribute
     */
    public String getEmail() {
        return email;
    }

    /**
     * Setter of the email attribute
     * @param email String the email to be set
     */
    public void setEmail(String email) {
        this.email = email;
    }

    /**
     * Getter of the password attribute
     * @return String of the password attribute
     */
    public String getPassword() {
        return password;
    }

    /**
     * Setter of the password attribute
     * @param password String the password to be set
     */
    public void setPassword(String password) {
        this.password = password;
    }

    /**
     * Getter of the age attribute
     * @return int of the age attribute
     */
    public int getAge() {
        return age;
    }

    /**
     * Setter of the age attribute
     * @param age int the age to be set
     */
    public void setAge(int age) {
        this.age = age;
    }

    /**
     * Compares this User to the User in the parameter using their id attribute
     * @param user the User to be compared
     * @return int the result of the comparison
     */
    @Override
    public int compareTo(User user) {
        return this.getId() - user.getId();
    }
}

package com.btp.dataStructures.trees;
import com.btp.dataStructures.nodes.UserTreeNode;
import com.btp.serverData.clientObjects.User;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

/**
 * A class that represents a Binary Tree
 */
@JsonIgnoreProperties(ignoreUnknown = true)
public class UserBST {
    private UserTreeNode root;

    /**
     * Constructor for the class
     */
    public UserBST() {
        this.root = null;
    }

    /**
     * Checks if the tree is empty
     * @return true if the tree is empty, false if not
     */
    public boolean isEmpty() {
        return this.root == null;
    }

    /**
     * Checks if an id is inside the tree. Calss checkById private method
     * @param email int of the id to be searched
     * @return boolean true if the id is on the tree, false if not
     */
    public boolean checkByEmail(String email){
        if(this.isEmpty()){
            return false;
        }
        else{
            return checkByEmail(email, this.root);
        }
    }

    /**
     * Checks if an id is inside the tree. Calls itself recursively
     * @param email int of the id to be searched
     * @param node current node is searching
     * @return boolean true if the id is on the tree, false if not
     */
    private boolean checkByEmail(String email, UserTreeNode node){
        if (node == null){
            return false;
        } else {
            int compareValue = email.compareToIgnoreCase(node.getElement().getEmail());

            if (compareValue < 0) {
                return checkByEmail(email, node.getLeft());
            } else if (compareValue > 0) {
                return checkByEmail(email, node.getRight());
            } else {
                return true;
            }
        }
    }

    /**
     * Gets an user of the tree using its id. Calls the private getElementById method
     * @param email the email of the user to be searched
     * @return the user of that respective id
     */
    public User getElementByEmail(String email){
        return getElementByEmail(email, this.root);
    }

    /**
     * Gets an user of the tree using its id. Calls itself recursively
     * @param email the email of the user to be searched
     * @param node the current node is searching
     * @return the user of that respective id
     */
    private User getElementByEmail(String email, UserTreeNode node){
        if (node == null){
            return null;
        }
        int compareValue = email.compareToIgnoreCase(node.getElement().getEmail());

        if(compareValue < 0){
            return getElementByEmail(email, node.getLeft());
        } else if (compareValue > 0) {
            return getElementByEmail(email, node.getRight());
        } else {
            return node.getElement();
        }
    }

    /**
     * Finds the minimun element in the tree.
     * Calls the recursive method findMin
     *
     * @return the smallest element found
     */
    public User findMin() {
        if (this.isEmpty()) {
            return null;
        } else {
            return this.findMin(this.root).getElement();
        }
    }

    /**
     * Recursive method that finds the minor element in the tree
     *
     * @param node The node on which the method starts checking
     * @return The found min element
     */
    public UserTreeNode findMin(UserTreeNode node) {
        if (node == null) {
            return null;
        } else if (node.getLeft() == null) {
            return node;
        } else {
            return findMin(node.getLeft());
        }
    }

    /**
     * Finds the maximum element in the tree.
     * Calls the private method findMax
     *
     * @return the biggest element found
     */
    public User findMax() {
        if (this.isEmpty()) {
            return null;
        } else {
            return this.findMax(this.root).getElement();
        }
    }

    /**
     * Method that finds the mayor element in the tree
     *
     * @param node The node on which the method starts checking
     * @return The found max element
     */
    public UserTreeNode findMax(UserTreeNode node) {
        if (node != null) {
            while (node.getRight() != null) {
                node = node.getRight();
            }
        }
        return node;
    }

    /**
     * Calls the recursive method insert
     *
     * @param user the element to be inserted
     */
    public void insert(User user) {
        this.root = this.insert(user, this.root);
    }

    /**
     * Recursive method that inserts a new node in the tree
     *
     * @param user the element to be inserted
     * @param current the current node being compared
     * @return the new TreeNode to be inserted
     */
    private UserTreeNode insert(User user, UserTreeNode current) {
        if (current == null) {
            UserTreeNode userTreeNode = new UserTreeNode( null, null);
            userTreeNode.setElement(user);
            return userTreeNode;

        }

        int compareValue = user.getEmail().compareToIgnoreCase(current.getElement().getEmail());

        if (compareValue < 0) {
            current.setLeft(this.insert(user, current.getLeft()));
        } else if (compareValue > 0) {
            current.setRight(this.insert(user, current.getRight()));
        }
        return current;
    }

    /**
     * Calls the recursive method remove
     *
     * @param email the element to be removed
     */
    public void remove(String email) {
        this.root = this.remove(getElementByEmail(email), this.root);
    }

    /**
     * Recursive method that removes a certain node
     * @param user the element that will be searched and deleted
     * @param current the current node being compared
     * @return current Node
     */
    private UserTreeNode remove(User user, UserTreeNode current) {
        if (current == null) {
            return current;
        }
        int compareValue = user.getEmail().compareToIgnoreCase(current.getElement().getEmail());

        if (compareValue < 0) {
            current.setLeft(this.remove(user, current.getLeft()));
        } else if (compareValue > 0) {
            current.setRight(this.remove(user, current.getRight()));
        } else if (current.getLeft() != null && current.getRight() != null) {
            current.setElement(findMin(current.getRight()).getElement());
            current.setRight(this.remove(current.getElement(), current.getRight()));
        } else {
            current = current.getLeft() != null ? current.getLeft() : current.getRight();
        }
        return current;
    }

    /**
     * Getter for the root attribute
     * @return the root TreeNode
     */
    public UserTreeNode getRoot() {
        return root;
    }

    /**
     * Prints the tree using each user's name attribute in preorder
     * Calls the private preorderUser function
     */
    public void preorder() {
        preorderUser(this.root);
    }

    /**
     * Prints the tree using each user's name attribute in preorder
     * Calls itself recursively
     * @param root the current node is printing
     */
    private void preorderUser(UserTreeNode root){
        if (root != null) {
            System.out.println(root.getElement().getFirstName() + " ");
            preorderUser(root.getLeft());
            preorderUser(root.getRight());
        }
    }
}
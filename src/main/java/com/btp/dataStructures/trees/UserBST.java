package com.btp.dataStructures.trees;
import com.btp.dataStructures.nodes.UserTreeNode;
import com.btp.serverData.clientObjects.User;
import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

/**
 * A class that represents a Binary Tree
¿ */
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
     * Checks if an element is inside the tree.
     * Calls the recursive method contains
     *
     * @param id The element to be checked
     * @return true if the element is contained, false if not
     */
    public boolean contains(int id) {
        return this.contains(id, this.root);
    }

    /**
     * Recursive method that actively checks if an element is in the tree
     *
     * @param id The element to be checked
     * @param node    The node on which the method starts checking
     * @return true if the element is contained, false if not
     */
    private boolean contains(int id, UserTreeNode node) {
        if (node == null) {
            return false;
        } else {
            int compareValue = id - node.getElement().getId();

            if (compareValue < 0) {
                return contains(id, node.getLeft());
            } else if (compareValue > 0) {
                return contains(id, node.getRight());
            } else {
                return true;
            }
        }
    }

    public boolean checkById(int id){
        if(this.isEmpty()){
            return false;
        }
        else{
            return checkById(id, this.root);
        }
    }

    private boolean checkById(int id, UserTreeNode node){
        if (node == null){
            return false;
        } else {
            int compareValue = id - node.getElement().getId();

            if (compareValue < 0) {
                return checkById(id, node.getLeft());
            } else if (compareValue > 0) {
                return checkById(id, node.getRight());
            } else {
                return true;
            }
        }
    }

    public User getElementById(int id){
        if (this.root.getElement().getClass().equals(User.class)){
            return getElementById(id, this.root);
        } else {
            System.out.println("Tree is not made of User type");
            return null;
        }
    }

    private User getElementById(int id, UserTreeNode node){
        if (node == null){
            return null;
        }
        int compareValue = id - node.getElement().getId();

        if(compareValue < 0){
            return getElementById(id, node.getLeft());
        } else if (compareValue > 0) {
            return getElementById(id, node.getRight());
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

        int compareValue = user.getId() - current.getElement().getId();

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
     * @param id the element to be removed
     */
    public void remove(int id) {
        this.root = this.remove(getElementById(id), this.root);
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
        int compareValue = user.getId() - current.getElement().getId();

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

    public void preorder() {
        if (this.root.getElement().getClass().equals(User.class)){
            preorderUser(this.root);
        } else {
            preorder(this.root);
        }
    }

    /**
     * private preorder traversal method
     * @param root root object of the SplayTree instance
     */
    private void preorder(UserTreeNode root) {
        if (root != null) {
            System.out.println(root.getElement() + " ");
            preorder(root.getLeft());
            preorder(root.getRight());
        }
    }

    private void preorderUser(UserTreeNode root){
        if (root != null) {
            System.out.println(root.getElement().getName() + " ");
            preorderUser(root.getLeft());
            preorderUser(root.getRight());
        }
    }
}






















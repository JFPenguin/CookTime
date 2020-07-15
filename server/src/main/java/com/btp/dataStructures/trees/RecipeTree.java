package com.btp.dataStructures.trees;

import com.btp.dataStructures.nodes.RecipeNode;
import com.btp.serverData.clientObjects.Recipe;
import com.btp.serverData.clientObjects.User;
import com.btp.serverData.repos.UserRepo;

import java.util.ArrayList;

/**
 * the public class for the SplayTree instances. This code is based in the tutorial found in
 * https://www.geeksforgeeks.org/avl-tree-set-1-insertion/, with major syntax modifications
 * for using generic type nodes and getter/setter methods for data-scope reduction.
 */
public class RecipeTree{
    protected RecipeNode root;
    // List used to make searches
    private ArrayList<String> recipeList = new ArrayList<>();

    /**
     * Constructor for the class
     */
    public RecipeTree(){this.root = null;}

    /**
     * Checks if the tree is empty
     * @return boolean true if is empty, false if not
     */
    public boolean isEmpty(){
        return this.root == null;
    }

    /**
     * Checks if an element is inside the tree. Calls the private constructor method
     * @param id The Recipe id to be checked
     * @return boolean true if the element is contained, false if not
     */
    public boolean checkById(int id){
        return this.checkById(id, this.root);
    }

    /**
     * Checks if an element is inside the tree. Calls itself recursively
     * @param id The id to be checked
     * @param node The current node is checking
     * @return boolean true if the element is contained, false if not
     */
    private boolean checkById(int id, RecipeNode node){
        if (node == null){
            return false;
        } else {
            int compareValue = id - node.getElement().getId();

            if (compareValue < 0){
                return checkById(id, node.getLeft());
            } else if (compareValue > 0){
                return checkById(id, node.getRight());
            } else {
                return true;
            }
        }
    }

    /**
     * Gets a Recipe of the tree using its id. Calls the private getElementById method
     * @param id the id of the Recipe to be searched
     * @return the Recipe of that respective id
     */
    public Recipe getElementById(int id){
        return getElementById(id, this.root);
    }

    /**
     * Gets a Recipe of the tree using its id. Calls itself recursively
     * @param id the id of the user to be searched
     * @param node the current node is searching
     * @return the user of that respective id
     */
    private Recipe getElementById(int id, RecipeNode node){
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
     * Finds the minimum element in the tree.
     * Calls the recursive method findMin
     *
     * @return the smallest element found
     */
    public Recipe findMin() {
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
    private RecipeNode findMin(RecipeNode node){
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
    public Recipe findMax() {
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
    private RecipeNode findMax(RecipeNode node) {
        if (node != null) {
            while (node.getRight() != null) {
                node = node.getRight();
            }
        }
        return node;
    }

    /**
     * Method that finds the height of a node
     * @param N Node to get its height
     * @return int height of the Node
     */
    private int height(RecipeNode N){
        if (N == null){
            return 0;
        }
        return N.getHeight();
    }

    /**
     * Gets the max value of two integers
     * @param a int to be compared
     * @param b int to be compared
     * @return int with the max value
     */
    private int max(int a, int b){
        return (a > b) ? a : b;
    }

    /**
     * Makes a right Simple Rotation of the AVLTree data structure
     * @param y node to be rotated around
     * @return new node in that position after rotation
     */
    private RecipeNode rightRotate(RecipeNode y){
        RecipeNode x = y.getLeft();
        RecipeNode T2 = x.getRight();

        x.setRight(y);
        y.setLeft(T2);

        y.setHeight(max(height(y.getLeft()), height(y.getRight())) + 1);
        x.setHeight(max(height(x.getLeft()), height(x.getRight())) + 1);

        return x;
    }

    /**
     * Makes a left Simple Rotation of the AVLTree data structure
     * @param x node to be rotated around
     * @return new node in that position after rotation
     */
    private RecipeNode leftRotate(RecipeNode x){
        RecipeNode y = x.getRight();
        RecipeNode T2 = y.getLeft();

        y.setLeft(x);
        x.setRight(T2);

        x.setHeight(max(height(x.getLeft()), height(x.getRight())) + 1);
        y.setHeight(max(height(y.getLeft()), height(y.getRight())) + 1);

        return y;
    }

    /**
     * Gets the balance of a node
     * @param Node Node to check the balance of
     * @return the balance of the node
     */
    private int getBalance(RecipeNode Node){
        if (Node == null){
            return 0;
        }

        return height(Node.getLeft()) - height(Node.getRight());
    }

    /**
     * Calls the recursive method insert
     *
     * @param element the element to be inserted
     */
    public void insert(Recipe element) {
        this.root = this.insert(element, this.root);
    }

    /**
     * Recursive method that inserts a new node in the tree
     *
     * @param element the element to be inserted
     * @param current the current node being compared
     * @return the new TreeNode to be inserted
     */
    public RecipeNode insert(Recipe element, RecipeNode current){
        if (current == null){
            RecipeNode recipeNode = new RecipeNode();
            recipeNode.setElement(element);
            return recipeNode;
        }
        int compareValue = element.getId() - current.getElement().getId();

        if (compareValue < 0){
            current.setLeft(this.insert(element, current.getLeft()));
        } else if (compareValue > 0) {
            current.setRight(this.insert(element, current.getRight()));
        }

        current.setHeight(1 + max(height(current.getLeft()), height(current.getRight())));

        int balance = getBalance(current);

        // Left Left Case
        if (balance > 1 && element.getId() - current.getElement().getId() < 0){
            return leftRotate(current);
        }

        // Right Right Case
        if (balance < -1 && element.getId() - current.getElement().getId() > 0){
            return rightRotate(current);
        }

        // Left Right Case
        if (balance > 1 && element.getId() - current.getElement().getId() > 0) {
            current.setLeft(leftRotate(current.getLeft()));
            return rightRotate(current);
        }

        // Right Left Case
        if (balance < -1 && element.getId() - current.getElement().getId() < 0){
            current.setRight(rightRotate(current.getRight()));
            return leftRotate(current);
        }

        return current;
    }

    /**
     * Prints the tree using preOrder
     * @param node Current Node to be printed
     */
    public void preOrder(RecipeNode node) {
        if (node != null) {
            System.out.print(node.getElement().getName() + " ");
            preOrder(node.getLeft());
            preOrder(node.getRight());
        }
    }

    public ArrayList<String> searchByName(String data) {
        return searchByName(data.toLowerCase(), this.root);
    }

    private ArrayList<String> searchByName(String data, RecipeNode root){
        if (root != null && this.recipeList.size() < 5) {
            Recipe recipe = root.getElement();
            User recipeAuthor = UserRepo.getUser(recipe.getAuthorEmail());
            if (recipe.getName().toLowerCase().contains(data)){
                this.recipeList.add(recipeAuthor.isChef()+";"+recipe.getName()+";"+recipe.getId());
            }
            searchByName(data, root.getLeft());
            searchByName(data, root.getRight());
        }
        return this.recipeList;
    }

    /**
     * Calls the recursive method remove
     *
     * @param id the element to be removed
     */
    public void delete(int id){
        this.root = this.delete(id, this.root);
    }

    /**
     * Recursive method that removes a certain node
     * @param id the element that will be searched and deleted
     * @param current the current node being compared
     * @return The node that was checked to be processed recursively
     */
    private RecipeNode delete(int id, RecipeNode current) {

        if (current == null)
            return current;

        int compareValue = id - current.getElement().getId();

        if (compareValue < 0){
            current.setLeft(this.delete(id, current.getLeft()));
        } else if (compareValue > 0) {
            current.setRight(this.delete(id, current.getRight()));
        } else {
            if ((current.getLeft() == null) || (current.getRight() == null))
            {
                RecipeNode tmp = null;
                if (tmp == current.getLeft()) {
                    tmp = current.getRight();
                } else {

                    tmp = current.getLeft();
                }

                if (tmp == null) {
                    tmp = current;
                    current = null;
                } else
                    current = tmp;
            }
            else
            {
                RecipeNode tmp = findMin(current.getRight());

                current.setElement(tmp.getElement());

                current.setRight(delete(tmp.getElement().getId(), current.getRight()));
            }
        }
        if (current == null)
            return current;

        current.setHeight(max(height(current.getLeft()), height(current.getRight())) + 1);

        int balance = getBalance(current);

        // If this node becomes unbalanced, then there are 4 cases
        // Left Left Case
        if (balance > 1 && getBalance(current.getLeft()) >= 0)
            return rightRotate(current);

        // Left Right Case
        if (balance > 1 && getBalance(current.getLeft()) < 0)
        {
            current.setLeft(leftRotate(current.getLeft()));
            return rightRotate(current);
        }

        // Right Right Case
        if (balance < -1 && getBalance(current.getRight()) <= 0)
            return leftRotate(current);

        // Right Left Case
        if (balance < -1 && getBalance(current.getRight()) > 0)
        {
            current.setRight( rightRotate(current.getRight()));
            return leftRotate(current);
        }

        return current;
    }
}

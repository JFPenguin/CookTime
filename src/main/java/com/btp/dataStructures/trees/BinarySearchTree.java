package com.btp.dataStructures.trees;
import com.btp.dataStructures.nodes.SplayNode;
import com.btp.dataStructures.nodes.TreeNode;
import com.btp.serverData.User;

/**
 * A class that represents a Binary Tree
 * @param <T> A generic type
 */
public class BinarySearchTree<T extends Comparable<T>> {
    private TreeNode<T> root;

    /**
     * Constructor for the class
     */
    public BinarySearchTree() {
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
     * @param element The element to be checked
     * @return true if the element is contained, false if not
     */
    public boolean contains(T element) {
        return this.contains(element, this.root);
    }

    /**
     * Recursive method that actively checks if an element is in the tree
     *
     * @param element The element to be checked
     * @param node    The node on which the method starts checking
     * @return true if the element is contained, false if not
     */
    private boolean contains(T element, TreeNode<T> node) {
        if (node == null) {
            return false;
        } else {
            int compareValue = element.compareTo(node.getElement());

            if (compareValue < 0) {
                return contains(element, node.getLeft());
            } else if (compareValue > 0) {
                return contains(element, node.getRight());
            } else {
                return true;
            }
        }
    }

    public boolean checkById(int id){
        if(this.isEmpty()){
            return false;
        }
        else if (this.root.getElement().getClass().equals(User.class)){
            return checkById(id, (TreeNode<User>) this.root);

        } else {
            System.out.println("Tree is not made of User type");
            return false;
        }
    }

    private boolean checkById(int id, TreeNode<User> node){
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

    public T getElement(T element){
        return getElement(element, this.root);
    }

    private T getElement(T element, TreeNode<T> node){
        if (node == null){
            return null;
        }
        int compareValue = element.compareTo(node.getElement());

        if(compareValue < 0){
            return getElement(element, node.getLeft());
        } else if (compareValue > 0) {
            return getElement(element, node.getRight());
        } else {
            return node.getElement();
        }
    }

    public User getElementById(int id){
        if (this.root.getElement().getClass().equals(User.class)){
            return getElementById(id, (TreeNode<User>) this.root);
        } else {
            System.out.println("Tree is not made of User type");
            return null;
        }
    }

    private User getElementById(int id, TreeNode<User> node){
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
    public T findMin() {
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
    public TreeNode<T> findMin(TreeNode<T> node) {
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
    public T findMax() {
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
    public TreeNode<T> findMax(TreeNode<T> node) {
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
     * @param element the element to be inserted
     */
    public void insert(T element) {
        this.root = this.insert(element, this.root);
    }

    /**
     * Recursive method that inserts a new node in the tree
     *
     * @param element the element to be inserted
     * @param current the current node being compared
     * @return the new TreeNode to be inserted
     */
    private TreeNode<T> insert(T element, TreeNode<T> current) {
        if (current == null) {
            return new TreeNode<>(element, null, null);
        }

        int compareValue = element.compareTo(current.getElement());

        if (compareValue < 0) {
            current.setLeft(this.insert(element, current.getLeft()));
        } else if (compareValue > 0) {
            current.setRight(this.insert(element, current.getRight()));
        }
        return current;
    }

    /**
     * Calls the recursive method remove
     *
     * @param element the element to be removed
     */
    public void remove(T element) {
        this.root = this.remove(element, this.root);
    }

    /**
     * Recursive method that removes a certain node
     * @param element the element that will be searched and deleted
     * @param current the current node being compared
     * @return
     */
    private TreeNode<T> remove(T element, TreeNode<T> current) {
        if (current == null) {
            return current;
        }
        int compareValue = element.compareTo(current.getElement());

        if (compareValue < 0) {
            current.setLeft(this.remove(element, current.getLeft()));
        } else if (compareValue > 0) {
            current.setRight(this.remove(element, current.getRight()));
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
    public TreeNode<T> getRoot() {
        return root;
    }

    public void preorder() {
        if (this.root.getElement().getClass().equals(User.class)){
            preorderUser((TreeNode<User>) this.root);
        } else {
            preorder(this.root);
        }
    }

    /**
     * private preorder traversal method
     * @param root root object of the SplayTree instance
     */
    private void preorder(TreeNode<T> root) {
        if (root != null) {
            System.out.println(root.getElement() + " ");
            preorder(root.getLeft());
            preorder(root.getRight());
        }
    }

    private void preorderUser(TreeNode<User> root){
        if (root != null) {
            System.out.println(root.getElement().getName() + " ");
            preorderUser(root.getLeft());
            preorderUser(root.getRight());
        }
    }
}






















package com.btp.dataStructures.nodes;

/**
 * This class represents a node that forms part of a tree
 * @param <T> A generic type
 */
public class TreeNode<T extends Comparable<T>> {
    private T element;
    private TreeNode<T> left;
    private TreeNode<T> right;

    /**
     * The constructor of the method called externally.
     * It calls the private constructor.
     * @param element The element attribute that the node will contain
     */
    public TreeNode(T element) {
        this(element, null, null);
    }

    /**
     * The actual constructor for this class
     * @param element The element attribute that the node will contain
     * @param left The left child of the node
     * @param right The right child of the node
     */
    public TreeNode(T element, TreeNode<T> left, TreeNode<T> right) {
        this.element = element;
        this.left = left;
        this.right = right;
    }

    /**
     * Getter for the element attribute
     * @return the value the node is storing
     */
    public T getElement() {
        return element;
    }

    /**
     * Getter for the left child
     * @return the TreeNode pointed by the left pointer
     */
    public TreeNode<T> getLeft() {
        return left;
    }

    /**
     * Setter for the left child
     * @param left the TreeNode left child
     */
    public void setLeft(TreeNode<T> left) {
        this.left = left;
    }

    /**
     * Getter for the right child
     * @return the TreeNode pointed by the right pointer
     */
    public TreeNode<T> getRight() {
        return right;
    }

    /**
     * Setter for the right child
     * @param right the TreeNode right child
     */
    public void setRight(TreeNode<T> right) {
        this.right = right;
    }
}

package dataStructures.tree;
import dataStructures.nodes.TreeNode;

/**
 * A class that represents a Binary Tree
 * @param <T> A generic type
 */
public class BinaryTree<T extends Comparable<T>> {
    public TreeNode<T> root;

    /**
     * Constructor for the class
     */
    public BinaryTree() {
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
     * @param element The element to be checked
     * @return true if the element is contained, false if not
     */
    public boolean contains(T element) {
        return this.contains(element, this.root);
    }

    /**
     * Recursive method that actively checks if an element is in the tree
     * @param element The element to be checked
     * @param node The node on which the method starts checking
     * @return true if the element is contained, false if not
     */
    private boolean contains(T element, TreeNode<T> node) {
        if (node == null) {
            return false;
        }
        else {
            int compareValue = element.compareTo(node.getElement());

            if (compareValue < 0) {
                return contains(element, node.getLeft());
            }
            else if (compareValue > 0) {
                return contains(element, node.getRight());
            }
            else {
                return true;
            }
        }
    }

    /**
     * Finds the minimun element in the tree.
     * Calls the recursive method findMin
     * @return the smallest element found
     */
    public T findMin() {
        if (this.isEmpty()) {
            return null;
        }
        else {
            return this.findMin(this.root).getElement();
        }
    }

    /**
     * Recursive method that finds the minor element in the tree
     * @param node The node on which the method starts checking
     * @return The found min element
     */
    public TreeNode<T> findMin(TreeNode<T> node) {
        if (node == null) {
            return null;
        }
        else if (node.getLeft() == null) {
            return node;
        }
        else {
            return findMin(node.getLeft());
        }
    }

    /**
     * Finds the maximum element in the tree.
     * Calls the private method findMax
     * @return the biggest element found
     */
    public T findMax() {
        if (this.isEmpty()) {
            return null;
        }
        else {
            return this.findMax(this.root).getElement();
        }
    }

    /**
     * Method that finds the mayor element in the tree
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
     * Calls the recursive method
     * @param element the element to be inserted
     */
    public void insert(T element) {
        this.root = this.insert(element, this.root);
    }

    private TreeNode<T> insert(T element, TreeNode<T> current) {
        if (current == null) {
            return new TreeNode<T>(element, null, null);
        }

        int compareValue = element.compareTo(current.getElement());

        if (compareValue < 0) {
            current.setLeft(this.insert(element, current.getLeft()));
        }
        else if (compareValue > 0) {
            current.setRight(this.insert(element, current.getRight()));
        }
        return current;
    }
}






















package com.btp.dataStructures.trees;

import com.btp.dataStructures.nodes.AVLNode;

/**
 * the public class for the SplayTree instances. This code is based in the tutorial found in
 * https://www.geeksforgeeks.org/avl-tree-set-1-insertion/, with major syntax modifications
 * for using generic type nodes and getter/setter methods for data-scope reduction.
 * @param <T> generic type of objects to possibly contain in the tree instance
 */
public class AVLTree<T extends Comparable<T>>{
    protected AVLNode<T> root;

    public AVLTree(){this.root = null;}

    public boolean isEmpty(){
        return this.root == null;
    }

    public boolean contains(T element){
        return this.contains(element, this.root);
    }

    private boolean contains(T element, AVLNode<T> node){
        if (node == null){
            return false;
        } else {
            int compareValue = element.compareTo(node.getElement());

            if (compareValue < 0){
                return contains(element, node.getLeft());
            } else if (compareValue > 0){
                return contains(element, node.getRight());
            } else {
                return true;
            }
        }
    }

    public T findMin() {
        if (this.isEmpty()) {
            return null;
        } else {
            return this.findMin(this.root).getElement();
        }
    }

    private AVLNode<T> findMin(AVLNode<T> node){
        if (node != null){
            while (node.getLeft() != null){
                node = node.getRight();
            }
        }
        return node;
    }

    public T findMax() {
        if (this.isEmpty()) {
            return null;
        } else {
            return this.findMax(this.root).getElement();
        }
    }

    private AVLNode<T> findMax(AVLNode<T> node) {
        if (node != null) {
            while (node.getRight() != null) {
                node = node.getRight();
            }
        }
        return node;
    }

    private int height(AVLNode<T> N){
        if (N == null){
            return 0;
        }
        return N.getHeight();
    }

    private int max(int a, int b){
        return (a > b) ? a : b;
    }

    private AVLNode<T> rightRotate(AVLNode<T> y){
        AVLNode<T> x = y.getLeft();
        AVLNode<T> T2 = x.getRight();

        x.setRight(y);
        y.setLeft(T2);

        y.setHeight(max(height(y.getLeft()), height(y.getRight())) + 1);
        x.setHeight(max(height(x.getLeft()), height(x.getRight())) + 1);

        return x;
    }

    private AVLNode<T> leftRotate(AVLNode<T> x){
        AVLNode<T> y = x.getRight();
        AVLNode<T> T2 = y.getLeft();

        y.setLeft(x);
        x.setRight(T2);

        x.setHeight(max(height(x.getLeft()), height(x.getRight())) + 1);
        y.setHeight(max(height(y.getLeft()), height(y.getRight())) + 1);

        return y;
    }

    private int getBalance(AVLNode<T> Node){
        if (Node == null){
            return 0;
        }

        return height(Node.getLeft()) - height(Node.getRight());
    }

    public void insert(T element) {
        this.root = this.insert(element, this.root);
    }

    public AVLNode<T> insert(T element, AVLNode<T> current){
        if (current == null){
            return new AVLNode<T>(element);
        }

        int compareValue = element.compareTo(current.getElement());

        if (compareValue < 0){
            current.setLeft(this.insert(element, current.getLeft()));
        } else if (compareValue > 0) {
            current.setRight(this.insert(element, current.getRight()));
        }

        current.setHeight(1 + max(height(current.getLeft()), height(current.getRight())));

        int balance = getBalance(current);

        // Left Left Case
        if (balance > 1 && element.compareTo(current.getLeft().getElement()) < 0){
            return leftRotate(current);
        }

        // Right Right Case
        if (balance < -1 && element.compareTo(current.getRight().getElement()) > 0){
            return rightRotate(current);
        }

        // Left Right Case
        if (balance > 1 && element.compareTo(current.getRight().getElement()) > 0) {
            current.setLeft(leftRotate(current.getLeft()));
            return rightRotate(current);
        }

        // Right Left Case
        if (balance < -1 && element.compareTo(current.getLeft().getElement()) < 0){
            current.setRight(rightRotate(current.getRight()));
            return leftRotate(current);
        }

        return current;
    }

    public void preOrder(AVLNode<T> node) {
        if (node != null) {
            System.out.print(node.getElement() + " ");
            preOrder(node.getLeft());
            preOrder(node.getRight());
        }
    }

    public void delete(T element){
        this.root = this.delete(element, this.root);
    }

    private AVLNode delete(T element, AVLNode<T> current) {

        if (current == null)
            return current;

        int compareValue = element.compareTo(current.getElement());

        if (compareValue < 0){
            current.setLeft(this.delete(element, current.getLeft()));
        } else if (compareValue > 0) {
            current.setRight(this.delete(element, current.getRight()));
        } else {
            if ((current.getLeft() == null) || (current.getRight() == null))
            {
                AVLNode tmp = null;
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
                AVLNode tmp = findMin(current.getRight());

                current.setElement((T) tmp.getElement());

                current.setRight(delete((T) tmp.getElement(), current.getRight()));
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

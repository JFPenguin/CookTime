package com.btp.dataStructures.sorters;

import com.btp.dataStructures.lists.SinglyList;
import com.btp.dataStructures.nodes.Node;
import java.util.Arrays;

public class Sorter {

    /**
     * bubble sort method, takes an integer list and orders it using the bubble sort algorithm
     * @param numberList an Integer list
     */
    public static void bubbleSort(SinglyList<Integer> numberList) {
        Node<Integer> tmp;
        Node<Integer> next;
        while (!checkSorted(numberList)) {
            for (int i = 0; i < numberList.getLength() - 1; i++) {
                tmp = numberList.get(i);
                next = numberList.get(i + 1);
                if(tmp.getData() > next.getData()){
                    numberList.swap(tmp,next);
                }
            }
        }
    }

    /**
     * bubble sort method, takes an integer list and orders it using the insertion sort algorithm
     * @param numberList an Integer list
     */
    public static void insertionSort(SinglyList<Integer> numberList){
        int tmp;
        for (int i = 1; i < numberList.getLength(); i++) {
            tmp = numberList.get(i).getData();
            int j = i - 1;

            while(j >= 0 && numberList.get(j).getData() > tmp){
                numberList.swap(numberList.get(j), numberList.get(j+1));
                j--;
            }
        }

    }

//    public static void quickSort(SinglyList<Integer> numberList){
//        int length = numberList.getLength();
//        int tmp = numberList.get(length/2).getData();
//    }

    /**
     * main method for radixSort implementation. Takes an integer list and sorts it using
     * the Radix Sorting algorithm.
     * note:
     * This method is based on the tutorial recovered from https://www.geeksforgeeks.org/radix-sort/
     * with only minor implementation-related changes.
     * @param numberList the integer-containing instance that the user desires to sort.
     */
    public static void radixSort(SinglyList<Integer> numberList){
        int n = numberList.getLength();

        // Uses getMax method to get the max value in the list
        int m = getMax(numberList, n);

        // starts doing counting sort for every digit.
        // passes an exponent of 10 (10 ^i) with i being the current positional value.
        for (int exp = 1; m/exp > 0; exp *= 10){
            countSort(numberList, n , exp);
        }
    }

    /**
     * Method for the counting sorth algorithm, working over an unsorted list.
     * note:
     * This method is based on the tutorial recovered from https://www.geeksforgeeks.org/radix-sort/
     * with only minor implementation-related changes.
     * @param numberList list to be sorted according to the digit being watched by Radix.
     * @param n amount of elements in the list (length)
     * @param exponent current digit being sorted by the main algorithm (base 10)
     */
    private static void countSort(SinglyList<Integer> numberList, int n, int exponent){
        int output[] = new int[n]; //output array
        int i;
        int count[] = new int[10];
        Arrays.fill(count,0);

        // Store count of found values in count[]
        for (i = 0; i < n; i++) {
            count[ (numberList.get(i).getData()/exponent) % 10 ]++;
        }

        // Change count[i] so that it contains
        // an actual position of this digit in output array.
        for (i = 1; i < 10; i++){
            count[i] += count[i - 1];
        }

        // Build the output array
        for (i = n - 1; i >= 0; i--){
            output[ count[ (numberList.get(i).getData()/exponent) % 10] - 1 ] = numberList.get(i).getData();
            count [ (numberList.get(i).getData() / exponent) % 10]--;
        }

        // Copy the output array to the list, so that the list now contains
        // sorted numbers according to current digit
        for (i = 0; i < n; i++){
            numberList.get(i).setData(output[i]);
        }
    }

    /**
     * this method is used to get the highest value in a singly-linked list.
     * can be modified to use a different data structure by declaring a different type in the parameters.
     * @param numberList the specific singly-linked instance that the user desires to check.
     * @param n the number of elements (nodes) in the structure at the moment of calling the method. Also known as length.
     * @return integer value obtained from the list, corresponding to the largest number in it.
     */
    private static int getMax(SinglyList<Integer> numberList, int n){
        int mx = numberList.getHead().getData();
        for (int i = 1; i < n; i++) {
            if (numberList.get(i).getData() > mx){
                mx = numberList.get(i).getData();
            }
        }
        return mx;
    }

    /**
     * This method takes a list and returns a boolean value, true if the list is sorted, false otherwise.
     * @param list an integer list
     * @return boolean value, true if sorted, false if not
     */
    public static boolean checkSorted(SinglyList<Integer> list){
        boolean sorted = false;
        Node<Integer> tmp;
        Node<Integer> next;
        for (int i = 0; i < list.getLength()-1; i++) {
            tmp = list.get(i);
            next = list.get(i + 1);
            if(tmp.getData() > next.getData()){
                sorted = false;
                i = list.getLength();
            }
            else{
                sorted = true;
            }
        }
        return sorted;
    }

}
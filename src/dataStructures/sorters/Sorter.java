package dataStructures.sorters;

import dataStructures.lists.SinglyList;
import dataStructures.nodes.Node;

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
//
//    public static void radixSort(SinglyList<Integer> numberList){
//        int maxNum = getMax(numberList);
//        int numLen = numberLen(maxNum);
//    }

    /**
     * This method takes a list and returns a boolean value, true if the list is sorted, false if its not sorted
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

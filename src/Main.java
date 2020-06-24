import dataStructures.lists.SinglyList;
import dataStructures.sorters.Sorter;
import dataStructures.tree.BinaryTree;

public class Main {

    static SinglyList<Integer> numberList = new SinglyList<>();

    public static void main(String[] args) {
        BinaryTree<Integer> tree = new BinaryTree<>();

//        numberList.add(8);
//        numberList.add(2);
//        numberList.add(5);
//        numberList.add(4);
//        numberList.add(3);
//        numberList.add(2);
//        numberList.add(3);
//        numberList.add(4);
//        numberList.print();
//        Sorter.insertionSort(numberList);
//        //Sorter.bubbleSort(numberList);
//        numberList.print();

        tree.insert(1);
        tree.insert(2);
        tree.insert(3);
        tree.insert(4);
        tree.insert(5);
        tree.insert(6);
        tree.insert(7);
        tree.insert(8);
        tree.insert(9);
    }
}

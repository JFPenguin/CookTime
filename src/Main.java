import dataStructures.lists.SinglyList;
import dataStructures.sorters.Sorter;

public class Main {

    static SinglyList<Integer> numberList = new SinglyList<>();


    public static void main(String[] args) {

        numberList.add(8);
        numberList.add(2);
        numberList.add(5);
        numberList.add(4);
        numberList.add(3);
        numberList.add(2);
        numberList.add(3);
        numberList.add(4);
        numberList.print();
        //Sorter.insertSort(numberList);
        Sorter.bubbleSort(numberList);
        numberList.print();
        System.out.println("hola");


    }

}

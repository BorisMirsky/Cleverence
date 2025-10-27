using System;
using System.Collections.Generic;
using System.Diagnostics;


public class FindDuplicates
{
    public static Boolean FindDuplicatesHashSet<T>(T[] array)
    {
        HashSet<T> elements = new HashSet<T>();
        Boolean result = false;
        foreach (T element in array)
        {
            if (!elements.Add(element))
            {
                result = true;
                break;
            }
        }
        return result;
    }

    public static void Main(string[] args)
    {
        int[] numbers = { 1, 2, 3, 4, 2, 5, 6, 1, 7, 8, 3 };     // true
        int[] numbers1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };          // false
        Boolean result = FindDuplicatesHashSet(numbers1);
        Debug.WriteLine(result.ToString());
    }
}






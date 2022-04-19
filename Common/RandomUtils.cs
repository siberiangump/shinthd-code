using UnityEngine;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;


public class RandomUtils
{
    public static T RandomExclude<T>(List<T> targetArray, List<T> exclude)
    {
        List<T> list = new List<T>();
        foreach (var item in targetArray)
        {
            if (exclude.Contains(item))
                continue;
            list.Add(item);
        }
        return list[Random.Range(0, list.Count)];
    }

    public static T RandomExclude<T>(T[] targetArray, List<T> exclude)
    {
        List<T> list = new List<T>();
        foreach (var item in targetArray)
        {
            if (exclude.Contains(item))
                continue;
            list.Add(item);
        }
        return list[Random.Range(0, list.Count)];
    }

    public static T[] RandomSetAutoInclude<T>(in T[] targetArray, in T[] autoIncludeInResult, int amount)
    {
        bool[] initialTakenMap = new bool[targetArray.Length];
        foreach (var item in autoIncludeInResult)
        {
            int index = Array.IndexOf(targetArray, item);
            if (index == -1)
                Debug.LogError("NoSuchItem");
            initialTakenMap[index] = true;
        }
        int[] indexes = RandomIndexSet(targetArray.Length, amount, initialTakenMap, true);
        T[] result = new T[amount];
        for (int i = 0; i < result.Length; i++)
            result[i] = targetArray[indexes[i]];
        return result;
    }

    public static T[] RandomSetExclude<T>(in T[] targetArray, in T[] excludeValues, int amount)
    {
        bool[] initialTakenMap = new bool[targetArray.Length];
        foreach (var item in excludeValues)
        {
            int index = Array.IndexOf(targetArray, item);
            if (index == -1)
                Debug.LogError("NoSuchItem");
            initialTakenMap[index] = true;
        }
        int[] indexes = RandomIndexSet(targetArray.Length, amount, initialTakenMap);
        T[] result = new T[amount];
        for (int i = 0; i < result.Length; i++)
            result[i] = targetArray[indexes[i]];
        return result;
    }

    public static T[] RandomSet<T>(in T[] targetArray, int amount)
    {
        bool[] initialTakenMap = new bool[targetArray.Length];
        int[] indexes = RandomIndexSet(targetArray.Length, amount);
        T[] result = new T[amount];
        for (int i = 0; i < result.Length; i++)
            result[i] = targetArray[indexes[i]];
        return result;
    }

    public static int[] RandomIndexSet(int arrayLength, int resultAmount, bool[] initialTakenMap = null, bool autoIncludeInitialTakenMap = false)
    {
        if (resultAmount > arrayLength)
            return new int[0];

        if (resultAmount == arrayLength)
            return GiveAllElementAsArray(arrayLength);

        int[] result = new int[resultAmount];
        int startFromIndex = 0;

        if (autoIncludeInitialTakenMap && initialTakenMap != null)
            for (int i = 0; i < initialTakenMap.Length; i++)
                if (initialTakenMap[i]) { result[startFromIndex] = i; startFromIndex++; }

        bool[] takenMap = initialTakenMap ?? new bool[arrayLength];

        for (int i = startFromIndex; i < resultAmount; i++)
        {
            int randomIndex = Random.Range(0, arrayLength-i);
            randomIndex = ModifyIndexByTakenMap(randomIndex);
            takenMap[randomIndex] = true;
            result[i] = randomIndex;
        }

        return result;

        // support methods
        int ModifyIndexByTakenMap(int index)
        {
            int avaliableInterations = takenMap.Length;
            while (takenMap[index])
            {
                index++;
                if (index >= takenMap.Length - 1)
                    index = 0;
                avaliableInterations--;
                if (avaliableInterations <= 0)
                    return -1;
            }
            return index;
        }

        int[] GiveAllElementAsArray(int length)
        {
            int[] resultArray = new int[length];
            for (int i = 0; i < length; i++)
                resultArray[i] = i;
            return resultArray;
        }
    }


    public List<int[]> RundomIndexesInArraySet(int[] arrayLength, int amount)
    {
        int[] candidatesPool = CreateCandidatePool();

        int[] randomFromPool = new int[0];
        for (int i = 0; i < randomFromPool.Length; i++)
            randomFromPool[i] = -1;

        for (int i = 0; i < amount; i++)
        {
            int[] uniqueCandidates = ExcludeFromArray(candidatesPool, randomFromPool);
            randomFromPool[i] = uniqueCandidates[Random.Range(0, uniqueCandidates.Length)];
        }

        List<int[]> resultIndexes = new List<int[]>();
        for (int i = 0; i < arrayLength.Length; i++)
        {
            int length = arrayLength[i];
            int displace = i > 0 ? arrayLength[i - 1] : 0;
            resultIndexes.Add(AllBelowLength(randomFromPool, length, displace));
        }

        return resultIndexes;

        int[] CreateCandidatePool()
        {
            int sum = 0;
            for (int i = 0; i < arrayLength.Length; i++)
                sum += arrayLength[i];
            int[] result = new int[sum];
            for (int i = 0; i < result.Length; i++)
                result[i] = i;
            return result;
        }

        int[] ExcludeFromArray(in int[] from, in int[] what)
        {
            if (what.Length == 0)
                return from;
            int excludeAmount = 0;
            for (int i = 0; i < what.Length; i++)
                if (what[i] != -1)
                    excludeAmount++;

            int[] result = new int[from.Length - excludeAmount];
            int resultIndex = 0;
            for (int i = 0; i < from.Length; i++)
            {
                if (Contains(what, from[i]))
                    continue;
                result[resultIndex++] = from[i];
            }
            return result;
        }

        bool Contains(in int[] targetArray, in int element)
        {
            for (int i = 0; i < targetArray.Length; i++)
                if (targetArray[i] == element)
                    return true;
            return false;
        }

        int[] AllBelowLength(in int[] targetArray, in int length, in int displace)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < targetArray.Length; i++)
            {
                if (targetArray[i] >= length && targetArray[i] < displace)
                    continue;
                result.Add(targetArray[i] - displace);
            }
            return result.ToArray();
        }
    }
}

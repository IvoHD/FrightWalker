using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsRand
{
    private static MathsRand instance = null;
    private MathsRand() { } // private constructor to prevent instantiation from outside

    public static MathsRand Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MathsRand();
            }
            return instance;
        }
    }

    public int RandNumOutOfRange(int min, int max)
    {
        return UnityEngine.Random.Range(min, max + 1);
    }
    public bool Chance(int rare)
    {
        if (1 == RandNumOutOfRange(1, rare)) return true;
        else return false;
    }
}
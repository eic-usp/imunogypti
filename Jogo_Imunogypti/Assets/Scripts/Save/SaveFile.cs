using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class SaveFile
{
    [SerializeField] private const int nStages = 10;
    public bool[] stagesWon = new bool[nStages];
    public int[] stars = new int[nStages];

    public SaveFile()
    {
        for(int i = 0; i < nStages; i++)
        {
            stagesWon[i] = false;
            stars[i] = 0;
        }
    }
}

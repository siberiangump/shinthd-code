using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField] Spot[] Spots;
    [SerializeField] FieldSide Side;

    private void Awake()
    {
        Init();
    }

    public void Init() 
    {
        for (int i = 0; i < Spots.Length; i++)
            Spots[i].Init();
    }

    public Spot[] GetSpots() => Spots;
    public FieldSide GetSide() => Side;
    public Spot GetSpot(int index) => Spots[index];

    public ISelectIntentTarget[] GetSelectTargets() // pattern and params
    {
        ISelectIntentTarget[] selectIntentTargets = new ISelectIntentTarget[Spots.Length];
        for (int i = 0; i < Spots.Length; i++)
            selectIntentTargets[i] = Spots[i];
        return selectIntentTargets;
    }

    public ISelectIntentTarget[] GetSelectTargets(int[] indexes) // pattern and params
    {
        ISelectIntentTarget[] selectIntentTargets = new ISelectIntentTarget[indexes.Length];
        int index = 0;
		for (int i = 0; i < indexes.Length; i++)
		    selectIntentTargets[i] = Spots[indexes[i]];
        return selectIntentTargets;
    }

    public CharacterView[] GetCharacters(int[] indexes)
    {
        CharacterView[] selectIntentTargets = new CharacterView[indexes.Length];
        int index = 0;
        for (int i = 0; i < Spots.Length; i++)
            if (indexes.Contains(i))
                selectIntentTargets[index++] = Spots[i].GetCheracter();
        return selectIntentTargets;
    }

    public int GetSpotIndex(Spot spot)
    {
        for (int i = 0; i < Spots.Length; i++)
            if (Spots[i] == spot)
                return i;
        return -1;
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clock : MonoBehaviour
{
    [SerializeField] List<ITickable> Tickables = new List<ITickable>();

    public void Add(ITickable tickable)
    {
        if (!Tickables.Contains(tickable))
            Tickables.Add(tickable);
    }

    private void Update()
    {
        for (int i = 0; i < Tickables.Count; i++)
            Tickables[i].Tick();
    }
}

public interface ITickable
{
    void Tick();
}

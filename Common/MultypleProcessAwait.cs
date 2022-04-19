using System.Collections.ObjectModel;
using System;

public class MultipleProcessAwait
{
    private Collection<ushort> WaitingFor;
    private bool InWait;
    private Action Callback;

    public MultipleProcessAwait(Action callback)
    {
        WaitingFor = new Collection<ushort>();
        InWait = false;
        Callback = callback;
    }

    public void AddProcess(ushort id)
    {
        if (InWait)
            return;
        if (!WaitingFor.Contains(id))
            WaitingFor.Add(id);
    }

    public void ProcessComplite(ushort id)
    {
        if (WaitingFor.Contains(id))
            WaitingFor.Remove(id);
        if (InWait)
            CheckAllProcessesDone();
    }

    public void StartWaiting()
    {
        InWait = true;
        CheckAllProcessesDone();
    }

    private void CheckAllProcessesDone()
    {
        if (WaitingFor.Count <= 0)
        {
            if(Callback != null)
                Callback();
            Callback = null;
        }
    }
}

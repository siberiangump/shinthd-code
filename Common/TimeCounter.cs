using DG.Tweening;
using System;
using System.Threading;
using System.Threading.Tasks;

public static class TimeCounter
{
    public static void Wait(float time, Action callback) 
    {
        DOVirtual.DelayedCall(time, () => callback());
    }
}

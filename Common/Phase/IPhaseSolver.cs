using UnityEngine;
using System.Collections;

public interface IPhaseSolver<T>
{
    T GetNext(T phase);
}

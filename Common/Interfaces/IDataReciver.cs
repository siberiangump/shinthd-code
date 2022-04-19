using UnityEngine;
using System.Collections;

public interface IDataReceiver<Data>
{
    Data DataObject { set; }
}

public interface IDataProvider<Data>
{
    Data DataObject { get; }
}
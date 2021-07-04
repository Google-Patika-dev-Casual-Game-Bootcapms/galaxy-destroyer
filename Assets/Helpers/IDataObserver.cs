using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataObserver<T>
{
    void Subscribe(IDataObservable<T> observable);
    void OnNotify();
}

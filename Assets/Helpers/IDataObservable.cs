using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataObservable<T>
{
    public void Attach(IDataObserver<T> observer);
    public void Detach(IDataObserver<T> observer);
    public void Notify();
}

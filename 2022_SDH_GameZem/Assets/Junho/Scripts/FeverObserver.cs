using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FeverObserver
{
    void ResisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObserver();
}
public interface IObserver
{
    void ColorChange();
}

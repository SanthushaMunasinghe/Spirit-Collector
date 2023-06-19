using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> _observers = new List<IObserver>();

    public void AttachObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void DetachObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    protected void NotifyObservers(int amount, ScoreType type)
    {
        _observers.ForEach((_observer) =>
        {
            _observer.OnNotify(amount, type);
        });
    }
}

public interface IObserver
{
    public void OnNotify(int amount, ScoreType type);
}

public enum ScoreType
{
    Score,
    DarkSpirit,
    LightSpirit,
}

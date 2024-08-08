using System.Collections.Generic;

public class Subject : ISubject
{
    private List<IObserver> observers = new List<IObserver>();

    public void RegisterObserver(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void UnregisterObserver(IObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public void NotifyObservers(object eventData)
    {
        foreach (var observer in observers)
        {
            observer.OnNotify(this, eventData);
        }
    }
}
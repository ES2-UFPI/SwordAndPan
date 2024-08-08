using System.Diagnostics;
using UnityEngine;

public class Observer : MonoBehaviour, IObserver
{
    public void OnNotify(object sender, object eventData)
    {
        // Handle the event
        UnityEngine.Debug.Log($"Received event from {sender} with data: {eventData}");
    }
}
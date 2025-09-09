using UnityEngine;
using System;

public class DestroyEventTrigger : MonoBehaviour
{
    public event Action OnDestroyed;

    private void OnDestroy()
    {
        OnDestroyed?.Invoke();
    }
}
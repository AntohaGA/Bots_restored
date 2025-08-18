using System;
using UnityEngine;

public class BotWithBoxDetector : MonoBehaviour
{
    public event Action<Bot> BotReceived;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bot bot) && bot.WithBox)
        {
            BotReceived?.Invoke(bot);
        }
    }
}
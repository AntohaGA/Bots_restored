using System;
using UnityEngine;

public class BotDetector : MonoBehaviour
{
    public event Action<Bot> BotReceived;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bot bot))
        {
            if (bot.BoxHandler.WithBox)
            {
                BotReceived?.Invoke(bot);
            }
        }
    }
}
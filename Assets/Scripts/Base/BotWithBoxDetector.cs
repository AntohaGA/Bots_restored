using System;
using System.Collections.Generic;
using UnityEngine;

public class BotWithBoxDetector : MonoBehaviour
{
    public event Action<Bot> OurBotReceived;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bot bot))
        {
            if (bot.BoxLifter.WithBox)
            {
                OurBotReceived?.Invoke(bot);
            }
        }
    }
}
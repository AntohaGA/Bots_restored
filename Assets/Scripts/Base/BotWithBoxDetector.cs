using System;
using System.Collections.Generic;
using UnityEngine;

public class BotWithBoxDetector : MonoBehaviour
{
    private List<Bot> _bots;

    public event Action<Bot> BotReceived;

    public void Init(List<Bot> bots)
    {
        _bots = bots;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bot bot))
        {
            if (bot.BoxHandler.WithBox)
            {
                if (IsOurBot(bot))
                {
                    BotReceived?.Invoke(bot);
                }
            }
        }
    }

    private bool IsOurBot(Bot bot)
    {
        if (_bots.Contains(bot))
        {
            return true;
        }

        return false;
    }
}
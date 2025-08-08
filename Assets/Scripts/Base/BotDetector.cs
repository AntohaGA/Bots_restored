using System;
using UnityEngine;

public class BotDetector : MonoBehaviour
{
    [SerializeField] BotCreator _botCreator;

    public event Action<Bot> BotReceived;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bot bot))
        {
            if (bot.BoxHandler.WithBox)
            {
                if (IsBotOur(bot))
                {
                    BotReceived?.Invoke(bot);
                }
            }
        }
    }

    private bool IsBotOur(Bot bot)
    {
        if (_botCreator.IsOurBot(bot))
        {
            return true;
        }

        return false;
    }
}
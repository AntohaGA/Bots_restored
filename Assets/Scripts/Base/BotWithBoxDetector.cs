using System;
using UnityEngine;

public class BotWithBoxDetector : MonoBehaviour
{
    private BotKeeper _botKeeper;

    public event Action<Bot> OurBotReceived;

    public void Init(BotKeeper botKeeper)
    {
        _botKeeper = botKeeper;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bot bot) && _botKeeper.IsBotWithBox(bot) )
        {
            OurBotReceived?.Invoke(bot);          
        }
    }
}
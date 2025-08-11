using System;
using UnityEngine;

public class BotWithBoxDetector : MonoBehaviour
{
    [SerializeField] private BotKeeper _botKeeper;

    public event Action<Bot> OurBotReceived;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Bot bot) && _botKeeper.IsBotWithBox(bot) )
        {
            OurBotReceived?.Invoke(bot);          
        }
    }
}
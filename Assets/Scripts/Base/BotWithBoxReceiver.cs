using System;
using UnityEngine;

[RequireComponent(typeof(BotWithBoxDetector))]
public class BotWithBoxReceiver : MonoBehaviour
{
    private BotWithBoxDetector _detectorBotsWithBox;

    public event Action<Box> BoxReceived;

    private void Awake()
    {
        _detectorBotsWithBox = GetComponent<BotWithBoxDetector>();
    }

    private void OnEnable()
    {
        _detectorBotsWithBox.BotReceived += HandleBotWithBox;
    }

    private void OnDisable()
    {
        _detectorBotsWithBox.BotReceived -= HandleBotWithBox;
    }

    private void HandleBotWithBox(Bot bot)
    {
        if (bot == null)
            return;

        if (bot.Box != null)
            BoxReceived?.Invoke(bot.Box);

        bot.MadeFree();
    }
}
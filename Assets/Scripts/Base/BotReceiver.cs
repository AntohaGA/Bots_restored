using UnityEngine;

[RequireComponent(typeof(BotDetector))]
public class BotReceiver : MonoBehaviour
{
    private BaseStorage _boxStorage;
    private BotDetector _botDetector;

    private void Awake()
    {
        _botDetector = GetComponent<BotDetector>();
    }

    private void OnEnable()
    {
        _botDetector.BotReceived += HandleBotWithBox;
    }

    private void OnDisable()
    {
        _botDetector.BotReceived -= HandleBotWithBox;
    }

    public void Init(BaseStorage boxStorage)
    {
        _boxStorage = boxStorage;
    }

    private void HandleBotWithBox(Bot bot)
    {
        if (bot == null)
            return;

        if (bot.Box != null)
            _boxStorage.AddBoxOnBase(bot.Box);

        bot.MadeFree();
    }
}
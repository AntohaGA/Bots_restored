using UnityEngine;

[RequireComponent(typeof(BotDetector))]
public class BotReceiver : MonoBehaviour
{
    private BaseStorage _boxStorage;
    private BotDetector _botDetector;
    private BotCreator _botCreator;

    private void Awake()
    {
        _botDetector = GetComponent<BotDetector>();
    }

    private void OnEnable()
    {
        _botDetector.BotReceived += CheckBotOurOrNot;
    }

    private void OnDisable()
    {
        _botDetector.BotReceived -= CheckBotOurOrNot;
    }

    public void Init(BaseStorage boxStorage, BotCreator botCreator)
    {
        _boxStorage = boxStorage;
        _botCreator = botCreator;
    }

    private void CheckBotOurOrNot(Bot bot)
    {
        if (_botCreator.CheckBot(bot))
        {
            HandleBotWithBox(bot);
        }
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
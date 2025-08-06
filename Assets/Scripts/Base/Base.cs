using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BotDetector))]
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BotCreator))]
[RequireComponent(typeof(ClickBaseDetector))]
[RequireComponent(typeof(MapFlagPlacer))]
[RequireComponent(typeof(BoxStorage))]
public class Base : MonoBehaviour
{
    [SerializeField] private Transform _pointOut;
    [SerializeField] private Transform _pointIn;
    [SerializeField] private BoxKeeper _boxKeeper;

    private BotDetector _botDetector;
    private BotCreator _botCreator;
    private MapFlagPlacer _mapFlagPlacer;
    private BoxStorage _boxStorage;

    public Vector3 GetPointIn() => _pointIn.position;
    public Vector3 GetPointOut() => _pointOut.position;

    private void Awake()
    {
        _botCreator = GetComponent<BotCreator>();
        _botDetector = GetComponent<BotDetector>();
        _mapFlagPlacer = GetComponent<MapFlagPlacer>();
        _boxStorage = GetComponent<BoxStorage>();
    }

    private void OnEnable()
    {
        _botDetector.BotReceived += TakeBotWithBox;
        _boxKeeper.OfferedClosestBox += TryAssignBot;
        _mapFlagPlacer.FlagPlaced += TryBildNewBase;
    }

    private void OnDisable()
    {
        _botDetector.BotReceived -= TakeBotWithBox;
        _boxKeeper.OfferedClosestBox -= TryAssignBot;
        _mapFlagPlacer.FlagPlaced -= TryBildNewBase;
    }

    public void TakeBotWithBox(Bot bot)
    {
        if (bot == null)
            return;

        _boxStorage.AddBoxOnBase(bot.Box);
        bot.MadeFree();
    }

    private void TryAssignBot(Box box)
    {
        Bot bot = _botCreator.TryGetFreeBot(GetPointOut());

        if (bot != null)
        {
           _boxStorage.ReserveBox(box);
            bot.BringBox(box, GetPointIn());
        }
    }

    private void TryBildNewBase(Vector3 position)
    {

    }
}
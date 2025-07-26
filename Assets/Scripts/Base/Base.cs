using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BotDetector))]
[RequireComponent(typeof(NavMeshObstacle))]
public class Base : MonoBehaviour
{
    [SerializeField] private Bot _botPrefab;
    [SerializeField] private PoolBots _poolBots;
    [SerializeField] private Transform _pointOut;
    [SerializeField] private Transform _pointIn;
    [SerializeField] private ResourceScanner _boxScanner;

    private BotDetector _botReceiver;

    private BotReceiver _botManager;
    private BoxReceiver _boxManager;

    private int _countMaxBots = 5;

    private void Awake()
    {
        _botReceiver = GetComponent<BotDetector>();
        _botManager = new BotReceiver(_poolBots, _countMaxBots);
        _botManager.Init(_botPrefab);
        _boxManager = new BoxReceiver(_boxScanner);
    }

    private void OnEnable()
    {
        _botReceiver.BotReceived += TakeBot;
        _boxScanner.OnBoxFound += HandleBoxFound;
    }

    private void OnDisable()
    {
        _botReceiver.BotReceived -= TakeBot;
        _boxScanner.OnBoxFound -= HandleBoxFound;
    }

    private void Start()
    {
        StartCoroutine(_boxScanner.ScanRoutine());
    }

    private void HandleBoxFound(Box box)
    {
        if (box != null)
        {
            AssignBot(box);
        }
    }

    public void TakeBot(Bot bot)
    {
        if (bot == null)
            return;

        _boxManager.TakeBox(bot.Box);
        _botManager.TakeBot(bot);
    }

    public Vector3 GetPointIn() => _pointIn.position;

    public Vector3 GetPointOut() => _pointOut.position;

    private void AssignBot(Box box)
    {
        Bot bot = _botManager.GetBot();
        if (bot != null)
        {
            bot.Init(this);
            box.IsTaken = true;
            bot.BringBox(box);
        }
    }
}
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BotDetector))]
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BotCreator))]
public class Base : MonoBehaviour
{
    [SerializeField] private Transform _pointOut;
    [SerializeField] private Transform _pointIn;
    [SerializeField] private BoxFounder _boxFounder;

    private BotDetector _botDetector;
    private BotCreator _botCreator;

    public Vector3 GetPointIn() => _pointIn.position;
    public Vector3 GetPointOut() => _pointOut.position;


    private void Awake()
    {
        _botCreator = GetComponent<BotCreator>();
        _botDetector = GetComponent<BotDetector>();
    }

    private void OnEnable()
    {
        _botDetector.BotReceived += TakeBotWithBox;
        _boxFounder.OfferedClosestBox += TryAssignBot;
    }

    private void OnDisable()
    {
        _botDetector.BotReceived -= TakeBotWithBox;
        _boxFounder.OfferedClosestBox -= TryAssignBot;
    }

    private void Start()
    {
        StartCoroutine(_boxFounder.ScanRoutine());
    }

    public void TakeBotWithBox(Bot bot)
    {
        if (bot == null)
            return;

        _boxFounder.ReturnBox(bot.Box);
        bot.MadeFree();
    }

    private void TryAssignBot(Box box)
    {
        Bot bot = _botCreator.TryGetFreeBot(GetPointOut());

        if (bot != null)
        {
            _boxFounder.SetBoxReserved(box);
            bot.BringBox(box, GetPointIn());
        }
    }
}
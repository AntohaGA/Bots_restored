using UnityEngine;

[RequireComponent(typeof(BotWithBoxDetector))]
public class BaseStorage : MonoBehaviour
{


    private BotWithBoxDetector _botWithBoxDetector;
    private int _countBoxes = 0;

    private void Awake()
    {
        _botWithBoxDetector = GetComponent<BotWithBoxDetector>();
    }

    private void OnEnable()
    {
        _botWithBoxDetector.OurBotReceived += AddBoxOnBase;
    }

    private void OnDisable()
    {
        _botWithBoxDetector.OurBotReceived -= AddBoxOnBase;
    }

    public bool GetBoxes(int count)
    {
        if(_countBoxes >= count)
        {
            _countBoxes -= count;

            return true;
        }

        return false;
    }

    private void AddBoxOnBase(Bot bot)
    {
          bot.ReleaseBox();
          _countBoxes++;
    }
}
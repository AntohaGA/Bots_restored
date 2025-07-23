using System.Collections;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private Bot _bot;
    [SerializeField] private PoolBoxes _poolBoxes;
    [SerializeField] private PoolBots _poolBots;
    [SerializeField] private float _scanInterval;
    [SerializeField] private Transform _pointOut;
    [SerializeField] private Transform _pointIn;

    private int _countMaxBots = 3;
    private int _countBoxes = 0;

    private void Awake()
    {
        _poolBots.Init(_bot);
    }

    private void Start()
    {
        StartCoroutine(ScanRoutine());
    }

    private IEnumerator ScanRoutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(_scanInterval);

            Box box = FindNearestBox();

            if (box != null)
            {
                AssignBots(box);
            }
        }
    }

    public void TakeBox(Box box)
    {
        if (box == null)
            return;

        box.IsTaken = false;
        box.transform.SetParent(null);

        Rigidbody rb = box.GetComponent<Rigidbody>();

        if (rb != null)
            rb.isKinematic = false;

        _countBoxes++;
        Debug.Log("box count" + _countBoxes);
        _poolBoxes.ReturnInstance(box);
    }

    public void TakeBot(Bot bot)
    {
        _poolBots.ReturnInstance(bot);
    }

    public Vector3 GetPointIn()
    {
        return _pointIn.position;
    }

    public Vector3 GetPointOut()
    {
        return _pointOut.position;
    }

    private void AssignBots(Box box)
    {
        Bot bot = GetBot();

        if (bot != null)
        {
            bot.Init(this);
            box.IsTaken = true;
            bot.BringBox(box);

            return;
        }
    }

    private Box FindNearestBox()
    {
        Box closestBox = null;
        float minDist = float.MaxValue;

        foreach (Box box in _poolBoxes)
        {
            if (box.IsTaken == false)
            {
                float distant = Vector3.Distance(transform.position, box.transform.position);

                if (distant < minDist)
                {
                    closestBox = box;
                    minDist = distant;
                }
            }
        }

        return closestBox;
    }

    private Bot GetBot()
    {
        if (_poolBots.ActiveCount < _countMaxBots)
        {
            return _poolBots.GetInstance();
        }

        return null;
    }
}
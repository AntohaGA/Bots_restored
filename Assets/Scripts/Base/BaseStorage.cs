using UnityEngine;

[RequireComponent(typeof(BotWithBoxReceiver))]
public class BaseStorage : MonoBehaviour
{
    private BotWithBoxReceiver _botWithBoxReceiver;

    private int _countBoxes = 0;

    private void Awake()
    {
        _botWithBoxReceiver = GetComponent<BotWithBoxReceiver>();
    }

    private void OnEnable()
    {
        _botWithBoxReceiver.BoxReceived += AddBoxOnBase;
    }

    private void OnDisable()
    {
        _botWithBoxReceiver.BoxReceived -= AddBoxOnBase;
    }

    public void AddBoxOnBase(Box box)
    {
        if (box == null)
            return;

        box.transform.SetParent(null);
        box.SetRigidBodyKinematic(false);
        box.Return();
        _countBoxes++;
    }
}
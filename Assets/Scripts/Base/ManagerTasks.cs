using System.Collections;
using UnityEngine;

public class ManagerTasks : MonoBehaviour
{
    [SerializeField] private BotKeeper _botKeeper;
    [SerializeField] private BoxKeeper _boxKeeper;
    [SerializeField] private Base _prefabBase;

    private CreatorTasksBringBox _creatorTasksBringBox;

    private bool _isBotCreating = true;

    private void Awake()
    {
        _creatorTasksBringBox = new CreatorTasksBringBox(_boxKeeper, transform);
    }

    public IEnumerator DoTasks()
    {
        while (enabled)
        {
            if (_isBotCreating)
            {
                if (_botKeeper.GetFree(out Bot bot) && _creatorTasksBringBox.CreateTask(out ITaskable _task))
                {
                    bot.DoJob(_task);
                }
            }
            else
            {

            }


            yield return new WaitForSeconds(0.3f);
        }
    }
}
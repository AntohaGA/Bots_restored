using System.Collections;
using UnityEngine;

public class ManagerTasks : MonoBehaviour
{
    private BotKeeper _botKeeper;
    private BoxKeeper _boxKeeper;
    private CreatorTasksBringBox _creatorTasksBringBox;
    private CreatorTasksBuildBase _creatorTasksBuildBase;

    private bool _isBotCreating = true;

    public void Init(BotKeeper botKeeper, BoxKeeper boxKeeper)
    {
        _botKeeper = botKeeper;
        _boxKeeper = boxKeeper;
        _creatorTasksBringBox = new CreatorTasksBringBox(_boxKeeper, transform);
        _creatorTasksBuildBase = new CreatorTasksBuildBase();
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
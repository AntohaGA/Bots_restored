using System.Collections;

public interface ITaskable
{
    public IEnumerator Do(Bot bot);
}
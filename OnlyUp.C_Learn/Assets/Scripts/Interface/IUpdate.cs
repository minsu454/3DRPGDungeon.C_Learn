public interface IUpdate
{
    public int depth { get; }

    void Connect();
    void OnUpdate();
    void OnFixedUpdate();
}

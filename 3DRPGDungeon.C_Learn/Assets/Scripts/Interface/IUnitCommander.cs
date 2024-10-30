using System;

/// <summary>
/// Unit에 커맨더 인터페이스
/// </summary>
public interface IUnitCommander
{
    /// <summary>
    /// update Event
    /// </summary>
    public event Action UpdateEvent;

    /// <summary>
    /// FixedUpdate Event
    /// </summary>
    public event Action FixedUpdateEvent;

    /// <summary>
    /// LateUpdate Event
    /// </summary>
    public event Action LateUpdateEvent;
}


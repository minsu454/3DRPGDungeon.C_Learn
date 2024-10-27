using System;

public interface IUnitCommander
{
    public event Action UpdateEvent;
    public event Action FixedUpdateEvent;
    public event Action LateUpdateEvent;
}


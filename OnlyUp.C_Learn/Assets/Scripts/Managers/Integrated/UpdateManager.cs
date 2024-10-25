using System;
using UnityEngine;
using System.Collections.Generic;

public sealed class UpdateManager : IManager
{
    private readonly SortedDictionary<int, List<Action>> updateEventSortedDic = new SortedDictionary<int, List<Action>>();
    private readonly SortedDictionary<int, List<Action>> fixedupdateEventSortedDic = new SortedDictionary<int, List<Action>>();

    public void Init()
    {
        
    }

    public void AddUpdateEvent(int depth, Action onUpdate)
    {
        if (!updateEventSortedDic.TryGetValue(depth, out var list))
        {
            list = new List<Action>();
            updateEventSortedDic[depth] = list;
        }

        list.Add(onUpdate);
    }

    public void AddFixedUpdateEvent(int depth, Action onFixedUpdate)
    {
        if (!fixedupdateEventSortedDic.TryGetValue(depth, out var list))
        {
            list = new List<Action>();
            fixedupdateEventSortedDic[depth] = list;
        }

        list.Add(onFixedUpdate);
    }

    public void OnUpdate()
    {
        foreach (var list in updateEventSortedDic.Values)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Invoke();
            }
        }
    }

    public void OnFixedUpdate()
    {
        foreach (var list in fixedupdateEventSortedDic.Values)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Invoke();
            }
        }
    }
}
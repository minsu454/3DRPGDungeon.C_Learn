using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public sealed class SceneManagerEx : IManager
{
    public void Init()
    {
        
    }

    private readonly Dictionary<SceneType, string> typeToStringDic = new Dictionary<SceneType, string>();

    /// <summary>
    /// 씬 로드 함수
    /// </summary>
    public void LoadScene(SceneType type)
    {
        SceneManager.LoadScene(GetSceneName(type));
    }

    /// <summary>
    /// 씬 이름 반환해주는 함수
    /// </summary>
    private string GetSceneName(SceneType type)
    {
        if (!typeToStringDic.TryGetValue(type, out string name))
        {
            name = type.ToString();
            typeToStringDic[type] = name;
        }

        return name;
    }

    /// <summary>
    /// 씬 로드됐을 때 호출할 action 붙이는 함수
    /// </summary>
    public void OnLoadCompleted(UnityAction<Scene, LoadSceneMode> callback)
    {
        SceneManager.sceneLoaded += callback;
    }
}

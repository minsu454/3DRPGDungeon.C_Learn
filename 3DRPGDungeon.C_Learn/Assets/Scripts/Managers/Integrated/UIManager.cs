using Core.StringExtensions;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public sealed class UIManager : MonoBehaviour, IManager
{
    private readonly Dictionary<Enum, GameObject> uiContainerDic = new Dictionary<Enum, GameObject>();     //UI 타입 별로 프리팹 저장하는 dic

    private readonly Stack<BaseUI> depth = new Stack<BaseUI>(); //UI 뎁스
    private Transform mainCanvas;                               //Scene마다 있는 mainCanvas저장 변수

    public void Init()
    {
        CreateDic<SceneType>("Prefabs/UI/Main");
        CreateDic<PopupType>("Prefabs/UI/Popup");

        Managers.Scene.OnLoadCompleted(FindMainCanvas);
    }

    /// <summary>
    /// container에 값 넣어주는 함수
    /// </summary>
    private void CreateDic<T>(string path) where T : Enum
    {
        foreach (T type in Enum.GetValues(typeof(T)))
        {
            GameObject go = Resources.Load<GameObject>(string.Format($"{path}/{type.ToString()}"));

            if (go == null)
                continue;

            uiContainerDic.Add(type, go);
        }
    }

    /// <summary>
    /// MainCanvas 찾아주는 함수
    /// </summary>
    public void FindMainCanvas(Scene scene, LoadSceneMode mode)
    {
        GameObject mainCanvasObj = GameObject.Find("MainCanvas");
        if (mainCanvasObj == null)
        {
            Debug.LogError("Is Not MainCanvas.");
            return;
        }

        mainCanvas = mainCanvasObj.transform;

        SetBaseUI(scene);
    }

    /// <summary>
    /// 씬에 기본 UI깔아주는 함수
    /// </summary>
    public void SetBaseUI(Scene scene)
    {
        SceneType type = StringExtensions.StringToEnum<SceneType>(scene.name);
        CreateUI(type);
    }

    /// <summary>
    /// UI 생성 함수
    /// </summary>
    public void CreateUI(Enum type, bool useMainCanvas = true, bool curPopupActive = true)
    {
        if (!uiContainerDic.TryGetValue(type, out GameObject uiGo))
        {
            Debug.LogWarning($"Is Not Scene base UI : {type}");
            return;
        }

        GameObject clone = Instantiate(uiGo, useMainCanvas ? mainCanvas : null);

        if (depth.TryPeek(out BaseUI beforeUI) && curPopupActive)
        {
            beforeUI.gameObject.SetActive(false);
        }
        
        BaseUI afterUI = clone.GetComponent<BaseUI>();
        afterUI.Init();

        depth.Push(beforeUI);
    }

    /// <summary>
    /// 팝업 닫는 함수
    /// </summary>
    public void CloseUI(Action LoadScene = null)
    {
        if (depth.Count == 1)
        {
            return;
        }

        if (LoadScene != null)
        {
            depth.Clear();
            LoadScene();
            return;
        }

        Destroy(depth.Pop());

        if (depth.TryPeek(out BaseUI baseUI))
        {
            baseUI.Init();
            baseUI.gameObject.SetActive(true);
        }
    }
}

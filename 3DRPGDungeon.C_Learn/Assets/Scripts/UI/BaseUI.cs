using UnityEngine;

public class BaseUI : MonoBehaviour
{
    /// <summary>
    /// 초기화 함수
    /// </summary>
    public virtual void Init()
    {

    }

    /// <summary>
    /// 닫는 함수
    /// </summary>
    protected virtual void Close()
    {
        Managers.UI.CloseUI();
    }

    /// <summary>
    /// 닫으면서 다음 씬 불러오는 함수
    /// </summary>
    protected virtual void Close(SceneType type)
    {
        Managers.UI.CloseUI(() => Managers.Scene.LoadScene(type));
    }
}

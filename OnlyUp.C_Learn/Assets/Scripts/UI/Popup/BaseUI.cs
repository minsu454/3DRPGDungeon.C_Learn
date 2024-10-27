using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public virtual void Init()
    {

    }

    protected virtual void Close()
    {
        Managers.UI.CloseUI();
    }

    protected virtual void Close(SceneType type)
    {
        Managers.UI.CloseUI(() => Managers.Scene.LoadScene(type));
    }
}

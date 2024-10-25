using UnityEngine;

public class BasePopup : MonoBehaviour
{
    protected virtual void Init()
    {

    }

    protected virtual void Close()
    {
        Managers.UI.CloseUI();
    }
}

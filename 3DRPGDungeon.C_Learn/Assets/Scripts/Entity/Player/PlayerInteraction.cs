using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour, IUnitParts
{
    [SerializeField] private float checkRate = 0.05f;
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private float maxDistance;
    private float lastCheckTime;
    [SerializeField] private LayerMask layerMask;

    private GameObject curInteractGo;

    public TextMeshProUGUI PromptText;
    private PlayerEquipment equipment;

    public void OnAwake(IUnitCommander commander)
    {
        commander.UpdateEvent += OnUpdate;

        Player player = commander as Player;
        equipment = player.equipment;
    }

    public void OnUpdate()
    {
        if (Time.time - lastCheckTime <= checkRate)
            return;

        lastCheckTime = Time.time;

        Ray ray = firstPersonCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            if (hit.collider.gameObject == curInteractGo)
                return;

            curInteractGo = hit.collider.gameObject;
            SetPromptText();
        }
        else
        {
            curInteractGo = null;
            PromptText.gameObject.SetActive(false);
        }
    }

    private void SetPromptText()
    {
        BaseItemSO baseItemSO = Managers.Addressable.LoadItem<ScriptableObject>(curInteractGo.name) as BaseItemSO;
        PromptText.text = $"{baseItemSO.displayName}\n{baseItemSO.description}";

        PromptText.gameObject.SetActive(true);
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractGo != null)
        {
            bool completed = equipment.TakeItem(curInteractGo.name);

            if (!completed)
            {
                Debug.Log("가방이 가득 찼습니다.");
                return;
            }

            Destroy(curInteractGo);
            PromptText.gameObject.SetActive(false);
        }
    }
}

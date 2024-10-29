using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour, IUnitParts
{
    [SerializeField] private float checkRate = 0.05f;
    [SerializeField] private float maxDistance;
    private Camera firstPersonCamera;
    private float lastCheckTime;
    [SerializeField] private LayerMask layerMask;

    private GameObject curInteractGo;
    private BaseItemSO curItemSO;

    public TextMeshProUGUI PromptText;
    private PlayerEquipment equipment;

    public void OnAwake(IUnitCommander commander)
    {
        commander.UpdateEvent += OnUpdate;

        firstPersonCamera = Camera.main;

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
            curItemSO = null;
            PromptText.gameObject.SetActive(false);
        }
    }

    private void SetPromptText()
    {
        Managers.Addressable.LoadItemAsync<BaseItemSO>(curInteractGo.name, (handle) =>
        {
            if(this == null)
                return;

            if (curInteractGo.name != handle.name)
                return;

            PromptText.text = $"{handle.displayName}\n{handle.description}";

            curItemSO = handle;
            PromptText.gameObject.SetActive(true);
        });
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractGo != null)
        {
            if (curItemSO.type == ItemType.MapInteraction)
            {
                //equipment.CurEquip
            }
            else
            {
                bool completed = equipment.TakeItem(curInteractGo.name, out bool isEquipped);

                if (!completed)
                {
                    Debug.Log("가방이 가득 찼습니다.");
                    return;
                }

                if (isEquipped)
                {
                    equipment.EquipItem(curInteractGo.name);
                }

                Destroy(curInteractGo);
                PromptText.gameObject.SetActive(false);
            }
        }
    }
}

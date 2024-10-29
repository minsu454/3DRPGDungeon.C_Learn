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
    public TextMeshProUGUI InteractionPromptText;
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

            SetPromptText(hit.collider.gameObject);
        }
        else
        {
            curInteractGo = null;
            curItemSO = null;
            PromptText.gameObject.SetActive(false);
            InteractionPromptText.gameObject.SetActive(false);
        }
    }

    private void SetPromptText(GameObject go)
    {
        curInteractGo = go;
        BaseItemSO baseItemSO = Managers.Addressable.LoadItem<BaseItemSO>(go.name);

        PromptText.text = $"{baseItemSO.displayName}\n{baseItemSO.description}";

        if (baseItemSO.type == ItemType.MapInteraction)
        {
            MapInteractionItemSO tempSO = baseItemSO as MapInteractionItemSO;
            InteractionPromptText.text = tempSO.needItemDescription;
            InteractionPromptText.gameObject.SetActive(true);
        }

        curItemSO = baseItemSO;
        PromptText.gameObject.SetActive(true);
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractGo != null)
        {
            if (curItemSO.type == ItemType.MapInteraction)
            {
                if (!curInteractGo.TryGetComponent<IMapInteraction>(out var mapInteracion))
                    return;

                if(equipment.CurEquip == null)
                    return;

                if (mapInteracion.EqualsItem(equipment.CurEquip.itemName))
                {
                    mapInteracion.Select();
                    equipment.UseItem();

                    InteractionPromptText.gameObject.SetActive(false);
                }
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

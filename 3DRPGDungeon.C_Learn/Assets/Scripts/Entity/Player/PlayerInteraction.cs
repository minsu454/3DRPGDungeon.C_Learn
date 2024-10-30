using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour, IUnitParts
{
    [SerializeField] private float checkRate = 0.05f;       //업데이트 실행 주기
    private float lastCheckTime;                            //체크 시간 저장 변수 
    [SerializeField] private float maxDistance;             //감지하는 ray의 최대 거리
    [SerializeField] private LayerMask layerMask;           //ray로 체크할 레이어들 저장 변수
    private Camera firstPersonCamera;                       //일인칭 카메라

    private GameObject curInteractGo;                       //현재 상호 작용하는 오브젝트 저장 변수
    private BaseItemSO curItemSO;                           //현재 상호 작용하는 오브젝트 SO 저장 변수

    public TextMeshProUGUI InfoCommentText;                 //아이템 정보 관련 텍스트 변수
    public TextMeshProUGUI MapItemInteractionCommentText;   //맵아이템에 상호작용 방법을 알려주는 텍스트 변수

    private PlayerEquipment equipment;                      //플레이어 장비

    public void OnInit(IUnitCommander commander)
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

        CheckInteraction();
    }

    /// <summary>
    /// 상호작용중인 오브젝트 감지 및 설정 함수
    /// </summary>
    private void CheckInteraction()
    {
        Ray ray = firstPersonCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            if (hit.collider.gameObject == curInteractGo)
                return;

            SetItemCommentText(hit.collider.gameObject);
        }
        else
        {
            curInteractGo = null;
            curItemSO = null;
            InfoCommentText.gameObject.SetActive(false);
            MapItemInteractionCommentText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 아이템 정보 관련 텍스트 설정해주는 함수
    /// </summary>
    private void SetItemCommentText(GameObject go)
    {
        curInteractGo = go;
        BaseItemSO baseItemSO = Managers.Addressable.LoadData<BaseItemSO>(go.name);

        InfoCommentText.text = $"{baseItemSO.displayName}\n{baseItemSO.description}";

        if (baseItemSO.type == ItemType.MapInteraction)
        {
            MapInteractionItemSO tempSO = baseItemSO as MapInteractionItemSO;
            MapItemInteractionCommentText.text = tempSO.needItemDescription;
            MapItemInteractionCommentText.gameObject.SetActive(true);
        }

        curItemSO = baseItemSO;
        InfoCommentText.gameObject.SetActive(true);
    }

    /// <summary>
    /// 아이템을 줍기 및 상호작용 키 다운 event
    /// </summary>
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractGo != null)
        {
            if (curItemSO.type == ItemType.MapInteraction)
            {
                TryMapInteractionItem();
            }
            else 
            {
                PickupItem();
            }
        }
    }

    /// <summary>
    /// 맵아이템 상호 작용에 사용될 아이템인지 검사하고 가능하다면 상호 작용시켜주는 함수
    /// </summary>
    public void TryMapInteractionItem()
    {
        if (!curInteractGo.TryGetComponent<IMapItemInteraction>(out var mapInteracion))
            return;

        if (equipment.CurEquip == null)
            return;

        if (mapInteracion.EqualsItem(equipment.CurEquip.itemName))
        {
            mapInteracion.Select();
            equipment.UseItem();

            MapItemInteractionCommentText.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 아이템 줍는 함수
    /// </summary>
    public void PickupItem()
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
        InfoCommentText.gameObject.SetActive(false);
    }
}

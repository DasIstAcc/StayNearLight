using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//public class EquipmentSlotUI : Image, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
//{
//    private EquipmentUI owner;
//    public EquipmentItem.SlotType slotType;
//    private ItemTooltipUI ItemTooltipImage;

//    public void Setup(EquipmentItem.SlotType slotType, Vector3 position, GameObject itemTooltipPrafab)
//    {
//        owner = GameManager._equipmentSystem.m_equipmentUI;
//        this.slotType = slotType;
//        rectTransform.position = position;
//        ItemTooltipImage = Instantiate(itemTooltipPrafab, transform).GetComponent<ItemTooltipUI>();
//        ItemTooltipImage.gameObject.SetActive(false);
//    }

//    public void OnPointerClick(PointerEventData eventData)
//    {
//        if (eventData.clickCount % 2 == 0 && eventData.button == PointerEventData.InputButton.Left)
//        {
//            owner.ItemSlotDoubleClicked(slotType);
//            return;
//        }

//        if (eventData.button == PointerEventData.InputButton.Left)
//        {
//            owner.ItemSlotLeftClicked(slotType);
//            return;
//        }

//        if (eventData.button == PointerEventData.InputButton.Right)
//        {
//            owner.ItemSlotRightClicked(slotType);
//            return;
//        }
//    }

//    public void OnPointerEnter(PointerEventData eventData)
//    {
//        if (owner.GetItemInSlot(slotType) != null)
//        {
//            ItemTooltipImage.gameObject.SetActive(true);
//            ItemTooltipImage.Show(owner.GetItemInSlot(slotType));
//        }
//    }

//    public void OnPointerExit(PointerEventData eventData)
//    {
//        ItemTooltipImage.gameObject.SetActive(false);
//    }

//    public void OnPointerMove(PointerEventData eventData)
//    {
        
//    }
//}

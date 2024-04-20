using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[Serializable]
//public class EquipmentSlot
//{
//    public EquipmentItem item;

//    public EquipmentItem.SlotType slotType;

//    public EquipmentSlot(EquipmentItem.SlotType slotType)
//    {
//        this.slotType = slotType;
//    }

//}


//[Serializable]
//public class Equipment
//{
//    private List<EquipmentSlot> itemSlots = new List<EquipmentSlot>();
//    public Action OnEquipmentChanged;

//    private CharacterData owner;

//    public void Setup(CharacterData owner)
//    {
//        itemSlots.Add(new EquipmentSlot(EquipmentItem.SlotType.Bracer));
//        itemSlots.Add(new EquipmentSlot(EquipmentItem.SlotType.Necklace));
//        itemSlots.Add(new EquipmentSlot(EquipmentItem.SlotType.Ring1));
//        itemSlots.Add(new EquipmentSlot(EquipmentItem.SlotType.Ring2));

//        this.owner = owner;
//    }

//    public void EquipItem(EquipmentItem item)
//    {
//        EquipmentSlot slot = itemSlots.Find(x => x.slotType == item.GetSlotType());
//        if (slot != null)
//        {
//            if (slot.item != null) slot.item.UnequippedBy(owner);
//            slot.item = item;
//            slot.item.EquippedBy(owner);
//        }
//        OnEquipmentChanged.Invoke();
//    }

//    public void RemoveItem(EquipmentItem.SlotType slotType)
//    {
//        EquipmentSlot slot = itemSlots.Find(x => x.slotType == slotType);
//        if (slot != null)
//        {
//            if (slot.item != null) 
//            {
//                slot.item.UnequippedBy(owner);
//                GameManager._inventorySystem.m_inventory.AddItem(slot.item);
//                slot.item = null;
//            }
//        }
//        OnEquipmentChanged.Invoke();
//    }

//    public EquipmentItem GetItem(EquipmentItem.SlotType slotType)
//    {
//        return itemSlots.Find(x => x.slotType == slotType).item;
//    }
//}

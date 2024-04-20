using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    public class EquipmentUI : MonoBehaviour
    {
        public ItemEntryUI HeadSlot;
        public ItemEntryUI TorsoSlot;
        public ItemEntryUI LegsSlot;
        public ItemEntryUI FeetSlot;
        public ItemEntryUI AccessorySlot;
        public ItemEntryUI WeaponSlot;

        public void Init(InventoryUI owner)
        {
            HeadSlot.Owner = owner;
            TorsoSlot.Owner = owner;
            LegsSlot.Owner = owner;
            FeetSlot.Owner = owner;
            AccessorySlot.Owner = owner;
            WeaponSlot.Owner = owner;
        }

        public void UpdateEquipment(EquipmentSystem equipment)
        {
            var head = equipment.GetItem(EquipmentItem.EquipmentSlot.Head);
            var torso = equipment.GetItem(EquipmentItem.EquipmentSlot.Torso);
            var legs = equipment.GetItem(EquipmentItem.EquipmentSlot.Legs);
            var feet = equipment.GetItem(EquipmentItem.EquipmentSlot.Feet);
            var accessory = equipment.GetItem(EquipmentItem.EquipmentSlot.Accessory);
            var weapon = equipment.Weapon;

            HeadSlot.SetupEquipment(head);
            TorsoSlot.SetupEquipment(torso);
            LegsSlot.SetupEquipment(legs);
            FeetSlot.SetupEquipment(feet);
            AccessorySlot.SetupEquipment(accessory);
            WeaponSlot.SetupEquipment(weapon);
        }
    }
}


//public class EquipmentUI : MonoBehaviour
//{
//    [SerializeField] private GameObject itemSlotUIPrefab;
//    [SerializeField] private GameObject itemToolTipPrefab;
//    private List<EquipmentSlotUI> itemSlotsUI = new List<EquipmentSlotUI>();
//    [SerializeField] private Sprite defaultSprite = null;

//    private Equipment bond_equipment;

//    public void Setup(CharacterData owner)
//    {
//        bond_equipment = owner.Equipment;
//        bond_equipment.OnEquipmentChanged += UpdateUI;

//        EquipmentSlotUI bracer_slot = Instantiate(itemSlotUIPrefab, transform).GetComponent<EquipmentSlotUI>();
//        bracer_slot.Setup(EquipmentItem.SlotType.Bracer, new Vector3(100, 400), itemToolTipPrefab);
//        itemSlotsUI.Add(bracer_slot);
//        EquipmentSlotUI necklace_slot = Instantiate(itemSlotUIPrefab, transform).GetComponent<EquipmentSlotUI>();
//        necklace_slot.Setup(EquipmentItem.SlotType.Necklace, new Vector3(100, 300), itemToolTipPrefab);
//        itemSlotsUI.Add(necklace_slot);
//        EquipmentSlotUI ring1_slot = Instantiate(itemSlotUIPrefab, transform).GetComponent<EquipmentSlotUI>();
//        ring1_slot.Setup(EquipmentItem.SlotType.Ring1, new Vector3(100, 200), itemToolTipPrefab);
//        itemSlotsUI.Add(ring1_slot);
//        EquipmentSlotUI ring2_slot = Instantiate(itemSlotUIPrefab, transform).GetComponent<EquipmentSlotUI>();
//        ring2_slot.Setup(EquipmentItem.SlotType.Ring2, new Vector3(100, 100), itemToolTipPrefab);
//        itemSlotsUI.Add(ring2_slot);

//        GetComponentInChildren<StatDisplayUI>().Setup(owner);
//    }

//    public void UpdateUI()
//    {
//        foreach (EquipmentItem.SlotType slot in Enum.GetValues(typeof(EquipmentItem.SlotType)))
//        {
//            if (GameManager._equipmentSystem.m_equipment.GetItem(slot) != null)
//            {
//                itemSlotsUI.Find(x => x.slotType == slot).sprite = GameManager._equipmentSystem.m_equipment.GetItem(slot).ItemSprite;
//            }
//            else
//            {
//                itemSlotsUI.Find(x => x.slotType == slot).sprite = defaultSprite;
//            }
//        }
//    }

//    public EquipmentItem GetItemInSlot(EquipmentItem.SlotType slotType)
//    {
//        return bond_equipment.GetItem(slotType);
//    }

//    public void ItemSlotDoubleClicked(EquipmentItem.SlotType slotType)
//    {
//        bond_equipment.RemoveItem(slotType);
//    }

//    internal void ItemSlotLeftClicked(EquipmentItem.SlotType slotType)
//    {
//        //Debug.Log("Need to select this slot");
//    }

//    internal void ItemSlotRightClicked(EquipmentItem.SlotType slotType)
//    {
//        //Debug.Log("Need to open context menu for this slot");
//    }
//}

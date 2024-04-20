using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VFX;


namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        public class DragData
        {
            public ItemEntryUI DraggedEntry;
            public RectTransform OriginalParent;
        }

        public RectTransform[] ItemSlots;

        public ItemEntryUI ItemEntryPrefab;
        public ItemTooltipUI Tooltip;

        public EquipmentUI EquipementUI;


        public Canvas DragCanvas;

        public DragData CurrentlyDragged { get; set; }
        public CanvasScaler DragCanvasScaler { get; private set; }

        public CharacterData Character
        {
            get { return m_Data; }
        }

        ItemEntryUI[] m_ItemEntries;
        ItemEntryUI m_HoveredItem;
        CharacterData m_Data;

        public void Init()
        {
            CurrentlyDragged = null;

            DragCanvasScaler = DragCanvas.GetComponentInParent<CanvasScaler>();

            m_ItemEntries = new ItemEntryUI[ItemSlots.Length];

            for (int i = 0; i < m_ItemEntries.Length; ++i)
            {
                m_ItemEntries[i] = Instantiate(ItemEntryPrefab, ItemSlots[i]);
                m_ItemEntries[i].gameObject.SetActive(false);
                m_ItemEntries[i].Owner = this;
                m_ItemEntries[i].InventoryEntry = i;
            }

            EquipementUI.Init(this);
        }

        void OnEnable()
        {
            m_HoveredItem = null;
            Tooltip.gameObject.SetActive(false);
        }

        public void Load(CharacterData data)
        {
            m_Data = data;
            EquipementUI.UpdateEquipment(m_Data.Equipment);

            for (int i = 0; i < m_ItemEntries.Length; ++i)
            {
                m_ItemEntries[i].UpdateEntry();
            }

            GetComponentInChildren<StatDisplayUI>().Setup(m_Data);
        }

        public void ObjectDoubleClicked(InventorySystem.InventoryEntry usedItem)
        {
            m_Data.Inventory.UseItem(usedItem);
                
            ObjectHoverExited(m_HoveredItem);
            Load(m_Data);
        }
        public void EquipmentDoubleClicked(EquipmentItem equItem)
        {
            m_Data.Equipment.Unequip(equItem.Slot);
            ObjectHoverExited(m_HoveredItem);
            Load(m_Data);
        }
        public void ObjectHoveredEnter(ItemEntryUI hovered)
        {
            m_HoveredItem = hovered;
            
            Item itemUsed = m_HoveredItem.InventoryEntry != -1 ? m_Data.Inventory.Entries[m_HoveredItem.InventoryEntry].Item : m_HoveredItem.EquipmentItem;

            if (itemUsed != null)
            {
                Tooltip.gameObject.SetActive(true);
                Tooltip.Name.text = itemUsed.Name;
                Tooltip.DescriptionText.text = itemUsed.GetDescription();
            }
        }

        public void ObjectHoverExited(ItemEntryUI exited)
        {
            if (m_HoveredItem == exited)
            {
                m_HoveredItem = null;
                Tooltip.gameObject.SetActive(false);
            }
        }

        public void HandledDroppedEntry(Vector3 position)
        {
            for (int i = 0; i < ItemSlots.Length; ++i)
            {
                RectTransform t = ItemSlots[i];

                if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
                {
                    if (m_ItemEntries[i] != CurrentlyDragged.DraggedEntry)
                    {
                        var prevItem = m_Data.Inventory.Entries[CurrentlyDragged.DraggedEntry.InventoryEntry];
                        m_Data.Inventory.Entries[CurrentlyDragged.DraggedEntry.InventoryEntry] = m_Data.Inventory.Entries[i];
                        m_Data.Inventory.Entries[i] = prevItem;

                        CurrentlyDragged.DraggedEntry.UpdateEntry();
                        m_ItemEntries[i].UpdateEntry();
                    }
                }
            }
        }
    }

    
}


//public class InventoryUI : MonoBehaviour
//{
//    [SerializeField] private GameObject itemSlotUIPrefab;
//    [SerializeField] private GameObject itemToolTipPrefab;
//    [SerializeField] private List<Image> slotImages = new List<Image>();
//    private List<ItemSlotUI> itemSlotsUI = new List<ItemSlotUI>();
//    [SerializeField] private Sprite defaultSprite = null;

//    private Inventory bond_inventory;

//    public void Setup()
//    {
//        bond_inventory = GameManager._inventorySystem.m_inventory;
//        bond_inventory.OnInventoryChanged += UpdateUI;
//        for (int i = 0; i < bond_inventory.GetSize(); i++)
//        {
//            ItemSlotUI new_slot = Instantiate(itemSlotUIPrefab, transform).GetComponent<ItemSlotUI>();

//            new_slot.Setup(this, i, new Vector3(200 + (i * 60) % 600, 300 - (i / 10) * 60), itemToolTipPrefab);

//            itemSlotsUI.Add(new_slot);
//        }

//        GetComponentInChildren<StatDisplayUI>().Setup(GameManager._manager.GetPlayer().m_data);
//    }

//    public void UpdateUI()
//    {
//        for (int i = 0; i < bond_inventory.GetSize(); i++)
//        {
//            if (bond_inventory.GetItem(i) != InventorySlot.NoItem)
//            {
//                itemSlotsUI[i].color = new Color(1, 1, 1, 1);
//                itemSlotsUI[i].sprite = bond_inventory.GetItem(i).Icon;
//            }
//            else
//            {
//                itemSlotsUI[i].color = new Color((float)0.1215, 0, 1, 1);
//                itemSlotsUI[i].sprite = defaultSprite;
//            }
//        }
//        //for (int i = inventory.GetSize(); i < itemSlotsUI.Count; i++)
//        //{
//        //    itemSlotsUI[i].color = new Color((float)0.1215, 0, 1, 1);
//        //    itemSlotsUI[i].sprite = defaultSprite;
//        //}
//    }

//    public Item GetItemByIndex(int id)
//    {
//        return bond_inventory.GetItem(id);
//    }

//    public void ItemSlotDoubleClicked(int index)
//    {
//        GameManager._inventorySystem.ItemUsed(index);
//    }

//    internal void ItemSlotLeftClicked(int index)
//    {
//        //Debug.Log("Need to select this slot");
//    }

//    internal void ItemSlotRightClicked(int index)
//    {
//        //Debug.Log("Need to open context menu for this slot");
//    }
//}
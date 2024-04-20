

public class InventorySystem
{
    /// <summary>
    /// One entry in the inventory. Hold the type of Item and how many there is in that slot.
    /// </summary>
    public class InventoryEntry
    {
        public int Count;
        public Item Item;
    }

    //Only 32 slots in inventory
    public InventoryEntry[] Entries = new InventoryEntry[32];

    CharacterData m_Owner;

    public void Init(CharacterData owner)
    {
        m_Owner = owner;
    }

    /// <summary>
    /// Add an item to the inventory. This will look if this item already exist in one of the slot and increment the
    /// stack counter there instead of using another slot.
    /// </summary>
    /// <param name="item">The item to add to the inventory</param>
    public void AddItem(Item item)
    {
        bool found = false;
        int firstEmpty = -1;
        for (int i = 0; i < 32; ++i)
        {
            if (Entries[i] == null)
            {
                if (firstEmpty == -1)
                    firstEmpty = i;
            }
            else if (Entries[i].Item == item)
            {
                Entries[i].Count += 1;
                found = true;
            }
        }

        if (!found && firstEmpty != -1)
        {
            InventoryEntry entry = new InventoryEntry();
            entry.Item = item;
            entry.Count = 1;

            Entries[firstEmpty] = entry;
        }
    }

    /// <summary>
    /// This will *try* to use the item. If the item return true when used, this will decrement the stack count and
    /// if the stack count reach 0 this will free the slot. If it return false, it will just ignore that call.
    /// (e.g. a potion will return false if the user is at full health, not consuming the potion in that case)
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public bool UseItem(InventoryEntry item)
    {
        //true mean it get consumed and so would be removed from inventory.
        //(note "consumed" is a loose sense here, e.g. armor get consumed to be removed from inventory and added to
        //equipement by their subclass, and de-equiping will re-add the equipement to the inventory 
        if (item.Item.UsedBy(m_Owner))
        {
            item.Count -= 1;

            if (item.Count <= 0)
            {
                //maybe store the index in the InventoryEntry to avoid having to find it again here
                for (int i = 0; i < 32; ++i)
                {
                    if (Entries[i] == item)
                    {
                        Entries[i] = null;
                        break;
                    }
                }
            }

            return true;
        }

        return false;
    }

    public int GetEmptySlotsCount()
    {
        int counter = 0;

        for (int i = 0; i < 32; ++i)
        {
            if (Entries[i] == null)
            {
                counter++;
            }
        }

        return counter;
    }
}



//[Serializable]
//public class InventorySlot
//{
//    public Item item;

//    public int count;

//    private int m_slot;
//    public int slot { get => m_slot; private set => m_slot = value; }

//    public InventorySlot(Item item, int slot)
//    {
//        this.item = item;
//        this.slot = slot;
//        count = 1;
//    }

//    public static Item NoItem = new();
//}

//[Serializable]
//public class InventorySystem
//{
//    private List<InventorySlot> itemSlots = new List<InventorySlot>();
//    private int size = 30;
//    public Action OnInventoryChanged;

//    //int bracerIndex = 101;
//    //int necklaceIndex = 102;
//    //int ringOneIndex = 103;
//    //int ringTwoIndex = 104;

//    //itemSlots.Add(new InventorySlot(Item.NoItem, bracerIndex, InventorySlot.SlotType.Bracer));
//    //itemSlots.Add(new InventorySlot(Item.NoItem, necklaceIndex, InventorySlot.SlotType.Necklace));
//    //itemSlots.Add(new InventorySlot(Item.NoItem, ringOneIndex, InventorySlot.SlotType.Ring1));
//    //itemSlots.Add(new InventorySlot(Item.NoItem, ringTwoIndex, InventorySlot.SlotType.Ring2));

//    public InventorySystem(int size)
//    {
//        this.size = size;

//        for (int i = 0; i < size; i++)
//        {
//            itemSlots.Add(new InventorySlot(InventorySlot.NoItem, i));
//        }
//    }

//    public bool AddItem(Item item)
//    {
//        InventorySlot slot = itemSlots.Find(x => x.item == item);
//        if (slot != null && slot.item.stackable)
//        {
//            slot.count++;
//        }
//        else
//        {
//            int free_slot_index = FindFreeSlotIndex();
//            if (free_slot_index == -1) return false;
//            slot = new InventorySlot(item, free_slot_index);
//            itemSlots[free_slot_index] = slot;
//        }
//        OnInventoryChanged.Invoke();
//        return true;
//    }

//    public void RemoveItemAt(int index)
//    {
//        InventorySlot slot = itemSlots.Find(x => x.slot == index);
//        if (slot != null)
//        {
//            slot.count--;
//            if (slot.count == 0) itemSlots[slot.slot].item = InventorySlot.NoItem;
//        }
//        OnInventoryChanged.Invoke();
//    }

//    public void RemoveItem(Item item)
//    {
//        InventorySlot slot = itemSlots.Find(x => x.item == item);
//        if (slot != null)
//        {
//            slot.count--;
//            if (slot.count == 0) itemSlots[slot.slot].item = InventorySlot.NoItem;
//        }
//        OnInventoryChanged.Invoke();
//    }

//    public Item GetItem(int slot_index)
//    {
//        InventorySlot toFind = itemSlots.Find(x => x.slot == slot_index);
//        return toFind != null ? toFind.item : null;
//    }

//    public int GetSize()
//    {
//        return size;
//    }

//    private int FindFreeSlotIndex()
//    {
//        for (int i = 0; i < size; i++)
//        {
//            InventorySlot toFind = itemSlots.Find(x => x.slot == i);
//            if (toFind == null || toFind.item == InventorySlot.NoItem)
//            {
//                return i;
//            }
//        }

//        return -1;
//    }

//    public int GetItemCount()
//    {
//        return itemSlots.Count;
//    }

//    public void UseItem()
//    {

//    }
//}

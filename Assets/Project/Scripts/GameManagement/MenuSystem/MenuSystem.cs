using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuSystem
{
    //private InventorySystem inventorySystem;
    //private EquipmentSystem equipmentSystem;

    //public MenuSystem(GameObject inventoryUIObj, GameObject equipmentUIObj)
    //{
    //    inventorySystem = new InventorySystem(GameManager._manager.GetPlayer().GetComponent<CharacterUnit>().m_data.inventory, inventoryUIObj);
    //    equipmentSystem = new EquipmentSystem(GameManager._manager.GetPlayer().GetComponent<CharacterUnit>().m_data.equipment, equipmentUIObj);
    //}


    //public bool IsInventoryOpen()
    //{
    //    return inventorySystem.IsOpen() || equipmentSystem.IsOpen();
    //}

    //public void InventoryKeyDown()
    //{
    //    if (equipmentSystem.IsOpen())
    //    {
    //        equipmentSystem.CloseEquipment();
    //    }

    //    if (inventorySystem.IsOpen())
    //    {
    //        inventorySystem.CloseInventory();
    //    }
    //    else
    //    {
    //        inventorySystem.OpenInventory();
    //    }
    //}

    //public void EquipmentKeyDown()
    //{
    //    if (inventorySystem.IsOpen())
    //    {
    //        inventorySystem.CloseInventory();
    //    }

    //    if (equipmentSystem.IsOpen())
    //    {
    //        equipmentSystem.CloseEquipment();
    //    }
    //    else
    //    {
    //        equipmentSystem.OpenEquipment();
    //    }
    //}

    //public void OnSaveClick()
    //{
    //    GameManager._loadSystem.SaveState();
    //}

    //public void OnLoadClick()
    //{
    //    GameManager._loadSystem.LoadState();
    //}

    //public void OnDeleteClick()
    //{
    //    GameManager._loadSystem.DeleteState("New Game");
    //}
}

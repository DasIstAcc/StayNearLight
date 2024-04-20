using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    [SerializeField]
    KeyCode inventoryKey = KeyCode.None;

    ThirdPersonInput personInput;
    ThirdPersonCamera TPCamera;

    private void Start()
    {
        personInput = GameManager._manager.GetPlayer().GetComponent<ThirdPersonInput>();
        personInput.isActive = true;
        if (Camera.main != null) TPCamera = Camera.main.GetComponent<ThirdPersonCamera>();
        TPCamera.isActive = true;
    }

    private void Update()
    {
        InventoryBehaviour();

        


        //personInput.isActive = !GameManager._inventorySystem.IsOpen() && !GameManager._equipmentSystem.IsOpen();
        //TPCamera.isActive = !GameManager._inventorySystem.IsOpen() && !GameManager._equipmentSystem.IsOpen();
    }


    public void InventoryBehaviour()
    {
        if (Input.GetKeyUp(inventoryKey))
            UISystem.Instance.ToggleInventory();
    }

    //public void InventoryKeyDown()
    //{
    //    if (GameManager._equipmentSystem.IsOpen())
    //    {
    //        GameManager._equipmentSystem.CloseEquipment();
    //    }

    //    if (GameManager._inventorySystem.IsOpen())
    //    {
    //        GameManager._inventorySystem.CloseInventory();
    //    }
    //    else
    //    {
    //        GameManager._inventorySystem.OpenInventory();
    //    }
    //}

    //public void EquipmentKeyDown()
    //{
    //    if (GameManager._inventorySystem.IsOpen())
    //    {
    //        GameManager._inventorySystem.CloseInventory();
    //    }

    //    if (GameManager._equipmentSystem.IsOpen())
    //    {
    //        GameManager._equipmentSystem.CloseEquipment();
    //    }
    //    else
    //    {
    //        GameManager._equipmentSystem.OpenEquipment();
    //    }
    //}

    //public void OpenEquipment()
    //{
    //    EquipmentKeyDown();
    //}

    //public void OpenInventory()
    //{
    //    InventoryKeyDown();
    //}
}

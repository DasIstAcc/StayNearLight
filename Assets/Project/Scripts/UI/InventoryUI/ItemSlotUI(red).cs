using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//namespace UI
//{
//    public class ItemSlotUI : Image, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
//    {
//        [SerializeField]
//        private int index;
//        private InventoryUI owner;
//        private ItemTooltipUI ItemTooltipImage;

//        public ItemSlotUI(int index, InventoryUI owner)
//        {
//            this.index = index;
//            this.owner = owner;
            
//        }

//        public void Setup(InventoryUI new_owner, int index, Vector3 position, GameObject toolTipPrefab)
//        {
//            owner = new_owner;
//            this.index = index;
//            rectTransform.position = position;

//            ItemTooltipImage = Instantiate(toolTipPrefab, transform).GetComponent<ItemTooltipUI>();
//            ItemTooltipImage.gameObject.SetActive(false);
//        }

//        public int GetIndex()
//        {
//            return index;
//        }

        

//        public void OnPointerClick(PointerEventData eventData)
//        {
//            if (eventData.clickCount % 2 == 0 && eventData.button == PointerEventData.InputButton.Left)
//            {
//                owner.ItemSlotDoubleClicked(index);
//                return;
//            }

//            if (eventData.button == PointerEventData.InputButton.Left)
//            {
//                owner.ItemSlotLeftClicked(index);
//                return;
//            }

//            if (eventData.button == PointerEventData.InputButton.Right)
//            {
//                owner.ItemSlotRightClicked(index);
//                return;
//            }


//        }

//        public void OnPointerEnter(PointerEventData eventData)
//        {
//            if (owner.GetItemByIndex(index) != InventorySlot.NoItem)
//            {
//                ItemTooltipImage.gameObject.SetActive(true);
//                ItemTooltipImage.Show(owner.GetItemByIndex(index));
//            }
//        }

//        public void OnPointerExit(PointerEventData eventData)
//        {
//            ItemTooltipImage.gameObject.SetActive(false);
//        }

//        public void OnPointerMove(PointerEventData eventData)
//        {
//            //if (ItemTooltipImage.isActiveAndEnabled == true)
//            //    ItemTooltipImage.transform.position = eventData.position;
//        }
//    }
//}


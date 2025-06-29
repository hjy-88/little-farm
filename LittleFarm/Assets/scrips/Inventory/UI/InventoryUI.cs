using MFarm.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mfarm.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        public ItemTooltip itemTooltip;
        [Header("��קͼƬ")]
        public Image dragItem;

        [Header("��ұ���UI")]
        [SerializeField] private GameObject bagUI;
        private bool bagOpened;

        [SerializeField] private SlotUI[] playerSlots;

        private void OnEnable()
        {
            EventHandler.UpdateInventoryUI += OnUpdateInventoryUI;
            //EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadedEvent;
            //EventHandler.BaseBagOpenEvent += OnBaseBagOpenEvent;
            //EventHandler.BaseBagCloseEvent += OnBaseBagCloseEvent;
            //EventHandler.ShowTradeUI += OnShowTradeUI;
        }

        private void OnDisable()
        {
            EventHandler.UpdateInventoryUI -= OnUpdateInventoryUI;
            //EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadedEvent;
            //EventHandler.BaseBagOpenEvent -= OnBaseBagOpenEvent;
            //EventHandler.BaseBagCloseEvent += OnBaseBagCloseEvent;
            //EventHandler.ShowTradeUI += OnShowTradeUI;
        }

        private void Start()
        {
            for(int i = 0; i<playerSlots.Length;i++)
            {
                playerSlots[i].slotIndex = i;
            }
            bagOpened = bagUI.activeInHierarchy;
        }


        ///���̰���b���򿪱���
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                OpenBagUI();
            }
        }


        private void OnUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
        {
            switch (location)
            {
                case InventoryLocation.Player:
                    for (int i = 0; i < playerSlots.Length; i++)
                    {
                        if (list[i].itemAmount > 0)
                        {
                            var item = InventoryManager.Instance.GetItemDetails(list[i].itemID);
                            playerSlots[i].UpdateSlot(item, list[i].itemAmount);
                        }
                        else
                        {
                            playerSlots[i].UpdateEmptySlot();
                        }
                    }
                    break;
            }
        }

        /// <summary>
        ///�򿪹رձ���UI,Button�����¼�
        /// </summary>
        public void OpenBagUI()
        {
            bagOpened = !bagOpened;

            bagUI.SetActive(bagOpened);
        }


    }
}

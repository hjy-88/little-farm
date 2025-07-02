using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace MFarm.Inventory
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        [Header("��Ʒ����")]
        public ItemDataList_SO itemDataList_SO;
        [Header("������ͼ")]
        public BluePrintDataList_SO bluePrintData;
        [Header("��������")]
        //public InventoryBag_SO playerBagTemp;
        public InventoryBag_SO playerBag;
        [Header("����")]
        public int PlayerMoney;
        private void OnEnable()
        {
            EventHandler.DropItemEvent += OnDropItemEvent;
            EventHandler.HarvestAtPlayerPosition += OnHarvestAtPlayerPosition;
            EventHandler.BuildFurnitureEvent += OnBuildFurnitureEvent;
        }
        private void OnDisable()
        {
            EventHandler.DropItemEvent -= OnDropItemEvent;
            EventHandler.HarvestAtPlayerPosition -= OnHarvestAtPlayerPosition;
            EventHandler.BuildFurnitureEvent -= OnBuildFurnitureEvent;
        }
        private void Start()
        {
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        private void OnBuildFurnitureEvent(int ID, Vector3 mousePos)
        {
            RemoveItem(ID, 1);
            BluePrintDetails bluePrint = bluePrintData.GetBluePrintDetails(ID);
            foreach (var item in bluePrint.resourceItem)
            {
                RemoveItem(item.itemID, item.itemAmount);
            }
        }

        private void OnDropItemEvent(int ID, Vector3 pos, ItemType itemType)
        {
            RemoveItem(ID, 1);
        }

        public ItemDetails GetItemDetails(int ID)
        {
            return itemDataList_SO.itemDetailsList.Find(i => i.itemID == ID);
        }

        public void AddItem(Item item, bool toDestory)
        {
            ///�����Ƿ��и�����
            var index = GetItemIndexInBag(item.itemID);

            AddItemAtIndex(item.itemID, index, 1);

            Debug.Log(GetItemDetails(item.itemID).itemID + "Name: " + GetItemDetails(item.itemID).itemName);
            if (toDestory)
            {
                Destroy(item.gameObject);
            }

            //����UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }
        /// <summary>
        /// ��鱳���Ƿ��п�λ
        /// </summary>
        /// <returns></returns>
        private bool CheckBagCapacity()
        {
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].itemID == 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// ͨ����ƷID�ҵ�����������Ʒλ��
        /// </summary>
        /// <param name="ID">��ƷID</param>
        /// <returns>-1��û�������Ʒ���򷵻����</returns>
        private int GetItemIndexInBag(int ID)
        {
            for (int i = 0; i < playerBag.itemList.Count; i++)
            {
                if (playerBag.itemList[i].itemID == ID)
                    return i;
            }
            return -1;
        }

        /// ��ָ���������λ�������Ʒ
        /// </summary>
        /// <param name="ID">��ƷID</param>
        /// <param name="index">���</param>
        /// <param name="amount">����</param>
        private void AddItemAtIndex(int ID, int index, int amount)
        {
            if (index == -1 && CheckBagCapacity())    //����û�������Ʒ ͬʱ�����п�λ
            {
                var item = new InventoryItem { itemID = ID, itemAmount = amount };
                for (int i = 0; i < playerBag.itemList.Count; i++)
                {
                    if (playerBag.itemList[i].itemID == 0)
                    {
                        playerBag.itemList[i] = item;
                        break;
                    }
                }
            }
            else    //�����������Ʒ
            {
                int currentAmount = playerBag.itemList[index].itemAmount + amount;
                var item = new InventoryItem { itemID = ID, itemAmount = currentAmount };

                playerBag.itemList[index] = item;
            }
        }

        /// <summary>
        /// Player������Χ�ڽ�����Ʒ
        /// </summary>
        /// <param name="fromIndex">��ʼ���</param>
        /// <param name="targetIndex">Ŀ���������</param>
        public void SwapItem(int fromIndex, int targetIndex)
        {
            InventoryItem currentItem = playerBag.itemList[fromIndex];
            InventoryItem targetItem = playerBag.itemList[targetIndex];

            if (targetItem.itemID != 0)
            {
                playerBag.itemList[fromIndex] = targetItem;
                playerBag.itemList[targetIndex] = currentItem;
            }
            else
            {
                playerBag.itemList[targetIndex] = currentItem;
                playerBag.itemList[fromIndex] = new InventoryItem();
            }

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        private void RemoveItem(int ID, int removeAmount)
        {
            var index = GetItemIndexInBag(ID);
            if (playerBag.itemList[index].itemAmount > removeAmount)
            {
                var amount = playerBag.itemList[index].itemAmount - removeAmount;
                var item = new InventoryItem { itemID = ID, itemAmount = amount };
                playerBag.itemList[index] = item;
            }
            else if (playerBag.itemList[index].itemAmount == removeAmount)
            {
                var item = new InventoryItem();
                playerBag.itemList[index] = item;
            }

            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        public void TradeItem(ItemDetails itemDetails, int amount, bool isSellTrade)
        {
            int cost = itemDetails.itemPrice * amount;
            //�����Ʒ����λ��
            int index = GetItemIndexInBag(itemDetails.itemID);

            if (isSellTrade)    //��
            {
                if (playerBag.itemList[index].itemAmount >= amount)
                {
                    RemoveItem(itemDetails.itemID, amount);
                    //�����ܼ�
                    cost = (int)(cost * itemDetails.sellPercentage);
                    PlayerMoney += cost;
                }
            }
            else if (PlayerMoney - cost >= 0)   //��
            {
                if (CheckBagCapacity())
                {
                    AddItemAtIndex(itemDetails.itemID, index, amount);
                }
                PlayerMoney -= cost;
            }
            //ˢ��UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        public bool CheckStock(int ID)
        {
            var bluePrintDetails = bluePrintData.GetBluePrintDetails(ID);

            foreach (var resourceItem in bluePrintDetails.resourceItem)
            {
                var itemStock = playerBag.GetInventoryItem(resourceItem.itemID);
                if (itemStock.itemAmount >= resourceItem.itemAmount)
                {
                    continue;
                }
                else return false;
            }
            return true;
        }



        private void OnHarvestAtPlayerPosition(int ID)
        {
            var index = GetItemIndexInBag(ID);
            AddItemAtIndex(ID, index, 1);
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

        //�����ѳ����嵽���淽��
        public void DropItemFromInventory(ItemDetails itemDetails, Vector3 dropPosition)
        {
            if (itemDetails != null && itemDetails.canDropped)
            {
                RemoveItem(itemDetails.itemID, 1);
                EventHandler.CallInstantiateItemInScene(itemDetails.itemID, dropPosition);
            }
        }

        //��������
        public void AddItemByID(int itemID, int amount)
        {
            // ��ȡ����Ʒ�ڱ����е�����
            int index = GetItemIndexInBag(itemID);

            // �����Ʒ
            AddItemAtIndex(itemID, index, amount);

            // ��ӡ��־ȷ��
            var details = GetItemDetails(itemID);
            if (details != null)
            {
                Debug.Log("�����Ʒ��" + details.itemName + " x" + amount);
            }

            // ����UI
            EventHandler.CallUpdateInventoryUI(InventoryLocation.Player, playerBag.itemList);
        }

    }

}

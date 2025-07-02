//using MFarm.Inventory;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class FurniturePickUp : MonoBehaviour
//{
//    public int itemID;
//    public int FuniturID;
//    private bool canPickUp = false;
//    private Item Item;
//    private Furniture Furniture;
//    private BluePrintDetails blueprintdetails;
//    private BluePrintDataList_SO bluePrintDataList_SO;

//    private void Update()
//    {
//        if (canPickUp && Input.GetKeyDown(KeyCode.F))
//        {
//            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(itemID);
//            if (itemDetails != null && itemDetails.canPickedup)
//            {
//                InventoryManager.Instance.AddItem(itemID, 1);
//                Destroy(gameObject);
//            }
//        }
//    }

//    private void OnTriggerEnter2D(Collider2D other)
//    {
//        Furniture furniture = other.GetComponent<Furniture>();
//        blueprintdetails.ID = furniture.itemID;
//        bluePrintDataList_SO.GetBluePrintDetails
//        BluePrintDataList_SO[] allBluePrintDataList = FindObjectsOfType<BluePrintDataList_SO>();
//        foreach (BluePrintDataList_SO blueprintdataList in allBluePrintDataList)
//        {
//            if (BluePrintDataList_SO. == id)
//                return item;
//        }
//        Item.itemID = 
//        itemID = furniture.itemID;
//        if (item != null)
//        {
//            currentItem = item;
//        }
//    }

//    private void OnTriggerExit2D(Collider2D other)
//    {
//        Item item = other.GetComponent<Item>();
//        if (item != null && item == currentItem)
//        {
//            currentItem = null;
//        }
//    }

//    public Item FindItemByID(int id)
//    {


//        return null;
//    }
//}
using MFarm.Inventory;
using UnityEngine;

public class FurniturePickUp : MonoBehaviour
{
    public int bluePrintID; // Furniture ���ص���ʵ����ͼID������ֱ�ӽ��������ƷID
    private bool canPickUp = false;
    public int itemID;
    public int FuniturID;
    Furniture currentFurniture;
    //private bool canPickUp = false;
    private Item Item;
    private Furniture Furniture;
    private BluePrintDetails blueprintdetails;
    private BluePrintDataList_SO bluePrintDataList_SO;
    public Item RealItem;
    public int id;

    private void Update()
    {
        if (canPickUp)
        {
            // ��� currentFurniture �Ƿ�������
            if (currentFurniture == null)
            {
                canPickUp = false;
                return;
            }

            // 1. ��ȡ��ͼ
            BluePrintDetails blueprint = InventoryManager.Instance.bluePrintData.GetBluePrintDetails(currentFurniture.itemID);
            if (blueprint == null)
            {
                Debug.LogWarning("�Ҳ�����Ӧ��ͼ��");
                return;
            }

            int RealItemID = blueprint.buildItemID;

            // 2. ��ȡ����
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(RealItemID);

            // 3. ʰȡ����
            if (Input.GetKeyDown(KeyCode.F))
            {
                InventoryManager.Instance.AddItemByID(RealItemID, 1);

                // ���ټҾ߶���
                Destroy(currentFurniture.gameObject);

                // ����״̬
                currentFurniture = null;
                canPickUp = false;
            }
        }
    }

    //private void Update()
    //{
    //    if (canPickUp)
    //    {
    //        // 1. ��ȡ��Ӧ��ͼ
    //        Furniture furniture = currentFurniture;
    //        //id = furniture.itemID;
    //        //blueprintdetails.ID = furniture.itemID;
    //        Debug.Log("�����ͼ��");
    //        BluePrintDetails blueprint = InventoryManager.Instance.bluePrintData.GetBluePrintDetails(furniture.itemID);
    //        int RealItemID = blueprint.buildItemID;
    //        Debug.Log("idΪ"+RealItemID);
    //        if (blueprint == null)
    //        {
    //            Debug.LogWarning("�Ҳ�����Ӧ��ͼ��");
    //            return;
    //        }
    //        // 2. ȡ����ͼ��ʵ�����ɵ���ƷID
    //        //int realItemID = blueprint.buildItemID;

    //        //3. ��ȡ����Ʒ����
    //        ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(RealItemID);
    //        if (Input.GetKeyDown(KeyCode.F))
    //        {
    //            InventoryManager.Instance.AddItemByID(RealItemID, 1);
    //            Destroy(furniture.gameObject);
    //        }
    //        //Item[] allItems = FindObjectsOfType<Item>();

    //        //foreach (Item item in allItems)
    //        //{
    //        //    if (item.itemID == RealItemID)
    //        //    {
    //        //        RealItem = item;
    //        //        Debug.Log("idΪ" + RealItem.itemID);
    //        //    }
    //        //}
    //        //if (Input.GetKeyDown(KeyCode.F))
    //        //{
    //        //    InventoryManager.Instance.AddItem(RealItem, true); // �������Ҫ�Լ�ʵ�� AddItemByID ���������Ϊ AddItem��
    //        //}


    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Furniture furniture = other.GetComponent<Furniture>();
        if (furniture != null)
        {
            currentFurniture = furniture;
            Debug.Log("�ҵ��Ҿ���");
            Debug.Log(currentFurniture.itemID);
            canPickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Furniture furniture = other.GetComponent<Furniture>();
        if (furniture != null && furniture == currentFurniture)
        {
            canPickUp = false;
            currentFurniture = null;
        }

    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        canPickUp = true;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        canPickUp = false;
    //    }
    //}
}

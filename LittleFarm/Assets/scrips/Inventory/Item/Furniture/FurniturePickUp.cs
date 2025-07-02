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
    public int bluePrintID; // Furniture 挂载的其实是蓝图ID，不是直接建造出的物品ID
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
            // 检查 currentFurniture 是否已销毁
            if (currentFurniture == null)
            {
                canPickUp = false;
                return;
            }

            // 1. 获取蓝图
            BluePrintDetails blueprint = InventoryManager.Instance.bluePrintData.GetBluePrintDetails(currentFurniture.itemID);
            if (blueprint == null)
            {
                Debug.LogWarning("找不到对应蓝图！");
                return;
            }

            int RealItemID = blueprint.buildItemID;

            // 2. 获取详情
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(RealItemID);

            // 3. 拾取按键
            if (Input.GetKeyDown(KeyCode.F))
            {
                InventoryManager.Instance.AddItemByID(RealItemID, 1);

                // 销毁家具对象
                Destroy(currentFurniture.gameObject);

                // 重置状态
                currentFurniture = null;
                canPickUp = false;
            }
        }
    }

    //private void Update()
    //{
    //    if (canPickUp)
    //    {
    //        // 1. 获取对应蓝图
    //        Furniture furniture = currentFurniture;
    //        //id = furniture.itemID;
    //        //blueprintdetails.ID = furniture.itemID;
    //        Debug.Log("获得蓝图了");
    //        BluePrintDetails blueprint = InventoryManager.Instance.bluePrintData.GetBluePrintDetails(furniture.itemID);
    //        int RealItemID = blueprint.buildItemID;
    //        Debug.Log("id为"+RealItemID);
    //        if (blueprint == null)
    //        {
    //            Debug.LogWarning("找不到对应蓝图！");
    //            return;
    //        }
    //        // 2. 取得蓝图中实际生成的物品ID
    //        //int realItemID = blueprint.buildItemID;

    //        //3. 获取该物品详情
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
    //        //        Debug.Log("id为" + RealItem.itemID);
    //        //    }
    //        //}
    //        //if (Input.GetKeyDown(KeyCode.F))
    //        //{
    //        //    InventoryManager.Instance.AddItem(RealItem, true); // 你可能需要自己实现 AddItemByID 方法（或改为 AddItem）
    //        //}


    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Furniture furniture = other.GetComponent<Furniture>();
        if (furniture != null)
        {
            currentFurniture = furniture;
            Debug.Log("找到家具了");
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

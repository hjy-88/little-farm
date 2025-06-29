using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDetails///����
{
    public int itemID;///��Ʒid��ѯ
    public string itemName;///��Ʒ����itemName
    public ItemType itemType;///��Ʒ����
    public Sprite itemIcon;///��Ʒͼ��
    public Sprite itemOnWorldSprite;///��Ʒ�������ͼ������ͼƬ
    public string itemDescription;///����
    public int itemUseRadius;///��Ʒ��ʹ�÷�Χ

    ///��Ʒ״̬
    public bool canPickedup;///ʰȡ
    public bool canDropped;///����
    public bool canCarried;///����
    public int itemPrice;///��ֵ
    [Range(0, 1)]
    public float sellPercentage;///�������ۿ�
}

[System.Serializable]
public struct InventoryItem
{
    public int itemID;
    public int itemAmount;
}

[System.Serializable]
public class AnimatorType
{
    public PartType partType;
    public PartName partName;
    public AnimatorOverrideController overrideController;
}

[System.Serializable]
public class Tileproperty
{
    public Vector2Int tileCoordinate;
    public GridType gridType;
    public bool boolTypeValue;
}

[System.Serializable]
public class TileDetails
{
    public int gridX, gridY;
    public bool canDig;
    public bool canDropItem;
    public bool isNPCObstacle;
    public int daySinceDug = -1;
    public int daysSinceWatered = -1;
    public int seedItemId = -1;
    public int growthDays = -1;
    public int daysSinceLastHarvset = -1;
}

[System.Serializable]
public class SerializableVector3
{
    public float x, y, z;

    public SerializableVector3(Vector3 pos)
    {
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }

    public Vector2Int ToVector2Int()
    {
        return new Vector2Int((int)x, (int)y);
    }
}

[System.Serializable]
public class SceneItem
{
    public int itemID;
    public SerializableVector3 position;
}
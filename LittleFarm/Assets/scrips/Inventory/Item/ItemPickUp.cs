//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace MFarm.Inventory
//{


//    public class ItemPickUp : MonoBehaviour
//    {
//        private void OnTriggerEnter2D(Collider2D other)
//        {
//            Item item = other.GetComponent<Item>();

//            if (item != null)
//            {
//                if (item.itemDetails.canPickedup)
//                {
//                    //拾取物品添加到背包
//                    InventoryManager.Instance.AddItem(item, true);
//                    //播放音效
//                    //EventHandler.CallPlaySoundEvent(SoundName.Pickup);
//                }
//            }
//        }

//    }
//}

//修改是否捡拾物品逻辑“按f键捡拾”
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFarm.Inventory
{
    public class ItemPickUp : MonoBehaviour
    {
        private Item currentItem;

        private void Update()
        {
            // 检测玩家是否按下F键，并且当前有可拾取的物品
            if (currentItem != null && Input.GetKeyDown(KeyCode.F))
            {
                if (currentItem.itemDetails.canPickedup)
                {
                    // 添加物品到背包
                    InventoryManager.Instance.AddItem(currentItem, true);
                    // 播放音效（如果你启用了音效系统）
                    //EventHandler.CallPlaySoundEvent(SoundName.Pickup);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Item item = other.GetComponent<Item>();
            if (item != null)
            {
                currentItem = item;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Item item = other.GetComponent<Item>();
            if (item != null && item == currentItem)
            {
                currentItem = null;
            }
        }
    }
}

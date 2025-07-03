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
//                    //ʰȡ��Ʒ��ӵ�����
//                    InventoryManager.Instance.AddItem(item, true);
//                    //������Ч
//                    //EventHandler.CallPlaySoundEvent(SoundName.Pickup);
//                }
//            }
//        }

//    }
//}

//�޸��Ƿ��ʰ��Ʒ�߼�����f����ʰ��
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
            // �������Ƿ���F�������ҵ�ǰ�п�ʰȡ����Ʒ
            if (currentItem != null && Input.GetKeyDown(KeyCode.F))
            {
                if (currentItem.itemDetails.canPickedup)
                {
                    // �����Ʒ������
                    InventoryManager.Instance.AddItem(currentItem, true);
                    // ������Ч���������������Чϵͳ��
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

using Mfarm.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFarm.Inventory
{
    [RequireComponent(typeof(SlotUI))]
    public class ActionBarButton : MonoBehaviour
    {
        public KeyCode key;
        private SlotUI slotUI;
        private bool canUse = true;

        private void Awake()
        {
            slotUI = GetComponent<SlotUI>();
        }

        /*       private void OnEnable()
               {
                   EventHandler.UpdateGameStateEvent += OnUpdateGameStateEvent;
               }

               private void OnDisable()
               {
                   EventHandler.UpdateGameStateEvent -= OnUpdateGameStateEvent;
               }

               private void OnUpdateGameStateEvent(GameState gameState)
               {
                   canUse = gameState == GameState.Gameplay;
               }*/


        private void Update()
        {
            if (Input.GetKeyDown(key) && canUse)
            {
                if (slotUI.itemDetails != null)
                {
                    slotUI.isSelected = !slotUI.isSelected;
                    /*if (slotUI.isSelected)
                        slotUI.inventoryUI.UpdateSlotHightlight(slotUI.slotIndex);
                    else
                        slotUI.inventoryUI.UpdateSlotHightlight(-1);*/
                    //Debug.LogError("111");
                    EventHandler.CallItemSelectedEvent(slotUI.itemDetails, slotUI.isSelected);
                }
            }
        }
    }
}

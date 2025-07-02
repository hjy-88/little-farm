using System.Collections;
using System.Collections.Generic;
using MFarm.Map;
using MFarm.Inventory;
using MFarm.CropPlant;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public Sprite normal, tool, seed,item;
    private Sprite currentSprite;
    private Image cursorImage;
    private RectTransform cursorCanvas;

    private Image buildImage;

    private Camera mainCamera;
    private Grid currentGrid;

    private Vector3 mouseWorldPos;
    private Vector3Int mouseGridPos;
    private bool cursorEnable;
    private bool cursorPositionValid;
    private ItemDetails currentItem;
    private Transform PlayerTransform => FindObjectOfType<Player>().transform;
    private void OnEnable()
    {
        EventHandler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHandler.BeforeSceneUnloadEvent += OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent += onAfterSceneLoadedEvent;
        
    }
    private void OnDisable()
    {
        EventHandler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHandler.BeforeSceneUnloadEvent -= OnBeforeSceneUnloadEvent;
        EventHandler.AfterSceneLoadedEvent -= onAfterSceneLoadedEvent;
        
    }
    
    private void Start()
    {
        /*cursorCanvas = GameObject.FindGameObjectWithTag("CursorCanvas").GetComponent<RectTransform>();
        cursorImage = cursorCanvas.GetChild(0).GetComponent<Image>();
        //拿到建造图标
        buildImage = cursorCanvas.GetChild(1).GetComponent<Image>();
        buildImage.gameObject.SetActive(false);*/

        currentSprite = normal;
        //SetCursorImage(normal);

        //mainCamera = Camera.main;
    }
    private void Update()
    {
        //if (cursorCanvas == null) return;
        mainCamera = Camera.main;
        if (cursorCanvas == null)
        {
            var cs = GameObject.FindGameObjectWithTag("CursorCanvas");
            if (cs != null)
            {
                cursorCanvas = cs.GetComponent<RectTransform>();
                cursorImage = cursorCanvas.GetChild(0).GetComponent<Image>();
                buildImage = cursorCanvas.GetChild(1).GetComponent<Image>();
                buildImage.gameObject.SetActive(false);

                SetCursorImage(normal);
            }

            return;
        }

        cursorImage.transform.position = Input.mousePosition;

        if (!InteractWithUI() && cursorEnable)
        {
            SetCursorImage(currentSprite);
            CheckCursorValid();
            CheckPlayerInput();
        }
        else
        {
            SetCursorImage(normal);
            buildImage.gameObject.SetActive(false);
        }
    }
    private void CheckPlayerInput()
    {
        if(Input.GetMouseButtonDown(0)&&cursorPositionValid)
        {
            EventHandler.CallMouseClickedEvent(mouseWorldPos, currentItem);
        }
    }
    private void onAfterSceneLoadedEvent()
    {
        currentGrid = FindObjectOfType<Grid>();
        //cursorEnable = true;
        /*if (currentGrid == null)
        {
            Debug.LogError("找不到Grid组件！请确保场景中有Grid组件");
            
        }*/
    }
    
    private void OnBeforeSceneUnloadEvent()
    {
        cursorEnable = false;
    }
    private void SetCursorImage(Sprite sprite)
    {
        cursorImage.sprite = sprite;
        cursorImage.color = new Color(1, 1, 1, 1);
    }
    private void SetCursorValid()
    {
        cursorPositionValid = true;
        cursorImage.color = new Color(1, 1, 1, 1);
        buildImage.color = new Color(1, 1, 1, 0.5f);
    }
    private void SetCursorInvalid()
    {
        cursorPositionValid = false;
        cursorImage.color = new Color(1, 0, 0, 0.5f);
        buildImage.color = new Color(1, 0, 0, 0.5f);
    }
    private void OnItemSelectedEvent(ItemDetails itemDetails,bool isSelected)
    {
        
        if(!isSelected)
        {
            currentItem = null;
            cursorEnable = false;
            currentSprite = normal;
            buildImage.gameObject.SetActive(false);
        }
        else
        {
            currentItem = itemDetails;
            
            currentSprite = itemDetails.itemType switch
            {
                ItemType.Seed=>seed,
                ItemType.HoeTool=>tool,
                ItemType.ChopTool => tool,
                ItemType.WaterTool => tool,
                ItemType.CollectTool => tool,
                ItemType.ReapTool => tool,
                ItemType.Commodity=>item,
                ItemType.Furniture => tool,
                ItemType.Buildtool => tool,
                _ =>normal
            };
            cursorEnable = true;

            if (itemDetails.itemType == ItemType.Furniture)
            {
                buildImage.gameObject.SetActive(true);
                buildImage.sprite = itemDetails.itemOnWorldSprite;
                buildImage.SetNativeSize();
            }
        }
        
    }
    //新增方法
    private bool HaveFurnitureInRadius(BluePrintDetails bluePrintDetails)
    {
        var buildItem = bluePrintDetails.buildPrefab;
        Vector2 point = mouseWorldPos;
        var size = buildItem.GetComponent<BoxCollider2D>().size;

        var otherColl = Physics2D.OverlapBox(point, size, 0);
        if (otherColl != null)
            return otherColl.GetComponent<Furniture>();
        return false;
    }

    private void CheckCursorValid()
    {
        Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Mathf.Abs(mainCamera.transform.position.z));
        mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        mouseGridPos = currentGrid.WorldToCell(mouseWorldPos);
        //Debug.Log("WorldPos:" + mouseWorldPos + " GridPos:" + mouseGridPos);

        var playerGridPos = currentGrid.WorldToCell(PlayerTransform.position);

        buildImage.rectTransform.position = Input.mousePosition;

        if (Mathf.Abs(mouseGridPos.x-playerGridPos.x)>currentItem.itemUseRadius||Mathf.Abs(mouseGridPos.y-playerGridPos.y)>currentItem.itemUseRadius)
        {
            SetCursorInvalid();
            return;
        }
        TileDetails currentTile = GridMapManager.Instance.GetTileDetailsOnMousePosition(mouseGridPos);
        if(currentTile!=null)
        {
            CropDetails currentCrop = CropManager.Instance.GetCropDetails(currentTile.seedItemId);
            Crop crop = GridMapManager.Instance.GetCropObject(mouseWorldPos);
            switch(currentItem.itemType)
            {
                case ItemType.Seed:
                    if (currentTile.daySinceDug > -1 && currentTile.seedItemId == -1) SetCursorValid(); else SetCursorInvalid();
                    break;
                case ItemType.Commodity:
                    if (currentTile.canDropItem&&currentTile.canDropItem) SetCursorValid(); else SetCursorInvalid();
                    break;
                case ItemType.HoeTool:
                    if (currentTile.canDig) SetCursorValid();
                    else SetCursorInvalid();
                    break;
                case ItemType.WaterTool:
                    if ((currentTile.daySinceDug > -1 && currentTile.daysSinceWatered == -1)) SetCursorValid(); else SetCursorInvalid();
                    break;
                case ItemType.ChopTool:
                    if (crop != null)
                    {
                        if (crop.CanHarvest && crop.cropDetails.CheckToolAvailable(currentItem.itemID)) SetCursorValid(); else SetCursorInvalid();
                    }
                    else SetCursorInvalid();
                    break;
                case ItemType.CollectTool:
                    if(currentCrop!=null)
                    {
                        if(currentCrop.CheckToolAvailable(currentItem.itemID))
                            if(currentTile.growthDays>=currentCrop.TotalGrowthDays) SetCursorValid(); else SetCursorInvalid();
                    }
                    else
                        SetCursorInvalid();
                    break;
                case ItemType.Furniture:
                    //新增修改
                    buildImage.gameObject.SetActive(true);
                    var bluePrintDetails = InventoryManager.Instance.bluePrintData.GetBluePrintDetails(currentItem.itemID);
                    //待修改
                    if (currentTile./*canDig*/canPlaceFurniture && InventoryManager.Instance.CheckStock(currentItem.itemID)/*&&!HaveFurnitureInRadius(bluePrintDetails)*/)
                        SetCursorValid();
                    else
                        SetCursorInvalid();
                    break;

            }

        }
        else
        {
            SetCursorInvalid();

        }
        //Debug.Log("WorldPos:" + mouseWorldPos + " GridPos:" + mouseGridPos);
    }
    private bool InteractWithUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return true;
        return false;
    }
}

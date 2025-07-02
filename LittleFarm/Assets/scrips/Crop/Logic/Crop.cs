using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public CropDetails cropDetails;
    public TileDetails tileDetails;
    private int harvestActionCount;
    public bool CanHarvest => tileDetails.growthDays >= cropDetails.TotalGrowthDays;

    private Animator anim;

    private Transform PlayerTransform => FindObjectOfType<Player>().transform;
    public void ProcessToolAction(ItemDetails tool, TileDetails tile)
    {
        tileDetails = tile;
        int requireActionCount = cropDetails.GetTotalRequireCount(tool.itemID);
        if (requireActionCount == -1) return;

        anim = GetComponentInChildren<Animator>();

        if (harvestActionCount < requireActionCount)
        {
            harvestActionCount++;

            if (anim != null && cropDetails.hasAnimation)
            {
                if (PlayerTransform.position.x < transform.position.x)
                    anim.SetTrigger("RotateRight");
                else
                    anim.SetTrigger("RotateLeft");
            }
            /*//播放粒子
            if (cropDetails.hasParticalEffect)
                EventHandler.CallParticleEffectEvent(cropDetails.particleEffectType, transform.position + cropDetails.particleEffectTypePos);
            //播放声音
            if (cropDetails.soundEffect != SoundName.none)
            {
                EventHandler.CallPlaySoundEvent(cropDetails.soundEffect);
            }*/
        }

        if (harvestActionCount >= requireActionCount)
        {
            if (cropDetails.generateAtPlayerPosition || !cropDetails.hasAnimation)
            {
                SpawnHarvestItems();
            }
            else if (cropDetails.hasAnimation)
            {
                if (PlayerTransform.position.x < transform.position.x)
                    anim.SetTrigger("FallingRight");
                else
                    anim.SetTrigger("FallingLeft");
                //EventHandler.CallPlaySoundEvent(SoundName.TreeFalling);
                //Debug.LogError("111");
                StartCoroutine(HarvestAfterAnimation());
            }
        }
    }
    
    private IEnumerator HarvestAfterAnimation()
    {
        //Debug.LogError("111");
        /*while (!anim.GetCurrentAnimatorStateInfo(0).IsName("END"))
        {
            yield return null;
        }*/
        // 先等待一帧确保动画触发
        yield return null;

        // 等待动画播放完成
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        // 确保对象还没被销毁
        if (this == null || gameObject == null) yield break;
        //Debug.LogError("100");
        SpawnHarvestItems();
        if (cropDetails.transferItemID > 0)
        {
            CreateTransferCrop();
        }
    }

    private void CreateTransferCrop()
    {
        tileDetails.seedItemId = cropDetails.transferItemID;
        tileDetails.daysSinceLastHarvset = -1;
        tileDetails.growthDays = 0;

        EventHandler.CallRefreshCurrentMap();
    }

    public void SpawnHarvestItems()
    {
        
        for (int i = 0; i < cropDetails.producedItemID.Length; i++)
        {
            int amountToProduce;

            if (cropDetails.producedMinAmount[i] == cropDetails.producedMaxAmount[i])
            {
                amountToProduce = cropDetails.producedMinAmount[i];
            }
            else
            {
                amountToProduce = Random.Range(cropDetails.producedMinAmount[i], cropDetails.producedMaxAmount[i] + 1);
            }

            for (int j = 0; j < amountToProduce; j++)
            {
                if (cropDetails.generateAtPlayerPosition)
                    EventHandler.CallHarvestAtPlayerPosition(cropDetails.producedItemID[i]);
                else
                {
                    var dirX = transform.position.x > PlayerTransform.position.x ? 1 : -1;
                    var spawnPos = new Vector3(transform.position.x + Random.Range(dirX, cropDetails.spawnRadius.x * dirX),
                    transform.position.y + Random.Range(-cropDetails.spawnRadius.y, cropDetails.spawnRadius.y), 0);

                    EventHandler.CallInstantiateItemInScene(cropDetails.producedItemID[i], spawnPos);
                }
            }
        }

        if (tileDetails != null)
        {
            tileDetails.daysSinceLastHarvset++;

            if (cropDetails.daysToRegrow > 0 && tileDetails.daysSinceLastHarvset < cropDetails.regrowTimes - 1)
            {
                tileDetails.growthDays = cropDetails.TotalGrowthDays - cropDetails.daysToRegrow;
                EventHandler.CallRefreshCurrentMap();
            }
            else
            {
                tileDetails.daysSinceLastHarvset = -1;
                tileDetails.seedItemId = -1;
                
            }

            Destroy(gameObject);

        }

    }
}

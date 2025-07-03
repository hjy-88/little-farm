using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public List<GameObject> poolPrefabs;
    private List<ObjectPool<GameObject>> poolEffectList = new List<ObjectPool<GameObject>>();

    private Queue<GameObject> soundQueue = new Queue<GameObject>();
    private void OnEnable()
    {
        //EventHandler.ParticleEffectEvent += OnParticleEffectEvent;
        EventHandler.InitSoundEffect += InitSoundEffect;
    }

    private void OnDisable()
    {
        //EventHandler.ParticleEffectEvent -= OnParticleEffectEvent;
        EventHandler.InitSoundEffect -= InitSoundEffect;
    }



    private void Start()
    {
        CreatePool();
    }

    /// <summary>
    /// 生成对象池
    /// </summary>
    private void CreatePool()
    {
        foreach (GameObject item in poolPrefabs)
        {
            Transform parent = new GameObject(item.name).transform;
            parent.SetParent(transform);

            var newPool = new ObjectPool<GameObject>(
                () => Instantiate(item, parent),
                e => { e.SetActive(true); },
                e => { e.SetActive(false); },
                e => { Destroy(e); }
            );

            poolEffectList.Add(newPool);
        }
    }

    //private void onparticleeffectevent(particleeffecttype effecttype, vector3 pos)
    //{
    //    //workflow:根据特效补全
    //    objectpool<gameobject> objpool = effecttype switch
    //    {
    //        particleeffecttype.leavesfalling01 => pooleffectlist[0],
    //        particleeffecttype.leavesfalling02 => pooleffectlist[1],
    //        particleeffecttype.rock => pooleffectlist[2],
    //        particleeffecttype.reapablescenery => pooleffectlist[3],
    //        _ => null,
    //    };

    //    gameobject obj = objpool.get();
    //    obj.transform.position = pos;
    //    startcoroutine(releaseroutine(objpool, obj));
    //}

    private IEnumerator ReleaseRoutine(ObjectPool<GameObject> pool, GameObject obj)
    {
        yield return new WaitForSeconds(1.5f);
        pool.Release(obj);
    }


    //private void InitSoundEffect(SoundDetails soundDetails)
    //{
    //    ObjectPool<GameObject> pool = poolEffectList[0];
    //    var obj = pool.Get();

    //    obj.GetComponent<Sound>().SetSound(soundDetails);
    //    StartCoroutine(DisableSound(pool, obj, soundDetails));
    //}

    //private IEnumerator DisableSound(ObjectPool<GameObject> pool, GameObject obj, SoundDetails soundDetails)
    //{
    //    yield return new WaitForSeconds(soundDetails.soundClip.length);
    //    pool.Release(obj);
    //}

    private void CreateSoundPool()
    {
        var parent = new GameObject(poolPrefabs[0].name).transform;
        parent.SetParent(transform);

        for (int i = 0; i < 20; i++)
        {
            GameObject newObj = Instantiate(poolPrefabs[0], parent);
            newObj.SetActive(false);
            soundQueue.Enqueue(newObj);
        }
    }

    private GameObject GetPoolObject()
    {
        if (soundQueue.Count < 2)
            CreateSoundPool();
        return soundQueue.Dequeue();
    }

    private void InitSoundEffect(SoundDetails soundDetails)
    {
        var obj = GetPoolObject();
        obj.GetComponent<Sound>().SetSound(soundDetails);
        obj.SetActive(true);
        StartCoroutine(DisableSound(obj, soundDetails.soundClip.length));
    }

    private IEnumerator DisableSound(GameObject obj, float duration)
    {
        yield return new WaitForSeconds(duration);
        obj.SetActive(false);
        soundQueue.Enqueue(obj);
    }
}

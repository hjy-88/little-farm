using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AdvancedSceneLoader : MonoBehaviour
{
    public int mainSceneIndex = 1;
    public int[] additionalScenes;
    public GameObject loadingPanel; // ���ؽ���UI

    public void StartGame()
    {
        StartCoroutine(LoadScenesWithProgress());
    }

    IEnumerator LoadScenesWithProgress()
    {
        loadingPanel.SetActive(true);

        // ����������
        AsyncOperation mainOp = SceneManager.LoadSceneAsync(mainSceneIndex, LoadSceneMode.Single);
        while (!mainOp.isDone)
        {
            UpdateProgressBar(mainOp.progress);
            yield return null;
        }

        // ���м��ظ��ӳ���
        foreach (int sceneIdx in additionalScenes)
        {
            AsyncOperation addOp = SceneManager.LoadSceneAsync(sceneIdx, LoadSceneMode.Additive);
            while (!addOp.isDone)
                yield return null;
        }

        loadingPanel.SetActive(false);
    }

    void UpdateProgressBar(float progress)
    {
        // ����UI�������߼�
    }
}
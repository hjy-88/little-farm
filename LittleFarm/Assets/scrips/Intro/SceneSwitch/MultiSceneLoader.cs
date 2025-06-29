using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI; // ���UI�����ռ�

public class MultiSceneLoader : MonoBehaviour
{
    public string[] scenesToLoad; // ����صĳ���������
    public string sceneToUnload;  // ��ж�صĳ�����

    // ��ʼ����������
    public void InitializeScenes(string[] scenesToLoad, string sceneToUnload = "")
    {
        this.scenesToLoad = scenesToLoad;
        this.sceneToUnload = sceneToUnload;
    }

    // ��ʼ���س������ɰ�ť�¼����ã�
    public void StartLoadScenes()
    {
        StartCoroutine(LoadScenes());
    }

    IEnumerator LoadScenes()
    {
        // ��鳡������
        if (scenesToLoad == null || scenesToLoad.Length == 0)
        {
            Debug.LogError("û��ָ��Ҫ���صĳ�����");
            yield break;
        }

        // ���������³���������ģʽ��
        AsyncOperation[] operations = new AsyncOperation[scenesToLoad.Length];
        for (int i = 0; i < scenesToLoad.Length; i++)
        {
            operations[i] = SceneManager.LoadSceneAsync(scenesToLoad[i], LoadSceneMode.Additive);
            Debug.Log($"��ʼ���س���: {scenesToLoad[i]}");
        }

        // �ȴ����г����������
        foreach (var op in operations)
        {
            while (!op.isDone)
            {
                // ����Ӽ��ؽ����������߼�
                yield return null;
            }
            Debug.Log("�����������");
        }

        // ж�ؾɳ�������ѡ��
        if (!string.IsNullOrEmpty(sceneToUnload))
        {
            Debug.Log($"��ʼж�س���: {sceneToUnload}");
            yield return SceneManager.UnloadSceneAsync(sceneToUnload);
            Debug.Log("����ж�����");
        }

        // ���������������ƹ���/��Ƶ�����ã�
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenesToLoad[0]));
        Debug.Log($"����������: {scenesToLoad[0]}");
    }
}
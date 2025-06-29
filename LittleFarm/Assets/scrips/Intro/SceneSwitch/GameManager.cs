// GameManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("��������")]
    public int mainMenuSceneIndex = 1;
    public int mainGameSceneIndex = 2;
    public int[] additiveScenes = { 3, 4 };

    [Header("�־û�����")]
    public GameObject audioManagerPrefab;

    private GameObject audioManagerInstance;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogError("���ֶ��GameManagerʵ����");
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("GameManagerʵ���Ѵ���");
        Initialize();
    }

    void Initialize()
    {
        if (audioManagerInstance == null && audioManagerPrefab != null)
        {
            audioManagerInstance = Instantiate(audioManagerPrefab);
            DontDestroyOnLoad(audioManagerInstance);
            Debug.Log("��Ƶ�������Ѵ���");
        }

        Debug.Log($"�������˵�����: {mainMenuSceneIndex}");
        SceneManager.LoadScene(mainMenuSceneIndex, LoadSceneMode.Additive);
    }

    public void LoadGameScenes()
    {
        Debug.Log("����LoadGameScenes()");
        StartCoroutine(LoadGameScenesCoroutine());
    }

    private IEnumerator LoadGameScenesCoroutine()
    {
        Debug.Log($"ж�����˵�����: {mainMenuSceneIndex}");
        SceneManager.UnloadSceneAsync(mainMenuSceneIndex);

        Debug.Log($"��������Ϸ����: {mainGameSceneIndex}");
        AsyncOperation mainLoad = SceneManager.LoadSceneAsync(mainGameSceneIndex, LoadSceneMode.Additive);
        yield return mainLoad;

        Debug.Log($"���û����: {mainGameSceneIndex}");
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(mainGameSceneIndex));

        Debug.Log($"���ظ��ӳ���: {string.Join(", ", additiveScenes)}");
        foreach (int sceneIndex in additiveScenes)
        {
            SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        }

        Debug.Log("�����������");
        yield break;
    }
}
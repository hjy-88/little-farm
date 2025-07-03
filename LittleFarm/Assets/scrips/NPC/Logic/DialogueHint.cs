using UnityEngine;
using UnityEngine.UI;

public class DialogueHint : MonoBehaviour
{
    public GameObject hintUIPrefab; // 对话提示UI的预制体
    private GameObject hintUIInstance;
    private Transform player;
    private bool playerInRange = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (hintUIPrefab != null)
        {
            hintUIInstance = Instantiate(hintUIPrefab, GameObject.Find("MainCanvas").transform);
            hintUIInstance.SetActive(false);
        }
    }

    void Update()
    {
        if (playerInRange && hintUIInstance != null)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
            hintUIInstance.transform.position = screenPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            EventHandler.CallPlaySoundEvent(SoundName.Meow);
            if (hintUIInstance != null) hintUIInstance.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (hintUIInstance != null) hintUIInstance.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (hintUIInstance != null)
        {
            Destroy(hintUIInstance);
        }
    }
}
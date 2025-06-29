using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour
{
    [Header("״̬Sprites")]
    public Sprite onSprite;
    public Sprite offSprite;

    private Image buttonImage;
    private bool isOn = false;

    void Start()
    {
        buttonImage = GetComponent<Image>();
        GetComponent<Button>().onClick.AddListener(ToggleState);
    }

    public void ToggleState()
    {
        isOn = !isOn;
        buttonImage.sprite = isOn ? onSprite : offSprite;

        // ���������ӿ���״̬�仯���߼�
        Debug.Log("Switch state: " + (isOn ? "ON" : "OFF"));
    }
}
//// ����SFX���ذ�ť
//using UnityEngine;
//using UnityEngine.UI;

//public class SFXToggleButton : MonoBehaviour
//{
//    public Image toggleImage; // ������ʾ����״̬��ͼƬ
//    public Sprite onSprite;   // SFX����ʱ��ͼƬ
//    public Sprite offSprite;  // SFX�ر�ʱ��ͼƬ

//    void Start()
//    {
//        UpdateButtonImage();
//        GetComponent<Button>().onClick.AddListener(ToggleSFX);
//    }

//    private void ToggleSFX()
//    {
//        SoundManager.Instance.ToggleSFX();
//        UpdateButtonImage();
//    }

//    private void UpdateButtonImage()
//    {
//        if (toggleImage != null && onSprite != null && offSprite != null)
//        {
//            toggleImage.sprite = SoundManager.Instance.IsSFXOn() ? onSprite : offSprite;
//        }
//    }
//}

//using UnityEngine;
//using UnityEngine.UI;

//public class SFXToggleButton : MonoBehaviour
//{
//    public Image toggleImage;
//    public Sprite onSprite;
//    public Sprite offSprite;

//    private void OnEnable()
//    {
//        // �����¼�
//        SoundManager.OnSFXStateChanged += UpdateButtonImage;
//        // ��ʼ����ť״̬
//        UpdateButtonImage(SoundManager.Instance?.IsSFXOn() ?? true);
//    }

//    private void OnDisable()
//    {
//        SoundManager.OnSFXStateChanged -= UpdateButtonImage;
//    }

//    public void OnButtonClick()
//    {
//        SoundManager.Instance?.ToggleSFX();
//    }

//    private void UpdateButtonImage(bool isOn)
//    {
//        if (toggleImage != null && onSprite != null && offSprite != null)
//        {
//            toggleImage.sprite = isOn ? onSprite : offSprite;
//        }
//    }
//}

using UnityEngine;
using UnityEngine.UI;

public class IntroSFXToggleButton : MonoBehaviour
{
    public Image toggleImage;
    public Sprite onSprite;
    public Sprite offSprite;

    private void OnEnable()
    {
        SoundManager.OnSFXStateChanged += UpdateButtonImage;
        UpdateButtonImage(SoundManager.Instance?.IsSFXOn() ?? true);
    }

    private void OnDisable()
    {
        SoundManager.OnSFXStateChanged -= UpdateButtonImage;
    }

    public void OnButtonClick()
    {
        SoundManager.Instance?.ToggleSFX();
    }

    private void UpdateButtonImage(bool isOn)
    {
        if (toggleImage != null && onSprite != null && offSprite != null)
        {
            toggleImage.sprite = isOn ? onSprite : offSprite;
        }
    }
}
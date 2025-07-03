//// ����BGM���ذ�ť
//using UnityEngine;
//using UnityEngine.UI;

//public class BGMToggleButton : MonoBehaviour
//{
//    public Image toggleImage; // ������ʾ����״̬��ͼƬ
//    public Sprite onSprite;  // BGM����ʱ��ͼƬ
//    public Sprite offSprite; // BGM�ر�ʱ��ͼƬ

//    void Start()
//    {
//        UpdateButtonImage();
//        GetComponent<Button>().onClick.AddListener(ToggleBGM);
//    }

//    private void ToggleBGM()
//    {
//        SoundManager.Instance.ToggleBGM();
//        UpdateButtonImage();
//    }

//    private void UpdateButtonImage()
//    {
//        if (toggleImage != null && onSprite != null && offSprite != null)
//        {
//            toggleImage.sprite = SoundManager.Instance.IsBGMOn() ? onSprite : offSprite;
//        }
//    }
//}
//using UnityEngine;
//using UnityEngine.UI;

//public class BGMToggleButton : MonoBehaviour
//{
//    public Image toggleImage;
//    public Sprite onSprite;
//    public Sprite offSprite;

//    private void OnEnable()
//    {
//        // �����¼�
//        SoundManager.OnBGMStateChanged += UpdateButtonImage;
//        // ��ʼ����ť״̬
//        UpdateButtonImage(SoundManager.Instance?.IsBGMOn() ?? true);
//    }

//    private void OnDisable()
//    {
//        SoundManager.OnBGMStateChanged -= UpdateButtonImage;
//    }

//    public void OnButtonClick()
//    {
//        SoundManager.Instance?.ToggleBGM();
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

public class IntroBGMToggleButton : MonoBehaviour
{
    public Image toggleImage;
    public Sprite onSprite;
    public Sprite offSprite;

    private void OnEnable()
    {
        IntroSoundManager.OnBGMStateChanged += UpdateButtonImage;
        UpdateButtonImage(IntroSoundManager.Instance?.IsBGMOn() ?? true);
    }

    private void OnDisable()
    {
        IntroSoundManager.OnBGMStateChanged -= UpdateButtonImage;
    }

    public void OnButtonClick()
    {
        IntroSoundManager.Instance?.ToggleBGM();
    }

    private void UpdateButtonImage(bool isOn)
    {
        if (toggleImage != null && onSprite != null && offSprite != null)
        {
            toggleImage.sprite = isOn ? onSprite : offSprite;
        }
    }
}
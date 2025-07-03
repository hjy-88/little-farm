//// 对于BGM开关按钮
//using UnityEngine;
//using UnityEngine.UI;

//public class BGMToggleButton : MonoBehaviour
//{
//    public Image toggleImage; // 用于显示开关状态的图片
//    public Sprite onSprite;  // BGM开启时的图片
//    public Sprite offSprite; // BGM关闭时的图片

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
//        // 订阅事件
//        SoundManager.OnBGMStateChanged += UpdateButtonImage;
//        // 初始化按钮状态
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
//// 对于SFX开关按钮
//using UnityEngine;
//using UnityEngine.UI;

//public class SFXToggleButton : MonoBehaviour
//{
//    public Image toggleImage; // 用于显示开关状态的图片
//    public Sprite onSprite;   // SFX开启时的图片
//    public Sprite offSprite;  // SFX关闭时的图片

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
//        // 订阅事件
//        SoundManager.OnSFXStateChanged += UpdateButtonImage;
//        // 初始化按钮状态
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
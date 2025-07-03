using UnityEngine;
using UnityEngine.UI;

public class GameSFXToggleButton : MonoBehaviour
{
    public Image toggleImage;       // 按钮图标引用
    public Sprite onSprite;         // 开启状态图标
    public Sprite offSprite;        // 关闭状态图标

    private void OnEnable()
    {
        // 订阅SFX状态变更事件
        GameSoundManager.OnSFXStateChanged += UpdateButtonImage;
        // 初始化按钮状态（默认开启）
        UpdateButtonImage(GameSoundManager.Instance?.IsSFXOn() ?? true);
    }

    private void OnDisable()
    {
        // 取消订阅事件
        GameSoundManager.OnSFXStateChanged -= UpdateButtonImage;
    }

    // 按钮点击事件处理
    public void OnButtonClick()
    {
        GameSoundManager.Instance?.ToggleSFX();  // 切换SFX状态
    }

    // 更新按钮图标
    private void UpdateButtonImage(bool isOn)
    {
        if (toggleImage != null && onSprite != null && offSprite != null)
        {
            toggleImage.sprite = isOn ? onSprite : offSprite;
        }
    }
}
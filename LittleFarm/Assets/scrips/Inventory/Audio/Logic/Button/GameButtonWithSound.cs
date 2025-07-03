using UnityEngine;
using UnityEngine.UI;

public class GameButtonWithSound : MonoBehaviour
{
    [Header("音效配置")]
    public bool playOnClick = true;           // 是否在点击时播放音效
    public AudioClip customClickSound;        // 自定义点击音效（可选）

    private Button button;                    // 按钮组件引用
    private GameSoundManager soundManager;    // 音频管理器引用

    void Start()
    {
        // 获取按钮组件
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("GameButtonWithSound需要绑定到Button组件上");
            return;
        }

        // 获取音频管理器
        soundManager = GameSoundManager.Instance;
        if (soundManager == null)
        {
            Debug.LogError("未找到GameSoundManager实例，请先添加GameSoundManager组件");
            return;
        }

        // 添加点击事件监听
        if (playOnClick)
        {
            button.onClick.AddListener(PlayButtonSound);
        }
    }

    // 播放按钮点击音效
    public void PlayButtonSound()
    {
        if (soundManager == null || soundManager.sfxSource == null) return;

        // 优先播放自定义音效，若无则使用音频管理器的默认音效
        if (customClickSound != null && soundManager.IsSFXOn())
        {
            soundManager.sfxSource.PlayOneShot(customClickSound);
        }
        else if (soundManager.buttonClickSound != null && soundManager.IsSFXOn())
        {
            soundManager.PlayButtonClickSound();
        }
    }

    // 手动触发音效播放（可指定音效）
    public void PlayCustomSound(AudioClip clip, float volume = 1f)
    {
        if (soundManager == null || soundManager.sfxSource == null || clip == null) return;
        if (soundManager.IsSFXOn())
        {
            soundManager.PlayGameEffectSound(clip, volume);
        }
    }

    // 当按钮交互状态改变时的回调（可选）
    void OnValidate()
    {
        // 在编辑器中更新配置
        if (Application.isPlaying) return;

        if (button == null)
            button = GetComponent<Button>();
    }
}
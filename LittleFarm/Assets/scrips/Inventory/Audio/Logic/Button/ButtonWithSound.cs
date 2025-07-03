//using UnityEngine;
//using UnityEngine.UI;

//public class ButtonWithSound : MonoBehaviour
//{
//    void Start()
//    {
//        Button button = GetComponent<Button>();
//        if (button != null)
//        {
//            button.onClick.AddListener(PlayButtonSound);
//        }
//    }

//    private void PlayButtonSound()
//    {
//        SoundManager.Instance.PlayButtonClickSound();
//    }
//}

using UnityEngine;
using UnityEngine.UI;

public class IntroButtonWithSound : MonoBehaviour
{
    [Header("音效配置")]
    public bool playOnClick = true;
    public AudioClip customClickSound;

    private Button button;
    private IntroSoundManager soundManager;

    void Start()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("IntroButtonWithSound需要绑定到Button组件上");
            return;
        }

        soundManager = IntroSoundManager.Instance;
        if (soundManager == null)
        {
            Debug.LogError("未找到IntroSoundManager实例，请先添加IntroSoundManager组件");
            return;
        }

        if (playOnClick)
        {
            button.onClick.AddListener(PlayButtonSound);
        }
    }

    public void PlayButtonSound()
    {
        if (soundManager == null || soundManager.sfxSource == null) return;

        if (customClickSound != null && soundManager.IsSFXOn())
        {
            soundManager.sfxSource.PlayOneShot(customClickSound);
        }
        else if (soundManager.buttonClickSound != null && soundManager.IsSFXOn())
        {
            soundManager.PlayButtonClickSound();
        }
    }

    public void ManualPlaySound()
    {
        PlayButtonSound();
    }
}
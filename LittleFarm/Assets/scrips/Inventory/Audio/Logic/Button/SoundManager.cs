//using UnityEngine;
//using UnityEngine.Audio;

//public class SoundManager : MonoBehaviour
//{
//    public static SoundManager Instance { get; private set; }

//    [Header("音频混合器")]
//    public AudioMixer audioMixer;

//    [Header("音效")]
//    public AudioClip buttonClickSound;

//    [Header("音频源")]
//    public AudioSource bgmSource;
//    public AudioSource sfxSource;

//    private bool isBgmOn = true;
//    private bool isSfxOn = true;

//    private const string BGM_VOLUME_KEY = "BGMVolume";
//    private const string SFX_VOLUME_KEY = "SFXVolume";

//    void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//        }
//        else
//        {
//            Destroy(gameObject);
//            return;
//        }

//        // 初始化音量设置
//        isBgmOn = PlayerPrefs.GetInt(BGM_VOLUME_KEY, 1) == 1;
//        isSfxOn = PlayerPrefs.GetInt(SFX_VOLUME_KEY, 1) == 1;

//        SetBGMVolume(isBgmOn);
//        SetSFXVolume(isSfxOn);
//    }

//    public void PlayButtonClickSound()
//    {
//        if (isSfxOn && buttonClickSound != null)
//        {
//            sfxSource.PlayOneShot(buttonClickSound);
//        }
//    }

//    public void ToggleBGM()
//    {
//        isBgmOn = !isBgmOn;
//        SetBGMVolume(isBgmOn);
//        PlayerPrefs.SetInt(BGM_VOLUME_KEY, isBgmOn ? 1 : 0);
//    }

//    public void ToggleSFX()
//    {
//        isSfxOn = !isSfxOn;
//        SetSFXVolume(isSfxOn);
//        PlayerPrefs.SetInt(SFX_VOLUME_KEY, isSfxOn ? 1 : 0);
//    }

//    private void SetBGMVolume(bool isOn)
//    {
//        // audioMixer.SetFloat("BGMVolume", isOn ? 0f : -80f);
//        bgmSource.mute = !isOn;
//    }

//    private void SetSFXVolume(bool isOn)
//    {
//        //audioMixer.SetFloat("SFXVolume", isOn ? 0f : -80f);
//        sfxSource.mute = !isOn;
//    }

//    public bool IsBGMOn()
//    {
//        return isBgmOn;
//    }

//    public bool IsSFXOn()
//    {
//        return isSfxOn;
//    }
//}


//using UnityEngine;
//using UnityEngine.Audio;

//public class SoundManager : MonoBehaviour
//{
//    public static SoundManager Instance { get; private set; }

//    [Header("Audio Clips")]
//    public AudioClip buttonClickSound;

//    [Header("Audio Sources")]
//    public AudioSource bgmSource;
//    public AudioSource sfxSource;

//    [Header("Audio Mixer (Optional)")]
//    public AudioMixer audioMixer;

//    private bool isBgmOn = true;
//    private bool isSfxOn = true;

//    private const string BGM_VOLUME_KEY = "BGMVolume";
//    private const string SFX_VOLUME_KEY = "SFXVolume";

//    void Awake()
//    {
//        // 单例模式实现
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//            InitializeAudio();
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void InitializeAudio()
//    {
//        // 确保AudioSource组件存在
//        if (bgmSource == null) bgmSource = gameObject.AddComponent<AudioSource>();
//        if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();

//        // 初始化音频源设置
//        bgmSource.playOnAwake = false;
//        bgmSource.loop = true;
//        sfxSource.playOnAwake = false;

//        // 加载保存的设置
//        LoadAudioSettings();
//    }

//    private void LoadAudioSettings()
//    {
//        isBgmOn = PlayerPrefs.GetInt(BGM_VOLUME_KEY, 1) == 1;
//        isSfxOn = PlayerPrefs.GetInt(SFX_VOLUME_KEY, 1) == 1;

//        ApplyAudioSettings();
//    }

//    private void ApplyAudioSettings()
//    {
//        // 应用BGM设置
//        if (audioMixer != null)
//        {
//            audioMixer.SetFloat("BGMVolume", isBgmOn ? 0f : -80f);
//            audioMixer.SetFloat("SFXVolume", isSfxOn ? 0f : -80f);
//        }
//        else
//        {
//            bgmSource.volume = isBgmOn ? 1f : 0f;
//            sfxSource.volume = isSfxOn ? 1f : 0f;
//        }
//    }

//    public void PlayButtonClickSound()
//    {
//        if (isSfxOn && buttonClickSound != null)
//        {
//            sfxSource.PlayOneShot(buttonClickSound);
//        }
//    }

//    public void ToggleBGM()
//    {
//        isBgmOn = !isBgmOn;
//        PlayerPrefs.SetInt(BGM_VOLUME_KEY, isBgmOn ? 1 : 0);
//        ApplyAudioSettings();
//        Debug.Log($"BGM toggled to: {isBgmOn}");
//    }

//    public void ToggleSFX()
//    {
//        isSfxOn = !isSfxOn;
//        PlayerPrefs.SetInt(SFX_VOLUME_KEY, isSfxOn ? 1 : 0);
//        ApplyAudioSettings();
//        Debug.Log($"SFX toggled to: {isSfxOn}");
//    }

//    public bool IsBGMOn() => isBgmOn;
//    public bool IsSFXOn() => isSfxOn;
//}

//using UnityEngine;
//using UnityEngine.Audio;

//public class SoundManager : MonoBehaviour
//{
//    public static SoundManager Instance { get; private set; }

//    [Header("Audio Clips")]
//    public AudioClip buttonClickSound;

//    [Header("Audio Sources")]
//    public AudioSource bgmSource;
//    public AudioSource sfxSource;

//    [Header("Audio Mixer (Optional)")]
//    public AudioMixer audioMixer;

//    // 事件通知按钮状态变化
//    public static event System.Action<bool> OnBGMStateChanged;
//    public static event System.Action<bool> OnSFXStateChanged;

//    private bool isBgmOn = true;
//    private bool isSfxOn = true;

//    private const string BGM_VOLUME_KEY = "BGMVolume";
//    private const string SFX_VOLUME_KEY = "SFXVolume";

//    void Awake()
//    {
//        if (Instance == null)
//        {
//            Instance = this;
//            DontDestroyOnLoad(gameObject);
//            InitializeAudio();
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }

//    private void InitializeAudio()
//    {
//        // 确保AudioSource组件存在
//        if (bgmSource == null) bgmSource = gameObject.AddComponent<AudioSource>();
//        if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();

//        // 初始化音频源设置
//        bgmSource.playOnAwake = false;
//        bgmSource.loop = true;
//        sfxSource.playOnAwake = false;

//        // 强制重置为默认开启状态（首次运行时）
//        if (!PlayerPrefs.HasKey(BGM_VOLUME_KEY)) PlayerPrefs.SetInt(BGM_VOLUME_KEY, 1);
//        if (!PlayerPrefs.HasKey(SFX_VOLUME_KEY)) PlayerPrefs.SetInt(SFX_VOLUME_KEY, 1);

//        // 加载设置
//        LoadAudioSettings();
//    }

//    private void LoadAudioSettings()
//    {
//        isBgmOn = PlayerPrefs.GetInt(BGM_VOLUME_KEY, 1) == 1;
//        isSfxOn = PlayerPrefs.GetInt(SFX_VOLUME_KEY, 1) == 1;

//        ApplyAudioSettings();

//        // 通知所有监听者初始状态
//        OnBGMStateChanged?.Invoke(isBgmOn);
//        OnSFXStateChanged?.Invoke(isSfxOn);
//    }

//    private void ApplyAudioSettings()
//    {
//        // 使用AudioMixer的情况
//        if (audioMixer != null)
//        {
//            audioMixer.SetFloat("BGMVolume", isBgmOn ? 0f : -80f);
//            audioMixer.SetFloat("SFXVolume", isSfxOn ? 0f : -80f);
//        }
//        // 不使用AudioMixer的情况
//        else
//        {
//            bgmSource.volume = isBgmOn ? 1f : 0f;
//            sfxSource.volume = isSfxOn ? 1f : 0f;
//        }
//    }

//    public void PlayButtonClickSound()
//    {
//        if (isSfxOn && buttonClickSound != null)
//        {
//            sfxSource.PlayOneShot(buttonClickSound);
//        }
//    }

//    public void ToggleBGM()
//    {
//        isBgmOn = !isBgmOn;
//        PlayerPrefs.SetInt(BGM_VOLUME_KEY, isBgmOn ? 1 : 0);
//        ApplyAudioSettings();
//        OnBGMStateChanged?.Invoke(isBgmOn);
//        Debug.Log($"BGM toggled to: {isBgmOn}");
//    }

//    public void ToggleSFX()
//    {
//        isSfxOn = !isSfxOn;
//        PlayerPrefs.SetInt(SFX_VOLUME_KEY, isSfxOn ? 1 : 0);
//        ApplyAudioSettings();
//        OnSFXStateChanged?.Invoke(isSfxOn);
//        Debug.Log($"SFX toggled to: {isSfxOn}");
//    }

//    public bool IsBGMOn() => isBgmOn;
//    public bool IsSFXOn() => isSfxOn;
//}
using UnityEngine;
using UnityEngine.Audio;

public class IntroSoundManager : MonoBehaviour
{
    public static IntroSoundManager Instance { get; private set; }

    [Header("音频剪辑")]
    public AudioClip buttonClickSound;        // 按钮点击音效

    [Header("音频源")]
    public AudioSource bgmSource;             // BGM音频源
    public AudioSource sfxSource;             // SFX音频源

    [Header("音频混音器（可选）")]
    public AudioMixer audioMixer;             // 音频混音器引用

    // 事件通知状态变化
    public static event System.Action<bool> OnBGMStateChanged;
    public static event System.Action<bool> OnSFXStateChanged;

    private bool isBgmOn = true;              // BGM开关状态
    private bool isSfxOn = true;              // SFX开关状态

    private const string INTRO_BGM_KEY = "IntroBGMState";    // Intro界面BGM状态键
    private const string INTRO_SFX_KEY = "IntroSFXState";    // Intro界面SFX状态键

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeAudio();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudio()
    {
        // 确保AudioSource组件存在
        if (bgmSource == null) bgmSource = gameObject.AddComponent<AudioSource>();
        if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();

        // 初始化音频源设置
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
        sfxSource.playOnAwake = false;
        sfxSource.volume = 1f;

        // 首次运行时设置默认开启状态
        if (!PlayerPrefs.HasKey(INTRO_BGM_KEY)) PlayerPrefs.SetInt(INTRO_BGM_KEY, 1);
        if (!PlayerPrefs.HasKey(INTRO_SFX_KEY)) PlayerPrefs.SetInt(INTRO_SFX_KEY, 1);

        // 加载设置
        LoadAudioSettings();
    }

    private void LoadAudioSettings()
    {
        isBgmOn = PlayerPrefs.GetInt(INTRO_BGM_KEY, 1) == 1;
        isSfxOn = PlayerPrefs.GetInt(INTRO_SFX_KEY, 1) == 1;

        ApplyAudioSettings();

        // 通知所有监听者初始状态
        OnBGMStateChanged?.Invoke(isBgmOn);
        OnSFXStateChanged?.Invoke(isSfxOn);
    }

    private void ApplyAudioSettings()
    {
        // 使用AudioMixer的情况
        if (audioMixer != null)
        {
            audioMixer.SetFloat("BGMVolume", isBgmOn ? 0f : -80f);
            audioMixer.SetFloat("SFXVolume", isSfxOn ? 0f : -80f);
        }
        // 不使用AudioMixer的情况
        else
        {
            bgmSource.volume = isBgmOn ? 1f : 0f;
            sfxSource.volume = isSfxOn ? 1f : 0f;
        }
    }

    // 播放按钮点击音效
    public void PlayButtonClickSound()
    {
        if (sfxSource == null) return; // 防止空引用
        if (isSfxOn && buttonClickSound != null)
        {
            sfxSource.PlayOneShot(buttonClickSound);
        }
    }

    // 切换BGM状态
    public void ToggleBGM()
    {
        isBgmOn = !isBgmOn;
        PlayerPrefs.SetInt(INTRO_BGM_KEY, isBgmOn ? 1 : 0);
        ApplyAudioSettings();
        OnBGMStateChanged?.Invoke(isBgmOn);
    }

    // 切换SFX状态
    public void ToggleSFX()
    {
        isSfxOn = !isSfxOn;
        PlayerPrefs.SetInt(INTRO_SFX_KEY, isSfxOn ? 1 : 0);
        ApplyAudioSettings();
        OnSFXStateChanged?.Invoke(isSfxOn);
    }

    // 获取当前状态
    public bool IsBGMOn() => isBgmOn;
    public bool IsSFXOn() => isSfxOn;

    // 场景切换时销毁实例
    public void DestroyOnSceneChange()
    {
        if (Instance == this)
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
}
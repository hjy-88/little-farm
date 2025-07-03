using UnityEngine;
using UnityEngine.Audio;

public class GameSoundManager : MonoBehaviour
{
    public static GameSoundManager Instance { get; private set; }

    [Header("音频剪辑")]
    public AudioClip buttonClickSound;        // 按钮点击音效
    public AudioClip gameEffectSound;         // 游戏内音效

    [Header("音频源")]
    public AudioSource sfxSource;             // SFX音频源

    [Header("音频混音器（可选）")]
    public AudioMixer audioMixer;             // 音频混音器引用

    // 事件通知SFX状态变化
    public static event System.Action<bool> OnSFXStateChanged;

    private bool isSfxOn = true;              // SFX开关状态

    private const string GAME_SFX_KEY = "GameSFXState";      // 游戏场景SFX状态键

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();

        // 初始化音频源设置
        sfxSource.playOnAwake = false;
        sfxSource.volume = 1f;

        // 首次运行时设置默认开启状态
        if (!PlayerPrefs.HasKey(GAME_SFX_KEY))
            PlayerPrefs.SetInt(GAME_SFX_KEY, 1);

        // 加载设置
        LoadAudioSettings();
    }

    private void LoadAudioSettings()
    {
        isSfxOn = PlayerPrefs.GetInt(GAME_SFX_KEY, 1) == 1;
        ApplyAudioSettings();

        // 通知所有监听者初始状态
        OnSFXStateChanged?.Invoke(isSfxOn);
    }

    private void ApplyAudioSettings()
    {
        // 使用AudioMixer的情况
        if (audioMixer != null)
        {
            audioMixer.SetFloat("SFXVolume", isSfxOn ? 0f : -80f);
        }
        // 不使用AudioMixer的情况
        else
        {
            sfxSource.volume = isSfxOn ? 1f : 0f;
        }
    }

    // 播放按钮点击音效
    public void PlayButtonClickSound()
    {
        if (sfxSource == null) return;
        if (isSfxOn && buttonClickSound != null)
        {
            sfxSource.PlayOneShot(buttonClickSound);
        }
    }

    // 播放游戏内音效
    public void PlayGameEffectSound(AudioClip clip)
    {
        if (sfxSource == null || clip == null) return;
        if (isSfxOn)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // 播放游戏内音效（带音量控制）
    public void PlayGameEffectSound(AudioClip clip, float volumeScale)
    {
        if (sfxSource == null || clip == null) return;
        if (isSfxOn)
        {
            sfxSource.PlayOneShot(clip, volumeScale);
        }
    }

    // 切换SFX状态
    public void ToggleSFX()
    {
        isSfxOn = !isSfxOn;
        PlayerPrefs.SetInt(GAME_SFX_KEY, isSfxOn ? 1 : 0);
        ApplyAudioSettings();
        OnSFXStateChanged?.Invoke(isSfxOn);
    }

    // 获取当前SFX状态
    public bool IsSFXOn() => isSfxOn;
}
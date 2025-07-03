//using UnityEngine;
//using UnityEngine.Audio;

//public class SoundManager : MonoBehaviour
//{
//    public static SoundManager Instance { get; private set; }

//    [Header("��Ƶ�����")]
//    public AudioMixer audioMixer;

//    [Header("��Ч")]
//    public AudioClip buttonClickSound;

//    [Header("��ƵԴ")]
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

//        // ��ʼ����������
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
//        // ����ģʽʵ��
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
//        // ȷ��AudioSource�������
//        if (bgmSource == null) bgmSource = gameObject.AddComponent<AudioSource>();
//        if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();

//        // ��ʼ����ƵԴ����
//        bgmSource.playOnAwake = false;
//        bgmSource.loop = true;
//        sfxSource.playOnAwake = false;

//        // ���ر��������
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
//        // Ӧ��BGM����
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

//    // �¼�֪ͨ��ť״̬�仯
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
//        // ȷ��AudioSource�������
//        if (bgmSource == null) bgmSource = gameObject.AddComponent<AudioSource>();
//        if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();

//        // ��ʼ����ƵԴ����
//        bgmSource.playOnAwake = false;
//        bgmSource.loop = true;
//        sfxSource.playOnAwake = false;

//        // ǿ������ΪĬ�Ͽ���״̬���״�����ʱ��
//        if (!PlayerPrefs.HasKey(BGM_VOLUME_KEY)) PlayerPrefs.SetInt(BGM_VOLUME_KEY, 1);
//        if (!PlayerPrefs.HasKey(SFX_VOLUME_KEY)) PlayerPrefs.SetInt(SFX_VOLUME_KEY, 1);

//        // ��������
//        LoadAudioSettings();
//    }

//    private void LoadAudioSettings()
//    {
//        isBgmOn = PlayerPrefs.GetInt(BGM_VOLUME_KEY, 1) == 1;
//        isSfxOn = PlayerPrefs.GetInt(SFX_VOLUME_KEY, 1) == 1;

//        ApplyAudioSettings();

//        // ֪ͨ���м����߳�ʼ״̬
//        OnBGMStateChanged?.Invoke(isBgmOn);
//        OnSFXStateChanged?.Invoke(isSfxOn);
//    }

//    private void ApplyAudioSettings()
//    {
//        // ʹ��AudioMixer�����
//        if (audioMixer != null)
//        {
//            audioMixer.SetFloat("BGMVolume", isBgmOn ? 0f : -80f);
//            audioMixer.SetFloat("SFXVolume", isSfxOn ? 0f : -80f);
//        }
//        // ��ʹ��AudioMixer�����
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

    [Header("��Ƶ����")]
    public AudioClip buttonClickSound;        // ��ť�����Ч

    [Header("��ƵԴ")]
    public AudioSource bgmSource;             // BGM��ƵԴ
    public AudioSource sfxSource;             // SFX��ƵԴ

    [Header("��Ƶ����������ѡ��")]
    public AudioMixer audioMixer;             // ��Ƶ����������

    // �¼�֪ͨ״̬�仯
    public static event System.Action<bool> OnBGMStateChanged;
    public static event System.Action<bool> OnSFXStateChanged;

    private bool isBgmOn = true;              // BGM����״̬
    private bool isSfxOn = true;              // SFX����״̬

    private const string INTRO_BGM_KEY = "IntroBGMState";    // Intro����BGM״̬��
    private const string INTRO_SFX_KEY = "IntroSFXState";    // Intro����SFX״̬��

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
        // ȷ��AudioSource�������
        if (bgmSource == null) bgmSource = gameObject.AddComponent<AudioSource>();
        if (sfxSource == null) sfxSource = gameObject.AddComponent<AudioSource>();

        // ��ʼ����ƵԴ����
        bgmSource.playOnAwake = false;
        bgmSource.loop = true;
        sfxSource.playOnAwake = false;
        sfxSource.volume = 1f;

        // �״�����ʱ����Ĭ�Ͽ���״̬
        if (!PlayerPrefs.HasKey(INTRO_BGM_KEY)) PlayerPrefs.SetInt(INTRO_BGM_KEY, 1);
        if (!PlayerPrefs.HasKey(INTRO_SFX_KEY)) PlayerPrefs.SetInt(INTRO_SFX_KEY, 1);

        // ��������
        LoadAudioSettings();
    }

    private void LoadAudioSettings()
    {
        isBgmOn = PlayerPrefs.GetInt(INTRO_BGM_KEY, 1) == 1;
        isSfxOn = PlayerPrefs.GetInt(INTRO_SFX_KEY, 1) == 1;

        ApplyAudioSettings();

        // ֪ͨ���м����߳�ʼ״̬
        OnBGMStateChanged?.Invoke(isBgmOn);
        OnSFXStateChanged?.Invoke(isSfxOn);
    }

    private void ApplyAudioSettings()
    {
        // ʹ��AudioMixer�����
        if (audioMixer != null)
        {
            audioMixer.SetFloat("BGMVolume", isBgmOn ? 0f : -80f);
            audioMixer.SetFloat("SFXVolume", isSfxOn ? 0f : -80f);
        }
        // ��ʹ��AudioMixer�����
        else
        {
            bgmSource.volume = isBgmOn ? 1f : 0f;
            sfxSource.volume = isSfxOn ? 1f : 0f;
        }
    }

    // ���Ű�ť�����Ч
    public void PlayButtonClickSound()
    {
        if (sfxSource == null) return; // ��ֹ������
        if (isSfxOn && buttonClickSound != null)
        {
            sfxSource.PlayOneShot(buttonClickSound);
        }
    }

    // �л�BGM״̬
    public void ToggleBGM()
    {
        isBgmOn = !isBgmOn;
        PlayerPrefs.SetInt(INTRO_BGM_KEY, isBgmOn ? 1 : 0);
        ApplyAudioSettings();
        OnBGMStateChanged?.Invoke(isBgmOn);
    }

    // �л�SFX״̬
    public void ToggleSFX()
    {
        isSfxOn = !isSfxOn;
        PlayerPrefs.SetInt(INTRO_SFX_KEY, isSfxOn ? 1 : 0);
        ApplyAudioSettings();
        OnSFXStateChanged?.Invoke(isSfxOn);
    }

    // ��ȡ��ǰ״̬
    public bool IsBGMOn() => isBgmOn;
    public bool IsSFXOn() => isSfxOn;

    // �����л�ʱ����ʵ��
    public void DestroyOnSceneChange()
    {
        if (Instance == this)
        {
            Instance = null;
            Destroy(gameObject);
        }
    }
}
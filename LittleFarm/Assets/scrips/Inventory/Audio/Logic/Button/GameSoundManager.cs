using UnityEngine;
using UnityEngine.Audio;

public class GameSoundManager : MonoBehaviour
{
    public static GameSoundManager Instance { get; private set; }

    [Header("��Ƶ����")]
    public AudioClip buttonClickSound;        // ��ť�����Ч
    public AudioClip gameEffectSound;         // ��Ϸ����Ч

    [Header("��ƵԴ")]
    public AudioSource sfxSource;             // SFX��ƵԴ

    [Header("��Ƶ����������ѡ��")]
    public AudioMixer audioMixer;             // ��Ƶ����������

    // �¼�֪ͨSFX״̬�仯
    public static event System.Action<bool> OnSFXStateChanged;

    private bool isSfxOn = true;              // SFX����״̬

    private const string GAME_SFX_KEY = "GameSFXState";      // ��Ϸ����SFX״̬��

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
        // ȷ��AudioSource�������
        if (sfxSource == null)
            sfxSource = gameObject.AddComponent<AudioSource>();

        // ��ʼ����ƵԴ����
        sfxSource.playOnAwake = false;
        sfxSource.volume = 1f;

        // �״�����ʱ����Ĭ�Ͽ���״̬
        if (!PlayerPrefs.HasKey(GAME_SFX_KEY))
            PlayerPrefs.SetInt(GAME_SFX_KEY, 1);

        // ��������
        LoadAudioSettings();
    }

    private void LoadAudioSettings()
    {
        isSfxOn = PlayerPrefs.GetInt(GAME_SFX_KEY, 1) == 1;
        ApplyAudioSettings();

        // ֪ͨ���м����߳�ʼ״̬
        OnSFXStateChanged?.Invoke(isSfxOn);
    }

    private void ApplyAudioSettings()
    {
        // ʹ��AudioMixer�����
        if (audioMixer != null)
        {
            audioMixer.SetFloat("SFXVolume", isSfxOn ? 0f : -80f);
        }
        // ��ʹ��AudioMixer�����
        else
        {
            sfxSource.volume = isSfxOn ? 1f : 0f;
        }
    }

    // ���Ű�ť�����Ч
    public void PlayButtonClickSound()
    {
        if (sfxSource == null) return;
        if (isSfxOn && buttonClickSound != null)
        {
            sfxSource.PlayOneShot(buttonClickSound);
        }
    }

    // ������Ϸ����Ч
    public void PlayGameEffectSound(AudioClip clip)
    {
        if (sfxSource == null || clip == null) return;
        if (isSfxOn)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    // ������Ϸ����Ч�����������ƣ�
    public void PlayGameEffectSound(AudioClip clip, float volumeScale)
    {
        if (sfxSource == null || clip == null) return;
        if (isSfxOn)
        {
            sfxSource.PlayOneShot(clip, volumeScale);
        }
    }

    // �л�SFX״̬
    public void ToggleSFX()
    {
        isSfxOn = !isSfxOn;
        PlayerPrefs.SetInt(GAME_SFX_KEY, isSfxOn ? 1 : 0);
        ApplyAudioSettings();
        OnSFXStateChanged?.Invoke(isSfxOn);
    }

    // ��ȡ��ǰSFX״̬
    public bool IsSFXOn() => isSfxOn;
}
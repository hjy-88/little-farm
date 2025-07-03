using UnityEngine;
using UnityEngine.UI;

public class GameButtonWithSound : MonoBehaviour
{
    [Header("��Ч����")]
    public bool playOnClick = true;           // �Ƿ��ڵ��ʱ������Ч
    public AudioClip customClickSound;        // �Զ�������Ч����ѡ��

    private Button button;                    // ��ť�������
    private GameSoundManager soundManager;    // ��Ƶ����������

    void Start()
    {
        // ��ȡ��ť���
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("GameButtonWithSound��Ҫ�󶨵�Button�����");
            return;
        }

        // ��ȡ��Ƶ������
        soundManager = GameSoundManager.Instance;
        if (soundManager == null)
        {
            Debug.LogError("δ�ҵ�GameSoundManagerʵ�����������GameSoundManager���");
            return;
        }

        // ��ӵ���¼�����
        if (playOnClick)
        {
            button.onClick.AddListener(PlayButtonSound);
        }
    }

    // ���Ű�ť�����Ч
    public void PlayButtonSound()
    {
        if (soundManager == null || soundManager.sfxSource == null) return;

        // ���Ȳ����Զ�����Ч��������ʹ����Ƶ��������Ĭ����Ч
        if (customClickSound != null && soundManager.IsSFXOn())
        {
            soundManager.sfxSource.PlayOneShot(customClickSound);
        }
        else if (soundManager.buttonClickSound != null && soundManager.IsSFXOn())
        {
            soundManager.PlayButtonClickSound();
        }
    }

    // �ֶ�������Ч���ţ���ָ����Ч��
    public void PlayCustomSound(AudioClip clip, float volume = 1f)
    {
        if (soundManager == null || soundManager.sfxSource == null || clip == null) return;
        if (soundManager.IsSFXOn())
        {
            soundManager.PlayGameEffectSound(clip, volume);
        }
    }

    // ����ť����״̬�ı�ʱ�Ļص�����ѡ��
    void OnValidate()
    {
        // �ڱ༭���и�������
        if (Application.isPlaying) return;

        if (button == null)
            button = GetComponent<Button>();
    }
}
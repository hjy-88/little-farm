using UnityEngine;
using UnityEngine.UI;

public class GameSFXToggleButton : MonoBehaviour
{
    public Image toggleImage;       // ��ťͼ������
    public Sprite onSprite;         // ����״̬ͼ��
    public Sprite offSprite;        // �ر�״̬ͼ��

    private void OnEnable()
    {
        // ����SFX״̬����¼�
        GameSoundManager.OnSFXStateChanged += UpdateButtonImage;
        // ��ʼ����ť״̬��Ĭ�Ͽ�����
        UpdateButtonImage(GameSoundManager.Instance?.IsSFXOn() ?? true);
    }

    private void OnDisable()
    {
        // ȡ�������¼�
        GameSoundManager.OnSFXStateChanged -= UpdateButtonImage;
    }

    // ��ť����¼�����
    public void OnButtonClick()
    {
        GameSoundManager.Instance?.ToggleSFX();  // �л�SFX״̬
    }

    // ���°�ťͼ��
    private void UpdateButtonImage(bool isOn)
    {
        if (toggleImage != null && onSprite != null && offSprite != null)
        {
            toggleImage.sprite = isOn ? onSprite : offSprite;
        }
    }
}
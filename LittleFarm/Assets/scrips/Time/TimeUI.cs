using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : MonoBehaviour
{
    public RectTransform dayNightImage;
    public Image seasonImage;
    public TextMeshProUGUI dateText;

    public Sprite[] seasonSprites;

    private List<GameObject> clockBlocks = new List<GameObject>();

    private void Awake()
    {

    }


    private void OnEnable()//ע���¼�
    {
        EventHandler.GameTimeEvent += OnGameTimeEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameTimeEvent -= OnGameTimeEvent;
    }

    private void OnGameTimeEvent(int second,int minute,int hour, int day, int month, int year, Season season)
    {
        dateText.text = year + "��" + month.ToString("00") + "��" + day.ToString("00") + "�� "+ hour.ToString("00") + ":" + minute.ToString("00")+":"+second.ToString("00");
        seasonImage.sprite = seasonSprites[(int)season];

        DayNightImageRotate(hour);
    }

    private void DayNightImageRotate(int hour)
    {
        var target = new Vector3(0, 0, hour * 15 - 90);
        dayNightImage.DORotate(target, 1f, RotateMode.Fast);
    }
}

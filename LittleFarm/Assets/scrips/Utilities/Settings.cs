using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Settings
{
    //public const float itemFadeDuration = 0.35f;
    public const float secondThreshold = 0.00000001f;
    public const int secondHold = 59;
    public const int minuteHold = 59;
    public const int hourHold = 23;
    public const int dayHold = 1;
    public const int seasonHold = 3;
    public const float fadeDuration = 1.5f;

    //light
    public const float lightChangeDuration = 25f;
    public static TimeSpan morningTime = new TimeSpan(5, 0, 0);
    public static TimeSpan nightTime = new TimeSpan(19, 0, 0);

    public static Vector3 playerStartPos = new Vector3(0f, 12f, 0);
    public const int playerStartMoney = 100;
}

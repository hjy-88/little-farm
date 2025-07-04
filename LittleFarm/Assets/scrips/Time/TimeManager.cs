using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private int gameSecond, gameMinute, gameHour, gameDay, gameMonth, gameYear;
    private Season gameSeason = Season.����;
    private int monthInSeason = 3;
    public bool gameClockPause;
    private float tikTime;
    private float timeDifference;
    public TimeSpan GameTime => new TimeSpan(gameHour, gameMinute, gameSecond);
    private void Awake()
    {
        NewGameTime();
    }
    private void Start()
    {
        EventHandler.CallGameTimeEvent(gameSecond, gameMinute, gameHour, gameDay, gameMonth, gameYear, gameSeason);
        EventHandler.CallLightShiftChangeEvent(gameSeason, GetCurrentLightShift(), timeDifference);
    }
    public void Update()
    {
        if(!gameClockPause)
        {
            tikTime += Time.deltaTime;
            if(tikTime>=Settings.secondThreshold)
            {
                tikTime -= Settings.secondThreshold;
                UpdateGameTime();

            }
        }
        if(Input.GetKey(KeyCode.T))
        {
            for(int i=0;i<60;i++)
            {
                UpdateGameTime();
            }
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            gameDay++;
            EventHandler.CallGameDayEvent(gameDay, gameSeason);
            EventHandler.CallGameTimeEvent(gameSecond, gameMinute, gameHour, gameDay, gameMonth, gameYear, gameSeason);
        }
    }
    private void NewGameTime()
    {
        gameSecond = 0;
        gameMinute = 0;
        gameHour = 9;
        gameDay = 1;
        gameMonth = 1;
        gameYear = 2025;
        gameSeason = Season.����;
    }
    private void UpdateGameTime()
    {
        gameSecond++;
        if(gameSecond>Settings.secondHold)
        {
            gameMinute++;
            gameSecond = 0;

            if(gameMinute>Settings.minuteHold)
            {
                gameHour++;
                gameMinute = 0;

                if(gameHour>Settings.hourHold)
                {
                    gameDay++;
                    gameHour = 0;

                    if(gameDay>Settings.dayHold)
                    {
                        gameMonth++;
                        gameDay = 1;

                        if (gameMonth > 12)
                            gameMonth = 1;

                        monthInSeason--;
                        if(monthInSeason==0)
                        {
                            monthInSeason = 3;

                            int seasonNumber = (int)gameSeason;
                            seasonNumber++;

                            if(seasonNumber>Settings.seasonHold)
                            {
                                seasonNumber = 0;
                                gameYear++;
                            }
                            gameSeason = (Season)seasonNumber;
                            if(gameYear>10)
                            {
                                gameYear = 2025;
                            }
                        }
                        EventHandler.CallGameDayEvent(gameDay, gameSeason);
                    }
                }
            }
        }
        //Debug.Log("Second:" + gameSecond + "Minute:" + gameMinute);
        EventHandler.CallGameTimeEvent(gameSecond, gameMinute, gameHour, gameDay, gameMonth, gameYear, gameSeason);

        EventHandler.CallLightShiftChangeEvent(gameSeason, GetCurrentLightShift(), timeDifference);
    }

    private LightShift GetCurrentLightShift()
    {
        if (GameTime >= Settings.morningTime && GameTime < Settings.nightTime)
        {
            timeDifference = (float)(GameTime - Settings.morningTime).TotalMinutes;
            return LightShift.Morning;
        }

        if (GameTime < Settings.morningTime || GameTime >= Settings.nightTime)
        {
            timeDifference = Mathf.Abs((float)(GameTime - Settings.nightTime).TotalMinutes);
            // Debug.Log(timeDifference);
            return LightShift.Night;
        }

        return LightShift.Morning;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : MonoSingleton<TimeManager>
{
    private int nowDay;
    private int loopTime;

    private void Start()
    {
        nowDay = 1;
        loopTime = 0;
    }

    public void Time_Change()
    {
        nowDay++;
        if (nowDay >= 7)
        {
            nowDay = 1;
            loopTime++;
        }

        switch (nowDay)
        {
            case 1:
                TimeEvent_1();
                break;
            case 2:
                TimeEvent_2();
                break;
            case 3:
                TimeEvent_3();
                break;
            case 4:
                TimeEvent_4();
                break;
            case 5:
                TimeEvent_5();
                break;
            case 6:
                TimeEvent_6();
                break;
            case 7:
                TimeEvent_7();
                break;
        }
    }

    public int Time_Get()
    {
        return nowDay;
    }

    private void TimeEvent_1()
    {

    }

    private void TimeEvent_2()
    {

    }

    private void TimeEvent_3()
    {

    }

    private void TimeEvent_4()
    {

    }

    private void TimeEvent_5()
    {

    }

    private void TimeEvent_6()
    {

    }

    private void TimeEvent_7()
    {

    }
}

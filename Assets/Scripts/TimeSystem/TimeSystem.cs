using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [SerializeField]
    private int gameTime_Hour = 4;
    private static float gameTime;

    public delegate void TimeEventHandler(float time);

    public event TimeEventHandler TimeTrigger;
    TimeEventHandler OnTime = (float time)=>Debug.Log(time);

    // Start is called before the first frame update
    void Start()
    {
        gameTime = gameTime_Hour * 60 * 60;
        TimeTrigger += OnTime;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
        if(gameTime <= gameTime_Hour * 60 * 60 - 60){
            TimeTrigger?.Invoke(gameTime);
            TimeTrigger -= OnTime;
        }
    }

    public static string ShowTime()
    {
        return (int)(gameTime / 3600) + ":" + (int)((gameTime / 60) % 60) + "." + (int)(gameTime % 60);
    }
    public static float GetTime()
    {
        return gameTime;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    [SerializeField]
    private int gameTime_Hour = 4;
    private static float gameTime;

    public Event OnTimeTrigger;

    // Start is called before the first frame update
    void Start()
    {
        gameTime = gameTime_Hour * 60 * 60;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime -= Time.deltaTime;
    }

    public static string GetTime()
    {
        return (int)(gameTime / 3600) + ":" + (int)((gameTime / 60) % 60) + "." + (int)(gameTime % 60);
    }
}

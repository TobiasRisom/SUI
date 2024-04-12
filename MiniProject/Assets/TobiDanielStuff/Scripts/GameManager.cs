using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI tmp_timer;
    float timer;
    public int crashCounter = 0;
    private bool isTimerRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            timer += Time.deltaTime;
            tmp_timer.text = System.TimeSpan.FromSeconds(timer).ToString("mm':'ss':'ff");
        }
    }

    public void stopTimer()
    {
        isTimerRunning = false;
        Debug.Log($"Final Time: {timer}");
    }

    public void startTimer()
    {
        isTimerRunning = true;
    }
}

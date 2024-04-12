using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI tmp_timer;
    float timer;
    private bool isTimerRunning = true;
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
    }
}

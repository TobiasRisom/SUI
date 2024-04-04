using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public GameObject Stalker;
    public GameObject Target;
    public float speedTarget = 3.0f;
    public float speed = 0f;
    public moveTrigger mt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Stalker.transform.position = Vector3.MoveTowards(Stalker.transform.position, Target.transform.position, speed * Time.deltaTime);
        checkSpeed();
    }

    void checkSpeed()
    {
        if(mt.isTouching == true)
        {
            if (speed < speedTarget)
            {
                speed += Time.deltaTime;
                if(speed > speedTarget)
                {
                    speed = speedTarget;
                }
            }
        }
        else
        {
            if(speed > 0)
            {
                speed -= Time.deltaTime;
                if (speed < 0)
                {
                    speed = 0;
                }
            }
        }
    }
}
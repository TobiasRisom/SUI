using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialDistance : MonoBehaviour
{
    public GameObject pos1;
    public GameObject pos2;
    public float speed;
    public Vector3 center = new Vector3(0, 0, 0);
    public FollowTarget ft;
    public moveTrigger mt;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mt.isTouching)
        {
        changeSpeed();
        center = (pos1.transform.position + pos2.transform.position) * 0.5f;

        transform.position = Vector3.MoveTowards(transform.position, center, speed * Time.deltaTime);
        //transform.position = center;
        }
    }

    void changeSpeed()
    {
      speed = ft.speed * 2;
    }
}
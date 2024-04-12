using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public List<GameObject> checkpoints = new List<GameObject>();
    public int currentActiveCP = 0;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GetComponent<GameManager>();

        // Make all
        for(int i = 0; i < checkpoints.Count; i++)
        {
            if(i == currentActiveCP)
            {
                checkpoints[i].GetComponent<Renderer>().material.color = Color.green;
            }

            else
            {
                checkpoints[i].GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateActiveCheckpoint()
    {
        if(currentActiveCP < checkpoints.Count - 1)
        {
            currentActiveCP += 1;
            checkpoints[currentActiveCP].GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            gm.stopTimer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Have we hit a checkpoint?
        if(other.CompareTag("checkpoint"))
        {

            Debug.Log("Checkpoint hit!");
            // Is it the current active checkpoint?
            if(other.GetComponent<Renderer>().material.color == Color.green)
            {

                Debug.Log("Right checkpoint hit!");

                // Change color to gray and transparency down
                other.GetComponent<Renderer>().material.color = new Color(0.5f, 0.5f, 0.5f, 0.4f);

                // Update Checkpoint
                updateActiveCheckpoint();

                // Play sound?
                // PLAY SOUND GO HERE
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCubeKillBallScript : MonoBehaviour {

    //Other scripts
    public SpawnBallScript SpawnBallScript;
    public CatchBallScript CatchBallScript;
    public ScoringScript ScoringScript;
    public KillBallScript KillBallScript;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Debug.Log("You hit a square");

            //Destroy the ball
            //DestroyObject(collision.gameObject);
        }
    }
}

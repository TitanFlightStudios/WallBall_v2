using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCubeKillBallScript : MonoBehaviour {

    //Other scripts
    public SpawnBallScript SpawnBallScript;
    public KillBallScript KillBallScript;
    //public CatchBallScript CatchBallScript;
    public ScoringScript ScoringScript;
    public SpawnMovingObjectScript SpawnMovingObjectScript;

    //Use this for setting the max speed the cubes can go at
    public float fCubeMaxSpeed;


    // Use this for initialization
    void Start () {

        ScoringScript = GameObject.Find("Canvas").GetComponent<ScoringScript>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {

            //Debug.Log("You hit a square!");
            ScoringScript.MissedBallPanel.SetActive(true);
            DestroyObject(collision.gameObject);

        }
    }
}

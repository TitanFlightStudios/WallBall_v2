using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSideWallLeftScript : MonoBehaviour {

    //Other scripts
    public SpawnBallScript SpawnBallScript;
    public BallBehaviorScript BallBehaviorScript;
    public CatchBallScript CatchBallScript;
    public ScoringScript ScoringScript;
    public RotateWallScript RotateWallScript;


    public bool didBallHitSideWallLeft = false;

    // Use this for initialization
    void Start () {
         didBallHitSideWallLeft = false;


    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            didBallHitSideWallLeft = true;
        }
    }
}

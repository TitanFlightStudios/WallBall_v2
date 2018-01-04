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
    public RedCubeKillBallScript RedCubeKillBallScript;


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
        //Rigidbody of the Red Cube that hits the wall
        Rigidbody RedCubeRigidbody;

        if (collision.gameObject.tag == "Ball")
        {
            didBallHitSideWallLeft = true;

            //Increase score multiplier
            ScoringScript.IncreaseScoreMultiplier(0.25f);
        }

        if (collision.gameObject.tag == "RedCube")
        {
            RedCubeRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (RedCubeRigidbody.velocity.x < RedCubeKillBallScript.fCubeMaxSpeed)
            {
                //Add force to the cube to continue its momentum
                RedCubeRigidbody.AddForce(0.5f, 0, 0, ForceMode.Impulse);
            }
            else
            {
                //if the cube is going over the set max speed, then set the velocity of the cube to the max speed while keeping other values as they were
                RedCubeRigidbody.velocity = new Vector3(RedCubeKillBallScript.fCubeMaxSpeed, RedCubeRigidbody.velocity.y);
            }
        }
    }
}

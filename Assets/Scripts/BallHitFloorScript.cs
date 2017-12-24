using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHitFloorScript : MonoBehaviour
{

    //Other scripts
    public SpawnBallScript SpawnBallScript;
    public BallBehaviorScript BallBehaviorScript;
    public CatchBallScript CatchBallScript;
    public ScoringScript ScoringScript;
    public RotateWallScript RotateWallScript;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        //Rigidbody of the Red Cube that hits the wall
        Rigidbody RedCubeRigidbody;

        if (collision.gameObject.tag == "Ball")
        {
            //Increase score multiplier
            ScoringScript.IncreaseScoreMultiplier(0.25f);
        }

        if (collision.gameObject.tag == "RedCube")
        {
            RedCubeRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            //Add force to the cube to continue its momentum
            RedCubeRigidbody.AddForce(0, .25f, 0, ForceMode.Impulse);
            Debug.Log("Ball hit floor...adding force...");
        }
    }
}
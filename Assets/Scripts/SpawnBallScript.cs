using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBallScript : MonoBehaviour {

    //Other Scripts
    public BallBehaviorScript BallBehaviorScript;
    public CatchBallScript CatchBallScript;
    public ScoringScript ScoringScript;

    //Game Object to hold the Player Camera
    public Camera PlayerCamera;

    //Ball game Object (Prefab)
    public GameObject Ball;

    //Speed to move the ball
    public float BallSpeed;

    //Speed that player choses the ball speed to b
    public float NewBallSpeed;

    [HideInInspector]
    //Boolean to use to keep track of a ball being in play
    public bool isBallSpawned;

    [HideInInspector]
    //Temp float to account for dir being less than 0 for X
    public float XDirection;

    [HideInInspector]
    //Temp float to account for dir being less than 0 for Y
    public float YDirection;

    //Ball's Rigidbody
    public Rigidbody BallRigidbody;

    public Vector3 LocationToAddForceToBallAt;

    [HideInInspector]
    //Game Object to store what object was hit by the player tapping the spawn position of the ball
    public GameObject ObjectThatWasHit;

    [HideInInspector]
    //Keep track of the newly spawned ball
    public GameObject SpawnedBall;

    [HideInInspector]
    //Vector 3 to hold the SpawnedBall's position
    public Vector3 SpawnedBallPosition;

    [HideInInspector]
    //Vector 3 to hold the position of where the player clicked/tapped
    public Vector3 TapPosition;
    [HideInInspector]

    

    

	// Use this for initialization
	void Start () {

        isBallSpawned = false;
        CatchBallScript.EndOfRoundPanel.SetActive(false);
        NewBallSpeed = BallSpeed;
		
	}
	
	// Update is called once per frame
	void Update () {

        //IF the player taps the screen (or clicks) to throw a ball
        if (Input.GetMouseButtonDown(0) && CatchBallScript.isBallCatchable == false && isBallSpawned == false && ScoringScript.MissedBallPanel.activeSelf == false)
        {
            SpawnBall();
        }

    }

    public void ChangeBallSpeed(float NewBallSpeed)
    {
        //Set the Ball speed to the new chosen ball speed
        BallSpeed = NewBallSpeed;
    }

    public void SpawnBall()
    {
        //Find Tap position location
        TapPosition = Input.mousePosition;

        //Set the ball's spawn location to the tap position location (then add some units to spawn ball in front of player camera
        SpawnedBallPosition = PlayerCamera.ScreenToWorldPoint(TapPosition) + new Vector3(0, 0, 5);

        //Set the SpawnedBall Game Object to the new ball being spawned into the scene using the tap position acquired earlier that was set to world coords
        SpawnedBall = Object.Instantiate(Ball, SpawnedBallPosition, Ball.transform.localRotation);

        //Set ball as being spawned
        isBallSpawned = true;

        //Set ball as being not catchable
        CatchBallScript.isBallCatchable = false;

        //Change the material of the ball so that it is not catchable
        BallBehaviorScript.BallRenderer.sharedMaterial = BallBehaviorScript.materials[0];

        //Add initial force to ball
        BallRigidbody = SpawnedBall.GetComponent<Rigidbody>();

        //Add force at position
        //Add force from where the ball is spawned towards where the player taps
        Vector3 direction = SpawnedBall.transform.position - TapPosition;


        // Create a ray from the current mouse coordinates var ray: 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // if something tagged "Ground" is hit... 
        if (Physics.Raycast(ray, out hit))
        {
            var dir = hit.point - SpawnedBallPosition;

            //Debug.Log("dir: " + dir);
            //If the player taps in the "problem area" where the direction calculation causes the calculation to go haywire, sending the ball in a reverse direction
            //COULD USE MORE POLISH HERE; 
            //throwing mechanic is usable as is though
            if (dir.x < 0 && dir.x > -1 || dir.x > 0 && dir.x < 1 || dir.y < 0 && dir.y > -1 || dir.y > 0 && dir.y < 1)
            {
                if (dir.x < 0 && dir.x > -1)
                {
                    //Account for x being between 0 and -1 (negative)

                    XDirection = -1;

                }
                if (dir.x > 0 && dir.x < 1)
                {
                    //Account for x being between 0 and 1

                    XDirection = 1;

                }
                if (dir.y < 0 && dir.y > -1)
                {
                    //Account for y being between 0 and -1

                    YDirection = -1;
                }
                if (dir.y > 0 && dir.y < 1)
                {
                    //Account for y being between 0 and 1

                    YDirection = 1;
                }

                BallRigidbody.AddForce(XDirection, YDirection, BallSpeed, ForceMode.Impulse);
            }
            else
            {
                BallRigidbody.AddForce(dir.normalized * BallSpeed, ForceMode.Impulse);
            }


        }
    }
}

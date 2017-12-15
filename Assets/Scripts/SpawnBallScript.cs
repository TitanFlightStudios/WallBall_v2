using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBallScript : MonoBehaviour {

    //Game Object to hold the Player Camera
    public Camera PlayerCamera;

    //Ball game Object (Prefab)
    public GameObject Ball;

    //Speed to move the ball
    public float BallSpeed;

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

        
		
	}
	
	// Update is called once per frame
	void Update () {

        //IF the player taps the screen (or clicks)
        if (Input.GetMouseButtonDown(0))
        {

            //Find Tap position location
            TapPosition = Input.mousePosition;

            Debug.Log("TapPosition: " + TapPosition);

            //Set the ball's spawn location to the tap position location (then add some units to spawn ball in front of player camera
            SpawnedBallPosition = PlayerCamera.ScreenToWorldPoint(TapPosition) + new Vector3(0, 0, 5);

            Debug.Log("SpawnedBallPosition: " + SpawnedBallPosition);

            //Set the SpawnedBall Game Object to the new ball being spawned into the scene using the tap position acquired earlier that was set to world coords
            SpawnedBall = Object.Instantiate(Ball, SpawnedBallPosition, Ball.transform.localRotation);

            //Add initial force to ball
            BallRigidbody = SpawnedBall.GetComponent<Rigidbody>();

            //Add force at position
            //Add force from where the ball is spawned towards where the player taps
            Vector3 direction = SpawnedBall.transform.position - TapPosition;


            // Create a ray from the current mouse coordinates var ray: 
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // if something tagged "Ground" is hit... 
            if (Physics.Raycast(ray, out hit) )
            {
                var dir = hit.point - SpawnedBallPosition;
                // calculated the direction... 
                // and kick!
                Debug.Log("dir: " + dir);
                if (dir.x < 0 && dir.x > -1 &&  dir.y < 0 && dir.y > -1 )
                {
                    //Lower left

                    XDirection = dir.x - 1;
                    YDirection = dir.y - 1;

                    BallRigidbody.AddForce(XDirection, YDirection, BallSpeed, ForceMode.Impulse);
                }
                else if (dir.x > 0 && dir.x < 1 && dir.y > 0 && dir.y < 1)
                {
                    //Upper Right

                    XDirection = dir.x - 1;
                    YDirection = 1 - dir.y;

                    BallRigidbody.AddForce(XDirection, YDirection, BallSpeed, ForceMode.Impulse);
                }
                else if (dir.x > 0 && dir.x < 1 && dir.y > 0 && dir.y < 1)
                {
                    //Lower Right

                    XDirection = dir.x - 1;
                    YDirection = dir.y - 1;

                    BallRigidbody.AddForce(XDirection, YDirection, BallSpeed, ForceMode.Impulse);
                }
                else if (dir.x > 0 && dir.x < 1 && dir.y < 0 && dir.y > -1)
                {

                    //Upper Left

                    XDirection = 1 - dir.x;
                    YDirection = 1 - dir.y;

                    BallRigidbody.AddForce(XDirection, YDirection, BallSpeed, ForceMode.Impulse);
                }
                else
                BallRigidbody.AddForce(dir.normalized * BallSpeed, ForceMode.Impulse);
            }





            //BallRigidbody.AddForceAtLocation();
        }

    }
}

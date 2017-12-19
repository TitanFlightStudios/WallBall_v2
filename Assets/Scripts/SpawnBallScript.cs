﻿using System.Collections;
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

    //Ball's Rigidbody
    public Rigidbody BallRigidbody;

    //unused
    //public Vector3 LocationToAddForceToBallAt;

    [HideInInspector]
    //Boolean to use to keep track of a ball being in play
    public bool isBallSpawned;

    [HideInInspector]
    //Temp float to account for tapCoords being less than 0 for X
    public float tapCoordsX;

    [HideInInspector]
    //Temp float to account for tapCoords being less than 0 for Y
    public float tapCoordsY;

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

    //Variable to hold the easyBallSpeed number
    public float easyBallSpeed;

    //Variable to hold the medBallSpeed number
    public float medBallSpeed;

    //Variable to hold the hardBallSpeed number
    public float hardBallSpeed;

    //Speed to move the ball
    [HideInInspector]
    public static float BallSpeed = 15;

    //Speed that player choses the ball speed to be
    [HideInInspector]
    public float NewBallSpeed;





    // Use this for initialization
    void Start () {

        isBallSpawned = false;
        CatchBallScript.EndOfRoundPanel.SetActive(false);
        ScoringScript.isEasySpeed = true;
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

        ////Set the variable to determine whether the ball speed is easy, med or hard
        //if (NewBallSpeed == easyBallSpeed)
        //{
        //    ScoringScript.isEasySpeed = true;
        //    ScoringScript.isMedSpeed = false;
        //    ScoringScript.isHardSpeed = false;
        //}
        //else if (NewBallSpeed == medBallSpeed)
        //{
        //    ScoringScript.isEasySpeed = false;
        //    ScoringScript.isMedSpeed = true;
        //    ScoringScript.isHardSpeed = false;
        //}
        //else if (NewBallSpeed == hardBallSpeed)
        //{
        //    ScoringScript.isEasySpeed = false;
        //    ScoringScript.isMedSpeed = false;
        //    ScoringScript.isHardSpeed = true;
        //}
        //else
        //{
        //    ScoringScript.isEasySpeed = true;
        //    ScoringScript.isMedSpeed = false;
        //    ScoringScript.isHardSpeed = false;
        //}
        //return;
    }

    public void SpawnBall()
    {
        //Find Tap position location
        TapPosition = Input.mousePosition;

        //Set the ball's spawn location to the tap position location (then add some units to spawn ball in front of player camera
        SpawnedBallPosition = PlayerCamera.ScreenToWorldPoint(TapPosition) + new Vector3(0, 0, 5);

        //Set the variable to determine whether the ball speed is easy, med or hard
        if (BallSpeed == easyBallSpeed)
        {
            ScoringScript.isEasySpeed = true;
            ScoringScript.isMedSpeed = false;
            ScoringScript.isHardSpeed = false;
        }
        else if (BallSpeed == medBallSpeed)
        {
            ScoringScript.isEasySpeed = false;
            ScoringScript.isMedSpeed = true;
            ScoringScript.isHardSpeed = false;
        }
        else if (BallSpeed == hardBallSpeed)
        {
            ScoringScript.isEasySpeed = false;
            ScoringScript.isMedSpeed = false;
            ScoringScript.isHardSpeed = true;
        }
        else
        {
            ScoringScript.isEasySpeed = true;
            ScoringScript.isMedSpeed = false;
            ScoringScript.isHardSpeed = false;
        }

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
        Vector3 tapCoordsection = SpawnedBall.transform.position - TapPosition;


        // Create a ray from the current mouse coordinates var ray: 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // if something tagged "Ground" is hit... 
        if (Physics.Raycast(ray, out hit))
        {
            var tapCoords = hit.point - PlayerCamera.ScreenToWorldPoint(TapPosition);

            //Debug.Log("tapCoords: " + tapCoords);
            //If the player taps in the "problem area" where the tapCoordsection calculation causes the calculation to go haywire, sending the ball in a reverse tapCoordsection
            //COULD USE MORE POLISH HERE; 
            //throwing mechanic is usable as is though
            if (tapCoords.x < 0 && tapCoords.x > -1 || tapCoords.x > 0 && tapCoords.x < 1 && tapCoords.y < 0 && tapCoords.y > -1 || tapCoords.y > 0 && tapCoords.y < 1)
            {
                //if (tapCoords.x < 0 && tapCoords.x > -1 || tapCoords.x > 0 && tapCoords.x < 1 && tapCoords.y < 0 && tapCoords.y > -1)
                //{
                    //Account for x being between 0 and -1 (negative)

                    tapCoordsX = Mathf.RoundToInt(tapCoords.x);



                //}


                //Debug.Log("tapCoords is: " + tapCoords);

                BallRigidbody.AddForce(tapCoords.normalized * BallSpeed, ForceMode.Impulse);
                //Debug.Log("Adding force, ball speed is: " + BallSpeed);
            }
            else
            {
                BallRigidbody.AddForce(tapCoords.normalized * BallSpeed, ForceMode.Impulse);
            }


        }
    }
}

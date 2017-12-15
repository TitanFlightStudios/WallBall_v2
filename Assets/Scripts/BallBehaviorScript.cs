﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviorScript : MonoBehaviour {

    //Other Scripts
    public SpawnBallScript SpawnBallScript;

    //Other Scripts
    public CatchBallScript CatchBallScript;

    //Renderer for the ball
    public Renderer BallRenderer;

    //Array for the different materials to use for the ball (being catchable vs not catchable)
    public Material[] materials;

    //Player Camera variable
    public Camera PlayerCamera;

    //Variable to hold the string tag of the object being hit by the ball
    public string ObjectThatWasHit;

    //Variable to determine the speed of the ball
    public float BallSpeed;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "WallCatchable")
        {
            //Make it so that the ball is catchable
            CatchBallScript.isBallCatchable = true;
            Debug.Log("Set ball as catchable" + CatchBallScript.isBallCatchable);

            //Change the material of the ball to make it visual that the ball is catchable
            BallRenderer.sharedMaterial = materials[1];
        }

    }

    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBallScript : MonoBehaviour {

    //Other scripts
    public SpawnBallScript SpawnBallScript;
    public KillBallScript KillBallScript;
    public BallBehaviorScript BallBehaviorScript;
    public ScoringScript ScoringScript;

    public static bool isBallCatchable;

    //Player Camera
    public Camera PlayerCamera;

    //String to hold the tag of the object being hit by the player tap
    [HideInInspector]
    public string ObjectThatWasHit;

    //UI Panel for End of Round
    public GameObject EndOfRoundPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && isBallCatchable == true)
        {
            CatchBall();
        }


        
		
	}

    IEnumerator WaitFunction(float SecondsToWait)
    {
        if(Input.GetMouseButtonDown(0))
        {
            isBallCatchable = false;
        }
        yield return new WaitForSeconds(SecondsToWait);
        SpawnBallScript.isBallSpawned = false;
        isBallCatchable = false;
    }

    public void CatchBall()
    {
        //Check for collision with ball collider trigger

        //Use raycast to start detection

        //Set up raycast hit variable
        RaycastHit hit;
        //find cursor position at click
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        Debug.Log("isBallCatchable: " + isBallCatchable);
        //Check to make sure player can only catch ball after it hits wall
        if (isBallCatchable == true)
        {
            Debug.Log("Entering Raycast function");
            //determine if cursor position is overlapping with ball
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Performing Raycast");
                Transform objectHit = hit.transform;
                //Set the Object's tag that was hit to variable ObjectThatWasHit
                ObjectThatWasHit = objectHit.tag;
                //If the object that was hit has a tag of "Ball"
                if (ObjectThatWasHit == "Ball")
                {
                    //Notify player they caught it - with score
                    //UI things here


                    Debug.Log("Congratulations, you caught it.");

                    //Call function from Scoring Script to add score
                    ScoringScript.BallCaught(true);


                    //Reset ball being spawned after catching
                    KillBallScript.DestroyObject(SpawnBallScript.SpawnedBall);

                    StartCoroutine(WaitFunction(0.25f));
                    //Fade in Score Text Amount
                    StartCoroutine(ScoringScript.Fade());


                }
                else
                {
                    Debug.Log("You missed the ball.");

                    //Activate UI Panel
                    EndOfRoundPanel.SetActive(true);

                    //Deactivate spawning of another ball
                    SpawnBallScript.isBallSpawned = true;

                    //Destroy the ball
                    KillBallScript.DestroyObject(SpawnBallScript.SpawnedBall);
                }
            }
            
        }
    }


}

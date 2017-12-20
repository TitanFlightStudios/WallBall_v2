using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBallScript : MonoBehaviour {

    //Other scripts
    public SpawnBallScript SpawnBallScript;
    public KillBallScript KillBallScript;
    public BallBehaviorScript BallBehaviorScript;
    public ScoringScript ScoringScript;
    public RotateWallScript RotateWallScript;
    public HitSideWallRightScript HitSideWallRightScript;


    public static bool isBallCatchable;

    //Player Camera
    public Camera PlayerCamera;

    //String to hold the tag of the object being hit by the player tap
    [HideInInspector]
    public string ObjectThatWasHit;

    //UI Panel for End of Round
    public GameObject EndOfRoundPanel;

    //Array list to hold all wall pieces
    public GameObject[] WallPieces;

    //Variable to keep track of how many catches have happened this round
    public static float CatchesNum;

    //Variable to hold the sum of the ball speed multiplier and the ball catches multiplier
    public float fSumOfCatchesAndBallSpeedMult;

    // Use this for initialization
    void Start () {
        CatchesNum = 0.0f;
        
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && isBallCatchable == true)
        {
            CatchBall();
        }

        fSumOfCatchesAndBallSpeedMult = ScoringScript.fCatchesMult + ScoringScript.fballSpeedMult;

        ScoringScript.NumCatchesMultText.text = fSumOfCatchesAndBallSpeedMult.ToString();


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
        //Debug.Log("isBallCatchable: " + isBallCatchable);
        //Check to make sure player can only catch ball after it hits wall
        if (isBallCatchable == true)
        {
            //Debug.Log("Entering Raycast function");
            //determine if cursor position is overlapping with ball
            if (Physics.Raycast(ray, out hit))
            {
                //Debug.Log("Performing Raycast");
                Transform objectHit = hit.transform;
                //Set the Object's tag that was hit to variable ObjectThatWasHit
                ObjectThatWasHit = objectHit.tag;
                //If the object that was hit has a tag of "Ball"
                if (ObjectThatWasHit == "Ball")
                {
                    //Notify player they caught it - with score
                    //UI things here

                    //Increment the number of successful catches
                    CatchesNum += 1;

                    //Debug.Log("Congratulations, you caught it.");

                    //Call function from Scoring Script to add score
                    ScoringScript.BallCaught(true);

                    if (CatchesNum > 5 && CatchesNum < 10 )
                    {
                        //Launch RemoveWallPiece Function
                        RemoveWallPiece();
                    }
                    else if (CatchesNum == 11)
                    {
                        //Debug.Log("Starting Rotation of wall");
                        RotateWallScript.isWallRotating = true;
                        StartCoroutine(RotateWallScript.RotateWall());
                    }

                    //Reset ball being spawned after catching
                    KillBallScript.DestroyObject(SpawnBallScript.SpawnedBall);

                    
                    StartCoroutine(WaitFunction(0.25f));
                    //Fade in Score Text Amount
                    StartCoroutine(ScoringScript.Fade());

                    Debug.Log("CatchesNum variable: " + CatchesNum);

                    HitSideWallRightScript.didBallHitSideWallRight = false;





                }
                else
                {
                    //Debug.Log("You missed the ball.");

                    //Fading number each time the ball is caught
                    //ScoringScript.ScoreIncreaseFadeNumberText.text = ScoringScript.fAmountToIncreaseScorePerCatch.ToString();

                    //Current game score shown in the top left
                    ScoringScript.UICurrentGameScoreText.text = ScoringScript.fCurrentScore.ToString();

                    //Update the final score text
                    //ScoringScript.EndOfRoundScoreText.text = ScoringScript.fCurrentScore.ToString();

                    //Update the Ball Speed Multiplier Text
                    ScoringScript.BallSpeedMultiplierText.text = ScoringScript.fballSpeedMult.ToString();

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

    public void RemoveWallPiece()
    {
        //pick a random number between 0 and the list of wall pieces
        int WallPieceToRemove = Random.Range(0, WallPieces.Length);

        if (WallPieces[WallPieceToRemove] != null)
        {
            WallPieces[WallPieceToRemove].SetActive(false);
        }
        else
        {
            WallPieceToRemove = Random.Range(0, WallPieces.Length);
            RemoveWallPiece();
        }
        //WallPieces.(WallPieceToRemove);

        //Debug.Log("New length of array: " + WallPieces.Length);
    }


}

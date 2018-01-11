using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBallScript : MonoBehaviour {

    //Other scripts
    public SpawnBallScript SpawnBallScript;
    public BallBehaviorScript BallBehaviorScript;
    public CatchBallScript CatchBallScript;
    public ScoringScript ScoringScript;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
                //Debug.Log("You missed the ball.");

                //Fading number each time the ball is caught
                ScoringScript.ScoreIncreaseFadeNumberText.text = ScoringScript.fAmountToIncreaseScorePerCatch.ToString();

                //Current game score shown in the top left
                ScoringScript.UICurrentGameScoreText.text = ScoringScript.fCurrentScore.ToString();

                //Update the final score text
                ScoringScript.EndOfRoundScoreText.text = ScoringScript.fCurrentScore.ToString();

                //Update the Ball Speed Multiplier Text
                //ScoringScript.BallSpeedMultiplierText.text = ScoringScript.fballSpeedMult.ToString();


                //Activate UI Panel
                //CatchBallScript.EndOfRoundPanel.SetActive(true);

                ScoringScript.MissedBallPanel.SetActive(true);

                //Deactivate spawning of another ball
                SpawnBallScript.isBallSpawned = true;

                //Destroy the ball
                DestroyObject(SpawnBallScript.SpawnedBall);

        }
    }

    public void DestroyObject(GameObject ObjectToDestroy)
    {
        Destroy(ObjectToDestroy);
        //Debug.Log("You missed the ball.");

    }
}

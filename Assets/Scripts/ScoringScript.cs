using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringScript : MonoBehaviour
{
    public SpawnBallScript SpawnBallScript;
    public BallBehaviorScript BallBehaviorScript;
    public KillBallScript KillBallScript;

    public float fAmountToIncreaseScore;
    public float fCurrentScore;
    public float fFinalScore;

    public Text UICurrentGameScoreText;
    public GameObject UICurrentGameScoreComponent;

    public Text ScoreIncreaseFadeNumber;


    public GameObject MissedBallPanel;

    Text ScoreNumberAmountTxtComponent;
    public GameObject ScoreNumberAmountTxt;

    Text PlusButtonForFadeNumberComponent;
    public GameObject PlusButtonForFadeNumberGameObject;

    public float fFadeSpeed;
    public float fFadeDuration;

    public GameObject ScoreIncreaseFadeNumberComponent;

    public Text FinalScoreText;
    public GameObject FinalScoreComponent;

    // Use this for initialization
    void Start()
    {
        //Assign the Score number text being displayed so we can change it when player catches ball (in the top left corner)
        ScoreNumberAmountTxtComponent = ScoreNumberAmountTxt.GetComponent<Text>();
        PlusButtonForFadeNumberComponent = PlusButtonForFadeNumberGameObject.GetComponent<Text>();
        //Assign the color to the fade text so we can fade it out later
        Color ScoreNumberAmountTxtColor = ScoreNumberAmountTxtComponent.color;
        Color PlusButtonForFadeNumberColor = PlusButtonForFadeNumberComponent.color;
        //Set the alpha to 0 by default so it starts invisible
        ScoreNumberAmountTxtColor.a = 0;
        PlusButtonForFadeNumberColor.a = 0;

        MissedBallPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {


    }

    public IEnumerator Fade()
    {
        //Set the speed to fade from full alpha to 0 over time (1/10) would be 10 seconds (1/5) 5 seconds, and so on
        fFadeSpeed = (float)1.0 / fFadeDuration;
        //Initiate the inital color of the score display component
        Color ScoreNumberAmountTxtColor = ScoreNumberAmountTxtComponent.color;
        Color PlusButtonForFadeNumberColor = PlusButtonForFadeNumberComponent.color;
        //ScoreNumberAmountTxt.SetActive(true);

        //for loop that fades from 0 alpha to 1 over a time that is the change of time times the fade speed
        for (float fFadeTime = 0.0f; fFadeTime < 1.0f; fFadeTime += Time.deltaTime * fFadeSpeed)
        {
            //Alpha changes over a lerp from 1 to 0 over a time that lasts an amount of fFadeTime
            ScoreNumberAmountTxtColor.a = Mathf.Lerp(1, 0, fFadeTime);
            PlusButtonForFadeNumberColor.a = Mathf.Lerp(1, 0, fFadeTime);
            //Sets Alpha that does the "fade"
            ScoreNumberAmountTxtComponent.color = ScoreNumberAmountTxtColor;
            PlusButtonForFadeNumberComponent.color = PlusButtonForFadeNumberColor;
            yield return true;
        }
    }

    public void BallCaught(bool isBallCaught)
    {
        //Set the Amount to increase score if ball is caught with current speed
        fAmountToIncreaseScore = SpawnBallScript.BallSpeed * SpawnBallScript.BallSpeed;
        //Add the score to the current score
        fCurrentScore += fAmountToIncreaseScore;
        //Add the score to the final score text
        //Debug.Log(fCurrentScore);
        //Fading number each time the ball is caught
        ScoreIncreaseFadeNumber.text = fAmountToIncreaseScore.ToString();

        //Current game score shown in the top left
        UICurrentGameScoreText.text = fCurrentScore.ToString();

        //Update the final score text
        FinalScoreText.text = fCurrentScore.ToString();

    }

    public void ResetBall()
    {
        fCurrentScore = 0;
        UICurrentGameScoreText.text = 0.ToString();
        FinalScoreText.text = 0.ToString();
        MissedBallPanel.SetActive(false);
        SpawnBallScript.isBallSpawned = false;
        CatchBallScript.isBallCatchable = false;

    }

}


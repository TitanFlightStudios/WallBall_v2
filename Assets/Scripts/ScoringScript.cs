using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringScript : MonoBehaviour
{
    public SpawnBallScript SpawnBallScript;
    public BallBehaviorScript BallBehaviorScript;
    public KillBallScript KillBallScript;
    public CatchBallScript CatchBallScript;
    public RotateWallScript RotateWallScript;
    public HitSideWallRightScript HitSideWallRightScript;
    public HitSideWallLeftScript HitSideWallLeftScript;
    public SpawnMovingObjectScript SpawnMovingObjectScript;
    public AssignBlockColorScript AssignBlockColorScript;

    //Amount to increase the score for every successful catch the player makes in a round
    public float fAmountToIncreaseScorePerCatch;

    [HideInInspector]
    public float fCurrentScore;
    [HideInInspector]
    public float fFinalScore;




    //[HideInInspector]
    //Easy speed bool
    //public bool isEasySpeed = true;
    //[HideInInspector]
    //Hard speed bool
    //public bool isHardSpeed;

    //Values for score per catch depending on speed difficulty
    //public float easySpeedMult;
    //public float hardSpeedMult;


    //[HideInInspector]
    //Variable to hold the amount that the score is multiplied at the end of the round
   //public float fballSpeedMult;

    //The end of round panel
    public GameObject MissedBallPanel;

    //Variables to hold the Current Game Score text, and to change the component values
    public Text UICurrentGameScoreText;
    public GameObject UICurrentGameScoreComponent;

    //Variable to use to change the alpha of the text
    [HideInInspector]
    public Text PlusButtonForFadeNumberText;
    public Text ScoreIncreaseFadeNumberText;
    public Text ScoreNumberAmountText;
    //Variable to hold the fballSpeedMult text to change depending on the difficulty mult
    //public Text BallSpeedMultiplierText;

    //Text Variable to hold the text that is displayed per catch
    public GameObject ScoreNumberAmountComponent;
    public GameObject PlusButtonForFadeNumberComponent;
    public GameObject ScoreNumberPanelObject;
    [HideInInspector]
    //Renderer for the Fade Number Panel
    public Renderer ScoreNumberPanelAlphaRenderer;

    //Variables to determine how long the text takes to fade
    public float fFadeSpeed;
    public float fFadeDuration;


    public GameObject ScoreIncreaseFadeNumberComponent;

    //Score to display calculation of score per catches times the multiplier
    public Text FinalScoreAfterMultText;
    //Number  to hold the value for the final score to be calculated
    [HideInInspector]
    public float fFinalScoreAfterMultNum;

    public Text EndOfRoundScoreText;
    public GameObject FinalScoreComponent;

    //Variable to hold the text element to display num of catches
    public Text NumCatchesText;
    public Text NumCatchesMultText;
    //Number to hold the value for the number of catches multiplier value
    [HideInInspector]
    public static float fCatchesMult;

    //Text to hold the current multiplier for ball catches in a row
    public Text fCurrentCatchesMult;

    //Walls Hit Variables
    public Text WallsHitText;
    public static float WallsHitNum;

    public GameObject WallsHitTextComponent;

    // Use this for initialization
    void Start()
    {
        //Assign the Score number text being displayed so we can change it when player catches ball (in the top left corner)
        ScoreNumberAmountText = ScoreNumberAmountComponent.GetComponent<Text>();
        PlusButtonForFadeNumberText = PlusButtonForFadeNumberComponent.GetComponent<Text>();
        WallsHitText = WallsHitTextComponent.GetComponent<Text>();
        //Assign the color to the fade text so we can fade it out later
        Color ScoreNumberAmountTxtColor = ScoreNumberAmountText.color;
        Color PlusButtonForFadeNumberColor = PlusButtonForFadeNumberText.color;
        //Set the alpha to 0 by default so it starts invisible
        ScoreNumberAmountTxtColor.a = 0;
        PlusButtonForFadeNumberColor.a = 0;

        ScoreNumberAmountText.color = ScoreNumberAmountTxtColor;
        PlusButtonForFadeNumberText.color = PlusButtonForFadeNumberColor;

        ScoreNumberPanelObject.SetActive(false);
        MissedBallPanel.SetActive(false);

        //isEasySpeed = true;
        //if (isEasySpeed == true)
        //{
        //    //Set the Amount to increase score if ball is caught with current speed
        //    //Set the fballSpeedMult to the appropriate setting
        //    fballSpeedMult = easySpeedMult;
        //}
        //else if (isHardSpeed == true)
        //{
        //    //Set the Amount to increase score if ball is caught with current speed
        //    //Set the fballSpeedMult to the appropriate setting
        //    fballSpeedMult = hardSpeedMult;
        //}
        //else
        //{
        //    //Set the Amount to increase score if ball is caught with current speed
        //    //Set the fballSpeedMult to the appropriate setting
        //    fballSpeedMult = easySpeedMult;
        //}

        //Update the Ball Speed Multiplier Text
        //BallSpeedMultiplierText.text = fballSpeedMult.ToString();

        fCatchesMult = 1.0f;
        CatchBallScript.CatchesNum = 0.0f;

        CatchBallScript.fSumOfCatchesAndWallHitMult = 1 + fCatchesMult;

        //Debug.Log("fSumofCatches: " + CatchBallScript.fSumOfCatchesAndWallHitMult);
        NumCatchesMultText.text = fFinalScoreAfterMultNum.ToString();//CatchBallScript.fSumOfCatchesAndWallHitMult.ToString();

        WallsHitNum = 1.0f;



    }

    // Update is called once per frame
    void Update()
    {



        //Calculation for number of catches
        fFinalScoreAfterMultNum = Mathf.RoundToInt(fCurrentScore * (WallsHitNum + fCatchesMult));

        //if (fFinalScoreAfterMultNum < fCurrentScore)
        //fFinalScoreAfterMultNum = fCurrentScore;

        fCurrentCatchesMult.text = CatchBallScript.fSumOfCatchesAndWallHitMult.ToString();

        //Update the final score text
        EndOfRoundScoreText.text = fCurrentScore.ToString();

        FinalScoreAfterMultText.text = fFinalScoreAfterMultNum.ToString();

        WallsHitText.text = WallsHitNum.ToString();

    }

    public void IncreaseScoreMultiplier(float AmountToIncreaseMult)
        {
        //Increase scoring multiplier here - for hit walls
        fCatchesMult += AmountToIncreaseMult;
        //Debug.Log("Ball Hit Wall: " + AmountToIncreaseMult);

        }

    public IEnumerator Fade()
    {
        //Set the speed to fade from full alpha to 0 over time (1/10) would be 10 seconds (1/5) 5 seconds, and so on
        fFadeSpeed = (float)1.0 / fFadeDuration;
        //Initiate the inital color of the score display component
        Color ScoreNumberAmountTxtColor = ScoreNumberAmountText.color;
        Color PlusButtonForFadeNumberColor = PlusButtonForFadeNumberText.color;
        Image ScoreNumberPanelImage = ScoreNumberPanelObject.GetComponent<Image>();

        ScoreNumberPanelObject.SetActive(true);
        //ScoreNumberAmountTxt.SetActive(true);

        ScoreNumberPanelImage.CrossFadeAlpha(1, 2, false);

        //for loop that fades from 0 alpha to 1 over a time that is the change of time times the fade speed
        for (float fFadeTime = 0.0f; fFadeTime < 1.0f; fFadeTime += Time.deltaTime * fFadeSpeed)
        {
            //Alpha changes over a lerp from 1 to 0 over a time that lasts an amount of fFadeTime
            ScoreNumberAmountTxtColor.a = Mathf.Lerp(1, 0, fFadeTime);
            PlusButtonForFadeNumberColor.a = Mathf.Lerp(1, 0, fFadeTime);
            // = Mathf.Lerp(1, 0, fFadeTime);
            //Sets Alpha that does the "fade"
            ScoreNumberAmountText.color = ScoreNumberAmountTxtColor;
            PlusButtonForFadeNumberText.color = PlusButtonForFadeNumberColor;
            yield return true;
        }
        ScoreNumberPanelObject.SetActive(false);
    }

    public void BallCaught(bool isBallCaught)
    {

        //Add the score to the current score
        fCurrentScore += fAmountToIncreaseScorePerCatch;

        //Set number of catches to the fCatchesMult variable
        IncreaseScoreMultiplier(0.25f);

        //Debug.Log("fCatchesMult: " + fCatchesMult);

        //Multiplier for ball speed
        //fFinalScoreAfterMultNum = fCurrentScore * fballSpeedMult * fCatchesMult;
        //FinalScoreAfterMultText.text = fFinalScoreAfterMultNum.ToString();

        //Multiplier for number of catches
        NumCatchesMultText.text = fCatchesMult.ToString();

        //Fading number each time the ball is caught
        ScoreIncreaseFadeNumberText.text = fAmountToIncreaseScorePerCatch.ToString();

        //Current game score shown in the top left
        UICurrentGameScoreText.text = fCurrentScore.ToString();

        //Update the Number of Catches Text
        NumCatchesText.text = CatchBallScript.CatchesNum.ToString();



    }

    public void ResetBall()
    {

        //Reset all UI score elements to 0 after the round has been reset
        fCurrentScore = 0;
        UICurrentGameScoreText.text = "0";
        EndOfRoundScoreText.text = "0";
        FinalScoreAfterMultText.text = "0";
        NumCatchesMultText.text = "0";
        CatchBallScript.CatchesNum = 0;
        fCatchesMult = 1.0f;
        WallsHitNum = 1.0f;
        WallsHitText.text = "0";

        //Reset whether ball hit right wall after spawning 
        HitSideWallRightScript.didBallHitSideWallRight = false;
        HitSideWallLeftScript.didBallHitSideWallLeft = false;

        //Reset the panel to being deactivated
        MissedBallPanel.SetActive(false);

        //Reset the ball values to be ready to spawn
        SpawnBallScript.isBallSpawned = false;
        CatchBallScript.isBallCatchable = false;

        //Reset all removed wall pieces
        for (int i = 0; i < CatchBallScript.WallPieces.Length; i++)
            CatchBallScript.WallPieces[i].SetActive(true);

        //Reset Red Cube spawning counter
        SpawnMovingObjectScript.SpawnCubeCounter = 0;

        //Reset Wall Removal Counter
        SpawnMovingObjectScript.RemoveWallCounter = 0;

        //Stop wall rotation

        RotateWallScript.isWallRotating = false;
        RotateWallScript.FullWall.transform.eulerAngles = new Vector3 (0,0,0);

        //if (isEasySpeed == true)
        //{
        //    //Set the Amount to increase score if ball is caught with current speed
        //    //Set the fballSpeedMult to the appropriate setting
        //    fballSpeedMult = easySpeedMult;
        //}
        //else if (isHardSpeed == true)
        //{
        //    //Set the Amount to increase score if ball is caught with current speed
        //    //Set the fballSpeedMult to the appropriate setting
        //    fballSpeedMult = hardSpeedMult;
        //}
        //else
        //{
        //    //Set the Amount to increase score if ball is caught with current speed
        //    //Set the fballSpeedMult to the appropriate setting
        //    fballSpeedMult = easySpeedMult;
        //}

        //Update the Ball Speed Multiplier Text
        //BallSpeedMultiplierText.text = fballSpeedMult.ToString();

        //Reset the block colors
        StartCoroutine(AssignBlockColorScript.AssignColorsToBlocksFunction());

        for (int i = 0; i < SpawnMovingObjectScript.AllRedCubesSpawned.Count; i++)
        {
            Destroy(SpawnMovingObjectScript.AllRedCubesSpawned[i]);
            Debug.Log("Destorying Cube");
        }

        //Clear the redcubearray
        SpawnMovingObjectScript.AllRedCubesSpawned.Clear();
        Debug.Log("Length Of Red Cube Array: " + SpawnMovingObjectScript.AllRedCubesSpawned.Count);



    }

}


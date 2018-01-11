using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using ChartboostSDK;
>>>>>>> 2b60f502a8947a201f9750c4d3c90cd68db472d5

public class InitializeChartboostScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

<<<<<<< HEAD

		
	}
=======
        Chartboost.cacheInterstitial(CBLocation.Default);
        Chartboost.cacheRewardedVideo(CBLocation.Default);

    }
>>>>>>> 2b60f502a8947a201f9750c4d3c90cd68db472d5
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeSave : MonoBehaviour
{

    public static ChallengeSave Instance;
    public string ChallengeData => "SaveChallengeFile" + ".es3";

    private void Awake()
    {
        Instance = this;
    }

    public void ChallengePased(int challengeNumber)
    {
        ES3.Save("ChallengeIndex", challengeNumber, ChallengeData);
    }
    
    
}

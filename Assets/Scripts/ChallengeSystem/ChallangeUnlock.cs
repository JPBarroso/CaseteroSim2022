using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallangeUnlock : MonoBehaviour
{

    [SerializeField] private int challengeNeededToUnlock;
    [SerializeField] private GameObject buttonObj;


    private void Start()
    {
        UnlockThisChallengeButton();
    }

    private void UnlockThisChallengeButton()
    {
        if (ES3.FileExists(ChallengeSave.Instance.ChallengeData))
        {
            int challengeLastIndex = ES3.Load<int>("ChallengeIndex", ChallengeSave.Instance.ChallengeData);
            Debug.Log(challengeLastIndex);
        
            if (challengeNeededToUnlock <= challengeLastIndex)
            {
                buttonObj.SetActive(true);
            }
            else
            {
                buttonObj.SetActive(false);
            }
        }
        else
        {
            buttonObj.SetActive(false);
        }
    }
    
    
}

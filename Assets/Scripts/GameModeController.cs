using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeController : MonoBehaviour
{
    public enum GameActualMode
    {
        BUILD,
        EDIT,
        EDITING,
        WAIT,
    }

    public GameActualMode actualMode;
    public static GameModeController Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        actualMode = GameActualMode.WAIT;
    }

    public void ChangeGameModeToWait()
    {
        actualMode = GameActualMode.WAIT;
    }

    public void ChangeModeToBuild()
    {
        actualMode = GameActualMode.BUILD;
    }

}

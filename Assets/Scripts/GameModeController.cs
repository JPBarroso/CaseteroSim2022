using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeController : MonoBehaviour
{
    public enum GameActualMode
    {
        BUILD,
        EDIT,
        NONE,
    }

    public GameActualMode actualMode;

}

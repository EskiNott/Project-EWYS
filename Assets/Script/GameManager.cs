using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    private bool gamePause;
    private void Start()
    {
        gamePause = false;
    }

    public void GamePause(bool Pause)
    {
        gamePause = Pause;
    }

    public static RaycastHit RaycastPhysical()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast((Ray)ray, out hit);
        return hit;
    }
}

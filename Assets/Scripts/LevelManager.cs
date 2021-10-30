using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager = default;


    void Initiliazed()
    {
        MagneticField.CubeEntered += HandleLevel;
    }

    private void Awake()
    {
        Initiliazed();
    }
    public void HandleLevel()
    {
        gameManager.currentCubeCount++;
        gameManager.ProggressBar();
        gameManager.LevelCompleted();
    }
}

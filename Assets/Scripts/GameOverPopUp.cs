using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOverPopUp : MonoBehaviour
{
    public GameObject gameOverPopup;
    void Start()
    {
        gameOverPopup.SetActive(false);

        GameEvents.OnGameOver += ShowGameOverPopup;
    }
    private void OnDisable()
    {
        GameEvents.OnGameOver -= ShowGameOverPopup;

    }
    private void ShowGameOverPopup()
    {
        gameOverPopup.SetActive(true);
    }
    void Update()
    {
        
    }
}

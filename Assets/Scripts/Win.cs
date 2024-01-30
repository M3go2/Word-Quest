using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject win;
    void Start()
    {
        win.SetActive(false);
    }

    private void OnEnable()
    {
        GameEvents.OnBoardCompleted += ShowWin;
    }

    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= ShowWin;

    }
    private void ShowWin()
    {
        win.SetActive(true);
    }
    public void LoadNextLevel()
    {
        GameEvents.LoadNextLevelMethod();
    }


}

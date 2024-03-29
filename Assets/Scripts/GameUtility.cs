using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUtility : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    } 
    public void ExitApp()
    {
        Application.Quit();
    }
    public void MuteToggleBackGroundMusic()
    {
        SoundManager.instance.ToggleBackGroundMusic();
    }
    public void MuteToggleSoundFx()
    {
        SoundManager.instance.ToggleSoundFx();
    }
}

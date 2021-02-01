using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Text ScoreCounter;
    private void Start()
    {
        if (MusicClass.Score < 1)
        ScoreCounter.text = MusicClass.Score.ToString() + "/8 rings!";
        else
            ScoreCounter.text = MusicClass.Score.ToString() + "/8 rings!";
    }
    public void Quit()
    {
        Application.Quit();
    }
}

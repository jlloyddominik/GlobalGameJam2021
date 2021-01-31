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
        ScoreCounter.text = MusicClass.Score.ToString() + " belonging!";
        else
            ScoreCounter.text = MusicClass.Score.ToString() + " belongings!";
    }
    public void Quit()
    {
        Application.Quit();
    }
}

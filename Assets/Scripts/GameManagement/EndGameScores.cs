using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//calculates the level and displays it at the end of the game
public class EndGameScores : MonoBehaviour
{
    [SerializeField] private Text[] scoresDisplayTxt = null;                   //text to display all scores to player
    private string[] messages = new string[4] { "Score: ", "Time: ", "Level Bonus: ", "Final Score: " };        //stores text that will be displayed per score
    private float[] scores = new float[4];          //stores all types of scores
    private bool canExitScores = false;             //checks if player can return back to main menu
    #region Visualization 
    //enum for personal visualisation
    private enum ScoreNames
    {
        Score,
        Time,
        LevelBonus,
        TotalScore
    }
    #endregion
    //sets up scores and displays them over time
    private void Start()
    {
        canExitScores = false;
        for (int i = 0; i < scoresDisplayTxt.Length; i++)
        {
            scoresDisplayTxt[i].text = messages[i];
        }
        CalculateScores();
        StartCoroutine(DisplayScoresSlowly());
    }

    //sets up and calculates all scores;
    private void CalculateScores()
    {
        float totalScore = 0;
        scores[0] = GameManager.instance.Points;
        scores[1] = GameManager.instance.GameTime;
        scores[2] = GameManager.instance.LevelBonus;

        totalScore = scores[0] + scores[1];
        totalScore = totalScore * scores[2];
        scores[3] = totalScore;
    }

    //used for slowly displaying scores one by one
    private IEnumerator DisplayScoresSlowly()
    {
        int iteration = 0;
        yield return new WaitForSeconds(0.5f);
        while(iteration < scoresDisplayTxt.Length)
        {   
            scoresDisplayTxt[iteration].text = messages[iteration] + scores[iteration].ToString("0");
            if(iteration == 2)
            {
                scoresDisplayTxt[iteration].text = messages[iteration] + scores[iteration].ToString("0.0");
            }
            iteration++;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
        canExitScores = true;
        yield return null;
    }

    //Destroys instance and returns player to main menu
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (canExitScores == true)
            {
                Application.Quit();
            }
        }
    }
}

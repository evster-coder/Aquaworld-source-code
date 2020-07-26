using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCompleted : MonoBehaviour {

    public GameObject Panel;
    private Player Statistic;
    public Text scoretext; //счёт за уровень текст
    public Text totalscoretext; //общий счёт текст
    public Text timescoretext; //бонус за время текст

    public AudioClip scoreadd;
    public AudioClip scoreend;
    private AudioSource sourse;

    private int timeScore; //для красивого вывода
    private int currScore;//чтоб красиво выводить
    private int currTotal;

    void Start () {
        sourse = Panel.GetComponent<AudioSource>();
        Panel.SetActive(false);
        Statistic = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        totalscoretext.text = Statistic.TotalScore.ToString();
        currTotal = Statistic.TotalScore;
    }

    public void NextLevel()
    {
        TotalStatistics.LevelsScore[TotalStatistics.CurrentLevel] = Statistic.Score + Statistic.TimeScore;
        TotalStatistics.CurrentLevel++;
        TotalStatistics.TotalScore = Statistic.TotalScore;
        if(TotalStatistics.CurrentLevel == 10)//если прошли все уровни то записываем рекорд
        {
            Highscore hs = new Highscore();
            hs.AddScore(TotalStatistics.Nickname, TotalStatistics.TotalScore);
        }
        SceneManager.LoadScene(2);
    }
    public void GoToMenu()
    {
        TotalStatistics.TotalScore = Statistic.TotalScore;
        TotalStatistics.LevelsScore[TotalStatistics.CurrentLevel] = Statistic.Score + Statistic.TimeScore;
        TotalStatistics.CurrentLevel++;
        SaveGame.SavingGame();
        if (TotalStatistics.CurrentLevel == 10)//если прошли все уровни то записываем рекорд
        {
            Highscore hs = new Highscore();
            hs.AddScore(TotalStatistics.Nickname, TotalStatistics.TotalScore);
        }
        SceneManager.LoadScene(0);
    }

    private int delay = 0;
    private void Update()
    {
        if (Panel.activeInHierarchy)
        {
            if (currScore == 0 && timeScore == 0)
                PlayEndOfScore();
            Time.timeScale = 0;
            delay += 1;
            if(delay > 100)
            {
                delay--;
                if (currScore > 10)
                {
                    currScore -= 10;
                    scoretext.text = "Очков за уровень: " + currScore;
                    currTotal += 10;
                    sourse.PlayOneShot(scoreadd);
                }
                else
                if (currScore <= 10 && currScore > 0)
                {
                    sourse.PlayOneShot(scoreadd);
                    currScore--;
                    scoretext.text = "Очков за уровень: " + currScore;
                    currTotal++;
                }
                if (timeScore > 5)
                {
                    sourse.PlayOneShot(scoreadd);
                    timeScore -= 5;
                    timescoretext.text = timeScore.ToString();
                    currTotal += 5;
                }
                else
                    if (timeScore > 0 && timeScore <= 5)
                    {
                        sourse.PlayOneShot(scoreadd);
                        timeScore--;
                        timescoretext.text = timeScore.ToString();
                        currTotal++;
                    }
                totalscoretext.text = currTotal.ToString();
            }
            else
            {
                scoretext.text = "Очков за уровень: " + currScore;
                timescoretext.text = timeScore.ToString();
                totalscoretext.text = currTotal.ToString();
            }
        }
        else
        {
            timeScore = Statistic.TimeScore;
            currScore = Statistic.Score;
        }  
    }

    private bool endOfScore = false;
    private void PlayEndOfScore()
    {
        if (!endOfScore)
        {
            sourse.PlayOneShot(scoreend);
            endOfScore = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DiedMenu : MonoBehaviour {

    public AudioClip youDie;//звук при смерти
    public GameObject Panel;
    private Player Statistic;
    public Text scoretext;
    private AudioSource sourse;
    private AudioSource onPlayerSource; //нужно замутить при воспроизведении другого источника
    private bool is_played = false;

    
    private void Start()
    {
        Panel.SetActive(false);
        sourse = Panel.GetComponent<AudioSource>();
        onPlayerSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        Statistic = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    void Update () {
        if (Panel.activeInHierarchy)
        {
            if (!is_played)//чтобы сыграть только 1 раз
            {
                is_played = true;
                onPlayerSource.mute = true;
                sourse.PlayOneShot(youDie);
            }
            Time.timeScale = 0;
            scoretext.text = "Набрано очков: " + Statistic.Score;
        }
	}
    public void GoToMenu()
    {
        TotalStatistics.TotalScore = Statistic.TotalScore;
        SceneManager.LoadScene(2);
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}

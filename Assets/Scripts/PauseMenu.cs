using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    private bool is_on_pause;
    public GameObject Panel;

    private void Start()
    {
        Time.timeScale = 1;
        is_on_pause = false;
    }
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!is_on_pause)
            {
                Time.timeScale = 0;
                is_on_pause = true;
                Panel.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                is_on_pause = false;
                Panel.SetActive(false);
            }
        }
	}
    public void Continue()
    {
        Time.timeScale = 1;
        is_on_pause = false;
        Panel.SetActive(false);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(2);
    }
}

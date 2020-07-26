using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChooseHero : MonoBehaviour {
    //Экран выбора персонажа
    public GameObject PanelNastya;
    public GameObject PanelGin;
    public GameObject PanelGosha;
    public Button ButtonOK;
    public InputField Nickname;
    public AudioSource sourceOnScene; //аудио на сцене(set volume)
    private GameObject NastyaInfo, GoshaInfo, GinInfo;

    private int result_of_choice;
    void Start()
    {
        //устанавливаем ссылки
        NastyaInfo = PanelNastya.transform.GetChild(2).gameObject;
        GinInfo = PanelGin.transform.GetChild(2).gameObject;
        GoshaInfo = PanelGosha.transform.GetChild(2).gameObject;

        ButtonOK.interactable = false;
        NastyaInfo.SetActive(false);
        GinInfo.SetActive(false);
        GoshaInfo.SetActive(false);

        sourceOnScene.volume = (80 - UIMenu.volume * (-1)) / 100;
    }
    public void OnClickNastya()
    {
        if (checkname(Nickname.text) != 0)
            ButtonOK.interactable = true;
        NastyaInfo.SetActive(true);
        GinInfo.SetActive(false);
        GoshaInfo.SetActive(false);
        result_of_choice = 1;
    }
    public void OnClicGin()
    {
        NastyaInfo.SetActive(false);
        GinInfo.SetActive(true);
        GoshaInfo.SetActive(false);
        if (checkname(Nickname.text) != 0)
            ButtonOK.interactable = true;
        result_of_choice = 2;
    }
    public void OnClickGosha()
    {
        NastyaInfo.SetActive(false);
        GinInfo.SetActive(false);
        GoshaInfo.SetActive(true);
        if (checkname(Nickname.text) != 0)
            ButtonOK.interactable = true;
        result_of_choice = 3;
    }
    public void OnInputName()
    {
        if (Nickname.text == "")
            ButtonOK.interactable = false;
        else
        {
            if (!NastyaInfo.activeInHierarchy && !GoshaInfo.activeInHierarchy && !GinInfo.activeInHierarchy)
                ButtonOK.interactable = false;
            else
            {
                if (checkname(Nickname.text) == 1)
                    ButtonOK.interactable = true;
                else
                    ButtonOK.interactable = false;
            }
        }
        
    }
    public void StartGame()
    {
        if (result_of_choice > 0 && result_of_choice < 4)
        {
            TotalStatistics.Character = result_of_choice;
            TotalStatistics.Nickname = Nickname.text;
            SceneManager.LoadScene(2);
        }
    }

    public int checkname(string nick)
    {
        if(nick.Length > 20)
            return 0;
        bool flag = false;
        if (nick.Contains("#"))
            return 0;
        if (nick == "")
            return 0;
        foreach (char c in nick)
        {
            if (c != ' ' && c != '.' && c != ',')
            {
                flag = true;
                break;
            }
        }
        if (!flag)
            return 0;
        else
            return 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.IO;

public class UIMenu : MonoBehaviour {
    public GameObject PanelMain;
    public GameObject PanelSettings;
    public AudioMixer sounds; //для громкости звука
    public Dropdown Dropdown; //выпадающее меню
    public Slider slider; //слайдер громкости
    public GameObject buttonStart;//кнопка начать
    public GameObject panelStart; //панель начала
    public GameObject panelHelp; //меню помощи
    public GameObject panelHighscore;//панель с рекордами

    public static float volume = 0; //громкостб
    public static int screensize = 0;

    private GameObject[] imagesHelp;
    private Animator _anim_start;
    private Resolution[] rsl; 
    private string[] resolutions;

    private SaveGame SaveGame; //для сохранения игры

    private void Awake()
    {
        SaveGame = gameObject.GetComponent<SaveGame>();

        rsl = Screen.resolutions;//массив из разрешений
        resolutions = new string[rsl.Length]; //массив из строк-разрешений
        int i = 0;
        for(int j = 0; j < rsl.Length; j++)
            if (rsl[j].width > 639 && rsl[j].height > 479)
                resolutions[i++] = rsl[j].width.ToString() + "x" + rsl[j].height.ToString();
        resolutions = resolutions.Where(x => x != null).ToArray();
        Dropdown.ClearOptions();//очищаем
        Dropdown.AddOptions(resolutions.ToList());//добавляем все пункты массива
        Dropdown.value = rsl.Length - 1 - screensize;

        //получение изображений для меню помощь
        imagesHelp = new GameObject[4];
        imagesHelp[0] = panelHelp.transform.GetChild(0).GetChild(0).gameObject;
        imagesHelp[1] = panelHelp.transform.GetChild(0).GetChild(1).gameObject;
        imagesHelp[2] = panelHelp.transform.GetChild(0).GetChild(2).gameObject;
        imagesHelp[3] = panelHelp.transform.GetChild(0).GetChild(3).gameObject;


        //заполнение рекордов
        Highscore hs = new Highscore();
        List <Highscore.records> list = hs.getScore();//копирую рекорды из класса
        int size = list.Count;
        Transform TableRecords = panelHighscore.transform.GetChild(1);
        for(int k = 0; k < size; k++)
        {
            TableRecords.GetChild(k).GetChild(1).GetComponent<Text>().text = list[k].name;
            TableRecords.GetChild(k).GetChild(2).GetComponent<Text>().text = list[k].score.ToString();
        }
        for(int k = size; k < 10; k++)
        {
            TableRecords.GetChild(k).GetChild(0).GetComponent<Text>().text = "";
            TableRecords.GetChild(k).GetChild(1).GetComponent<Text>().text = "";
            TableRecords.GetChild(k).GetChild(2).GetComponent<Text>().text = "";
        }
    }
    private void Start()
    {
        panelStart.SetActive(false);
        slider.value = volume;
    }
    public void StartGame()
    {
        buttonStart.SetActive(false);
        panelStart.SetActive(true);
    }
    public void StartNewGame()
    {
        TotalStatistics.Character = 1;
        TotalStatistics.TotalScore = 0;
        TotalStatistics.CurrentLevel = 0;
        for (int i = 0; i < 10; i++)
            TotalStatistics.LevelsScore[i] = 0;
        SaveGame.StartNewGame();//создание файла с сохранениями
        sounds.GetFloat("VolumeMaster", out volume);
        screensize = rsl.Length - 1 - Dropdown.value;
        SceneManager.LoadScene(3);
    }
    public void ContinueGame()
    {
        SaveGame.ContinueGame();//загрузка файла с сохранениями
        sounds.GetFloat("VolumeMaster", out volume);
        screensize = rsl.Length - 1 - Dropdown.value;
        SceneManager.LoadScene(2);
    }
    public void BackToGame()
    {
        buttonStart.SetActive(true);
        panelStart.SetActive(false);
    }
    public void Help()
    {
        panelHelp.SetActive(true);
        PanelMain.SetActive(false);
        numOfImg = 1;
        imagesHelp[0].SetActive(true);
    }
    public void HighscoreButt()
    {
        PanelMain.SetActive(false);
        panelHighscore.SetActive(true);
    }
    public void SettingsGame()
    {
        PanelMain.gameObject.SetActive(false);
        PanelSettings.gameObject.SetActive(true);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    //Настройки меню Settings
    public void AudioVolume(float sliderValue)//так сказатб громкость
    {
        sounds.SetFloat("VolumeMaster", sliderValue);
    }
    public void ScreenSize()
    {
        Screen.SetResolution(rsl[Dropdown.value].width, rsl[Dropdown.value].height, true);
    }
    public void BackToMainMenu()
    {
        PanelSettings.gameObject.SetActive(false);
        PanelMain.gameObject.SetActive(true);
    }
    //Помощь меню
    private int numOfImg;
    public void ButtNext()//кнопка дальше
    {
        if (numOfImg < 4)
        {
            imagesHelp[numOfImg].SetActive(true);
            imagesHelp[numOfImg - 1].SetActive(false);
            numOfImg++;
        }
        else
        {
            imagesHelp[3].SetActive(false);
            imagesHelp[0].SetActive(true);
            panelHelp.SetActive(false);
            PanelMain.SetActive(true);
        }
    }
    //Рекорды меню\
    public void ClickBackButton()
    {
        PanelMain.SetActive(true);
        panelHighscore.SetActive(false);
    }
}

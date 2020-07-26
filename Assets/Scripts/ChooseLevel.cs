using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour {

    public AudioSource sourceOnScene; //звуковой источник на сцену
    public Text ScoreText;
    public Image CharacterImg;
    public Text NicknameText;
    public Text CurrentLevelText;
    public GameObject ChoiceLevel;
    public Button PlayStart;
    public GameObject InfoLevel; //панель с информацией об уровне
    public GameObject Container; //контейнер с картинками рыб-целей уровней
    public GameObject LevelPlayerPanel; //панель  с уровнем игрока
    public GameObject GameFinished;

    private GameObject transparent; //прозрачная картинка
    private Image img1, img2; //для целей на каждый из уровней
    private Text target1, target2; //для целей
    private int currLevel; //текущий уровень(чтобы не лазить каждый раз в TotalStatistics)
    private int[] levelsExp; //массив опыта для каждого нового уровня

    void Awake () {
        SaveGame.SavingGame();
        //settings of volume
        sourceOnScene.volume = (80 - UIMenu.volume * (-1)) / 100;

        PlayStart.interactable = false; //изначально уровень не выбран

        currLevel = TotalStatistics.CurrentLevel;
        ScoreText.text = TotalStatistics.TotalScore.ToString();
        img1 = InfoLevel.transform.GetChild(2).GetComponent<Image>();
        img2 = InfoLevel.transform.GetChild(4).GetComponent<Image>();
        transparent = GameObject.Find("prozrachno");
        target1 = InfoLevel.transform.GetChild(3).GetComponent<Text>();
        target2 = InfoLevel.transform.GetChild(5).GetComponent<Text>();

        if (TotalStatistics.Character == 1)
            CharacterImg.sprite = GameObject.FindGameObjectWithTag("Fish1").GetComponent<SpriteRenderer>().sprite;
        else
            if(TotalStatistics.Character == 2)
                CharacterImg.sprite = GameObject.FindGameObjectWithTag("Fish2").GetComponent<SpriteRenderer>().sprite;
            else
                if(TotalStatistics.Character == 3)
                    CharacterImg.sprite = GameObject.FindGameObjectWithTag("Fish3").GetComponent<SpriteRenderer>().sprite;

        NicknameText.text = "Здравствуй,\n" + TotalStatistics.Nickname;
        if (currLevel < 10)
            CurrentLevelText.text = (currLevel + 1).ToString();
        else
            CurrentLevelText.text = "Игра завершена!";

        //подсчёт уровня игрока
        levelsExp = new int[20]{ 100, 500, 1000, 1100, 1250, 1500, 1750, 2000, 2500, 3000, 3500, 4000, 4500, 5000, 5200, 5400, 5500, 6000, 10000, 50000 };

        TotalStatistics.levelPlayer = PlayersLevel();
        LevelPlayerPanel.transform.GetChild(0).GetComponent<Text>().text = PlayersLevel().ToString();
        LevelPlayerPanel.transform.GetChild(3).GetComponent<Text>().text = "+" + ((int)TotalStatistics.levelPlayer / 2).ToString();
        if (TotalStatistics.levelPlayer < 21)
        {
            if (TotalStatistics.levelPlayer > 1)
            {
                int sum = 0;
                for (int i = 0; i < TotalStatistics.levelPlayer - 1; i++)
                    sum += levelsExp[i];
                LevelPlayerPanel.transform.GetChild(1).GetComponent<Text>().text = (TotalStatistics.TotalScore - sum).ToString();
            }
            else
                LevelPlayerPanel.transform.GetChild(1).GetComponent<Text>().text = TotalStatistics.TotalScore.ToString();
            LevelPlayerPanel.transform.GetChild(2).GetComponent<Text>().text = levelsExp[TotalStatistics.levelPlayer - 1].ToString();
        }
        else
        {
            LevelPlayerPanel.transform.GetChild(1).GetComponent<Text>().text = (TotalStatistics.TotalScore - 113800).ToString();
            LevelPlayerPanel.transform.GetChild(2).GetComponent<Text>().text = "Infinity";
        }

        //подгрузка задач на каждый уровень
        img2.sprite = transparent.GetComponent<SpriteRenderer>().sprite;
        img1.sprite = transparent.GetComponent<SpriteRenderer>().sprite;
        target1.text = "";
        target2.text = "";
    }
    private void Start()
    {
        if (currLevel < 10)
        {
            for (int i = 0; i <= currLevel; i++)
                ChoiceLevel.transform.GetChild(i).GetComponent<Button>().interactable = true;
            for (int i = currLevel + 1; i < 10; i++)
                ChoiceLevel.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
        else
        {
            for (int i = 0; i < 10; i++)
                ChoiceLevel.transform.GetChild(i).GetComponent<Button>().interactable = true;
            GameFinished.SetActive(true);
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        SaveGame.SavingGame();
        SceneManager.LoadScene(0);
    }
    public void ClickButtonLevel(int numOfButt)//при клике на кнопку уровня
    {
        if (numOfButt == currLevel)
            PlayStart.interactable = true;
        else
            PlayStart.interactable = false;

        InfoLevel.transform.GetChild(0).GetComponent<Text>().text = "Уровень " + (numOfButt + 1).ToString();
        InfoLevel.transform.GetChild(1).GetComponent<Text>().text = "Счет: " + TotalStatistics.LevelsScore[numOfButt];
        img1.sprite = Container.transform.Find(TotalStatistics.targets[numOfButt].name1).GetComponent<SpriteRenderer>().sprite;
        target1.text = TotalStatistics.targets[numOfButt].target_amount1.ToString();
        if(TotalStatistics.targets[numOfButt].name2 != "None")
        {
            img2.sprite = Container.transform.Find(TotalStatistics.targets[numOfButt].name2).GetComponent<SpriteRenderer>().sprite;
            target2.text = TotalStatistics.targets[numOfButt].target_amount2.ToString();
        }
        else
        {
            img2.sprite = transparent.GetComponent<SpriteRenderer>().sprite;
            target2.text = "";
        }

    }
    public int PlayersLevel()
    {
        int sum = 0; //сумма очков
        int exp = TotalStatistics.TotalScore;
        if (exp < levelsExp[0])// от 0 до 100 опыта
            return 1;
        int i;
        for (i = 0; i < 20; i++)
            if (exp >= sum)
                sum += levelsExp[i];
            else
                break;
        if (exp < sum)
            return i;
        else
            return 21;
    }
    public void ClickOK()//при прохождении всех уровней открывается эта панель
    {
        GameFinished.SetActive(false);
    }
}

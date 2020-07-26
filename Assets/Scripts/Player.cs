using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class Player : MonoBehaviour {

    public int Score; //ОЧКИ ИГРОКА
    public int TotalScore;//ОБЩИЙ СЧЁТ
    public int TimeScore;//БОНУС ЗА ВРЕМЯ
    public int Lifes; //КОЛИЧЕСТВО ЖИЗНЕЙБ
    public int Rate; //МНОЖИТЕЛЬ ОЧКОВ

    protected float cooldown;
    protected struct MissionTarget//цели на уровень
    {
        public string name;
        public int current_amount;
        public int target_amount;
    };
    protected MissionTarget[] targets;
    public Vector3 Scale; //размеры по х и у

    private GameObject ScoreT;
    private GameObject ScoreL;
    private GameObject TimerT;
    private GameObject TargetT; //задача на уровенб

    private GameObject DiedPanel;
    private GameObject LevelCompletedPanel;
    private GameObject TargetIMGS; //контейнер с картинками для целей уровня
    protected Image cooldownIMG;

    private Text ScoreText;
    private Text LifesText;
    private Text TimerText;
    private Text[] TargetTexts;

    private bool endOfBonusTime = false; //когда бонусные очки за время перестают начисляться

    public string lifesText//сеттер для жизней текста
    {
        set
        {
            LifesText.text = value;
        }
    }
    public void minus15Sec()
    {
        if (timerSecond >= 15)
            timerSecond -= 15;
        else
        {
            if (timerMinute > 0)
            {
                timerMinute--;
                timerSecond -= 15;
                timerSecond = 60 + timerSecond;
            }
            else
                timerSecond = 0;
        }
    }

    protected Animator _anim;//аниматор
    protected PlayerCntrl cntrl; //управление

    private void Awake()
    {
        int currentLevel = TotalStatistics.CurrentLevel;

        TotalScore = TotalStatistics.TotalScore;
        ScoreT = GameObject.FindGameObjectWithTag("ScoreText");
        TimerT = GameObject.FindGameObjectWithTag("TimerText");
        TargetT = GameObject.FindGameObjectWithTag("TargetPanel");
        ScoreL = GameObject.FindGameObjectWithTag("LifeText");
        DiedPanel = GameObject.FindGameObjectWithTag("DiedPanel");
        LevelCompletedPanel = GameObject.FindGameObjectWithTag("LevelCompletedPanel");
        TargetIMGS = GameObject.FindGameObjectWithTag("TargetsIMG");
        cooldownIMG = GameObject.FindGameObjectWithTag("Cooldown").GetComponent<Image>();

        //установление заданий на уровень
        targets = new MissionTarget[2];
        TargetTexts = new Text[2];
        for (int i = 0; i < 2; i++) //поменять можно
        {
            targets[i] = new MissionTarget();
        }
        TotalStatistics.Mission targetToThisLevel = TotalStatistics.targets[currentLevel];

        targets[0].name = targetToThisLevel.name1;
        targets[0].current_amount = 0;
        targets[0].target_amount = targetToThisLevel.target_amount1;
        targets[1].name = targetToThisLevel.name2;
        targets[1].current_amount = 0;
        targets[1].target_amount = targetToThisLevel.target_amount2;
        //получаю тексты
        TargetTexts[0] = TargetT.transform.GetChild(0).GetChild(0).GetComponent<Text>();
        TargetTexts[1] = TargetT.transform.GetChild(1).GetChild(0).GetComponent<Text>();

        //получаю картинки
        for (int i = 0; i < 2; i++)
        {
            string name = targets[i].name;
            if (name != "None")
            {
                if (name == "Enemys1")
                    TargetT.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = TargetIMGS.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                else
                {
                    if (name == "Enemys2")
                        TargetT.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = TargetIMGS.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
                    else
                    {
                        if (name == "Enemys3")
                            TargetT.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = TargetIMGS.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite;
                        else
                        {
                            if(name == "Enemys4")
                                TargetT.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = TargetIMGS.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite;
                            else
                            {
                                if(name == "Enemys5")
                                    TargetT.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = TargetIMGS.transform.GetChild(4).GetComponent<SpriteRenderer>().sprite;
                                else
                                    if(name == "Enemys6")
                                        TargetT.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = TargetIMGS.transform.GetChild(5).GetComponent<SpriteRenderer>().sprite;
                            }
                        }
                    }
                }

            }
        }
        //установка размеров
        Scale.Set(targetToThisLevel.sizePlayer, targetToThisLevel.sizePlayer, 1);
        transform.localScale = Scale;

        _anim = GetComponent<Animator>();
        cntrl = GetComponent<PlayerCntrl>();
        ScoreText = ScoreT.GetComponent<Text>();
        LifesText = ScoreL.GetComponent<Text>();
        TimerText = TimerT.GetComponent<Text>();
    }
    protected virtual void Start()
    {
        Lifes = 3 + (int)(TotalStatistics.levelPlayer / 2);
        Score = 0;
        Rate = 1;
        cooldown = 0;

        for (int i = 0; i < 2; i++)
        {
            if (targets[i].name != "None")
                TargetTexts[i].text = targets[i].current_amount + " / " + targets[i].target_amount;
        }
        ScoreText.text = "СЧЕТ: " + Score;
        LifesText.text = "" + Lifes;
    }

    private float timerSecond = 0; //для таймера
    private float timerMinute = 0;
    protected virtual void Update()
    { 
        timerSecond += Time.deltaTime;
        Change_time();
    }
    public bool is_target()
    {
        for (int i = 0; i < 2; i++)
            if (targets[i].current_amount != targets[i].target_amount)
                return false;
        return true;
    }
    public void IsTarget(string name)
    {
        for (int i = 0; i < 2; i++)
            if (name == targets[i].name && targets[i].current_amount != targets[i].target_amount)
            {
                targets[i].current_amount++;
                TargetTexts[i].text = targets[i].current_amount + " / " + targets[i].target_amount;
                if (is_target())
                {
                    if (timerMinute < 3)
                        TimeScore = ((2 - (int)timerMinute) * 60 + (60 - (int)timerSecond)) * 10;
                    else
                        TimeScore = 0;
                    TotalScore += TimeScore + Score;
                    Invoke("LevelCompleted", 2f);
                    cntrl.godMode = true;
                }
            }
    }
    public void Change_score(int score)
    {
        transform.localScale = Scale;
        Score += score * Rate;
        ScoreText.text = "СЧЕТ: " + Score;
    }
    public void Change_life(int i)
    {
        PlayerDie();

        _anim.SetBool("is_die", true);
        Lifes -= i;
        if (Lifes < 0)
            DiedPanel.SetActive(true);
        LifesText.text = "" + Lifes;
    }
    public void Change_time()
    {
        if (timerSecond > 59)
        {
            timerMinute++;
            timerSecond = 0;
            if (!endOfBonusTime && timerMinute > 2)
            {
                endOfBonusTime = true;
                TimerText.color = new Color(1f, 0f, 0f);
            }
        }
        if (timerMinute < 10)
            TimerText.text = "0" + (int)timerMinute + " : ";
        else
            TimerText.text = (int)timerMinute + " : ";
        if (timerSecond < 10)
            TimerText.text += "0" + (int)timerSecond;
        else
            TimerText.text += (int)timerSecond;
        if (timerMinute > 59)
            DiedPanel.SetActive(true);
    }

    protected void PlayerDie()
    {
        if (Lifes > 0)
        {
            StartCoroutine(MapEditor.youDie());
            StartCoroutine(diePerson());
            transform.position = new Vector3(0, 0, 2);
        }
    }
    protected void LevelCompleted()
    {
        LevelCompletedPanel.SetActive(true);
    }
    protected IEnumerator diePerson()//при каждой смерти персонажа
    {
        transform.position = new Vector3(0, 5, 2);
        yield return new WaitForSeconds(2f);
        transform.position = new Vector3(0, 0, -1);
    }
    public void ChangeRateDown()//уменьшить коэффициент очков
    {
        Invoke("rateDiv2", 5f);
    }
    private void rateDiv2()//само уменьшение
    {
        Rate /= 2;
    }
    public string getNameOfTargets()
    {
        for (int i = 1; i >= 0; i--)
            if (targets[i].current_amount < targets[i].target_amount)
            {
                IsTarget(targets[i].name);
                return targets[i].name;
            }
        return "None";
    }

    protected abstract void UseSkill();//скилл у каждой рыбки свой
}

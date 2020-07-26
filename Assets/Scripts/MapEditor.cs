using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class MapEditor : MonoBehaviour {

    public GameObject walls;
    public GameObject back;
    private static GameObject BonusPanel; //панель бонусов
    public AudioSource onPanelCompleted; //аудиоисточник на панеле окончания уровня
    public AudioSource onPanelDied; //при смерти
    private AudioSource onPlayer;//аудиоисточник на игроке
    public GameObject[] resultOfChoice;//массив из трёх возможных игроков
    public GameObject[] bonus; //массив из бонусов
    public Camera mapCam;//камера для миникарты
    private static float size;//размер карты
    private float timer; //для появления бонусов
    public GameObject CameraPrefub;//cimemachinecamera prefub
    private static Cinemachine.CinemachineVirtualCamera cameraPlayer; //cmc object on scene
    public GameObject ContainerForLetters;
    private static GameObject[] letters;
    private static int activ_bonus; //работающий бонус

    private static GameObject player;
    private BoxCollider2D top, right, down, left;

    private void Awake()
    {
        int current_level = TotalStatistics.CurrentLevel;

        timer = 0; //установка таймера в 5 секунд по умолчанию
       switch (TotalStatistics.Character)//Спавним игрока
        {
            case 1:
                Instantiate(resultOfChoice[0], new Vector3(0, 0, -1), Quaternion.identity);
                break;
            case 2:
                Instantiate(resultOfChoice[1], new Vector3(0, 0, -1), Quaternion.identity);
                break;
            case 3:
                Instantiate(resultOfChoice[2], new Vector3(0, 0, -1), Quaternion.identity);
                break;
            default:
                Instantiate(resultOfChoice[0], new Vector3(0, 0, -1), Quaternion.identity);
                break;
        }
        GameObject a = GameObject.FindGameObjectWithTag("Player");

        //size of the map and borders
        size = TotalStatistics.targets[current_level].sizeMap;
        back.transform.localScale = new Vector3(size, size, 1);
        top = walls.transform.Find("up").GetComponent<BoxCollider2D>();
        right = walls.transform.Find("right").GetComponent<BoxCollider2D>();
        left = walls.transform.Find("left").GetComponent<BoxCollider2D>();
        down = walls.transform.Find("down").GetComponent<BoxCollider2D>();

        //settings of Cinemachine camera
        a.transform.GetChild(0).gameObject.AddComponent<Cinemachine.CinemachineBrain>();
        CameraPrefub.GetComponent<Cinemachine.CinemachineVirtualCameraBase>().Follow = a.transform;
        CameraPrefub.GetComponent<Cinemachine.CinemachineConfiner>().m_BoundingShape2D = back.GetComponent<PolygonCollider2D>();
        CameraPrefub.GetComponent<Cinemachine.CinemachineVirtualCamera>().m_Lens.Orthographic = true;
        cameraPlayer = Instantiate(CameraPrefub, new Vector3(0, 0, -2), Quaternion.identity).GetComponent<CinemachineVirtualCamera>();
        cameraPlayer.m_Lens.OrthographicSize = 14 * a.transform.localScale.y;

        //sounds and volume of game
        onPlayer = a.GetComponent<AudioSource>();
        onPanelCompleted.volume = (80 - UIMenu.volume * (-1)) / 100;
        onPanelDied.volume = (80 - UIMenu.volume * (-1)) / 100;
        onPlayer.volume = (80 - UIMenu.volume * (-1)) / 100;

        //отступы
        top.offset = new Vector2(top.offset.x, size * 12.5f);
        down.offset = new Vector2(down.offset.x, -size * 22f);
        left.offset = new Vector2(-size * 25f, left.offset.y);
        right.offset = new Vector2(size * 25f, right.offset.y);

        mapCam.orthographicSize = (top.offset.y - down.offset.y) / 2;

        //размеры
        top.size = new Vector2(size * 65f, top.size.y);
        down.size = new Vector2(size * 65f, down.size.y);
        left.size = new Vector2(left.size.x, size * 50f);
        right.size = new Vector2(right.size.x, size * 50f);
        BonusPanel = GameObject.FindGameObjectWithTag("BonusPanel");
        activ_bonus = -1;

        //letters for every die
        letters = new GameObject[10];
        for(int i = 0; i < 10; i++)
        {
            letters[i] = ContainerForLetters.transform.GetChild(i).gameObject;
        }
    }
    void Start () {
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public static void CheckCamera()//приближение камеры
    {
        if (player.transform.localScale.y < 1.2f * size)
            cameraPlayer.m_Lens.OrthographicSize = 12 * player.transform.localScale.y;
    }
    private void LateUpdate()//движение миникарты за игроком
    {
        mapCam.transform.position = player.transform.position - new Vector3(0,0,1);
    }
    private int localResult;
    private void Update()
    {
        if(activ_bonus != -1)//затушить картинку бонуса
        {
            StartCoroutine(SetDisable(activ_bonus));
            activ_bonus = -1;
        }
        if (timer > 0)//спавн бонусов
            timer -= Time.deltaTime;
        else
        {
            localResult = Random.Range(0, 5);
            switch (localResult)
            {
                case 0: Instantiate(bonus[0], new Vector3(0, 0, -1), Quaternion.identity); break;
                case 1: Instantiate(bonus[1], new Vector3(0, 0, -1), Quaternion.identity); break;
                case 2: Instantiate(bonus[2], new Vector3(0, 0, -1), Quaternion.identity); break;
                case 3: Instantiate(bonus[3], new Vector3(0, 0, -1), Quaternion.identity); break;
                case 4: Instantiate(bonus[4], new Vector3(0, 0, -1), Quaternion.identity); break;
            }
            timer = 10f;
     
        }
    }
    private IEnumerator SetDisable(int i)//угасание бонуса
    {
        Image img = BonusPanel.transform.GetChild(i).GetComponent<Image>();//запоминаем картинку
        var color = img.color;//запоминаем её цвет\
        color.a = 0.2f;//делаем её цвет полупрозрачным
        yield return new WaitForSeconds(5f);
        img.color = color;//устанавливаем этот цвет на картинку
    }
    public static void SetActive(int i)//подсветка бонусов
    {
        Image img = BonusPanel.transform.GetChild(i).GetComponent<Image>();//запоминаем картинку
        var color = img.color;//запоминаем её цвет
        color.a = 1;//делаем её непрозрачной
        img.color = color;//устанавливаем этот непрозрачный цвет на картинку
        activ_bonus = i;
    }
    public static IEnumerator youDie()//для красивого вывода букв
    {
        for (int i = 0; i < 10; i++)//печать по одной
        {
            letters[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 10; i++)//убираем все
        {
            letters[i].SetActive(false);
        }
    }
}

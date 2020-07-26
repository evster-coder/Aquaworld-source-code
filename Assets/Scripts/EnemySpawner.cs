using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {
    public GameObject[] Enemys = new GameObject[6];//контейнеры для врагов
    public GameObject Enemy1, Enemy2, Enemy3, Enemy4, Enemy5, Enemy6;//префабы.
    public GameObject imgContainer;
    public Image img;//кого могу съесть

    private PlayerCntrl player; //игрок
    private int levelNum;
    private float[] massMass;//массив из масс
	private void Awake ()//спавн врагов в зависимости от уровня
    { 
        Transform tr1 = Enemys[0].transform;
        Transform tr2 = Enemys[1].transform;
        Transform tr3 = Enemys[2].transform;
        Transform tr4 = Enemys[3].transform;
        Transform tr5 = Enemys[4].transform;
        Transform tr6 = Enemys[5].transform;
        levelNum = TotalStatistics.CurrentLevel;
        GameObject obj;
        Vector3 position = new Vector3(0, 0, -1);
        massMass = new float[6];
        switch(levelNum)
        {
            case 0:
                for (int i = 0; i < 10; i++)
                {
                    obj = Instantiate(Enemy6, position, Quaternion.identity);
                    obj.transform.parent = tr6;
                }
                break;
            case 1:
                for(int i = 0; i < 5; i++)
                {
                    obj = Instantiate(Enemy4, position, Quaternion.identity);
                    obj.transform.parent = tr4;
                }
                for(int i = 0; i < 3; i++)
                {
                    obj = Instantiate(Enemy1, position, Quaternion.identity);
                    obj.transform.parent = tr1;
                }
                break;
            case 2:
                for(int i = 0; i < 5; i++)
                {
                    obj = Instantiate(Enemy4, position, Quaternion.identity);
                    obj.transform.parent = tr4;
                }
                for(int i = 0; i < 3; i++)
                {
                    obj = Instantiate(Enemy1, position, Quaternion.identity);
                    obj.transform.parent = tr1;
                }
                for(int i = 0; i < 5; i++)
                {
                    obj = Instantiate(Enemy5, position, Quaternion.identity);
                    obj.transform.parent = tr5;
                }
                break;
            case 3:
                for (int i = 0; i < 5; i++)
                {
                    obj = Instantiate(Enemy4, position, Quaternion.identity);
                    obj.transform.parent = tr4;
                }
                for (int i = 0; i < 5; i++)
                {
                    obj = Instantiate(Enemy1, position, Quaternion.identity);
                    obj.transform.parent = tr1;
                }
                for (int i = 0; i < 3; i++)
                {
                    obj = Instantiate(Enemy5, position, Quaternion.identity);
                    obj.transform.parent = tr5;
                }
                break;
            case 4:
                for (int i = 0; i < 5; i++)
                {
                    obj = Instantiate(Enemy5, position, Quaternion.identity);
                    obj.transform.parent = tr5;
                }
                for(int i = 0; i < 3; i++)
                {
                    obj = Instantiate(Enemy1, position, Quaternion.identity);
                    obj.transform.parent = tr1;
                }
                for(int i = 0; i < 5; i++)
                {
                    obj = Instantiate(Enemy2, position, Quaternion.identity);
                    obj.transform.parent = tr2;
                }
                break;
            case 5:
                for (int i = 0; i < 10; i++)
                {
                    obj = Instantiate(Enemy4, position, Quaternion.identity);
                    obj.transform.parent = tr4;
                }
                for (int i = 0; i < 3; i++)
                {
                    obj = Instantiate(Enemy5, position, Quaternion.identity);
                    obj.transform.parent = tr5;
                }
                for (int i = 0; i < 5; i++)
                {
                    obj = Instantiate(Enemy1, position, Quaternion.identity);
                    obj.transform.parent = tr1;
                }
                for (int i = 0; i < 6; i++)
                {
                    obj = Instantiate(Enemy2, position, Quaternion.identity);
                    obj.transform.parent = tr2;
                }
                break;
            case 6:
                for (int i = 0; i < 5; i++)
                {
                    obj = Instantiate(Enemy4, position, Quaternion.identity);
                    obj.transform.parent = tr4;
                }
                for (int i = 0; i < 5; i++)
                {
                    obj = Instantiate(Enemy1, position, Quaternion.identity);
                    obj.transform.parent = tr1;
                }
                for (int i = 0; i < 6; i++)
                {
                    obj = Instantiate(Enemy2, position, Quaternion.identity);
                    obj.transform.parent = tr2;
                }
                for(int i = 0; i < 2; i++)
                {
                    obj = Instantiate(Enemy3, position, Quaternion.identity);
                    obj.transform.parent = tr3;
                }
                break;
            case 7:
                for (int i = 0; i < 5; i++)
                {
                    obj = Instantiate(Enemy1, position, Quaternion.identity);
                    obj.transform.parent = tr1;
                }
                for (int i = 0; i < 6; i++)
                {
                    obj = Instantiate(Enemy2, position, Quaternion.identity);
                    obj.transform.parent = tr2;
                }
                for (int i = 0; i < 3; i++)
                {
                    obj = Instantiate(Enemy3, position, Quaternion.identity);
                    obj.transform.parent = tr3;
                }
                break;
            case 8:
                for (int i = 0; i < 8; i++)
                {
                    obj = Instantiate(Enemy1, position, Quaternion.identity);
                    obj.transform.parent = tr1;
                }
                for (int i = 0; i < 7; i++)
                {
                    obj = Instantiate(Enemy3, position, Quaternion.identity);
                    obj.transform.parent = tr3;
                }
                break;
            case 9:
                for (int i = 0; i < 6; i++)
                {
                    obj = Instantiate(Enemy4, position, Quaternion.identity);
                    obj.transform.parent = tr4;
                }
                for (int i = 0; i < 3; i++)
                {
                    obj = Instantiate(Enemy5, position, Quaternion.identity);
                    obj.transform.parent = tr5;
                }
                for (int i = 0; i < 5; i++)
                {
                    obj = Instantiate(Enemy1, position, Quaternion.identity);
                    obj.transform.parent = tr1;
                }
                for (int i = 0; i < 4; i++)
                {
                    obj = Instantiate(Enemy2, position, Quaternion.identity);
                    obj.transform.parent = tr2;
                }
                for (int i = 0; i < 4; i++)
                {
                    obj = Instantiate(Enemy3, position, Quaternion.identity);
                    obj.transform.parent = tr3;
                }
                break;
            default:
                for (int i = 0; i < 6; i++)
                {
                    obj = Instantiate(Enemy4, position, Quaternion.identity);
                    obj.transform.parent = tr4;
                }
                break;
        }
	}
    private void Start()
    {
        for(int i = 0; i < 6; i++)
            if (Enemys[i].transform.childCount > 0)
            massMass[i] = Enemys[i].transform.GetChild(0).GetComponent<EnemyBehaviour>().EnemyMass;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCntrl>();
    }
    private float masMax; //максимальная среди доступных масс
    private void Update()
    {
        //определяем какая картинка изображена на панеле доступной рыбки
        masMax = 0;
        int j;
        for (int i = 0; i < 6; i++)//нахождение максимальной массы и её номера
            if (player.UserMass > massMass[i] && massMass[i] > masMax)
                masMax = massMass[i];
        for (j = 0; j < 6; j++)
            if (masMax == massMass[j])
                break;

        img.sprite = imgContainer.transform.GetChild(j).GetComponent<SpriteRenderer>().sprite;

    }
}

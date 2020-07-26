using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour {

    private Vector2 position_move;//точка куда движемся
    private bool check_stop;//дошли до точки или нет

    private GameObject Walls;//края карты
    private BoxCollider2D Top, Right;//объекты коллайдеров верха и право

    private float pos_x, pos_y;//позиция движения по x и по y
    public float speed = 2.0f;//скорость

    private float scale_x, scale_y, scale_z;

    private Camera CamHero; //камера персонажа

    public Vector2 Position_move{
        get
        {
            return position_move;
        }
        set
        {
            position_move = value;
        }
    }

    void Start()
    {
        CamHero = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        Walls = GameObject.FindWithTag("Walls");
        Top = Walls.transform.Find("up").GetComponent<BoxCollider2D>();
        Right = Walls.transform.Find("right").GetComponent<BoxCollider2D>();
        transform.position = get_random_pos();
        change_position();
    }

    public void change_position()//для новой координаты
    {
        check_stop = false;//Ещё не попали в ту же позицию

        pos_x = Right.offset.x;//вычисляем макс смещение по x
        pos_y = Top.offset.y;//макс смещение по y
        //определяем новую точку для движения
        position_move = new Vector2(Random.Range(-pos_x + 1, pos_x - 1), Random.Range(-pos_y + 1, pos_y - 1));
    } 
    public Vector3 get_random_pos()// для спавна в рандомной точке
    {
        pos_x = Right.offset.x;//вычисляем макс смещение по x
        pos_y = Top.offset.y;//макс смещение по y
        Vector3 position;
        bool on_visible;
        do//проверка чтобы не соспавнить юнит внутри видимости основной камеры
        { 
            position = new Vector3(Random.Range(-pos_x + 1, pos_x - 1), Random.Range(-pos_y + 1, pos_y - 1), -1);
            Vector3 a = CamHero.WorldToViewportPoint(position);
            if (a.x >= 0 && a.x <= 1 && a.y >= 0 && a.y <= 1)
                on_visible = true;
            else
                on_visible = false;
        } while (on_visible);
        return position;
    }
	void Update () {
        if (!check_stop)//не дошли до точки
        {
            if ((Vector2)transform.position == position_move)//вдруг дошли до точки на этот кадр
                check_stop = true;
            else
            {
                //перемещаемся со скоростью speed
                transform.position = Vector2.MoveTowards((Vector2)transform.position, position_move, speed * Time.deltaTime);
                scale_x = transform.localScale.x;
                scale_y = transform.localScale.y;
                scale_z = transform.localScale.z;

                //поворот спрайта в зависимости от направления движения
                if (position_move.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-Mathf.Abs(scale_x), scale_y, scale_z);//отражаем зеркально
                }
                else
                {
                    transform.localScale = new Vector3(Mathf.Abs(scale_x), scale_y, scale_z);//то же самое
                }
            } 
        }
        else
            change_position();//меняем точку движения
	}
}

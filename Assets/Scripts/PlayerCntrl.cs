using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCntrl : MonoBehaviour {

    private Vector2 mousePosition;
    private float delta;
    private float quot;
  
    private Player Stats;//статы из другого скрипта(очки, жизни и тд)

    public float UserMass;
    private Animator _anim;
    private AudioSource sourse; //аудиосурс
    public AudioClip eatSound;

    private float scale_x, scale_y, scale_z;// для вращения при движении
    private Transform thisTransform;

    public bool godMode; // неуязвимость при получении урона!

    void Start()
    {
        sourse = GetComponent<AudioSource>();
        godMode = false;
        thisTransform = transform;
        _anim = GetComponent<Animator>();
        Stats = gameObject.GetComponent<Player>();
        delta = 5;
        UserMass = Mathf.Abs(transform.localScale.x) * 5;
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            //Угол между объектами
            float angle = Vector2.Angle(Vector2.right, mousePosition - (Vector2)transform.position);
            scale_x = thisTransform.localScale.x;
            scale_y = thisTransform.localScale.y;
            scale_z = thisTransform.localScale.z;
            if (angle > 90)//если угол больше 90 градусов
            {
                thisTransform.localScale = new Vector3(-Math.Abs(scale_x), scale_y, scale_z);//отражаем зеркально
            }
            else
            {
                thisTransform.localScale = new Vector3(Math.Abs(scale_x), scale_y, scale_z);//то же самое
            }
            mousePosition -= (Vector2)transform.position;
            quot = Mathf.Sqrt(mousePosition.x * mousePosition.x + mousePosition.y * mousePosition.y + 0.1f) / delta;
            mousePosition /= quot;
            transform.Translate(mousePosition * Time.deltaTime);
        }
    }

    private int count = 0;
    private void OnTriggerStay2D(Collider2D col)//при соприкосновении
    {
        if (!godMode)// если не было недавно урона
        {
            if (col.gameObject.tag == "Enemy")
            {
                Vector2 mouseP = Input.mousePosition;
                mouseP = Camera.main.ScreenToWorldPoint(mouseP);

                bool enemy_mov = col.gameObject.GetComponent<EnemyMoving>().enabled;

                float EnemyMass = col.gameObject.GetComponent<EnemyBehaviour>().EnemyMass;//получил массу етого объекта
                if (EnemyMass < UserMass)
                {
                    _anim.SetBool("is_eating", true);
                    if (transform.localScale.x > 0)
                        Stats.Scale = transform.localScale + new Vector3(EnemyMass / 80, EnemyMass / 80, 0);
                    else
                        Stats.Scale = transform.localScale + new Vector3(-EnemyMass / 80, EnemyMass / 80, 0);

                    sourse.PlayOneShot(eatSound);
                    Stats.Change_score((int)(10f * EnemyMass));//добавление очков
                    Stats.IsTarget(col.transform.parent.name);
                    EnemyMoving mov = col.GetComponent<EnemyMoving>();
                    mov.enabled = true; //восстанавливаем движение врага
                    UserMass += EnemyMass / 10;
                    EnemyBehaviour.Changepos(mov);
                    count++;
                    if (count > 5)
                    {
                        MapEditor.CheckCamera();
                        count = 0;
                    }
                }
                else
                {
                    if (enemy_mov)//если враг в движении
                    {
                        godMode = true; // включаем режим бессмертия
                        Invoke("OffGodMode", 3.5f);//через 2 секунды вырубаем
                        Stats.Change_life(1);//меняем жизни
                    }
                }
            }
        }
    }
    private void StopAnims()
    {
        _anim.SetBool("is_eating", false);
        _anim.SetBool("is_die", false);
        _anim.SetBool("is_using_skill", false);
    }
    private void OffGodMode() //отключить неуязвимость
    {
        godMode = false;
    }
}

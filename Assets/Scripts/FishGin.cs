using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGin : Player {

    private float timeSkill;
    private Vector2 mousePosition;
    private GameObject Enemy; //враг к которому касание происходит
    public GameObject ball;//электрический шар(префабб)
    private GameObject F;//объект электрический шар
    private Dictionary<string, int> Dictionary;//словарик для остановки объектов от скила

    protected override void Start()
    {
        timeSkill = 5f + TotalStatistics.levelPlayer * 0.1f;//время действия умения
        base.Start();
        cooldown = 1f; //изначально умение перезаряжено
        Dictionary = new Dictionary<string, int>();//и словарь
        GameObject a = GameObject.FindGameObjectWithTag("Deactive"); //для хранения префабов
        Pool.Init(a.transform);
    }
    protected override void Update()
    {
        base.Update();
        cooldownIMG.fillAmount = cooldown;
        if (cooldown < 1)//перезарядка скилла
            cooldown += Time.deltaTime / 5;
        if (Input.GetKeyDown(KeyCode.Space))//нажат пробел
            if (cooldown >= 1)//умение перезаряжено
            {
                UseSkill();
            }
    }

    protected override void UseSkill()//останавливает врага, находящегося в этой точке
    {
        if (Time.timeScale == 1 && transform.position.z < 2)
        {
            cooldown = 0;
            _anim.SetBool("is_using_skill", true);//запуск анимации
            F = Pool.GetObjectFromPool(ball);//создаём электрический шар
            F.transform.position = transform.position;
            mousePosition = Input.mousePosition; //получаем координаты мышки
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            F.GetComponent<BallBehaviour>().FollowTo = mousePosition;

            Collider2D collider_enemy = Physics2D.OverlapPoint(mousePosition); //получаем колллайдер по этим 
            if (collider_enemy != null && collider_enemy.gameObject.tag == "Enemy")//если он не нулевой и его тег - враг, то останавливаем его
            {
                Enemy = collider_enemy.gameObject;
                Enemy.GetComponent<EnemyMoving>().enabled = false;
                StartCoroutine(ResumeEnemy(Enemy));
            }
            StartCoroutine(DeleteObj(F));
        }   
    }
    private IEnumerator DeleteObj(GameObject ball)//удаление объекта
    {
        yield return new WaitForSeconds(timeSkill);
        Pool.PushToPool(ball);
        
    }

    private IEnumerator ResumeEnemy(GameObject enemy)//восстановление движения врага
    {
        string name = enemy.name;
        if (!Dictionary.ContainsKey(name))
            Dictionary.Add(name, 1);
        else
            Dictionary[name]++;
        yield return new WaitForSeconds(timeSkill);
        if(Dictionary[name] <= 1)
            enemy.GetComponent<EnemyMoving>().enabled = true;
        Dictionary[name]--;
    }
}

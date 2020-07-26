using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class FishGosha : Player {

    private float old_mass;
    private Vector3 old_Scale;

    protected override void Start()
    {
        base.Start();
        cooldown = 1f; //изначально умение перезаряжено
    }

    protected override void Update()
    {
        base.Update();
        cooldownIMG.fillAmount = cooldown;
        if (cooldown < 1) //10 сек на перезарядку умения
            cooldown += Time.deltaTime / 10;
        if (Input.GetKeyDown(KeyCode.Space))//нажат пробел
            if (cooldown >= 1)//перезаряжено умение
            {
                UseSkill();
            }
    }

    protected override void UseSkill()
    {
        if (Time.timeScale == 1 && transform.position.z < 2)
        {
            cooldown = 0;
            old_mass = 0.25f * cntrl.UserMass;//сохраняем старую массу, чтобы потом её вычесть
            old_Scale = 0.25f * transform.localScale;//сохраняем старые размеры
            _anim.SetBool("is_using_skill", true);//проигрывание анимации
            cntrl.UserMass *= 1.25f;//увеличиваем массу и размер
            Scale = new Vector3(transform.localScale.x * 1.25f, transform.localScale.y * 1.25f, transform.localScale.z);
            Change_score(0);//вызываем применение изменений
            MapEditor.CheckCamera();//проверяем увеличение камеры
            float timeSkill = 5 + TotalStatistics.levelPlayer * 0.1f;
            Invoke("ChangeSizeBack", timeSkill);//вызываем функцию, возвращающую всё назад (на сколько время рыбка большая от уровня игрока)
        }
    }

    private void ChangeSizeBack()//возвращение размера рыбки назад
    {
        cntrl.UserMass -= old_mass;
        if(transform.localScale.x < 0)//зависит от поворота
            transform.localScale = new Vector3(transform.localScale.x + Math.Abs(old_Scale.x), transform.localScale.y - old_Scale.y, transform.localScale.z);
        else
            transform.localScale = new Vector3(transform.localScale.x - Math.Abs(old_Scale.x), transform.localScale.y - old_Scale.y, transform.localScale.z);
        Change_score(0);//вызываем применение изменени
        MapEditor.CheckCamera();//нужно ли приблизить камеру
        _anim.SetBool("is_using_skill", false);//проигрываем анимацию
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNastya : Player {

    public GameObject areaPrefub;
    private GameObject area;
    public float skillTime;

    protected override void Start()
    {
        base.Start();
        skillTime = 3f + TotalStatistics.levelPlayer * 0.25f;
        cooldown = 1f; //изначально умение перезаряжено
        area = Instantiate(areaPrefub, transform.position, Quaternion.identity);
        area.SetActive(false);
    }
    protected override void Update()
    {
        base.Update();
        cooldownIMG.fillAmount = cooldown;
        if (cooldown < 1) //10 сек на перезарядку умения
            cooldown += Time.deltaTime / 10;
        if (Input.GetKeyDown(KeyCode.Space))
            if (cooldown >= 1)
            {
                UseSkill();
            }
    }

    protected override void UseSkill()
    {
        if(Time.timeScale == 1 && transform.position.z < 2)
        {
            cooldown = 0;
            _anim.SetBool("is_using_skill", true);
            area.SetActive(true);
            Invoke("stop_skill", skillTime);
        }
    }

    void stop_skill()
    {
        _anim.SetBool("is_using_skill", false);
        area.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonus : BonusBehaviour {
    protected override void useBonus(GameObject Player)//бонус добавить жизнь
    {
        MapEditor.SetActive(3);
        Player player = Player.GetComponent<Player>();
        player.minus15Sec();
        Destroy(gameObject);
    }
}

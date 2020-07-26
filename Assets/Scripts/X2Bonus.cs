using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class X2Bonus : BonusBehaviour {//бонус на x2 опыта
    private Player player;
    protected override void useBonus(GameObject Player)
    {
        MapEditor.SetActive(4);
        player = Player.GetComponent<Player>();
        player.Rate *= 2;
        player.ChangeRateDown();
        Destroy(gameObject);
    }
}

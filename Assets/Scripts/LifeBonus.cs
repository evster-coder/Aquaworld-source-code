using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBonus : BonusBehaviour {
    protected override void useBonus(GameObject Player)//бонус добавить жизнь
    {
        MapEditor.SetActive(0);
        Player player = Player.GetComponent<Player>();
        player.Lifes++;
        player.lifesText = "" + player.Lifes;
        Destroy(gameObject);
    }
}

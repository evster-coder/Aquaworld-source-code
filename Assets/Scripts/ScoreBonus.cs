using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBonus : BonusBehaviour {
    protected override void useBonus(GameObject Player)//бонус добавить жизнь
    {
        MapEditor.SetActive(2);
        Player player = Player.GetComponent<Player>();
        player.Change_score(1000);
        Destroy(gameObject);
    }
}

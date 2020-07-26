using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBonus : BonusBehaviour {
    protected override void useBonus(GameObject Player)
    {
        MapEditor.SetActive(1);
        Player player = Player.GetComponent<Player>();
        string s = player.getNameOfTargets();
        if(s != "None")
        {
            float mass = GameObject.Find(s).transform.GetChild(0).GetComponent<EnemyBehaviour>().EnemyMass;
            Player.GetComponent<PlayerCntrl>().UserMass += mass / 10;
            if (player.transform.localScale.x > 0)
                player.Scale = player.transform.localScale + new Vector3(mass / 80, mass / 80, 0);
            else
                player.Scale = player.transform.localScale + new Vector3(-mass / 80, mass / 80, 0);

            player.Change_score((int)(30* mass));
        }
        Destroy(gameObject);
    }
}

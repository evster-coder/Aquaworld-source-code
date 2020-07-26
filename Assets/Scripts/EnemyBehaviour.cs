using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    public float EnemyMass;

	void Awake () {
        EnemyMass = Mathf.Abs(transform.localScale.x) * 5;
    }

    public static void Changepos(EnemyMoving a)
    {
        a.change_position();
        a.transform.position = a.get_random_pos();
    }
}

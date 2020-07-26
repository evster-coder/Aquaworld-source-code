using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {

    public Vector2 FollowTo;

    void Update () {
         transform.position = Vector2.MoveTowards(transform.position, FollowTo, 50f * Time.deltaTime);
	}
}

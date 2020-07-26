using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BonusBehaviour : MonoBehaviour {

    private BoxCollider2D top, right;//для выбора места спавна
    private Rigidbody2D forGravity;
    private float pos_down;

    protected void Awake () {
        forGravity = GetComponent<Rigidbody2D>();//получение компонента rigidbody
        forGravity.gravityScale = 0.15f;//устанавливаем гравитацию для бонусов

        //получение точки спавна
        Transform walls = GameObject.FindGameObjectWithTag("Walls").GetComponent<Transform>();
        top = walls.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        right = walls.GetChild(3).gameObject.GetComponent<BoxCollider2D>();
        pos_down = walls.GetChild(1).gameObject.GetComponent<BoxCollider2D>().offset.y;
        float pos_x = right.offset.x;//вычисляем макс смещение по x
        float pos_y= top.offset.y;//макс смещение по y
        transform.position = new Vector2(Random.Range(-pos_x + 1, pos_x - 1), pos_y + 3);
	}
    protected void Update()
    {
        if (transform.position.y < pos_down - 1)
            Destroy(gameObject);
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && collision.transform.position.z != 2)//касание с живым игроком
        {
            useBonus(collision.gameObject);
        }
    }
    protected abstract void useBonus(GameObject Player);

}

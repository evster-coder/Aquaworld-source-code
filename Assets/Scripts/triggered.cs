using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggered : MonoBehaviour {

    private GameObject Player;

    private bool isEnable;//включен объект или нет
    private float time;//время действия умения
    private Transform playerTR;//транформ игрока

    private bool endOfCoroutine = false; 
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerTR = Player.transform;
        transform.SetPositionAndRotation(playerTR.position, Quaternion.Euler(0,0,90));

        time = Player.GetComponent<FishNastya>().skillTime - 0.1f;
    }
    private void Update()
    {
        Vector3 position = playerTR.position;
        Vector3 scale = playerTR.localScale;
        transform.localScale = new Vector3(10 * Mathf.Abs(scale.x), 10 * scale.y, 1);
        if (playerTR.localScale.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
            transform.position = new Vector3(position.x + 5 * scale.y, position.y, position.z + 1);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
            transform.position = new Vector3(position.x - 5 * scale.y, position.y, position.z + 1);
        }
    }
    private void OnEnable()
    {
        StartCoroutine("WaitForSecondss");
        isEnable = true;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isEnable)
        {
            if (collision.gameObject.tag == "Enemy")
                collision.gameObject.GetComponent<EnemyMoving>().enabled = false;
        }
        else
        {
            if (collision.gameObject.tag == "Enemy")
                collision.gameObject.GetComponent<EnemyMoving>().enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.GetComponent<EnemyMoving>().enabled = true;
    }
    private IEnumerator WaitForSecondss()
    {
        yield return new WaitForSeconds(time);
        if (endOfCoroutine)
            isEnable = false;
        else
            endOfCoroutine = true;
    }
}

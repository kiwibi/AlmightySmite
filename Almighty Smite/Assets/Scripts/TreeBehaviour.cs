using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeBehaviour : MonoBehaviour
{
    public GameObject FloatingScore;
    private TreeMaster TreesAlive;
    private Transform Alive;
    private Transform Dead;
    private float Timer = 20.0f;
    private BoxCollider2D TreeCollider;
    private int RemoveFromScore;

    void Start()
    {
        TreesAlive = GameObject.Find("Tree Master").GetComponent<TreeMaster>();
        TreesAlive.TreesAlive++;
        Alive = transform.GetChild(0);
        Dead = transform.GetChild(1);
        Dead.gameObject.SetActive(false);
        TreeCollider = GetComponentInChildren<BoxCollider2D>();
        RemoveFromScore = 10;
    }

    void Update()
    {
        if (Alive.gameObject.activeSelf == false)
        {
            DeadUpdate();
        }
    }

    private void DeadUpdate()
    {
        Timer -= 1 * Time.deltaTime;
        if (Timer < 0)
        {
            Timer = 20.0f;
            TreesAlive.TreesAlive++;
            SwitchState();
        }
    }

    private void SwitchState()
    {
        if (Alive.gameObject.activeSelf == true)
        {
            Alive.gameObject.SetActive(false);
            Dead.gameObject.SetActive(true);
            var clone = Instantiate(FloatingScore, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, GameObject.Find("GameUI").transform);
            clone.GetComponent<floatingScore>().setParent(transform);
            clone.GetComponent<Text>().text = "-" + RemoveFromScore.ToString();
            clone.GetComponent<Text>().color = Color.red;
            ScoreManaging.RemoveScore(1);
        }
        else
        {
            Alive.gameObject.SetActive(true);
            Dead.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Disaster" && Alive.gameObject.activeSelf == true)
        {
            SwitchState();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    private TreeMaster TreesAlive;
    private Transform Alive;
    private Transform Dead;
    private float Timer = 20.0f;
    private BoxCollider2D TreeCollider;

    void Start()
    {
        TreesAlive = GameObject.Find("Tree Master").GetComponent<TreeMaster>();
        TreesAlive.TreesAlive++;
        Alive = transform.GetChild(0);
        Dead = transform.GetChild(1);
        Dead.gameObject.SetActive(false);
        TreeCollider = GetComponentInChildren<BoxCollider2D>();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_PLAYER_STATE
{
    Moving,
    Idle
}

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    private PlayerDir dir;
    private CharacterController controller;
    public E_PLAYER_STATE state;

    public bool isMoving = false;
    private void Start()
    {
        dir = this.GetComponent<PlayerDir>();
        controller = this.GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    void Move()
    {
        //计算目标与角色现在的距离
        float distance = Vector3.Distance(dir.targetPos, this.transform.position);
        //如果距离大于一个很小的数值
        if (distance > 0.3f)
        {
            isMoving = true;
            //移动状态
            state = E_PLAYER_STATE.Moving;
            //就简单移动(直接朝着面向走)
            controller.SimpleMove(transform.forward * speed);
        }
        else 
        {
            isMoving = false;
            //站立状态
            state = E_PLAYER_STATE.Idle;
        }
    }
}

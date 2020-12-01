using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //保持对玩家的引用
    private Transform player;
    private Vector3 offsetPosition;//位置偏移

    //拉进拉远的距离
    private float distance = 0;
    //鼠标滑轮的速度
    private float scrollSpeed=10;
    //滑轮的速度
    private float rotateSpeed=10;

    private bool isRotating;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Consts.Tags.Player).transform;
        //让相机的朝向 对着Player
        this.transform.LookAt(player);
        //计算偏移量
        offsetPosition = transform.position - player.position;
    }

    private void Update()
    {
        //跟随
        transform.position = offsetPosition + player.position;
        //处理视野的旋转
        RotateView();
        //处理拉进拉远
        ScrollView();
    }

    private void ScrollView() 
    {
        //初始化当前的距离
        distance = offsetPosition.magnitude;
        //设置新的距离
        distance += Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        //给距离设置一个限制
        distance = Mathf.Clamp(distance, 2, 16);
        //返回给偏移量
        offsetPosition = offsetPosition.normalized * distance;
    }

    private void RotateView() 
    {
        //Input.GetAxis("Mouse X");//得到鼠标在水平方向的滑动
        //Input.GetAxis("Mouse Y");//得到鼠标在垂直方向的滑动
        if (Input.GetMouseButtonDown(1)) 
        {
            isRotating = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRotating = false;
        }

        if (isRotating) 
        {
            //三个参数 1围绕哪个点 2围绕哪个轴  3旋转的度数
            transform.RotateAround(player.position, player.up, rotateSpeed * Input.GetAxis("Mouse X"));
            //旋转影响的属性有两个 1pos 2rot

            Vector3 originalPos = transform.position;
            Quaternion originalRotation = transform.rotation;

            transform.RotateAround(player.position, this.transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
            //对旋转角度进行限制
            float x = transform.eulerAngles.x;
            if (x<10||x>80) //当超出范围之后,我们将属性归为原来的 就是让旋转无效
            {
                transform.position = originalPos;
                transform.rotation = originalRotation;
            }
        }
        //更新offsetPosition
        offsetPosition = transform.position - player.position;
    }
}

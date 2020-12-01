using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerDir : MonoBehaviour
{
    private PlayerMove playerMove;

    /// <summary>
    /// 表示鼠标是否按下 是否可以移动
    /// </summary>
    private bool isClick;
    /// <summary>
    /// 目标位置
    /// </summary>
    [HideInInspector]public Vector3 targetPos = Vector3.zero;

    private void Start()
    {
        //开启的时候加载资源

        playerMove = this.GetComponent<PlayerMove>();
        //在开启的时候 把目标位置设置为默认的当前位置
        targetPos = this.transform.position;
    }

    private void Update()
    {
        //如果点击了 并且没有按到UI
        if (Input.GetMouseButtonDown(0)&& !EventSystem.current.IsPointerOverGameObject())
        {
            CheckClickGround();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isClick = false;
        }

        //只要他鼠标按下了就要改变朝向
        if (isClick)
        {
            ChangeFace();
        }
        else 
        {
            //没有按鼠标的情况下 如果他在跑 要更新一下朝向
            if (playerMove.isMoving) 
            {
                LookAtTarget(targetPos);
            }
        }
    }

    void CheckClickGround()
    {
        //射线检测 从主相机到鼠标的位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        //收集碰撞信息
        bool isColider = Physics.Raycast(ray, out hitInfo);
        //如果成功返回并且点击的是地面
        if (isColider && hitInfo.collider.tag == Consts.Tags.Ground)
        {
            isClick = true;
            //我们就实例化点击出来的效果
            ShowClickEffect(hitInfo.point);
            //顺便改一下朝向
            LookAtTarget(hitInfo.point);
        }
    }

    /// <summary>
    /// 改变角色的朝向 对着鼠标点击的位置
    /// </summary>
    void ChangeFace()
    {
        //得到要移动的目标位置
        //让主角朝向目标位置

        //射线检测 从主相机到鼠标的位置
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        //收集碰撞信息
        bool isColider = Physics.Raycast(ray, out hitInfo);
        //如果成功返回并且点击的是地面
        if (isColider && hitInfo.collider.tag == Consts.Tags.Ground)
        {
            LookAtTarget(hitInfo.point);
        }
    }

    private void LookAtTarget(Vector3 hitPos)
    {
        //保存目标位置
        targetPos = hitPos;
        //只需要在垂直于y轴的方向控制移动就可
        targetPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
        //改变朝向
        this.transform.LookAt(targetPos);
    }

    /// <summary>
    /// 实例化出来点击的效果
    /// </summary>
    /// <param name="hitPoint"></param>
    private void ShowClickEffect(Vector3 hitPoint)
    {
        hitPoint = new Vector3(hitPoint.x, hitPoint.y + 0.1f, hitPoint.z);
        //GameObject.Instantiate(effect_click_prefab, hitPoint, Quaternion.identity);
        PoolMgr.GetInstance().GetObj("Effects/Click/Click_Green", (obj) => { obj.transform.position = hitPoint;});
    }
}

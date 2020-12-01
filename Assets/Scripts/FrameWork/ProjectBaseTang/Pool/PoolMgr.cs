using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoolData
{
    //对象挂载的父节点
    public GameObject fatherObj;
    public List<GameObject> poolList;

    public PoolData(GameObject obj,GameObject poolObj)
    {
        //创建一个父对象 子池子的节点
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.SetParent(poolObj.transform);
        poolList = new List<GameObject>() { };
        //然后压一下
        PushObj(obj);
    }
    /// <summary>
    /// 压物体到对象池里面
    /// </summary>
    /// <param name="obj"></param>
    public void PushObj(GameObject obj)
    {
        //外部失活
        obj.SetActive(false);
        //存起来
        poolList.Add(obj);
        //设置父对象
        obj.transform.SetParent(fatherObj.transform);   
    }
    public  GameObject GetObj()
    {
        GameObject obj = null;
        // 取出第一个
        obj = poolList[0];
        //移除第一个位置
        poolList.RemoveAt(0);
        //外部激活
        obj.SetActive(true);
        //断开物体和Pool的父子关系
        obj.transform.parent = null;
        return obj;
    }
}


/// <summary>
/// 缓存池模块
/// </summary>
public class PoolMgr :BaseManager<PoolMgr>
{
    //缓存池容器
    public Dictionary<string, PoolData> poolDic = new Dictionary<string, PoolData>();

    private GameObject poolObj;


    //往外拿
    public void GetObj(string name,UnityAction<GameObject> callBack)
    {
        //查看对应的是否有这个list以及里面有没有物体
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count>0)
        {
            GameObject obj = poolDic[name].GetObj();

            //如果有这个组件的话
            try
            {
                obj.GetComponent<IReusable>().OnSpawn();
            }
            catch (System.Exception)
            {
                Debug.Log("缓存的物体 没有继承IReusable这个接口");
            }
            //再掉传进来的委托
            callBack(obj);
        }
        else//没有的话就实例化一个拿出去
        {
            //异步加载
            ResMgr.GetInstance().LoadAsync<GameObject>(name, (o) =>
            {
                o.name = name;//把对象名改成和池子名字一样
                //如果有这个组件的话
                try
                {
                    o.GetComponent<IReusable>().OnSpawn();
                }
                catch (System.Exception)
                {
                    Debug.Log("缓存的物体 没有继承IReusable这个接口");
                }
                //再调用传进来的委托
                callBack(o);
            });
        }
    }

    /// <summary>
    /// 往外拿 同步版本
    /// </summary>
    /// <param name="name"></param>
    public GameObject GetObj(string name)
    {
        GameObject obj = null;
        //有属于这个物体的小池子 并且里面要有东西
        if (poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0)
        {
            //取出来
            obj = poolDic[name].GetObj();
            //调用方法
            obj.GetComponent<IReusable>().OnSpawn();
        }
        else//没有的话就只能创建出来
        {
            //创建游戏物体
            obj = ResMgr.GetInstance().Load<GameObject>(name);
            //修改名字
            obj.name = name;
            //调用方法
            obj.GetComponent<IReusable>().OnSpawn();
        }
        return obj;
    }

    //往里放
    public void PushObj(string name,GameObject obj)
    {
        if (poolObj == null)
        {
            poolObj = new GameObject("Pool");
        }

        //查看是否有这个名字的PoolData
        if (poolDic.ContainsKey(name))
        {
            poolDic[name].PushObj(obj);
        }
        else
        {
            poolDic.Add(name,new PoolData(obj,poolObj));//创建一个PoolData并且直接放进去
        }
        //如果有这个组件的话
        try
        {
            obj.GetComponent<IReusable>().OnSpawn();
        }
        catch (System.Exception)
        {
            Debug.Log("没有这个组件");
        }
    }
    //清空缓存池 主要用在场景切换
    public void Clear()
    {
        poolDic.Clear();
        poolObj = null;
    }
}

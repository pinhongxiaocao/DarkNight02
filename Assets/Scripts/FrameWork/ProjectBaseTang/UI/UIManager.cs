using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public enum E_UI_Layer
{
    Bot,
    Mid,
    Top,
    System
}


public class UIManager : BaseManager<UIManager>
{
    public Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform system;

    public RectTransform canvas;

    public UIManager()
    {
        //去加载Canvas  并且让它在切换场景的时候不被销毁
        GameObject obj= ResMgr.GetInstance().Load<GameObject>("UI/Canvas");
        canvas = obj.transform as RectTransform;
        GameObject.DontDestroyOnLoad(obj);

        //找到各层
        bot = canvas.Find("Bot");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");

        //创建EventSystem 并且让它在切换场景的时候不被销毁
        obj = ResMgr.GetInstance().Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);

    }

    public Transform GetLayerFather(E_UI_Layer layer)
    {
        switch (layer)
        {
            case E_UI_Layer.Bot:
                return this.bot;
            case E_UI_Layer.Mid:
                return this.mid;
            case E_UI_Layer.Top:
                return this.top;
            case E_UI_Layer.System:
                return this.system;
        }
        return null;
    }


    /// <summary>
    /// 显示面板
    /// </summary>
    /// <typeparam name="T">面板脚本类型</typeparam>
    /// <param name="panelName"></param>
    /// <param name="layer"></param>
    /// <param name="callBack">创建成功后处理的逻辑</param>
    public void ShowPanel<T>(string panelName,E_UI_Layer layer=E_UI_Layer.Mid,UnityAction<T> callBack=null)where T:BasePanel
    {
        ResMgr.GetInstance().LoadAsync<GameObject>("UI/" + panelName,(obj)=> 
        {
            if (panelDic.ContainsKey(panelName))// 如果已经创建了一次 就直接调用它后面的逻辑
            {
                panelDic[panelName].ShowMe();
                if (callBack != null)
                {
                    callBack(panelDic[panelName]as T);
                }
                //避免面板重复加载
                return;
            }

            //把它放在Canvas下面
            //设置它的相对位置

            //找到父对象 到底显示在哪一层
            Transform father = bot;
            switch (layer)
            {
                case E_UI_Layer.Mid:
                    father = mid;
                    break;
                case E_UI_Layer.Top:
                    father = top;
                    break;
                case E_UI_Layer.System:
                    father = system;
                    break;
                default:
                    break;
            }
            //设置父对象
            obj.transform.SetParent(father);
            //设置相对位置和大小
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;
            //得到预设体身上的面板脚本
            T panel = obj.GetComponent<T>();
            panel.ShowMe();
            //处理面板创建后的逻辑
            if (callBack != null)
            {
                callBack(panel);
            }
            //把面板存起来
            panelDic.Add(panelName, panel);
        });
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <param name="panelName"></param>
    public void HidePanel(string panelName)
    {
        if (panelDic.ContainsKey(panelName))
        {
            panelDic[panelName].HideMe();
            GameObject.Destroy(panelDic[panelName].gameObject);
            panelDic.Remove(panelName);
        }
        else 
        {
            Debug.Log($"没有{panelName}这个面板");
        }
    }

    /// <summary>
    /// 得到一个已经显示的面板
    /// </summary>
    public T GetPanel<T>(string panelName)where T:BasePanel
    {

        if (panelDic.ContainsKey(panelName))
        {
            return panelDic[panelName] as T;
        }
        return null;
    }

    /// <summary>
    /// 给控件添加自定义事件监听
    /// </summary>
    /// <param name="control">控件对象</param>
    /// <param name="type">事件类型</param>
    /// <param name="callBack">事件的响应函数</param>
    public static void AddCustomEventListener(UIBehaviour control, EventTriggerType type, UnityAction<BaseEventData> callBack)
    {
        EventTrigger trigger = control.GetComponent<EventTrigger>();
        if (trigger == null)
            trigger = control.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = type;
        entry.callback.AddListener(callBack);

        trigger.triggers.Add(entry);
    }
}

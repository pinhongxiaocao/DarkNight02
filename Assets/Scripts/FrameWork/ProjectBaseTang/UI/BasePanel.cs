using System.Collections;
using LB_MVC;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BasePanel:View
{
    //里氏转换原则 存基类来存所有控件 
    // 这样写List就是解决一个物体有多个控件的情况  比如btnStart 的list有button和image控件
    private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string,List<UIBehaviour>>();

    public override string Name
    {
        get { return Consts.MVCView_Name.BasePanel; }
    }

    protected virtual  void Awake()
    {
        FindChildrenControl<Button>();
        FindChildrenControl<Image>();
        FindChildrenControl<Text>();
        FindChildrenControl<Toggle>();
        FindChildrenControl<Slider>();
        FindChildrenControl<InputField>();
    }

    #region  提供给子类显隐的接口
    public virtual void ShowMe()
    {

    }

    public virtual void HideMe()
    {

    }
    #endregion

    #region  帮子类封装好事件处理
    protected virtual void OnClick(string btnName)
    {

    }

    protected virtual void OnValueChanged(string toggleName,bool value)
    {

    }
    #endregion

    /// <summary>
    /// 得到对应名字的对应控件脚本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="controlName"></param>
    /// <returns></returns>
    public T GetControl<T>(string controlName) where T : UIBehaviour
    {
        if (controlDic.ContainsKey(controlName))
        {
            for (int i = 0; i < controlDic[controlName].Count; i++)
            {
                if(controlDic[controlName][i] is T)
                {
                    return controlDic[controlName][i] as T;
                }
            }
        }
        return null;
    }
    /// <summary>
    /// 找到子对象的对应控件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private void FindChildrenControl<T>()where T:UIBehaviour
    {
        T[] controls = this.GetComponentsInChildren<T>();
        for (int i = 0; i < controls.Length; i++)
        {
            string objName = controls[i].gameObject.name;
            if (controlDic.ContainsKey(objName)) //有的话直接加到那个list当中去
            {
                controlDic[objName].Add(controls[i]);
            }
            else
            {
                controlDic.Add(objName, new List<UIBehaviour>() { controls[i]});//没有的话 我们就新建一个list
            }
            //如果是按钮控件
            if(controls[i]is Button)
            {
                (controls[i] as Button).onClick.AddListener(()=> 
                {
                    OnClick(objName);
                });
            }
            //如果是toggle控件
            else if (controls[i] is Toggle)
            {
                (controls[i] as Toggle).onValueChanged.AddListener((value) =>
                {
                    OnValueChanged(objName, value);
                });
            }

        }
    }

    /// <summary>
    /// 处理MVC的消息
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="data"></param>
    public override void HandleEvent(string eventName, LBMVC_Args data = null)
    {
        
    }
}

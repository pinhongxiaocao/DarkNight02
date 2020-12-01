using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarNpc : BaseNpc
{

    /// <summary>
    /// 当鼠标在碰撞体上面时
    /// </summary>
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            //打开任务面板
            UIManager.GetInstance().ShowPanel<QuestPanel>("QuestPanel",E_UI_Layer.Mid,(panel)=> 
            {
                //加载完后委派任务
                this.GetComponent<Questable>().DelegateQuest(panel);
            });
           
        }
    }
}

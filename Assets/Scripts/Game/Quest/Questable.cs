using System.Collections;
using System.Collections.Generic;
using Consts;
using UnityEngine;

/// <summary>
/// 这个脚本放在所有派发任务的NPC上 
/// 这里就懒得采用摄像机那种方式了 直接用Unity的生命周期函数
/// </summary>
public class Questable : MonoBehaviour
{
    public Quest quest=new Quest();

    private void Start()
    {
        EventCenter.GetInstance().AddEventListener(Consts.EventCenter_Events.AcceptQuest,AcceptQuest);
        EventCenter.GetInstance().AddEventListener(Consts.EventCenter_Events.CompleteQuest,CompleteQuest);
    }

    private void OnDestroy()
    {
        EventCenter.GetInstance().RemoveEventListener(Consts.EventCenter_Events.AcceptQuest, AcceptQuest);
        EventCenter.GetInstance().RemoveEventListener(Consts.EventCenter_Events.CompleteQuest, CompleteQuest);
    }

    /// <summary>
    /// 委派任务
    /// </summary>
    public void DelegateQuest(QuestPanel panel)
    {
        if (quest.questStatus == E_Quest_Status.Waiting)
        {
            panel.UpdateQuestPanel(E_Quest_Status.Waiting, quest.waitingQuestDes);
        }
        else if(quest.questStatus == E_Quest_Status.Accepted) 
        {
            panel.UpdateQuestPanel(E_Quest_Status.Accepted, quest.acceptQuestDes);
        }
        else if (quest.questStatus == E_Quest_Status.Completed) 
        {
            panel.UpdateQuestPanel(E_Quest_Status.Completed, quest.completeQuestDes);
        }
    }

    #region 事件监听
    void AcceptQuest() 
    {
        //修改quest的状态
        quest.questStatus = E_Quest_Status.Accepted;
        //获取面板 并且 更新面板
        QuestPanel panel=UIManager.GetInstance().GetPanel<QuestPanel>("QuestPanel");
        DelegateQuest(panel);
    }

    void CompleteQuest()
    {

    }
    #endregion
}

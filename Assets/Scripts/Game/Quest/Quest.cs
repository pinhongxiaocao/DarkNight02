using UnityEngine;
using Consts;

[System.Serializable]
public class Quest
{
    public string questName;
    public E_Quest_Type questType;
    public E_Quest_Status questStatus;

    /// <summary>
    /// 未接收状态的任务描述
    /// </summary>
    public string waitingQuestDes;
    /// <summary>
    /// 接收状态的任务描述
    /// </summary>
    public string acceptQuestDes;
    /// <summary>
    /// 完成状态的任务描述
    /// </summary>
    public string completeQuestDes;
}

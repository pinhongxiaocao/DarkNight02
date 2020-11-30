using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPanel : BasePanel
{
    /// <summary>
    /// MVC中View的名字
    /// </summary>
    public override string Name
    {
        get { return Consts.MVCView_Name.MainPanel; }
    }

    /// <summary>
    /// 来对按钮进行监听
    /// </summary>
    /// <param name="btnName"></param>
    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "btnSetting":
                break;
            case "btnBag":
                //TODO 打开背包
                UIManager.GetInstance().ShowPanel<BagPanel>("BagPanel");
                break;
            case "btnStatus":
                break;
            case "btnEquip":
                //打开装备
                UIManager.GetInstance().ShowPanel<EquipPanel>("EquipPanel");
                break;
            case "btnSkill":
                break;
        }
    }
}

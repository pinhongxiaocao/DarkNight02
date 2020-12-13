using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionNpc : BaseNpc
{
    /// <summary>
    /// 当鼠标移动到上面的时候
    /// </summary>
    private void OnMouseOver()
    {
        //如果按下鼠标左键
        if (Input.GetMouseButtonDown(0)) 
        {
            //我们就打开商店面板(设置内容 只能卖药)
            UIManager.GetInstance().ShowPanel<ShopPanel>("ShopPanel");
        }
    }
}

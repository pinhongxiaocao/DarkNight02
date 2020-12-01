using System.Collections;
using System.Collections.Generic;
using LB_MVC;
using UnityEngine;
public class CharacterCreator:MonoBehaviour,IOtherObjectMono
{
    private const string Magician_Idle = "Character/Idle/Magician_Idle";
    private const string SwordMan_Idle = "Character/Idle/SwordMan_Idle";



    //游戏人物预制体们
    private List<GameObject> characterList=new List<GameObject>();
    private int selectIndex = 0;//当前选择的角色的索引

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        CreateCharacter();
        //监听切换角色的事件
        EventCenter.GetInstance().AddEventListener<int>(Consts.EventCenter_Events.SwitchCharacter, SwitchCharacter);
    }

    /// <summary>
    /// 消除
    /// </summary>
    public void Remove()
    {
        //移除切换角色的事件
        EventCenter.GetInstance().RemoveEventListener<int>(Consts.EventCenter_Events.SwitchCharacter, SwitchCharacter);
        //销毁自己
        Destroy(this.gameObject);
    }

    private void SwitchCharacter(int plus) 
    {
        selectIndex+=plus;
        //防止超出
        if(selectIndex>=characterList.Count) 
        {
            selectIndex = 0;
        }
        //防止减少
        if (selectIndex < 0) 
        {
            selectIndex = characterList.Count-1;
        }
        //更新角色显隐
        UpdateCharacter(selectIndex);
    }

    /// <summary>
    /// 资源加载出人物来
    /// </summary>
    private void CreateCharacter()
    { 
        //加载出两个角色来
        characterList.Add(ResMgr.GetInstance().Load<GameObject>(Magician_Idle));
        characterList.Add(ResMgr.GetInstance().Load<GameObject>(SwordMan_Idle));


        //设置角色的位置
        for (int i = 0; i < characterList.Count; ++i)
        {
            //设置父物体
            characterList[i].transform.SetParent(this.transform);
            //设置下相对位置
            characterList[i].transform.localPosition = Vector3.zero;
        }

        //更新角色显隐
        UpdateCharacter(selectIndex);
    }

    /// <summary>
    /// 切换角色显隐
    /// </summary>
    private void UpdateCharacter(int index) 
    {
        
        for (int i = 0; i < characterList.Count; ++i)
        {
            //其他的全部关掉
            characterList[i].gameObject.SetActive(false);
            //只设置当前的索引为显示
            if (i == index) 
            {
                characterList[i].gameObject.SetActive(true);
            }
        }
    }
}
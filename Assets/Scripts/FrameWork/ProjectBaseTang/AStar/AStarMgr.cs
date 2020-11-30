using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A星寻路管理器
/// </summary>
public class AStarMgr : BaseManager<AStarMgr>
{
    //地图的宽 高
    private int mapW;
    private int mapH;

    //地图相关所有格子对象容器
    public AStarNode[,] nodes;

    //开启列表
    private List<AStarNode> openList=new List<AStarNode>();
    //关闭列表
    private List<AStarNode> closeList=new List<AStarNode>();

    /// <summary>
    /// 初始化格子信息
    /// </summary>
    /// <param name="w"></param>
    /// <param name="h"></param>
    public void InitMapInfo(int w,int h)
    {
        //存储宽 高
        this.mapW = w;
        this.mapH = h;

        //声明大小 开辟内存
        nodes = new AStarNode[w, h];

        //根据宽高 创造格子 随机生成障碍
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                //随机阻挡 直接为了掩饰 
                //以后这些阻挡信息 用地图配置文件中读取出来
                AStarNode node = new AStarNode(i, j, Random.Range(0, 100) < 20 ? E_Node_Type.Stop : E_Node_Type.Walk);
                //装入内存
                nodes[i, j] = node;
            }
        }
    }

    /// <summary>
    /// 寻路方法 提供给外部使用 
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <returns></returns>
    public List<AStarNode> FindPath(Vector2 startPos,Vector2 endPos)
    {
        //实际项目中 传入的点往往是 坐标系中的位置
        //我们这里省略换算的过程 直接认为传入的是格子坐标

        //首先判断传入的两个点是否合法(1 在地图范围内 2是不是阻挡)
        //1 先判断在不在地图内
        if (startPos.x < 0 || startPos.x >= mapW ||
            startPos.y < 0 || startPos.y >= mapH ||
            endPos.x < 0 || endPos.x >= mapW ||
            endPos.y < 0 || endPos.y >= mapH)
        {
            Debug.LogError("开始或者结束点在地图格子范围之外!");
            //如果不合法直接返回
            return null;
        }
        //2 再判断是否为阻挡
        //应该得到起点和终点对应的格子
        AStarNode start = nodes[(int)startPos.x, (int)startPos.y];
        AStarNode end = nodes[(int)endPos.x, (int)endPos.y];
        if(start.type==E_Node_Type.Stop||
           end.type == E_Node_Type.Stop)
        {
            Debug.LogError("开始或者结束点是阻挡!");
            //如果不合法直接返回
            return null;
        }

        //清空上一次相关的数据 避免他们影响这一次的寻路计算

        //清空开启和关闭列表
        openList.Clear();
        openList.Clear();

        //把开始点放入关闭列表里面
        start.father = null;
        start.f = 0;
        start.g = 0;
        start.h = 0;
        closeList.Add(start);

        while (true)
        {
            //左上  x-1 y-1
            FindNearlyNodeToOpenList(start.x - 1, start.y - 1, 1.4f, start, end);
            //上   x   y-1
            FindNearlyNodeToOpenList(start.x, start.y - 1, 1f, start, end);
            //右上 x+1 y-1
            FindNearlyNodeToOpenList(start.x + 1, start.y - 1, 1.4f, start, end);
            //左   x-1 y
            FindNearlyNodeToOpenList(start.x - 1, start.y, 1f, start, end);
            //右   x+1 y
            FindNearlyNodeToOpenList(start.x + 1, start.y, 1f, start, end);
            //左下 x-1 y+1
            FindNearlyNodeToOpenList(start.x - 1, start.y + 1, 1.4f, start, end);
            //下   x   y+1
            FindNearlyNodeToOpenList(start.x, start.y + 1, 1f, start, end);
            //右下 x+1 y+1
            FindNearlyNodeToOpenList(start.x + 1, start.y + 1, 1.4f, start, end);

            //死路判断 开启列表里面为空 都还没找到终点
            if (openList.Count == 0)
            {
                Debug.Log("死路!");
                return null;
            }


            //选出开启列表中 寻路消耗最小的点
            openList.Sort(SortOpenList);
            //放入关闭列表中 
            closeList.Add(openList[0]);
            //找到的这个点又变成了新的起点 进行下一次计算
            start = openList[0];
            //再从开启列表中移除
            openList.RemoveAt(0);
            //如果这个点已经是终点了 就直接得到结果返回
            if (start == end)
            {
                //找完了
                List<AStarNode> path = new List<AStarNode>();
                path.Add(end);
                while (end.father!=null)
                {
                    path.Add(end.father);
                    end = end.father;
                }
                //列表翻转的API
                path.Reverse();

                return path;
            }
            //如果不是 那以这个点为起点 继续寻路
        }
    }

    /// <summary>
    /// 把邻近的点放入开启列表
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="g"></param>
    /// <param name="father"></param>
    private void FindNearlyNodeToOpenList(int x,int y,float g,AStarNode father,AStarNode end)
    {
        //边界判断
        if(x<0||x>=mapW||
           y <0|| y>= mapH)
        {
            return;
        }
        //在范围内再去取点
        AStarNode node = nodes[x, y];
        //判断是否为空以及类型是否为阻拦以及是否在这两个列表
        if(node==null||
           node.type == E_Node_Type.Stop||
           closeList.Contains(node)||
           openList.Contains(node))
        {
            return;
        }
        //记录父对象
        node.father = father;
        //计算f值
        //f=g+h

        //计算g 我离起点的距离=父亲离起点的距离+我离我父亲的距离
        node.g = father.g + g;
        node.h = Mathf.Abs(end.x - node.x) + Mathf.Abs(end.y - node.y);
        node.f = node.g + node.h;
        //通过了上面的合法验证 就把他放在开启列表中
        openList.Add(node);
    }

    /// <summary>
    /// 开启列表排序
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    private int SortOpenList(AStarNode a, AStarNode b)
    {
        if (a.f > b.f)
            return 1;
        else if (a.f == b.f)
            return 1;
        else
            return -1;
    }
}

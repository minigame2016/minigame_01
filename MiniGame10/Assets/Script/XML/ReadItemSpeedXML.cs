using UnityEngine;
using System.Collections;

public class ItemSpeed//读取后除以10保存
{
    public int id = 1000;
    public double speed_V = 0.0;//上下
    public double speed_H = 0.0;//左右
}

public class ReadItemSpeedXML : MonoBehaviour
{

    public TextAsset _xmldata;

    public ArrayList _speedListR = new ArrayList();

    void Start()
    {
        ReadSpeed();
        XMLManager.Instance._speedList = _speedListR;

        //给GameSystem 每个item赋值
        SetGameItemSpeed();
    }

    // 读取规则XML
    private void ReadSpeed()
    {
        XMLParser xmlparse = new XMLParser();
        XMLNode node = xmlparse.Parse(_xmldata.text);

        XMLNodeList list = node.GetNodeList("ROOT>0>table");
        for (int i = 0; i < list.Count; i++)
        {
            string id = node.GetValue("ROOT>0>table>" + i + ">@id");
            string speedv = node.GetValue("ROOT>0>table>" + i + ">@speedv");
            string speedh = node.GetValue("ROOT>0>table>" + i + ">@speedh");

            ItemSpeed itemSpeed = new ItemSpeed();
            itemSpeed.id = int.Parse(id);
            itemSpeed.speed_V = (double)(double.Parse(speedv) / 10);
            itemSpeed.speed_H = (double)(double.Parse(speedh) / 10);

            _speedListR.Add(itemSpeed);
        }
    }

    public void SetGameItemSpeed()
    {
        ArrayList speedList = XMLManager.Instance._speedList;

        ItemSpeed itemSpeed_1001 = (ItemSpeed)speedList[0];
        GameSystem.Instance.Item_1001_V_Speed = itemSpeed_1001.speed_V;
        GameSystem.Instance.Item_1001_H_Speed = itemSpeed_1001.speed_H;

        ItemSpeed itemSpeed_1002 = (ItemSpeed)speedList[1];
        GameSystem.Instance.Item_1002_V_Speed = itemSpeed_1002.speed_V;
        GameSystem.Instance.Item_1002_H_Speed = itemSpeed_1002.speed_H;

        ItemSpeed itemSpeed_1003 = (ItemSpeed)speedList[2];
        GameSystem.Instance.Item_1003_V_Speed = itemSpeed_1003.speed_V;
        GameSystem.Instance.Item_1003_H_Speed = itemSpeed_1003.speed_H;

        ItemSpeed itemSpeed_1004 = (ItemSpeed)speedList[3];
        GameSystem.Instance.Item_1004_V_Speed = itemSpeed_1004.speed_V;
        GameSystem.Instance.Item_1004_H_Speed = itemSpeed_1004.speed_H;

        ItemSpeed itemSpeed_1005 = (ItemSpeed)speedList[4];
        GameSystem.Instance.Item_1005_V_Speed = itemSpeed_1005.speed_V;
        GameSystem.Instance.Item_1005_H_Speed = itemSpeed_1005.speed_H;

        ItemSpeed itemSpeed_1006 = (ItemSpeed)speedList[5];
        GameSystem.Instance.Item_1006_V_Speed = itemSpeed_1006.speed_V;
        GameSystem.Instance.Item_1006_H_Speed = itemSpeed_1006.speed_H;

        ItemSpeed itemSpeed_1007 = (ItemSpeed)speedList[6];
        GameSystem.Instance.Item_1007_V_Speed = itemSpeed_1007.speed_V;
        GameSystem.Instance.Item_1007_H_Speed = itemSpeed_1007.speed_H;

        ItemSpeed itemSpeed_1008 = (ItemSpeed)speedList[7];
        GameSystem.Instance.Item_1008_V_Speed = itemSpeed_1008.speed_V;
        GameSystem.Instance.Item_1008_H_Speed = itemSpeed_1008.speed_H;

        ItemSpeed itemSpeed_1009 = (ItemSpeed)speedList[8];
        GameSystem.Instance.Item_1009_V_Speed = itemSpeed_1009.speed_V;
        GameSystem.Instance.Item_1009_H_Speed = itemSpeed_1009.speed_H;

        ItemSpeed itemSpeed_1010 = (ItemSpeed)speedList[9];
        GameSystem.Instance.Item_1010_V_Speed = itemSpeed_1010.speed_V;
        GameSystem.Instance.Item_1010_H_Speed = itemSpeed_1010.speed_H;

        ItemSpeed itemSpeed_1011 = (ItemSpeed)speedList[10];
        GameSystem.Instance.Item_1011_V_Speed = itemSpeed_1011.speed_V;
        GameSystem.Instance.Item_1011_H_Speed = itemSpeed_1011.speed_H;

        ItemSpeed itemSpeed_1012 = (ItemSpeed)speedList[11];
        GameSystem.Instance.Item_1012_V_Speed = itemSpeed_1012.speed_V;
        GameSystem.Instance.Item_1012_H_Speed = itemSpeed_1012.speed_H;

        ItemSpeed itemSpeed_1013 = (ItemSpeed)speedList[12];
        GameSystem.Instance.Item_1013_V_Speed = itemSpeed_1013.speed_V;
        GameSystem.Instance.Item_1013_H_Speed = itemSpeed_1013.speed_H;

        ItemSpeed itemSpeed_1014 = (ItemSpeed)speedList[13];
        GameSystem.Instance.Item_1014_V_Speed = itemSpeed_1014.speed_V;
        GameSystem.Instance.Item_1014_H_Speed = itemSpeed_1014.speed_H;

        ItemSpeed itemSpeed_1015 = (ItemSpeed)speedList[14];
        GameSystem.Instance.Item_1015_V_Speed = itemSpeed_1015.speed_V;
        GameSystem.Instance.Item_1015_H_Speed = itemSpeed_1015.speed_H;
    }
}

using UnityEngine;
using System.Collections;

public class UIGameItemCreate : MonoBehaviour
{

    // panel的预设
    public GameObject _item_1001;
    public GameObject _item_1002;
    public GameObject _item_1003;
    public GameObject _item_1004;
    public GameObject _item_1005;
    public GameObject _item_1006;
    public GameObject _item_1007;
    public GameObject _item_1008;
    public GameObject _item_1009;
    public GameObject _item_1010;
    public GameObject _item_1011;
    public GameObject _item_1012;
    public GameObject _item_1013;
    public GameObject _item_1014;
    public GameObject _item_1015;
    // 添加到谁下
    public GameObject _panelRoot;

    private float _curTime = 0.0f;
    private float _ramCurTime = 0.0f;
    private float createIntervalTime = 2.0f;

    private int randomCounter = 1;
    private int randomXLbs = 1;

    // Use this for initialization
    void Start()
    {
        _curTime = Time.time;
        _ramCurTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameSystem.Instance.isPauseState)
        {
            if (Time.time - _ramCurTime > 3f)
            {
                createIntervalTime = Random.Range(1, 3);
                _ramCurTime = Time.time;
            }

            if (Time.time - _curTime >= createIntervalTime)
            {
                //随机生成
                if (randomCounter > GameSystem.Instance.ramCount)//三个以内不能相同
                {
                    randomCounter = 1;
                }
                CreateItem(randomCounter);
                randomCounter++;
                _curTime = Time.time;
            }
        }

    }

    public void CreateItem(int randomCounter)
    {
        //随机出创建item的X坐标
        if (randomXLbs > 2)//三个以内不能相同
        {
            randomXLbs = 1;
        }
        float itemXCoordinate = ramItemXCoordinate(randomXLbs);
        randomXLbs++; 

        //随机出创建哪个item
        int createWhichItem = 0;
        if (randomCounter == 1)//1-5随机
        {
            createWhichItem = Random.Range(1, 6);
        }
        if (randomCounter == 2)
        {
            createWhichItem = Random.Range(6, 11);
        }
        if (randomCounter == 3)
        {
            createWhichItem = Random.Range(11, 16);
        }
        //createWhichItem = 1;//先做8个,预计15个

        if (createWhichItem == 1)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1001);
            panel.transform.localPosition = new Vector3(-itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 2)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1002);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 3)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1003);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 4)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1004);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 5)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1005);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 6)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1006);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 7)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1007);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 8)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1008);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 9)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1009);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 10)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1010);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 11)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1011);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 12)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1012);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 13)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1013);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 14)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1014);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }
        if (createWhichItem == 15)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1015);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
        }

    }

    private float ramItemXCoordinate(int randomXLbs)
    {
        float itemXCoordinate = 0;
        int var = 0;
        if (randomXLbs == 1)
        {
            var = Random.Range(1, 5);
        }
        else if (randomXLbs == 2)
        {
            var = Random.Range(5, 8);
        }
        
        switch (var)
        {
            case 1:
                itemXCoordinate = -260f;
                break;
            case 2:
                itemXCoordinate = -180f;
                break;
            case 3:
                itemXCoordinate = -100f;
                break;
            case 4:
                itemXCoordinate = -20f;
                break;
            case 5:
                itemXCoordinate = 60f;
                break;
            case 6:
                itemXCoordinate = 140f;
                break;
            case 7:
                itemXCoordinate = 230f;
                break;
            default:
                itemXCoordinate = 220f;
                break;
        }
        return itemXCoordinate;
    }
}

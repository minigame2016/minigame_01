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

	// Use this for initialization
	void Start () {
        _curTime = Time.time;
        _ramCurTime = Time.time;
	}
    
	// Update is called once per frame
	void Update () {
        if(!GameSystem.Instance.isPauseState)
        {
            if (Time.time - _ramCurTime > 3f)
            {
                createIntervalTime = Random.Range(2, 4);
                _ramCurTime = Time.time;
            }
            if (Time.time - _curTime >= createIntervalTime)
            {   
                //随机生成
                CreateItem();
                _curTime = Time.time;
            }
        }
        
	}

    public void CreateItem()
    {   
        //随机出创建item的X坐标
        float itemXCoordinate = Random.Range(-250, 250);//-160~160

        //随机出创建哪个item
        int createWhichItem = Random.Range(1, 15);//先做8个,预计15个
        //int createWhichItem = 1;//先做8个,预计15个

        if (createWhichItem == 1)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1001);
            panel.transform.localPosition = new Vector3(-itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 2)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1002);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 3)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1003);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 4)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1004);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 5)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1005);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 6)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1006);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 7)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1007);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 8)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1008);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 9)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1009);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 10)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1010);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 11)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1011);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 12)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1012);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 13)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1013);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 14)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1014);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        if (createWhichItem == 15)
        {
            GameObject panel = NGUITools.AddChild(_panelRoot, _item_1015);
            panel.transform.localPosition = new Vector3(itemXCoordinate, 460, 0);
        }
        
    }
}

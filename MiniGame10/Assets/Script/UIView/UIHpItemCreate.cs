using UnityEngine;
using System.Collections;

public class UIHpItemCreate : MonoBehaviour
{

    // panel的预设
    public GameObject _hp;
    
    // 添加到谁下
    public GameObject _panelRoot;

    private float _curTime = 0.0f;

    void Start()
    {
        _curTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameSystem.Instance.Hp < 3)//血量小于3才创建
        {
            if (!GameSystem.Instance.isPauseState)
            {
                if (Time.time - _curTime > TableNum.HpCreatTime)
                {
                    CreateItem();
                    _curTime = Time.time;
                }
            }
            
        }
    }

    public void CreateItem()
    {
        float itemXCoordinate = Random.Range(-250, 250);
        GameObject panel = NGUITools.AddChild(_panelRoot, _hp);
        panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
    }

    
}

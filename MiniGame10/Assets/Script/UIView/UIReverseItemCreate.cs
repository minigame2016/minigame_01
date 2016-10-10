using UnityEngine;
using System.Collections;

public class UIReverseItemCreate : MonoBehaviour
{

    // panel的预设
    public GameObject _reverse;
    
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
        if (!GameSystem.Instance.isPauseState)
        {
            if (Time.time - _curTime > TableNum.ReverseCreateTime)
            {
                CreateItem();
                _curTime = Time.time;
            }
            if (GameSystem.Instance.isClickReverseItem)
            {
                if (Time.time - GameSystem.Instance.reverseCreateTime < GameSystem.Instance.reverseEndTime)
                {
                    Time.timeScale = TableNum.ReverseAddSpeed;
                }
                else
                {
                    Time.timeScale = GameSystem.Instance.reverseCreTimeScale;//回复生效前的TImeScale
                    GameSystem.Instance.isClickReverseItem = false;
                    GameSystem.Instance.reverseCreateTime = 0.0f;
                    GameSystem.Instance.reverseEndTime = 0.0f;
                    GameSystem.Instance.reverseCreTimeScale = 1f;
                }
            }
        }
    }

    public void CreateItem()
    {
        float itemXCoordinate = Random.Range(-250, 250);
        GameObject panel = NGUITools.AddChild(_panelRoot, _reverse);
        panel.transform.localPosition = new Vector3(itemXCoordinate, 450, 0);
    }

    
}

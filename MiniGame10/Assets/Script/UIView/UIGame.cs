using UnityEngine;
using System.Collections;

public class UIGame : MonoBehaviour {

    public GameObject _PausePanel;

    //技能
    public GameObject _skillFour;
    public GameObject _skillThree;
    public GameObject _skillTwo;
    public GameObject _skillOne;
    
    public UILabel _userName;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    private float timeCount = 0;
	void Update () {
        //测试计时
        if(Time.time - timeCount >= 2)
        {
            if (_skillOne.active == false)
            {
                _skillOne.SetActive(true);
            }
            else if (_skillTwo.active == false)
            {
                _skillTwo.SetActive(true);
            }
            else if (_skillThree.active == false)
            {
                _skillThree.SetActive(true);
            }
            else if (_skillFour.active == false)
            {
                _skillFour.SetActive(true);
            }
            timeCount = Time.time;
        }
	}

    void FixedUpdate()
    {
        
    }

    void OnEnable()
    {   
        //测试带数据
        _userName.text = LoginSystem.Instance._userName;
    }

    public void OnClickUseSkillBtn()
    {
        Debug.Log("UIGame OnClickUseSkillBtn");

        //控制技能点显示
        if (_skillFour.active == true)
        {
            _skillFour.SetActive(false);
        }
        else if (_skillThree.active == true)
        {
            _skillThree.SetActive(false);
        }
        else if (_skillTwo.active == true)
        {
            _skillTwo.SetActive(false);
        }
        else if (_skillOne.active == true)
        {
            _skillOne.SetActive(false);
        }
    }

    public void OnClickPauseBtn()
    {
        Debug.Log("UIGame OnClickPauseBtn ");
        _PausePanel.SetActive(true);

        //暂停游戏等操作  TODO
        Time.timeScale = 0;
    }

    public void OnClickReturnGameBtn()
    {
        _PausePanel.SetActive(false);

        //继续游戏等操作  TODO
        Time.timeScale = 1;
    }

    public void OnClickReStartBtn()
    {
        //重新开始 TODO
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void OnClickReturnMainBtn()
    {
        //返回主界面 TODO
        Application.LoadLevel("MainScene");
    }
}

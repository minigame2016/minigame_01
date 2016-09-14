using UnityEngine;
using System.Collections;

public class UIMainPage : MonoBehaviour, IEventListener{

    public GameObject _rulePanel;
    public GameObject _rankPanel;

    public UILabel _ruleLabel;

    public UILabel[] rank;

    public UILabel _userName;

    void OnEnable()
    {
        AttachEvent();
        //测试带数据
        _userName.text = LoginSystem.Instance._userName;
    }
    void OnDisable()
    {
        DetachEvent();
    }

    void Update()
    {
        NetWorkListerer();
    }

    public void OnClickGameStartBtn()
    {
        Debug.Log("UIMainPage OnClickGameStartBtn");
        MainSystem.Instance.GameStart();
    }

    public void OnClickRuleBtn()
    {
        Debug.Log("UIMainPage OnClickRuleBtn");
        _rulePanel.SetActive(true);

        SetRuleLabel();
    }

    public void OnClickCloseRuleBtn()
    {
        _rulePanel.SetActive(false);
    }

    public void OnClickRankBtn()
    {
        Debug.Log("UIMainPage OnClickRankBtn");
        _rankPanel.SetActive(true);

        //请求排行榜
        RankSystem.Instance.SendGetRankListMsg();
    }

    public void OnClickCloseRankBtn()
    {
        _rankPanel.SetActive(false);
    }

    private void SetRankItem()
    {
        int [] rankList = RankSystem.Instance.RankList;
        for (int i = 0; i < rankList.Length; i++ )
        {
            rank[i].text = rankList[i].ToString();
        }
    }

    private void SetRuleLabel()
    {
        ArrayList ruleList = XMLManager.Instance._ruleList;
        RuleData ruleData = (RuleData)ruleList[1];
        _ruleLabel.text = ruleData.rule;
    }

    private void NetWorkListerer()
    {
        if (NetWork.Instance._isRankListFinish)
        {
            NetWork.Instance.SaveRankListInfoSC();
            NetWork.Instance._isRankListFinish = false;
        }
    }

    public bool OnFireEvent(uint key, object param1, object param2)
    {
        if (key == MiniGameEvent.GET_RANK)
        {
            //给Label赋值
            SetRankItem();
        }

        return true;
    }

    public int GetListenerPriority(uint eventKey)
    {
        return 0;
    }

    public void AttachEvent()
    {
        GameEventSystem.rootEventDispatcher.AttachListenerNow(this, MiniGameEvent.GET_RANK);
    }

    public void DetachEvent()
    {
        GameEventSystem.rootEventDispatcher.DetachListenerNow(this, MiniGameEvent.GET_RANK);
    }
}

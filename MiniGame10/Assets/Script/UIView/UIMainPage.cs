using UnityEngine;
using System.Collections;

public class UIMainPage : MonoBehaviour {

    public GameObject _rulePanel;
    public GameObject _rankPanel;

    public UILabel _ruleLabel;

    public UILabel[] rank;

    public UILabel _userName;

    void Update()
    {
        if (NetWork.Instance._isRankListFinish)
        {
            NetWork.Instance.SaveRankListInfo();
            NetWork.Instance._isRankListFinish = false;
        }
    }

    void OnEnable()
    {   
        //测试带数据
        _userName.text = LoginSystem.Instance._userName;
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

        //请求排行榜 TODO
        RankSystem.Instance.GetRankList();
        //给Label赋值
        SetRankItem();
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
}

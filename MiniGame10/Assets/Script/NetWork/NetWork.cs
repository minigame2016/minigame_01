using UnityEngine;
using System.Collections;
using AVOSCloud;
using System.Threading.Tasks;
using System.Collections.Generic;

public class NetWork {

    private static NetWork instance=null;

    #region 单例模式抽象出来（优化）
    private NetWork()
    {

    }

    public static NetWork Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new NetWork();
            }
            return instance;
        }
    }
    #endregion

    public bool _isLoginSuccessCall = false;
    public bool _isLoginFailedCall = false;

    public bool _isRegisterSuccessCall = false;
    public bool _isRegisterFailedCall = false;

    public bool _isRankListFinish = false;

    public List<int> rankListTemp = new List<int>();

    #region 登录
    public void SendLoginMsgCS(string userName, string passWord)
    {   
        if(MainSystem.Instance.isOpenNetWork)
        {
            Debug.Log("NetWork Send " + userName + " " + passWord);
            AVUser.LogInAsync(userName, passWord).ContinueWith(t =>
            {
                if (t.IsFaulted || t.IsCanceled)
                {
                    Debug.Log("NetWork Send failed");
                    _isLoginFailedCall = true;
                }
                else
                {
                    Debug.Log("NetWork Send 1111登陆成功");
                    _isLoginSuccessCall = true;
                }
            }); 
        }
        else
        {
            _isLoginSuccessCall = true;
        }
        
    }

    public void LoginResultSC()
    {
        LoginSystem.Instance.LoginResult();
    }
    #endregion

    #region 注册
    public void SendRegsiterMsgCS(string userName, string passWord)
    {
        if (MainSystem.Instance.isOpenNetWork)
        {
            if (userName != null && passWord != null)
            {
                var user = new AVUser();
                user.Username = userName;
                user.Password = passWord;

                user.SignUpAsync().ContinueWith(t =>
                {
                    if (!t.IsFaulted)
                    {
                        Debug.Log("NetWork SendRegsiterMsgCS 注册成功");
                        var uid = user.ObjectId;
                        _isRegisterSuccessCall = true;
                    }
                    else
                    {
                        Debug.Log("NetWork SendRegsiterMsgCS 注册失败");
                        _isRegisterFailedCall = true;
                    }

                });
            }
            else
            {
                _isRegisterFailedCall = true;
            }
        }
        else
        {
            _isRegisterSuccessCall = true;
        }
        
    }
    #endregion

    #region 拉排行榜数据
    public void SendGetRankListMsgCS()
    {
        if (MainSystem.Instance.isOpenNetWork)
        {
            string userName = LoginSystem.Instance._userName;
            Debug.Log("NetWork SendGetRankListMsgCS " + userName);

            AVObject gameScore = new AVObject("GameScore");
            AVQuery<AVObject> query = AVObject.GetQuery("GameScore");
            query = query.WhereContains("playerName", userName).OrderByDescending("score");
            query.FindAsync().ContinueWith(t =>
            {
                Task task = Task.FromResult(0);
                int cnt = 0;
                List<int> list_score = new List<int>();

                foreach (AVObject result in t.Result)
                {
                    gameScore = result;
                    if (cnt < 5)
                    {
                        cnt = cnt + 1;
                        list_score.Add(gameScore.Get<int>("score"));
                    }
                }
                Debug.Log("NetWork SendResultMsg get rank.");

                rankListTemp = list_score;
                _isRankListFinish = true;
            });
        }
        else
        {
            _isRankListFinish = true;
            rankListTemp.Clear();
            rankListTemp.Add(111); rankListTemp.Add(222);
        }
        
    }

    public void SaveRankListInfoSC()
    {
        RankSystem.Instance.RankList = rankListTemp.ToArray();
        RankSystem.Instance.GetRankListResut();
    }
    #endregion

    #region 结算上报
    public void SendResultMsg(string username, string totalGrade)
    {
        if (MainSystem.Instance.isOpenNetWork)
        {
            Debug.Log("NetWork SendResultMsg save grade.");
            AVObject gameScore = new AVObject("GameScore");

            string playerName = username;
            int playerScore = int.Parse(totalGrade);

            gameScore["playerName"] = playerName;
            gameScore["score"] = playerScore;
            gameScore.SaveAsync();
        }
    }
    #endregion
}

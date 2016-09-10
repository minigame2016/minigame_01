using UnityEngine;
using System.Collections;
using AVOSCloud;
using System.Threading.Tasks;
using System.Collections.Generic;

public class NetWork {
    //TODO NetWork

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

    public bool _isLoginCall = false;
    public bool _isLoginFailedCall = false;

    public bool _isRegisterSuccessCall = false;
    public bool _isRegisterFailedCall = false;

    public bool _isRankListFinish = false;

    public List<int> rankListTemp = new List<int>();

    public void SendLoginMsg(string[] sendMsg)
    {
        Debug.Log("NetWork Send " + sendMsg[0] + " " + sendMsg[1]);
        AVUser.LogInAsync(sendMsg[0], sendMsg[1]).ContinueWith(t =>
        {
            if (t.IsFaulted || t.IsCanceled)
            {
                Debug.Log("NetWork Send failed");
                _isLoginFailedCall = true;
            }
            else
            {
                Debug.Log("NetWork Send 1111登陆成功");
                _isLoginCall = true;
            }
        }); 
    }

    public void RegisterUserCS(string username_rig, string pwd_rig)
    {
        //注册代码
        var user = new AVUser();
        user.Username = username_rig;
        user.Password = pwd_rig;

        user.SignUpAsync().ContinueWith(t =>
        {
            if (!t.IsFaulted)
            {
                Debug.Log("NetWork RegisterUserCS 注册成功");
                var uid = user.ObjectId;
                _isRegisterSuccessCall = true;
            }
            else
            {
                Debug.Log("NetWork RegisterUserCS 注册失败");
                _isRegisterFailedCall = true;
            }

        });
    }

    public void LoginResultSC()
    {
        LoginSystem.Instance.LoginResultSC();
    }

    public void GetRankListCS()
    {
        //填写拉取排行榜数据Code
        //TODO
        AVObject gameScore = new AVObject("GameScore");
        AVQuery<AVObject> query = AVObject.GetQuery("GameScore");
        query = query.WhereContains("playerName",LoginSystem.Instance._inputUserName).OrderByDescending("score");
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

        //返回数据存储到 RankSystem 中的RankList中，没有的数据需要传回0
    }

    public void SaveRankListInfo()
    {
        RankSystem.Instance.RankList = rankListTemp.ToArray();
    }

    public void SendResultMsg(string username, string totalGrade)
    {
        Debug.Log("NetWork SendResultMsg save grade.");
        AVObject gameScore = new AVObject("GameScore");

        string newplayername = username;
        int newscore = int.Parse(totalGrade);
        
        gameScore["score"] = newscore;
        gameScore["playerName"] = newplayername;
        gameScore.SaveAsync();
                    
    }
}

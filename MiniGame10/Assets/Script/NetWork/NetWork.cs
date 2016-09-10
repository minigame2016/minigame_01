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

    public void SendLoginMsg(string[] sendMsg)
    {
        Debug.Log("NetWork Send " + sendMsg[0] + " " + sendMsg[1]);
        AVUser.LogInAsync(sendMsg[0], sendMsg[1]).ContinueWith(t =>
        {
            if (t.IsFaulted || t.IsCanceled)
            {
                //System.AggregateException msg2 = t.Exception; // 登录失败，可以查看错误信息。
                //msg2 = "登录失败";
                //Debug.Log("NetWork Send " + msg2.Message);
                Debug.Log("NetWork Send failed");
            }
            else
            {
                //msg2 = "登陆成功";//登录成功
                Debug.Log("NetWork Send 1111登陆成功");
                _isLoginCall = true;
                //this.LoginResultSC();
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


        //返回数据存储到 RankSystem 中的RankList中，没有的数据需要传回0
    }

    public void SendResultMsg(string username, string totalGrade)
    {
        Debug.Log("NetWork SendResultMsg save grade");
        AVObject gameScore = new AVObject("GameScore");

        string newplayername = username;
        int newscore = int.Parse(totalGrade);
        

        gameScore["score"] = newscore;
        gameScore["playerName"] = newplayername;

       
        AVQuery<AVObject> query = new AVQuery<AVObject>("GameScore").WhereEqualTo("playerName", newplayername);

        query.CountAsync().ContinueWith(t =>
        {
            int count = t.Result;
            if (count >= 1)
            {
                Debug.Log("已经有了");
                query.FindAsync().ContinueWith(t1 =>
                {
                    Task task = Task.FromResult(0);
                    int cnt = 0;
                    foreach (AVObject result in t1.Result)
                    {
                        cnt = cnt + 1;
                        gameScore = result;
                        int val = gameScore.Get<int>("score");
                        if (val < newscore)
                            gameScore["score"] = newscore;
                        // 针对每一个AVObject,task都去调用SaveAsync这个方法。
                        task = task.ContinueWith(_ =>
                        {
                            // 返回一个Task。当保存完毕之后这个Task就会标记为完成。
                            return gameScore.SaveAsync();
                        });
                    };
                    Debug.Log("NetWork SendResultMsg Username 录入成功 TotalGrade:" + username + "   " + totalGrade);
                });
            }
            else
            {
                Debug.Log("第一次出现");
                gameScore.SaveAsync();
            }

        });
        
    }
}

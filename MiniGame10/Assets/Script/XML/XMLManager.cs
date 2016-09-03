using UnityEngine;
using System.Collections;

public class XMLManager
{
    private static XMLManager instance = null;

    #region 单例模式抽象出来（优化）
    private XMLManager()
    {

    }

    public static XMLManager Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new XMLManager();
            }
            return instance;
        }
    }
    #endregion


    public ArrayList _ruleList = new ArrayList();

    public ArrayList _speedList = new ArrayList();

    
}

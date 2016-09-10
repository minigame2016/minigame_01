using UnityEngine;
using System.Collections;

public class NetWorkListener : MonoBehaviour {

    private bool _isLogin;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        _isLogin = NetWork.Instance._isLoginCall;
        if (_isLogin)
        {
            //Debug.LogWarning("---" + _isLogin);
            NetWork.Instance.LoginResultSC();
            NetWork.Instance._isLoginCall = false;
        }
	}
}

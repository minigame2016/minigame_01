using UnityEngine;
using System.Collections;

public class UIMainPage : MonoBehaviour {

    public void OnClickQuitGameBtn()
    {
        Debug.Log("UIMainPage OnClickQuitGameBtn Quit");
        Application.Quit();
    }
}

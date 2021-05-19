using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class New_Game : MonoBehaviour
{
    public Button b1 = null;
    public Button b2 = null;
    public Text t = null;

    public void OnButtonPress(){
        Debug.Log("clicked");
        b1.gameObject.SetActive(!b1.gameObject.active);
        b2.gameObject.SetActive(b1.gameObject.active);
        t.gameObject.SetActive(b1.gameObject.active);
    }

    public void go_to_game(){
        SceneManager.LoadScene("MainGame");
    }
}

using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Button b = null;
    public Text t = null;
    public static bool ended = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "player1"){
            t.text = "You Won!";
            ended = true;
        }
        if(col.gameObject.tag == "bot"){
            t.text = "You Lose!";
            ended = true;
        }

        b.gameObject.SetActive(true);
        t.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }
}

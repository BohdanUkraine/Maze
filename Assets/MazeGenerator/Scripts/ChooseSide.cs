using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseSide : MonoBehaviour
{

    public int r;

    public Button b1;
    public Button b2;
    public GameObject go;
    public float time = 10;
    public GameObject bot_up;
    public GameObject bot_down;
    public GameObject player_up;
    public GameObject player_down;

    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new System.Random();
        r = rnd.Next(0, 1);
    }

    public void UpSide(){
        b1.gameObject.SetActive(false);
        b2.gameObject.SetActive(false);
        go.SetActive(false);

        bot_down.SetActive(true);
        player_up.SetActive(true);
    }

    public void DownSide(){
        b1.gameObject.SetActive(false);
        b2.gameObject.SetActive(false);
        go.SetActive(false);

        bot_up.SetActive(true);
        player_down.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //if(timer.timeValue <= 0)
          //  if(r == 1) DownSide();
            //    else UpSide();

        //time -= Time.deltaTime;
        //timer.timeValue = time;
    }
}

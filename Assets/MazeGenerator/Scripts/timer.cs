using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timer : MonoBehaviour
{
    public float timeValue = 10;
    private static bool playerturn2 = true;
    public static bool playerturn = true;
    public Text text;

    void Update()
    {   
        
        if(timeValue > 0)
            timeValue -= Time.deltaTime;
        else
            timeValue = 0;

        DisplayTime(timeValue);
    }

    public void DisplayTime(float time){
        if (EndGame.ended == true)
            return;

        if(time < 0){
            timeValue = 10;
            playerturn = !playerturn;
            playerturn2 = playerturn;
        }

        if(playerturn2 != playerturn){
            timeValue = 10;
            playerturn2 = playerturn;
        }

        string s;

        s = playerturn ? "Your turn: " : "Opponent's turn: ";

        int a = (int)time;

        text.text = s + a.ToString();
    }
}

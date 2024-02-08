using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class False : MonoBehaviour
{
    public static int score; // スコア
    private GameControl gameControl;

    void Start()
    {
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            GameControl.stage_x += 42.2f;
            GameControl.stage_z -= 60f; 
    
            Destroy(this.gameObject);
            if(this.tag == "True")
            {    
            GameControl.score ++;
            Debug.Log("正解!!" + GameControl.score);
            gameControl.Right(); 
            }
            if(this.tag == "False")
            {
            GameControl.score = 0;
            Debug.Log("ぶーーー!!");
            gameControl.Wrong();
            }
        }        
    }
}

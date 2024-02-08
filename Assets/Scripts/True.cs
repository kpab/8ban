using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class True : MonoBehaviour
{
    public static int score; // スコア
    private GameControl gameControl;
    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.Find("GameControl").GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            GameControl.stage_x -= 42.26f;
            GameControl.stage_z += 5.5f; 
            GameControl.stage_r += 180f;
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

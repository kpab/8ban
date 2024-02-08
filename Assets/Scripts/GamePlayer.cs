using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    public static int score; // スコア
    public GameControl gameControl;
    
    // Start is called befoe the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerExit(Collider other)
    {
        if(other.name == "BlockTrue")
        {
            GameControl.stage_x -= 42f;
            GameControl.stage_z += 5.5f; 
            GameControl.stage_r = 180f;

            if(other.gameObject.tag == "True")
            {    
            Destroy(other.gameObject);
            score += 1;
            Debug.Log("正解!!" + score);
            gameControl.Right();
            }
            if(other.gameObject.tag == "False")
            {
            Destroy(other.gameObject);
            score = 0;
            Debug.Log("ぶーーー!!");
            gameControl.Wrong();
            }
        }
        if(other.name == "BlockFalse")
        {
            GameControl.stage_x += 42f;
            GameControl.stage_z -= 59.85f; 
            GameControl.stage_r = 0f;
            if(other.gameObject.tag == "True")
            {
            Destroy(other.gameObject);
            score += 1;
            Debug.Log("正解!!" + score);
            gameControl.Right();
            }
            if(other.gameObject.tag == "False")
            {
            Destroy(other.gameObject);
            score = 0;
            Debug.Log("ぶーーー!!");
            gameControl.Wrong();
            }
        }


        
        
    }
}

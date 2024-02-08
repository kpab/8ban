using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameControl : MonoBehaviour
{
    public List<GameObject> Panels = new List<GameObject>(); //問題パネルリスト
    public List<Texture> texture_list = new List<Texture>(); //問題リスト
    public List<Texture> now_list = new List<Texture>(); // 現在の問題リスト
    private List<int> answers = new List<int>() {0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0}; // 0:正解 1:不正解
    public List<int> now_answers = new List<int>(); // 現在の問題の答えリスト
    public List<Texture> wrong_questions = new List<Texture>(); // 間違えた問題の答えリスト
    public List<int> wrong_answers = new List<int>(); // 間違えた問題の答えリスト
    private Texture randomTexture;
    Material material;
    public static bool Normality; // True:正常 False:異常
    
    public GameObject RightWall; // 右進んだ時の透過オブジェクト
    public GameObject LeftWall; // 左進んだ時の透過オブジェクト
    public GameObject Stage; // ステージプレハブ

    public static GameObject oldStage; // 前回ステージ
    public static int score; // スコア
    private TextMeshProUGUI ScoreText; // スコアテキスト

    public static float stage_x;
    public static float stage_z;

    public static int stage_number;
    private int random;
    private GameObject newStage;
    

    // Start is called before the first frame update
    void Start()
    {
        stage_number = 1;
        score = 0;
        stage_x = 0;
        stage_z = 0;

        Texture tmp;
        for(int i=1; i<answers.Count; i++) // 全ての問題をリストに格納
        {
            tmp = Resources.Load("Questions/"+i) as Texture;
            texture_list.Add(tmp);
        }

        CreateStage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CreateStage()
    {   
        Debug.Log("Createスタート!");
        newStage = Instantiate(Stage, new Vector3(stage_x, 0, stage_z), Quaternion.identity);
        RightWall = newStage.transform.Find("BlockTrue").gameObject;
        LeftWall = newStage.transform.Find("BlockFalse").gameObject;
        ScoreText = GameObject.Find(newStage.name+"/Canvas/ScoreText").GetComponent<TextMeshProUGUI>();

        for(int i=0; i<3; i++)
        {
            Panels.Add(newStage.transform.Find("Panel"+i).gameObject);
            //Panels.Add(GameObject.Find("Panel"+i));
            material = Panels[i].GetComponent<MeshRenderer>().material;
            random = Random.Range(0, texture_list.Count-1);
            randomTexture = texture_list[random];
            material.mainTexture = randomTexture; // i番パネルにテクスチャ設定

            now_list.Add(randomTexture); // 現在の問題リストに追加
            now_answers.Add(answers[random]); // 現在の答え保存
            texture_list.RemoveAt(random); //インデックスrandomの問題をリストから削除
            answers.RemoveAt(random); //インデックスrandomの解答をリストから削除
        }
        // ----------おじパネル----------
        Panels.Add(newStage.transform.Find("Oji/Panel3").gameObject);
        //Panels.Add(GameObject.Find("Panel"+i));
        material = Panels[3].GetComponent<MeshRenderer>().material;
        random = Random.Range(0, texture_list.Count-1);
        randomTexture = texture_list[random];
        material.mainTexture = randomTexture; // i番パネルにテクスチャ設定

        now_list.Add(randomTexture); // 現在の問題リストに追加
        now_answers.Add(answers[random]); // 現在の答え保存
        texture_list.RemoveAt(random); //インデックスrandomの問題をリストから削除
        answers.RemoveAt(random); //インデックスrandomの解答をリストから削除
        // ----------------------------

        newStage.name = "Stage"+stage_number;
        stage_number++;

        ScoreText.SetText(score.ToString());
        Debug.Log("スコアテキストに "+score+" を設定");

        Debug.Log("問題設置!");
        SetQuestion();

    }

    public void SetQuestion()
    {
        //ScoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        

        

        if(now_answers.Contains(1))
        {
            Debug.Log("異常あり");
            Normality = false;
            RightWall.tag = "False";
            LeftWall.tag = "True";

        }
        else
        {
            Debug.Log("正常");
            Normality = true;
            RightWall.tag = "True";
            LeftWall.tag = "False";
        }
    }

    public void Wrong()
    {
        Debug.Log("WrongだからRight送るよ!");
        wrong_questions.AddRange(now_list);
        wrong_answers.AddRange(now_answers);
        Right();
    }
    public void Right()
    {
        Debug.Log("Rightだよ!");
        now_list.Clear();
        now_answers.Clear();
        Panels.Clear();

        for(int i=0; i<4; i++)
        {
            GameObject oldPanel = GameObject.Find("Panel"+i);
            Destroy(oldPanel);
        }
        oldStage = newStage;

        CreateStage();
    }

}

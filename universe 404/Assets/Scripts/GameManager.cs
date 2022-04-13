using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Fungus;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    /// <summary>
    /// 目标 HUD 的图像
    /// </summary>
    public List<Sprite> TargetImages;

    public GameObject Player;
    public GameObject myCanvas;
    public GameObject Flowchart_Enter3D;
    public GameObject BBGM;
   
    /// <summary>
    /// 3D 场景中，找到目标的时间限制
    /// </summary>
    public float FindObjectTime = 30f;

    /// <summary>
    /// 上一个检查点的 UniqueId
    /// </summary>
    public string LastCheckpoint = "";

    /// <summary>
    /// 离开 2D 空间时的位置
    /// </summary>
    public Vector3 Last2DPosition;

    /// <summary>
    /// 所有已经收集的碎片，包括已经用掉的
    /// </summary>
    public List<string> CollectedShards;

    /// <summary>
    /// 当前可以使用的碎片数量
    /// </summary>
    public int ShardCount = 0;

    /// <summary>
    /// 消耗的碎片数量
    /// </summary>
    public int SpendCount = 3;

    /// <summary>
    /// 所有已经触发的对话
    /// </summary>
    public List<string> TriggeredFlowcharts;

    Text _textTimer;
    Text _textShard;
    float _timeLeft = 30f;
    bool _transitionBegan = false;


    public Vector3 currentPos;
    //
    public bool isOver_start;
    public bool isOver_switch;
    public bool isOver_111;
    public bool isOver_222;
    public bool isOver_333;
    public bool isOver_444;
    public bool isOver_555;
    public bool isOver_666;
    public bool isOver_777;

    /// <summary>
    /// 拾取物体后的效果
    /// </summary>
    public bool canJump;
    public bool canBBGM;




    public Flowchart flowchart_111;
    private void Start()
    {
        CollectedShards = new List<String>();
        TriggeredFlowcharts = new List<String>();

    }

    private void Awake()
    {
        // 不允许多个 manager 存在
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
     

        // manager 重置场景时保存（存储碎片/检查点信息）
        instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(myCanvas);
        DontDestroyOnLoad(Flowchart_Enter3D);
        DontDestroyOnLoad(BBGM);

        SceneManager.sceneLoaded += OnSceneLoaded;
        // SceneManager.sceneUnloaded += OnSceneUnloaded;
        BlockSignals.OnBlockEnd += OnBlockEnd;


        _textShard = GameObject.Find("ShardText")?.GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
        var scene_name = SceneManager.GetActiveScene().name;
        // 3D 场景下，30秒倒计时后激活传送对话
        if (!_transitionBegan && scene_name == "3D" )
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0 )
            {
                Cursor.lockState = CursorLockMode.None;
                GameObject.Find("Flowchart_Timeout").GetComponent<Flowchart>().ExecuteBlock("Start");
                _timeLeft = 0;
                _transitionBegan = true;
            }
            _textTimer.text = "Time: " + Math.Round(_timeLeft, 1);
        } else if (scene_name == "2D" && Player != null)
        {
            Last2DPosition = Player.transform.position;
        }

         Ray.pos  = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f);
        //物品收集
        if (Ray.isComputer)
        {
            Destroy(GameObject.Find("computer"));
            canJump = true;
        }
        if (Ray.isErji)
        {
            Destroy(GameObject.Find("耳机"));
            canBBGM = true;
        }
        if (Ray.isNike)
        {
            Destroy(GameObject.Find("耐克"));
            PlayerController2D.m_JumpForce = 1000f;
        }

        _textShard.text = " " + ShardCount;
    } 
   

    /// <summary>
    /// 收集一个碎片。
    /// </summary>
    /// <param name="shard">碎片类</param>
    public void CollectShard(Shard shard)
    {
        var id = shard.GetComponent<UniqueId>();

        // 不能重复收集碎片
        if (CollectedShards.Contains(id.uniqueId))
            return;

        CollectedShards.Add(id.uniqueId);
        ShardCount++;


        var total = CollectedShards.Count;

        // 每 7 个碎片触发 Fungus 的对话和场景转换
        //if (total % 7 == 0)
        //{
            //FlowChartSwitch(total);
            //对话时角色无法行动
            //Player = GameObject.FindGameObjectWithTag("Player");
            //Player.GetComponent<PlayerMovement>().canMove = false;
        //}

        FlowChartSwitch(CollectedShards.Count);
        
    }

    public void SetCanMoveToTrue()
    {
        Player.GetComponent<PlayerMovement>().canMove = true;
    }

    public void SetCanMoveToFalse()
    {
        Player.GetComponent<PlayerMovement>().canMove = false;
    }

    public void setCanvas()
    {
        myCanvas.SetActive(true);
    }
    

    /// <summary>
    /// 重置当前场景。玩家的位置会设置在上一个存档点（如果有）。
    /// </summary>
    public void Reload()
    {
        // 不使用上次位置而返回检查点
        Last2DPosition = Vector3.zero;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// 对话内容转换方法，在7 14 21 28时候调用
    /// </summary>
    /// <param name="a">碎片数量</param>
    private void FlowChartSwitch(int a)
    {
        var flowchartGameObject = GameObject.Find("Flowchart_Enter3D");
        var comp = flowchartGameObject.GetComponent<Flowchart>();
        comp.SetIntegerVariable("TotalShardCount", a);
        comp.ExecuteBlock("Start");

    }
  


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "2D")
        {
            if (canBBGM)
            {
                GameObject.Find("Audio Source").GetComponent<AudioSource>().volume = 1;
            }
            
            // 2D 场景
            Player = GameObject.FindGameObjectWithTag("Player");
            _textShard = GameObject.Find("Text_Shard").GetComponent<Text>();
            // 还原碎片和对话进度
            foreach (Shard shard in FindObjectsOfType<Shard>())
            {
                if (CollectedShards.Contains(shard.GetComponent<UniqueId>().uniqueId))
                    shard.SetCollected(true);
            }
            foreach (var flowchart in FindObjectsOfType<Flowchart>())
            {
                var uniqueId = flowchart.GetComponent<UniqueId>();
                if (uniqueId != null && TriggeredFlowcharts.Contains(uniqueId.uniqueId))
                    Destroy(flowchart.gameObject);
            }

            // 还原玩家位置
            if (Last2DPosition != null && Last2DPosition != Vector3.zero)
            {
                Player.transform.position = Last2DPosition;
                Last2DPosition = Vector3.zero;
            } else if (LastCheckpoint != null)
            {
                foreach (Checkpoint cp in FindObjectsOfType<Checkpoint>())
                {
                    if (cp.GetComponent<UniqueId>().uniqueId == LastCheckpoint)
                    {
                        Player.transform.position = cp.transform.position;
                        break;
                    }

                }
            }

            if (isOver_111)
            {
                GameObject.Find("Flowchart_111").SetActive(false);
            }
            if (isOver_222)
            {
                GameObject.Find("Flowchart_222").SetActive(false);
            }

        } else if (scene.name == "3D")
        {
            // 3D 场景

            _transitionBegan = false;
            _timeLeft = FindObjectTime;
            _textTimer = GameObject.Find("Text_TimeLeft").GetComponent<Text>();
            ShardCount -= SpendCount;
            var image = GameObject.FindGameObjectWithTag("TargetCanvas").GetComponent<Image>();
            image.sprite = TargetImages[CollectedShards.Count / 7];
            //从2D来到3D时开启voluem；
            PlayerController2D.isDead = true;
        }
        //从3D返回2D时关闭voluem；
        if(scene.name == "2D" && PlayerController2D.isDead){
            PlayerController2D.isDead = false;
        }
    }

    private void OnSceneUnloaded(Scene scene)
    {

    }

    void OnBlockEnd(Block block)
    {
        Debug.Log("Block ended: " + block.BlockName);
        /*
        if (block.BlockName == "ReturnTo2D")
        {
            SceneManager.LoadScene("2D");
        }
        */
    }
   
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    Vector3[] paths = new Vector3[4];
    public float speed = 2f;  //角色左右移动速度
    public float jumpSpeed = 5; //角色向上跳跃速度
    private bool isBottomkeyClick = false;  //判断是否按下键盘

    bool isJustOnGround;

    bool isLastDownArrowPressed = false;         //判断是否按了两次向下
    bool isLastUpArrowPressed = false;           //判断是否按了两次向上

    GameObject collisionGameObject;              //碰撞对象
    GameObject colliderGameObject;               //碰撞层对象
    public GameObject stuff1;                    //地面碎片
    public GameObject stuff2;
    public GameObject stuff3;
    public GameObject stuff4;

    public GameObject camera1;                   //1号摄像机
    public GameObject camera2;                   //2号摄像机
    public GameObject obj;                             

    float x = 0.4f;

    public int FloorBroken = -1;                   //完成楼层
    public int Score = 0;                          //分数
    public int CoinCount = 0;                      //金币数量
    public float time = 0;

    public AudioClip Clip;

    Texture textureUp;
    Texture textureDown;
    Texture textureLeft;
    Texture textureRight;
    // Use this for initialization
    void Start()
    {
        //if (!Input.gyro.enabled)
        //    Input.gyro.enabled = true;

        textureUp = Resources.Load<Texture>("Keyboard/A");
        textureDown = Resources.Load<Texture>("Keyboard/B");
        textureLeft = Resources.Load<Texture>("Keyboard/Left");
        textureRight = Resources.Load<Texture>("Keyboard/Right");
    }

    //播放获取金币音乐
    public void PlayPickSound()
    {
        Clip = (AudioClip)Resources.Load("Sounds/pick");
        GetComponent<AudioSource>().clip = Clip;
        GetComponent<AudioSource>().Play();
    }
    void CameraDes()
    {
        camera2.SetActive(true);                  //启动2号摄像机
        camera1.SetActive(false);                 //关闭1号摄像机
        Destroy(camera1, 1.5f);                     //销毁1号摄像机
        Destroy(obj, 1f);
    }

    //摄像机的切换
    void CameraMove()
    {
        iTween.MoveTo(camera1, obj.transform.position, 1f);
    }

    //角色还原
    void Back()
    {
        this.gameObject.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
    }
    //进入碰撞
    void OnCollisionEnter(Collision info)
    {
        collisionGameObject = info.gameObject;
        Debug.Log("碰到物体："+collisionGameObject.name);

        //角色变大
        if(collisionGameObject.tag.CompareTo("Bigger")==0)
        {
            this.gameObject.transform.localScale = new Vector3(this.transform.localScale.x,
                                                               this.transform.localScale.y + 0.4f,
                                                               this.transform.localScale.z + 0.4f);
            Invoke("Back", 5f);
            Destroy(collisionGameObject);
        }
        //跳起踩到到敌人
        if (collisionGameObject.tag.CompareTo("Enemy") == 0
            && GetComponent<StatusControl>().isGround == false)
        {
            Destroy(collisionGameObject);
            Score++;
        }

        //碰到机器人，角色死亡
        if (collisionGameObject.tag.CompareTo("RobotEnemy") == 0
            && GetComponent<StatusControl>().isGround == false)
        {
            Destroy(collisionGameObject);
            Score++;
        }
        //踩到机器人，消除机器人
        if (collisionGameObject.tag.CompareTo("RobotEnemy") == 0)
        {
            ToDie();
        }
        //碰到子弹，角色死亡
        if(collisionGameObject.tag.CompareTo("Bullet") == 0)
        {   
            ToDie();
        }
        if (collisionGameObject.tag.CompareTo("GroundX") == 0)
        {
            GetComponent<StatusControl>().isJump = true;
            GetComponent<StatusControl>().isGround = true;
        }

        //角色进入地下后，可撞破地面
        if (GetComponent<StatusControl>().CanBreakBricks == true
            && collisionGameObject.tag.CompareTo("Brick") == 0)
        {
            //加分
            Score++;
            //销毁
            Destroy(collisionGameObject);
            //生成地面碎片
            GameObject.Instantiate(stuff1,
                new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
                Quaternion.identity);
            GameObject.Instantiate(stuff2,
                new Vector3(this.transform.position.x - 0.2f, this.transform.position.y + 0.2f, this.transform.position.z),
                Quaternion.identity);
            GameObject.Instantiate(stuff3,
                new Vector3(this.transform.position.x - 0.3f, this.transform.position.y, this.transform.position.z),
                Quaternion.identity);
            GameObject.Instantiate(stuff4,
                new Vector3(this.transform.position.x + 0.2f, this.transform.position.y, this.transform.position.z),
                Quaternion.identity);
        }
    }

    //退出碰撞
    void OnCollisionExit(Collision info)
    {
        Debug.Log("离开物体：" + collisionGameObject.name);
    }

    //进入碰撞层
    void OnTriggerEnter(Collider info)
    {
        colliderGameObject = info.gameObject;

        if (colliderGameObject.tag.CompareTo("GroundX") == 0)
        {
            GetComponent<StatusControl>().isGround = true;
            GetComponent<StatusControl>().isJump = true;
        }

        //碰到子弹，角色死亡
        if (colliderGameObject.tag.CompareTo("Bullet") == 0)
        {
            ToDie();
        }

        Debug.Log("碰到碰撞层：" + colliderGameObject.name);
    }

    //离开碰撞层
    void OnTriggerExit(Collider info)
    {

        if (colliderGameObject.tag.CompareTo("AirY")== 0)
        {
            GetComponent<StatusControl>().CanBreakBricks = false;
            GetComponent<StatusControl>().isJump = true;

            //撞破的层数加一
            FloorBroken++;
        }
        if (colliderGameObject.tag.CompareTo("IntoScene")==0)
        {
            GetComponent<StatusControl>().isLeaveTube = true;
            Invoke("CameraMove",0f);
            Invoke("CameraDes", 1.5f);
        }
        if (colliderGameObject.tag.CompareTo("GroundX") == 0)
        {
            GetComponent<StatusControl>().isGround = false;
            GetComponent<StatusControl>().isMove = false;
        }
        Debug.Log("离开碰撞层" + colliderGameObject.name);

    }

    //角色死亡
    void ToDie()
    {
        this.transform.localScale = new Vector3(this.transform.localScale.x,
                                             -this.transform.localScale.y,
                                              this.transform.localScale.z);
        GameObject.Find("Canvas").GetComponent<GameMenu>().OnGameOver(FloorBroken, Score, CoinCount, time);
    }

    // Update is called once per frame
    void Update()
    {
        //限制角色移动范围
        if (GetComponent<StatusControl>().isLeaveTube)
        {
            if (this.transform.position.x < -2f)
            {
                this.transform.position = new Vector3(-2f, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.x > 1.95f)
            {
                this.transform.position = new Vector3(1.95f, this.transform.position.y, this.transform.position.z);
            }
        }
        else
        {
            if (this.transform.position.x > 1.95f)
            {
                this.transform.position = new Vector3(1.95f, this.transform.position.y, this.transform.position.z);
            }
            if (this.transform.position.x < -18f)
            {
                this.transform.position = new Vector3(-18f, this.transform.position.y, this.transform.position.z);
            }
        }

        //摄像机跟随
        camera2.transform.position = new Vector3(camera2.transform.position.x,
                                                 this.transform.position.y - 1.3f,
                                                 camera2.transform.position.z);

        time = Time.time /60;        //计算总时间
    }

    //虚拟方向按键
    void OnGUI()
    {   GUI.color = Color.white;
       // GUI.backgroundColor = Color.white;
        float hh =0;
        Vector3 v = GetComponent<Rigidbody>().velocity;
        GUILayout.BeginArea(new  Rect(0, 3*Screen.height / 4, Screen.width, Screen.height / 4));
        GUILayout.BeginHorizontal();                //横向
        GUILayout.Space(10);
        GUILayout.BeginVertical();                  //纵向

        if(GUILayout.Button(textureUp, GUILayout.Width(5 * Screen.height / 64), GUILayout.Height(5 * Screen.height / 64)))
        {
            Debug.Log("高" +  Screen.height  +"宽"+Screen.width);
            if (GetComponent<StatusControl>().isGround && GetComponent<StatusControl>().isJump == true)
            {
                //按向上箭头，控制角色跳跃
                GetComponent<Rigidbody>().velocity = new Vector3(v.x, jumpSpeed + 1, v.z);
                Debug.Log("UPjump");
                GetComponent<StatusControl>().isJump = false;
                Clip = (AudioClip)Resources.Load("Sounds/jump");
                GetComponent<AudioSource>().clip = Clip;
                GetComponent<AudioSource>().Play();
            }
        }

        GUILayout.Space(20);

        if (GUILayout.RepeatButton(textureLeft,GUILayout.Width(5 * Screen.height / 64), GUILayout.Height(5 * Screen.height / 64)))
        {
            if (GetComponent<StatusControl>().isGround &&isJustOnGround==false)
            {
                hh = -1f;
                this.gameObject.transform.Translate(new Vector3(0, 0, hh * speed * Time.deltaTime));
                this.GetComponent<Animator>().SetBool("RUN", true);
            }
        }

        GUILayout.EndVertical();       //结束纵向
        GUILayout.FlexibleSpace();     //左右对齐
        GUILayout.BeginVertical();     //纵向

        if (GUILayout.Button(textureDown, GUILayout.Width(5 * Screen.height / 64), GUILayout.Height(5 * Screen.height / 64)))
        {
            if (GetComponent<StatusControl>().isGround && GetComponent<StatusControl>().isJump == true)
            {
                //按向下箭头，控制角色跳跃
                GetComponent<Rigidbody>().velocity = new Vector3(v.x, jumpSpeed, v.z);
                Debug.Log("jump");

                Clip = (AudioClip)Resources.Load("Sounds/jump");
                GetComponent<AudioSource>().clip = Clip;
                GetComponent<AudioSource>().Play();

                GetComponent<StatusControl>().isJump = false;

                // 改变能否破砖的状态
                GetComponent<StatusControl>().CanBreakBricks = true;
            }
        }

        GUILayout.Space(20);


        if (GUILayout.RepeatButton(textureRight, GUILayout.Width(5 * Screen.height / 64), GUILayout.Height(5 * Screen.height / 64)))
        {
            if (GetComponent<StatusControl>().isGround && isJustOnGround==false)
            {
                this.GetComponent<Animator>().SetBool("RUN", true);
                hh = 1f;
                this.gameObject.transform.Translate(new Vector3(0, 0,hh * speed * Time.deltaTime ));
            }
        }

        GUILayout.EndVertical();     //结束纵向
        GUILayout.Space(10);
        GUILayout.EndHorizontal();   //结束横向
        GUILayout.EndArea();
        
    }

    void FixedUpdate()
    {

        bool isNowDownArrowPressed = Input.GetKeyDown(KeyCode.DownArrow);
        bool isDOWNJustPressed = (isLastDownArrowPressed == false && isNowDownArrowPressed == true);
        bool isNowUpArrowPress = Input.GetKeyDown(KeyCode.UpArrow);
        bool isUpJustPressde = (isLastUpArrowPressed == false && isNowUpArrowPress == true);

        //判断角色是不是刚刚落到地面
        isJustOnGround = (GetComponent<StatusControl>().isLastOnGround == false 
                               && GetComponent<StatusControl>().isGround == true);

        //判断角色状态
        if (GetComponent<StatusControl>().isGround == true)
        {

            // 刚从空中落下
            if (isJustOnGround)
            {
                // 判断是否能破坏砖
                Debug.Log("just on ground");
                GetComponent<StatusControl>().isMove = true;
            }
            else
            {
                Vector3 v = GetComponent<Rigidbody>().velocity;
                if (GetComponent<StatusControl>().isMove)
                {
                    //在平地走，控制角色左右移动 
                    float h = Input.GetAxis("Horizontal");
                   this.gameObject.transform.Translate( new Vector3(0 ,0, h * speed* Time.deltaTime));
                //  this.gameObject.transform.Translate( new Vector3(0 ,0, Input.acceleration.x));
                //    this.gameObject.transform.Translate(new Vector3(0, 0, Input.gyro.attitude.x));
                    v = GetComponent<Rigidbody>().velocity;

                    this.GetComponent<Animator>().SetBool("RUN", true);
                }
                if (isDOWNJustPressed && GetComponent<StatusControl>().isJump == true)
                {
                    //按向下箭头，控制角色跳跃
                    GetComponent<Rigidbody>().velocity = new Vector3(v.x, jumpSpeed, v.z);
                    Debug.Log("jump");

                    Clip = (AudioClip)Resources.Load("Sounds/jump");
                    GetComponent<AudioSource>().clip = Clip;
                    GetComponent<AudioSource>().Play();

                    GetComponent<StatusControl>().isJump = false;

                    // 改变能否破砖的状态
                    GetComponent<StatusControl>().CanBreakBricks = true;
                }
                if (isUpJustPressde && GetComponent<StatusControl>().isJump == true)
                {
                    //按向上箭头，控制角色跳跃
                    GetComponent<Rigidbody>().velocity = new Vector3(v.x, jumpSpeed + 1, v.z);
                    Debug.Log("UPjump");
                    GetComponent<StatusControl>().isJump = false;
                    Clip = (AudioClip)Resources.Load("Sounds/jump");
                    GetComponent<AudioSource>().clip = Clip;
                    GetComponent<AudioSource>().Play();
                }
            }
        }
        else
        {
            //角色不在地面，什么都不做
        }

        GetComponent<StatusControl>().isLastOnGround = GetComponent<StatusControl>().isGround;
        isLastDownArrowPressed = isNowDownArrowPressed;
        isLastUpArrowPressed = isNowUpArrowPress;
    }
}

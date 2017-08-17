using UnityEngine;
using System.Collections;

public class BombController : MonoBehaviour {

    float x;
    float y;

    GameObject collisionGameObject;              //碰撞对象
    GameObject colliderGameObject;

    public GameObject stuff1;                    //地面碎片
    public GameObject stuff2;
    public GameObject stuff3;
    public GameObject stuff4;

    public AudioClip Clip;

	// Use this for initialization
    void Start()
    {

        x = Random.Range(80f, 200f);
        y = Random.Range(80f, 200f);
        this.GetComponent<Rigidbody>().AddForce(x, y, 0);
    }

    //播放爆炸声音
    void PlayBoom(string str)
    {
        Clip = (AudioClip)Resources.Load("Sounds/bombExplosion");
        GetComponent<AudioSource>().clip = Clip;
        GetComponent<AudioSource>().Play();
    }
	
    void Borken()
    {
  
        GameObject.Instantiate(stuff1,
            new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z),
            this.transform.rotation);
        GameObject.Instantiate(stuff2,
            new Vector3(this.transform.position.x - 0.2f, this.transform.position.y + 0.2f, this.transform.position.z),
            this.transform.rotation);
        GameObject.Instantiate(stuff3,
            new Vector3(this.transform.position.x - 0.3f, this.transform.position.y, this.transform.position.z),
            this.transform.rotation);
        GameObject.Instantiate(stuff4,
            new Vector3(this.transform.position.x + 0.2f, this.transform.position.y, this.transform.position.z),
            this.transform.rotation);
        PlayBoom("bombExplosion");
    }

    //进入碰撞
    void OnCollisionEnter(Collision info)
    {
        collisionGameObject = info.gameObject;
        if(collisionGameObject.tag.CompareTo("Brick")==0)
        {

            //销毁地面
            Destroy(collisionGameObject,2.9f);
            //生成地面碎片
            Invoke("Borken", 2.9f); 
            //销毁炸弹
            Destroy(this.gameObject, 3f);
            
        }
        if (collisionGameObject.tag.CompareTo("Award") == 0)
        {

            PlayBoom("bombExplosion");
            //销毁地面
            Destroy(collisionGameObject);
            //销毁炸弹
            Destroy(this.gameObject, 3f);

        }



    }

    //退出碰撞
    void OnCollisionExit(Collision info)
    {
        

    }

    //进入碰撞层
    void OnTriggerEnter(Collider info)
    {
        colliderGameObject = info.gameObject;

    }

    //离开碰撞层
    void OnTriggerExit(Collider info)
    {
        if (colliderGameObject.tag.CompareTo("Column") == 0)
        {
     
            this.GetComponent<Collider>().isTrigger = false;
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
}

using UnityEngine;
using System.Collections;

public class BrokeController : MonoBehaviour {

    GameObject collisionGameObject;
    GameObject colliderGameObject;
    GameObject Player;
    bool isBroke = false;

    public GameObject stuff1;                    //地面碎片
    public GameObject stuff2;
    public GameObject stuff3;
    public GameObject stuff4;

    int count = 2;

	// Use this for initialization
	void Start () {

        Player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider info)
    {
        colliderGameObject = info.gameObject;
        if (colliderGameObject.tag.CompareTo("AirY") == 0)
        {
            count--;
            if (count == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    void OnCollisionEnter(Collision info)
    {
        collisionGameObject = info.gameObject;
        if(collisionGameObject.tag.CompareTo("Player")==0&&
            Player.GetComponent<PlayerController>().GetComponent<StatusControl>().isGround == false)
        {
            isBroke = true;
        }

        if (collisionGameObject.tag.CompareTo("Award") == 0||
            collisionGameObject.tag.CompareTo("Enemy") == 0||
            collisionGameObject.tag.CompareTo("RobotEnemy") == 0||
            collisionGameObject.tag.CompareTo("Bigger") == 0||
            collisionGameObject.tag.CompareTo("Broke") == 0)
        {
            Destroy(collisionGameObject);
        }

    }

    void OnCollisionStay(Collision info)
    {
        collisionGameObject = info.gameObject;
            if (collisionGameObject.tag.CompareTo("Brick") == 0 && isBroke == true)
            {
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
             //   Player.GetComponent<PlayerController>().FloorBroken++;
            }
    }
}

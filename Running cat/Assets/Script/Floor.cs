using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour { 
	
	private Object[] textures ;
	private Texture2D texture;
	public GameObject bgF ;
    public GameObject player;
    float PosY;

    public GameObject[] character = null;
    public GameObject[] coinGrounp = null;

	// Use this for initialization
	void Start () {

        if (GameObject.Find("Directional Light").GetComponent<Game>().Count == 9)
        {
            texture = Resources.Load<Texture2D>("Pass");
            bgF.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
        }
        else
        {
            //随机背景贴图
            textures = Resources.LoadAll("Textures");
            texture = (Texture2D)textures[Random.Range(0, textures.Length)];
            bgF.GetComponent<Renderer>().material.SetTexture("_MainTex", texture);
        }

        player = GameObject.FindWithTag("Player");


        int i = Random.Range(0, character.Length);
        int n = Random.Range(0, coinGrounp.Length);

        float RandomX = Random.Range(-2.5f,0f);
        float RandomY = Random.Range(1f, 3.2f);

        GameObject sutff = Instantiate(character[i],
                                    new Vector3(this.transform.position.x+RandomX,this.transform.position.y + 3.35f, player.transform.position.z),
                                    Quaternion.identity) as GameObject;
        GameObject coin = Instantiate(coinGrounp[n],
                                    new Vector3(this.transform.position.x + RandomX, this.transform.position.y + RandomY, player.transform.position.z),
                                    Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {

        PosY = this.transform.position.y ;
        if( PosY> player.transform.position.y+10)
        {
            Destroy(this.gameObject);
            GameObject.Find("Directional Light").GetComponent<Game>().FloorCount--;
            Debug.Log("剩下层数：" + GameObject.Find("Directional Light").GetComponent<Game>().FloorCount);
        }
	}
	
}

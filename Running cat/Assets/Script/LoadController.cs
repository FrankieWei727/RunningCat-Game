using UnityEngine;
using System.Collections;

public class LoadController : MonoBehaviour {

    public Sprite[] sprites;
    public float framesPerSecond;
    public float Speed;
    private SpriteRenderer spriteRenderer;
    public string String;
    public string sceneString;
    public float weight;

    public GameObject maincanera;
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;   
        spriteRenderer = GetComponent<Renderer>() as SpriteRenderer;
        sprites = Resources.LoadAll<Sprite>(String);
    }

    // Update is called once per frame
    void Update()
    {
        int index = (int)(Time.timeSinceLevelLoad * framesPerSecond);
        index = index % sprites.Length;
        spriteRenderer.sprite = sprites[index];

        maincanera.transform.position = new Vector3(maincanera.transform.position.x + Time.deltaTime * Speed,
                                                    maincanera.transform.position.y,
                                                    maincanera.transform.position.z);

        if (maincanera.transform.position.x > weight)
        {
            Application.LoadLevel(sceneString);
        }
    }
}

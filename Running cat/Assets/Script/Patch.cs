using UnityEngine;
using System.Collections;

public class Patch : MonoBehaviour {

	// Use this for initialization

    float x;
    float y;
	void Start () {

        Destroy(this.gameObject, 1.5f);
        x = Random.Range(5f, 120f);
        y = Random.Range(5f, 130f);
        this.GetComponent<Rigidbody>().AddForce(x, y, 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

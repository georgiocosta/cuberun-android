using UnityEngine;
using System.Collections;

public class Bong : Pickup {

	/* Use this for initialization
	void Start () {
        Invoke("decay", 30);
	} */

    public Bong()
    {
        decayTime = 30f;
        pickupTime = 0.125f;
        rotationSpeed = 5f;
        rotationAxes = new Vector3(0, 30, 0);
    }
	
	/* Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime * 5f);
	}
    void pickUp()
    {
        Invoke("decay", 0.125f);
    }

    void decay()
    {
        Destroy(gameObject);
    } */
}

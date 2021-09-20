using UnityEngine;
using System.Collections;

public class PlayerMagnet : MonoBehaviour {

    public GameObject player;
	
	// Update is called once per frame
	void Update () {
        if(GetComponent<Renderer>().enabled == true)
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, player.transform.position.z);
    }
}

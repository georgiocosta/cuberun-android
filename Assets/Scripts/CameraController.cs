using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public GameObject player;

    private Vector3 offset;
    private Vector3 velocity;

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;
        velocity = Vector3.zero;
	}
	
	// LateUpdate is called once per frame after update
	void FixedUpdate () {
        Vector3 targetPosition;
        if (player.GetComponent<PlayerController>().IsGrounded())
            targetPosition = new Vector3(30.5f, player.transform.position.y + 2f * Time.deltaTime / 1000f, player.transform.position.z + offset.z);
        else
            targetPosition = new Vector3(30.5f, transform.position.y, player.transform.position.z + offset.z);
        
        if(player.GetComponent<PlayerController>().jumpSpeed > PlayerController.DEFAULTJUMPHEIGHT)
        {
            transform.position += Random.insideUnitSphere * 0.5f;
            foreach(Transform child in transform)
            {
                if(child.CompareTag("WeedScreen"))
                {
                    child.GetComponent<Renderer>().enabled = true;
                }
            }
        }

        else
        {
            foreach(Transform child in transform)
            {
                child.GetComponent<Renderer>().enabled = false;
            }
        } 

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, 0.3f);

    }
}

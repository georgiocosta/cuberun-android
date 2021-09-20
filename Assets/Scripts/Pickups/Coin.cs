using UnityEngine;
using System.Collections;

public class Coin : Pickup
{
    public GameObject player;

    private Vector3 velocity;

    public Coin()
    {
        decayTime = 0.1f;
        pickupTime = 0.25f;
        rotationSpeed = 2.5f;
        rotationAxes = new Vector3(0, 30, 30);
    }

    new void Update()
    {
        base.Update();
        followMagnet();
    }
   
    void followMagnet()
    {
        foreach (Collider other in Physics.OverlapSphere(transform.position, 10.0f))
        {
            if (other.CompareTag("Player"))
            {
                player = other.gameObject;
                if (player.GetComponent<PlayerController>().isMagnet == true)
                {
                    transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, 0.1f);
                }
            }
        }
    }

    /*
    void Start()
    {
        Invoke("decay", 30f);
        velocity = Vector3.zero;
    } */

    /* Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 30, 30) * Time.deltaTime * 2.5f);

        foreach(Collider other in Physics.OverlapSphere(transform.position, 10.0f))
        {
            if(other.CompareTag("Player"))
            {
                player = other.gameObject;
                if(player.GetComponent<playerController>().isMagnet == true)
                {
                    transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref velocity, 0.3f);
                }
            }
        }
    } */

    /*
    void pickUp()
    {
        Invoke("decay", 0.25f);
    }

    void decay()
    {
        Destroy(gameObject);
    } */
}
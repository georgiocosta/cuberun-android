using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{

    public float decayTime, pickupTime, rotationSpeed;
    public Vector3 rotationAxes;

    // Use this for initialization

    // Update is called once per frame
    protected void Update()
    {
        transform.Rotate(rotationAxes * Time.deltaTime * rotationSpeed);
    }

    public void pickUp()
    {
        Invoke("decay", pickupTime);
    }

    public void startDecay()
    {
        Invoke("decay", decayTime);
    }

    public void decay()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class DestroyBox : MonoBehaviour {

	void Update () {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.up * -1, out hit, 3))
        {
            if (hit.collider.CompareTag("Respawn"))
            {
                GetComponent<Renderer>().enabled = false;
                transform.localEulerAngles = new Vector3(0, 0, 0);
                GetComponent<AudioSource>().Play();
                GetComponent<ParticleSystem>().Emit(100);
                Invoke("killBox", 1.0f);
            }
        }
    }

    void killBox()
    {
        Destroy(gameObject);
    }
}

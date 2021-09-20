using UnityEngine;
using System.Collections;

public class FloorGenerator : MonoBehaviour {

    public GameObject platform, coin, player, spinner, bong, magnet;
    public bool isHit;
    public float nextHeight, coinQuantity, boxChance, pwrupChance;

	void Start () {
        isHit = false;
        nextHeight = Mathf.Round(Random.Range(-5.0f, 2.0f));
        boxChance = Mathf.Round(Random.Range(0f, 5f));
        pwrupChance = Mathf.Round(Random.Range(0f, 10f));
    }
	
	void FixedUpdate () {

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.up, out hit, 50) && isHit == false)
        {
            isHit = true;
            Instantiate(platform, new Vector3(0, transform.position.y + nextHeight, transform.position.z + 95 + player.GetComponent<Rigidbody>().velocity.magnitude / 4), Quaternion.identity);
            coinQuantity = Mathf.Round(Random.Range(0.0f, 5.0f));
            createProps();
        }
        else if (Physics.Raycast(transform.position + (Vector3.forward * 140) + (Vector3.up * nextHeight), Vector3.up, out hit, 20))
        {
            if (hit.collider.CompareTag("Player"))
                Destroy(gameObject);
        }

        

        Debug.DrawRay(transform.position, Vector3.up * 50, Color.blue);
        Debug.DrawRay(transform.position + (Vector3.forward * 140) + (Vector3.up * nextHeight), Vector3.up * 20, Color.yellow);
	}

    void createProps() {
        for (int i = 0; i < coinQuantity; i++)
        {
            float zPosition = transform.position.z + Mathf.Round(Random.Range(70.0f, 140.0f));
            Instantiate(coin, new Vector3(0, transform.position.y + nextHeight + Random.Range(2.0f, 3.0f), zPosition), Quaternion.identity);
        }
        if(boxChance == 0f)
        {
            for (int i = 0; i < Mathf.Round(Random.Range(0f, 3f)); i++) {
                float zPosition = transform.position.z + Mathf.Round(Random.Range(70.0f, 140.0f));
                Instantiate(spinner, new Vector3(-0.25f, transform.position.y + nextHeight + 1.6f, zPosition), Quaternion.identity);
            }
        }
        if (pwrupChance == 0f)
        {
            float zPosition = transform.position.z + Mathf.Round(Random.Range(70.0f, 140.0f));
            Instantiate(bong, new Vector3(-0.25f, transform.position.y + nextHeight + 2.0f, zPosition), Quaternion.identity);
        }
        if (pwrupChance == 1f)
        {
            float zPosition = transform.position.z + Mathf.Round(Random.Range(70.0f, 140.0f));
            Instantiate(magnet, new Vector3(-0.25f, transform.position.y + nextHeight + 2.5f, zPosition), Quaternion.identity);
        }
    }
}

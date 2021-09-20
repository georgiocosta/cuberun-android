using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Assertions.Must;
using UnityEngine.Android;

public class PlayerController : MonoBehaviour {

    public static int DEFAULTJUMPHEIGHT = 7;

    public int jumpSpeed;
    public float moveSpeed;
    public float maxSpeed;
    public Vector3 cubeSpeed;
    public bool isMagnet, isGrounded;
    public Text countText, deathText;
    public AudioClip coinSound, deathSound, smokeWeed, getWeed, getMagnet;
    public AudioSource sfx, music;
    public GameObject playerMagnet;
    public KeyCode Jump, Dash;

    public Camera mainCamera;
    public Light mainLightSource;

    private Rigidbody rb;
    private Animator animator;
    private int count;
    private Color backgroundColor;

    void Start () {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        jumpSpeed = DEFAULTJUMPHEIGHT;
        moveSpeed = 1600.0f;
        maxSpeed = 50.0f;
        count = 0;
        setText();
        isMagnet = false;
        isGrounded = false;

        if (PlayerPrefs.GetString("Jump") == "")
            Jump = KeyCode.Space;
        else
            Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump"));

        if (PlayerPrefs.GetString("Dash") == "")
            Dash = KeyCode.RightArrow;
        else
            Dash = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Dash"));
	}
	
	void FixedUpdate () {

        RaycastHit hit;

        if (((Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetKey(Jump)) && IsGrounded())
            {
                {
                    rb.velocity = new Vector3(0, jumpSpeed, rb.velocity.z);
                    AnimateJump();
                }
            }

        else if (IsGrounded() && rb.velocity.y < 0f)
            endAnim();

        if(Input.GetKey(Dash))
        {

        }

        if(Physics.Raycast(transform.position, Vector3.up * -1, out hit, 3))
        {
            if(hit.collider.CompareTag("Respawn"))
            {
                endAnim();
                sfx.clip = deathSound;
                sfx.Play();
                GetComponent<ParticleSystem>().Emit(100);
                Invoke( "ResetLevel" , 5f);
                deathText.text = "YOUR SCORE WAS " + count.ToString();
            }
        }

        if(rb.velocity.z < maxSpeed && isGrounded) 
            rb.AddForce(Vector3.forward * moveSpeed * Time.deltaTime);

        cubeSpeed = rb.velocity;

        backgroundColor = new Color((rb.velocity.z / maxSpeed) / 2f, 0f + (rb.velocity.z / maxSpeed) / 10f, 0f + (rb.velocity.z / maxSpeed) / 10f);
        mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor, backgroundColor, 2f);
        mainLightSource.color = Color.Lerp(mainLightSource.color, backgroundColor, 2f);

        Debug.DrawRay(transform.position, Vector3.up * -0.25f, Color.red);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            count += 1;
            other.GetComponent<Renderer>().enabled = false;
            other.SendMessage("pickUp");
            sfx.clip = coinSound;
            sfx.Play();
            setText();
        }            
        else if (other.CompareTag("Bong"))
        {
            count += 1;
            other.GetComponent<ParticleSystem>().Emit(5000);
            other.SendMessage("pickUp");
            sfx.clip = getWeed;
            sfx.Play();
            music.clip = smokeWeed;
            music.Play();
            jumpSpeed = DEFAULTJUMPHEIGHT * 2;
            Invoke("returnDefault", 19f);
        }
        else if (other.CompareTag("Magnet"))
        {
            isMagnet = true;
            Destroy(other.gameObject);
            Invoke("demagnetise", 19f);
            sfx.clip = getMagnet;
            sfx.Play();
            playerMagnet.GetComponent<Renderer>().enabled = true;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.up * -0.025f, out hit, 1))
        {
            if (hit.collider.CompareTag("Floor") && rb.velocity.y <= 0)
            {
                animator.SetBool("isGrounded", true);
                return true;
            }
            else
            {
                animator.SetBool("isGrounded", false);
                return false;
            }
        }
        else
        {
            animator.SetBool("isGrounded", false);
            return false;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.transform.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    void AnimateJump()
    {
        animator.Play("jump");
    }

    void AnimateDash()
    {
        animator.Play("dash");
    }

    void ResetLevel()
    {
        SceneManager.LoadScene("main");
    }

    void setText()
    {
        countText.text = "Score: " + count.ToString();
    }

    void endAnim()
    {
        animator.StopPlayback();
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    void returnDefault()
    {
        if(music.isPlaying == false)
            jumpSpeed = DEFAULTJUMPHEIGHT;
    }

    void demagnetise()
    {
        isMagnet = false;
        playerMagnet.GetComponent<Renderer>().enabled = false;
    }

}

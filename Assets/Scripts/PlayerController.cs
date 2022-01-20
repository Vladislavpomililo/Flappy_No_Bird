using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float floatForce;
    private Rigidbody playerRb;
    private GameManager gameManager;
    [SerializeField] private AudioClip cornSound;
    [SerializeField] private AudioClip gameOverSound;
    private AudioSource playerAudio;
    private AudioSource mainSourse;

    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainSourse = GameObject.Find("GameManager").GetComponent<AudioSource>();
        Physics.gravity = new Vector3(0,-14.7f,0);

        playerRb = GetComponent<Rigidbody>();

        playerRb.AddForce(Vector3.up * 3, ForceMode.Impulse);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt) && gameManager.isUlta == false)
        {
            gameManager.positionPlayer = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            gameManager.Ulta();
            Destroy(gameObject);
        }
    }

    public virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pipe"))
        {
            mainSourse.Stop();
            mainSourse.PlayOneShot(gameOverSound, 1.0f);
            gameManager.GameOver();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * floatForce * 0.5f, ForceMode.Impulse);
        }

        else if (other.gameObject.CompareTag("Sky"))
        {
            playerRb.AddForce(Vector3.up * floatForce * -0.1f, ForceMode.Impulse);
        }

        else if (other.gameObject.CompareTag("Corn"))
        {
            playerAudio.PlayOneShot(cornSound, 1.0f);
            gameManager.isUlta = false;
            Destroy(other.gameObject);
        }

    }
}

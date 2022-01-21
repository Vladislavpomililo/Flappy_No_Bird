using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected float floatForce; // ���� ������ ������
    protected Rigidbody playerRb; // ���������� ��� ������������ ���������� Rigidbody � ������ Start. �������������� � �������
    private GameManager gameManager; // ������ � ������ GameManager. ������������� � ������ Start
    [SerializeField] private AudioClip cornSound; // ���� ������ �� ���� ���������� �����(�����)
    [SerializeField] private AudioClip gameOverSound; // ���� ������ �� ���� ��������
    protected AudioSource playerAudio; // ������ � ���������� AudioSource ������� ������. ������������� � ������ Start
    private AudioSource mainSourse; // ������ � ���������� AudioSource ������� GameManager. ������������� � ������ Start

    public virtual void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainSourse = GameObject.Find("GameManager").GetComponent<AudioSource>();
        Physics.gravity = new Vector3(0, -14.7f, 0); // ���������� ���� ���������� �� ������

        playerRb = GetComponent<Rigidbody>();

        playerRb.AddForce(Vector3.up * 3, ForceMode.Impulse); // ��� ������ ����� ��� ������� ������. ��� ���� ����� �� ������ �����
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse); // ��� ������� �� ������ ������� �� ��� y � �������� ����� ������ ������.
        }

        // ��� ������� ������ ALT � ���� ����� ������� ���������� ������� ������ ��������� � � GameManager ��� ������ ������� ������, ������� ��� ���� ����� ��������� ����� � ������� 
        // ������ �� �����.

        if (Input.GetKeyDown(KeyCode.LeftAlt) && gameManager.isUlta == false)
        {
            gameManager.positionPlayer = new Vector3(transform.position.x, transform.position.y, transform.position.z); 
            gameManager.Ulta();
            Destroy(gameObject);
        }
    }

    // ��������� ��� ������� �������� � ��������� ������� ���� ��� ������� ����
    public virtual void OnCollisionEnter(Collision other)
    {
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Pipe":
                mainSourse.Stop();
                mainSourse.PlayOneShot(gameOverSound, 1.0f);
                gameManager.GameOver();
                Destroy(gameObject);
                break;

            case "Ground":
                playerRb.AddForce(Vector3.up * floatForce * 0.5f, ForceMode.Impulse);
                break;

            case "Sky":
                playerRb.AddForce(Vector3.up * floatForce * -0.1f, ForceMode.Impulse);
                break;

            case "Corn":
                playerAudio.PlayOneShot(cornSound, 1.0f);
                gameManager.isUlta = false;
                Destroy(other.gameObject);
                break;

            default:
                break;
        }
    }
}

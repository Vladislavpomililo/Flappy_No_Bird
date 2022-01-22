using UnityEngine;

// Контролируем птичку

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected float floatForce; 
    protected Rigidbody playerRb; 
    private GameManager gameManager; 
    [SerializeField] private AudioClip cornSound; 
    [SerializeField] private AudioClip gameOverSound; 
    protected AudioSource playerAudio; 
    private AudioSource mainSourse; 

    public virtual void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        mainSourse = GameObject.Find("GameManager").GetComponent<AudioSource>();
        Physics.gravity = new Vector3(0, -14.7f, 0); // Увеличение силы гравитации на игрока

        playerRb = GetComponent<Rigidbody>();

        playerRb.AddForce(Vector3.up * 3, ForceMode.Impulse); // При старте сцены даём импульс птичке. Для того чтобы не падала резко
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * floatForce, ForceMode.Impulse); // При нажатии на пробел прыгаем по оси y с указаной силой прыжка птички.
        }

        // При нажатии левого ALT и если ульта активна записываем позицию игрока передавая её в GameManager для спавна толстой птички, вызывая при этом метод активации ульты и удаляем 
        // птичку со сцены.

        if (Input.GetKeyDown(KeyCode.LeftAlt) && gameManager.isUlta == false)
        {
            gameManager.positionPlayer = new Vector3(transform.position.x, transform.position.y, transform.position.z); 
            gameManager.Ulta();
            Destroy(gameObject);
        }
    }

    // Проверяем тег обьекта коллизии и выполняем участок кода для данного тега
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

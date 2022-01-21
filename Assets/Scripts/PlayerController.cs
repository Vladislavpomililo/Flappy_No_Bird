using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] protected float floatForce; // Сила прыжка птички
    protected Rigidbody playerRb; // Переменная для присваивания компонента Rigidbody в методе Start. Взаимодействие с физикой
    private GameManager gameManager; // Доступ к скирту GameManager. Присваивается в методе Start
    [SerializeField] private AudioClip cornSound; // Поле ссылки на звук подбирания зерна(ульты)
    [SerializeField] private AudioClip gameOverSound; // Поле ссылки на звук пригрыша
    protected AudioSource playerAudio; // Доступ к компоненту AudioSource обьекта игрока. Присваивается в методе Start
    private AudioSource mainSourse; // Доступ к компоненту AudioSource обьекта GameManager. Присваивается в методе Start

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

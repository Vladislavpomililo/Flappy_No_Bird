using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // Базовый класс движения на сцене. В Update удаление обьектов которые покинули сцену 

    [SerializeField] private float speed;
    private GameManager gameManager;
    private bool check = true;

    public virtual void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public virtual void Update()
    {
        if (gameManager.isGameOver == true)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        if(check == true && transform.position.x < -8.5 && gameObject.CompareTag("Pipe"))
        {
            gameManager.UpdateScore(5);
            check = false;
            if(gameManager.isActivUlta == true)
            {
                gameManager.UpdateScore(5);
            }
        }

        if (transform.position.x < -30)
        {
            Destroy(gameObject);
        }
    }
}

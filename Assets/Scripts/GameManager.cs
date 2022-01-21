using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabPipe;
    [SerializeField] private GameObject prefabCorn;
    [SerializeField] private GameObject prefabPlayerUlta;
    [SerializeField] private GameObject prefabPlayer;
    [SerializeField] private float startDelay;
    [SerializeField] private float repeatRate;
    private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI ultaText;
    [SerializeField] private TextMeshProUGUI timerUltaText;
    public bool isGameOver;
    [SerializeField] private GameObject gameOverMenu;
    private int countCorn;
    public bool isUlta;
    public bool isActivUlta;
    public Vector3 positionPlayer;
    [SerializeField] private float timerUlta;
    private float newTimerUlta;
    [SerializeField] private AudioSource gameSource;

    void Start()
    {
        Cursor.visible = false;
        Instantiate(prefabPlayer, new Vector3(-6.9000001f, 1.42999995f, 0), transform.rotation);
        newTimerUlta = timerUlta;
        isUlta = true;
        isGameOver = true;
        isActivUlta = false;
        countCorn = 4;
        InvokeRepeating("SpawnPipe", startDelay, repeatRate);

        if (MainManager.Instance != null)
        {
            nameText.text = "Èãðîê: " + MainManager.Instance.name;
        }

        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(isGameOver == false)
        {
            CancelInvoke("SpawnPipe");
        }
        if(isUlta == false)
        {
            ultaText.gameObject.SetActive(true);
        }
        else
        {
            ultaText.gameObject.SetActive(false);
        }
        if(isActivUlta == true)
        {
            if (timerUlta == newTimerUlta)
            {
                timerUltaText.gameObject.SetActive(true);
            }

            timerUlta -= Time.deltaTime;
            timerUltaText.text = timerUlta.ToString("0,0");

            if (timerUlta < 0)
            {
                timerUltaText.gameObject.SetActive(false);
                NormalPlayer();
                timerUlta = newTimerUlta;
            }
        }
    }

    private void SpawnPipe()
    {
        countCorn++;
        float randomY = RandomPositionY();
        Quaternion quaternion = new Quaternion(180, 0, 0,0);
        Instantiate(prefabPipe, new Vector3 (transform.position.x + 20, randomY, transform.position.z), quaternion);
        Instantiate(prefabPipe, new Vector3(transform.position.x + 20, randomY - 16, transform.position.z), transform.rotation);
        if(countCorn == 5)
        {
            Instantiate(prefabCorn, new Vector3(transform.position.x + 20, randomY - 8, transform.position.z), transform.rotation);
            countCorn = 0;
        }
    }

    private float RandomPositionY()
    {
        float randomY = Random.Range(5f, 12.6f);
        return randomY;
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Ñ÷¸ò: " + score;
    }

    public void GameOver()
    {
        Cursor.visible = true;
        isGameOver = false;
        gameOverMenu.gameObject.SetActive(true);
        ultaText.gameObject.SetActive(false);
        if (score > MainManager.Instance.score)
        {
            MainManager.Instance.score = score;
            MainManager.Instance.SaveRecord();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Ulta()
    {
        gameSource.Pause();
        ultaText.gameObject.SetActive(false);
        Instantiate(prefabPlayerUlta, positionPlayer, transform.rotation);
        isUlta = true;
        isActivUlta = true;
    }

    public void NormalPlayer()
    {
        gameSource.Play();
        var block = GameObject.FindWithTag("PlayerUlta");
        positionPlayer = block.transform.position;
        Destroy(block);
        Instantiate(prefabPlayer, positionPlayer, transform.rotation);
        isActivUlta = false;
    }
}

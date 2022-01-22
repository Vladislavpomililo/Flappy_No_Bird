using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float xRange;
    [SerializeField] private float ySpawnPos;
    private float speed;

    [SerializeField] private GameObject fireWork;

    private Color color;

    void Start()
    {
        transform.position = RandomSpawnPos();

        color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        gameObject.GetComponent<SpriteRenderer>().material.color = color;

        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    { 
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        if(transform.position.y > 40)
        {
            Destroy(gameObject);
        }
    }


    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 40);
    }

    private void OnMouseDown()
    {
        TargetManager.targetColor = color;
        Instantiate(fireWork, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);  
    }
}

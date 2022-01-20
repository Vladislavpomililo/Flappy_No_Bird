using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    private float repeatWidth;

    void Start()
    {
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;
    }

    void Update()
    {
            if (transform.position.x < -23 - repeatWidth)
            {
                Vector3 startPos = new Vector3(33f, transform.position.y, transform.position.z);
                transform.position = startPos;
            }
    }
}

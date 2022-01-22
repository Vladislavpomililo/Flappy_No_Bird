using UnityEngine;

public class BackgroundMove : MoveLeft
{
    private float repeatWidth;

    public override void Start()
    {
        base.Start();
        repeatWidth = GetComponent<BoxCollider2D>().size.x / 2;
    }

    public override void Update()
    {
        base.Update();

            if (transform.position.x < -23 - repeatWidth)
            {
                Vector3 startPos = new Vector3(33f, transform.position.y, transform.position.z);
                transform.position = startPos;
            }
    }
}

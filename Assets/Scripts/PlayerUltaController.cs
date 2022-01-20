using UnityEngine;

public class PlayerUltaController : PlayerController
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private AudioClip pipeSound;
    [SerializeField] private AudioClip ultaSound;

    public override void Start()
    {
        base.Start();
        playerAudio.PlayOneShot(ultaSound, 1f);
    }

    public override void OnCollisionEnter(Collision other)
    {
        string tag = other.gameObject.tag;
        switch (tag)
        {
            case "Pipe":
                playerAudio.PlayOneShot(pipeSound, 1f);
                explosionParticle.Play();
                Destroy(other.gameObject);
                break;

            case "Ground":
                playerRb.AddForce(Vector3.up * floatForce * 0.5f, ForceMode.Impulse);
                break;

            case "Sky":
                playerRb.AddForce(Vector3.up * floatForce * -0.1f, ForceMode.Impulse);
                break;

            case "Corn":
                Destroy(other.gameObject);
                break;

            default:
                break;
        }
    }   
}

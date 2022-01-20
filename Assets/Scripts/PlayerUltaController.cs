using UnityEngine;

public class PlayerUltaController : PlayerController
{
    [SerializeField] private ParticleSystem explosionParticle;
    [SerializeField] private AudioClip pipeSound;
    private AudioSource playerAudio;
    [SerializeField] private AudioClip ultaSound;

    private void Awake()
    {
        playerAudio = GetComponent<AudioSource>();
        playerAudio.PlayOneShot(ultaSound, 1f);
    }

    public override void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Pipe"))
        {
            playerAudio.PlayOneShot(pipeSound, 1f);
            explosionParticle.Play();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Corn"))
        {
            Destroy(other.gameObject);
        }
    }   
}

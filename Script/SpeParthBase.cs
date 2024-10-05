using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpeParthBase : MonoBehaviour
{
    public ParticleSystem particleSystem2;
    public GameObject player;
    private MonoBehaviour playerController;

    protected abstract MonoBehaviour GetPlayerController();
    protected abstract bool IsSpecial();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerController = GetPlayerController();

        StopParticles2();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (IsSpecial())
        {
            PlayParticles2();
        }
        else
        {
            StopParticles2();
        }
    }

    public void PlayParticles2()
    {
        if (particleSystem2 != null && !particleSystem2.isPlaying)
        {
            particleSystem2.Play();
        }
    }

    public void StopParticles2()
    {
        if (particleSystem2 != null && particleSystem2.isPlaying)
        {
            particleSystem2.Stop();
        }
    }
}

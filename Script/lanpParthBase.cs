using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LanpParthBase : MonoBehaviour
{
    public ParticleSystem lanpAttack;
    protected GameObject player;
    protected GameObject weapon;

    // コントローラーは抽象化し、子クラスで定義
    protected MonoBehaviour playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetPlayerController();
        StopParticles();
    }

    // Update is called once per frame
    void Update()
    {
        weapon = GetWeapon();

        if (IsAttackConditionMet() && weapon != null && weapon.CompareTag("Item"))
        {
            StartCoroutine(WaitBefore(0.2f));
        }
        else
        {
            StopParticles();
        }
    }

    protected abstract MonoBehaviour GetPlayerController();
    protected abstract GameObject GetWeapon();
    protected abstract bool IsAttackConditionMet();

    public void PlayParticles()
    {
        if (lanpAttack != null && !lanpAttack.isPlaying)
        {
            lanpAttack.Play();
        }
    }

    public void StopParticles()
    {
        if (lanpAttack != null && lanpAttack.isPlaying)
        {
            lanpAttack.Stop();
        }
    }

    IEnumerator WaitBefore(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PlayParticles();
    }
}

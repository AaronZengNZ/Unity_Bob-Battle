using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFollow : MonoBehaviour
{
    public Transform player;
    bool followPlayer = false;
    public Animator animator;
    public Transform boss;
    public GameObject shootThingy;
    public bool phase2 = false;
    public DialougeTrigger two;
    public bool phase3 = false;
    public DialougeTrigger three;
    public bool phase4 = false;
    public DialougeTrigger four;
    public bool phase5 = false;
    public DialougeTrigger five;
    public ParticleSystem esplosion;
    public ParticleSystem shoot;
    public AudioSource smash;
    public AudioSource pew;

    void Start()
    {
        animator.SetBool("Slam", false);
        shootThingy.SetActive(false);
        StartCoroutine(Shoot());
    }

    public void Phase1()
    {
        StartCoroutine(PhaseOne());
    }

    public void Phase2()
    {
        phase2 = true;
        StartCoroutine(PhaseOnee());
    }

    public void Phase3()
    {
        //phase2 = true;
        StartCoroutine(PhaseOne());
    }

    public void Phase4()
    {
        phase4 = true;
        phase3 = false;
        var shootcol = shoot.collision;
        shootcol.lifetimeLoss = 0f;
        Phase2();
    }

    public void Phase5()
    {
        phase4 = true;
        phase3 = true;
        phase2 = true;
        phase5 = true;
        StartCoroutine(infinite());
    }

    IEnumerator infinite()
    {
        for (var i = 0; i < 69420; i++){
            yield return new WaitForSeconds(3.5f);
            Smash();
        }
    }

    void Update()
    {
        shootThingy.SetActive(phase2);
        shootThingy.transform.LookAt(player);
        if (followPlayer)
        {
            boss.position += new Vector3((boss.position.x - player.position.x) * -Time.deltaTime * 5, 0, (boss.position.z - player.position.z) * -Time.deltaTime * 5) ;
        }
    }

    IEnumerator Shoot()
    {
        for(var i = 0; i < 10000000000000; i++)
        {
            yield return new WaitForSeconds(0.6f);
            if (phase2)
            {
                shoot.Play();
                pew.Play();
            }
        }
    }

    public void Explode()
    {
        smash.Play();
        if (phase3)
        {
            esplosion.Play();
        }
    }

    IEnumerator PhaseOnee()
    {
        yield return new WaitForSeconds(2f);
        Smash();
        yield return new WaitForSeconds(5f);
        Smash();
        yield return new WaitForSeconds(5f);
        Smash();
        yield return new WaitForSeconds(5f);
        Smash();
        yield return new WaitForSeconds(5f);
        Smash();
        yield return new WaitForSeconds(2f);
        if (phase4) {
            five.TriggerDialouge();
            phase2 = false;
            phase3 = false;
            phase4 = false;
        }
        else
        {
            shootThingy.SetActive(false);
            phase2 = false;
            if (phase3 != true)
            {
                phase3 = true;
                three.TriggerDialouge();
            }
        }
    }

    IEnumerator PhaseOne()
    {
        yield return new WaitForSeconds(2f);
        Smash();
        yield return new WaitForSeconds(2.5f);
        Smash();
        yield return new WaitForSeconds(2.5f);
        Smash();
        yield return new WaitForSeconds(2.5f);
        Smash();
        yield return new WaitForSeconds(2.5f);
        Smash();
        yield return new WaitForSeconds(2.5f);
        Smash();
        yield return new WaitForSeconds(2.5f);
        Smash();
        yield return new WaitForSeconds(2.5f);
        Smash();
        yield return new WaitForSeconds(2.5f);
        Smash();
        yield return new WaitForSeconds(2.5f);
        Smash();
        yield return new WaitForSeconds(2f);
        if (phase2 != true)
        {
            two.TriggerDialouge();
        }
        if(phase3 == true)
        {
            four.TriggerDialouge();
            phase3 = false;
        }
    }

    IEnumerator DeactivateFollow()
    {
        animator.SetBool("Slam", false);
        yield return new WaitForSeconds(0f);
        followPlayer = false;
    }

    private void Smash()
    {
        animator.SetBool("Slam", true);
        followPlayer = true;
    }
}

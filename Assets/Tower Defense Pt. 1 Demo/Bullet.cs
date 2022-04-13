using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;

    private EnemyDemo enemy;

    public AudioSource audioSrc;
    public AudioClip audioToPlay;
    public void ChaseTarget(Transform _target)
    {
        target = _target;
    }

    private void Start()
    {
        enemy = GameObject.Find("Knight_die_01").GetComponent<EnemyDemo>();
        audioSrc = enemy.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // https://www.youtube.com/watch?v=oqidgRQAMB8
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        Debug.Log(direction.magnitude + " " + distanceThisFrame);
        
        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        
    }

    void HitTarget()
    {

        if (enemy.health <= 2)
        {
            audioSrc.clip = audioToPlay;
            audioSrc.Play();
        }
        
        Destroy(gameObject);
        enemy.health -= 1;
        enemy.healthBar.value = enemy.health;
        if (enemy.health <= 0)
        {
            enemy.coins += 1;
            enemy.tmp_text.SetText("COINS: " + enemy.coins);
            
            audioSrc.clip = audioToPlay;
            audioSrc.Play();
            
            Destroy(enemy.gameObject);
            SceneManager.LoadScene("Restart");

        }
    }
    
}

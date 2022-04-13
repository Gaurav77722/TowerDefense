using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerHealth : MonoBehaviour
{
    
    private EnemyDemo enemy;
    private GameObject waypoint;
    private float health = 3f;
    public Slider healthBarTower;
    
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Knight_die_01").GetComponent<EnemyDemo>();
        waypoint = GameObject.Find("Goal");
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.speed == 0)
        {
            health -= Time.deltaTime;
            healthBarTower.value = health;
        }

        if (healthBarTower.value == 0)
        {
            Destroy(waypoint);
            Debug.Log("YOU LOST");
            SceneManager.LoadScene("Restart");
        }
    }
}

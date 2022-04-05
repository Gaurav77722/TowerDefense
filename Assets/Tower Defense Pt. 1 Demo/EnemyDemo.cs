using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDemo : MonoBehaviour
{
    // todo #1 set up properties
    //   health, speed, coin worth
    public int health = 3;
    public float speed = 3f;
    public int coins = 0;

    public List<Transform> waypointList;
    private int targetWaypointIndex;

    public Animator animator;

    public TMP_Text tmp_text;
    //   waypoints
    //   delegate event for outside code to subscribe and be notified of enemy death
    public delegate void EnemyDied(EnemyDemo deadEnemy);

    public event EnemyDied onEnemyDied;

    public Slider healthBar;

    // NOTE! This code should work for any speed value (large or small)

    //-----------------------------------------------------------------------------
    void Start()
    {
        // todo #2
        //   Place our enemy at the starting waypoint
        transform.position = new Vector3(waypointList[0].position.x, waypointList[0].position.y, waypointList[0].position.z - 3f);
        targetWaypointIndex = 1;
        animator = GetComponent<Animator>();
    }

    //-----------------------------------------------------------------------------
    void Update()
    {
        animator.SetFloat("speed", speed);
        
        // todo #3 Move towards the next waypoint
        // Vector3 targetPosition = waypointList[targetWaypointIndex].position;
        // Vector3 movementDir = (targetPosition - transform.position).normalized;
        //
        // Vector3 newPosition = transform.position;
        // newPosition += movementDir * speed * Time.deltaTime;
        //
        // transform.position = newPosition;

        bool enemyDied = false;
        
        // todo #4 Check if destination reaches or passed and change target

        //Debug.Log(movementDir);
        
        // if (movementDir == new Vector3(0, 0, 0) && targetWaypointIndex < 7)
        // {
        //     targetWaypointIndex += 1;
        // }
        

        // Waypoint Navigation
        // https://www.youtube.com/watch?v=KxZBAAVvuY4
        transform.position = Vector3.MoveTowards(transform.position,
            waypointList[targetWaypointIndex].transform.position, speed * Time.deltaTime);

        if (transform.position == waypointList[targetWaypointIndex].transform.position && targetWaypointIndex<7)
        {
            targetWaypointIndex++;
        }
        
        

        if (!(targetWaypointIndex < 7) && transform.position == waypointList[targetWaypointIndex].transform.position)
        {
            Debug.Log("YOU LOST");
        }


        // Ray casting to reduce health of the enemy
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Debug.DrawLine (Camera.main.transform.position, hitInfo.point, Color.red, 0.5f);
                if (hitInfo.collider.gameObject.tag == "Enemy")
                {
                    Debug.Log("HIT");
                    health -= 1;
                    Debug.Log("Health: " + health);

                    // Destroy enemy if health < 0
                    if (health < 0)
                    {
                        coins += 1;
                        enemyDied = true;
                        Destroy(hitInfo.collider.gameObject);

                        tmp_text.SetText("COINS: " + coins);
                    }

                }
                else
                {
                    Debug.DrawLine (Camera.main.transform.position, hitInfo.point, Color.red, 0.5f);
                }
            }

        }

        healthBar.value = health;

    } // End of Ray casting if


}


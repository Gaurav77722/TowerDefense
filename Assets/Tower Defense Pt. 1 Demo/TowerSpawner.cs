using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    public GameObject towers;

    private EnemyDemo coinsLeft;

    // Start is called before the first frame update
    void Start()
    {
        coinsLeft = GameObject.Find("Knight_die_01").GetComponent<EnemyDemo>();
        towers = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (coinsLeft.coins > 0)
                {
                    Instantiate(towers, hitInfo.point, Quaternion.identity);
                    coinsLeft.coins -= 1;
                    Debug.Log("Cost of placing a tower is 1$");
                    coinsLeft.tmp_text.SetText("COINS: " + coinsLeft.coins);
                }

            }
        }
    }
}

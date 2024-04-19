using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainControl : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] InputField inputField;
    [SerializeField] GameObject Win;
    [SerializeField] GameObject Lose;

    private Player_Behavior PlayerBehavior;
    private Enemy_behavior EnemyBehavior;
    // Start is called before the first frame update
    void Start()
    {
        PlayerBehavior = player.GetComponent<Player_Behavior>();
        EnemyBehavior = enemy.GetComponent<Enemy_behavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBehavior.getnumber("HP") == 0)
        {
            Win.SetActive(true);
        }
        if (EnemyBehavior.getnumber("HP") == 0)
        {
            Lose.SetActive(true);
        }
    }

    public void CheckAttack(string skill)
    { 
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewAttack : MonoBehaviour
{
    [SerializeField] Text CountdownUI;
    [SerializeField] Text Enemy_NP;
    [SerializeField] Text Player_NP;
    [SerializeField] Text Enemy_HP;
    [SerializeField] Text Player_HP;
    [SerializeField] Text AttackType;
    [SerializeField] InputField passwordInputField;
    [SerializeField] GameObject Win;
    [SerializeField] GameObject Lose;
    public int Enemy_NP_num = 0;
    public int Enemy_HP_num = 100;
    public int Player_NP_num = 0;
    public int Player_HP_num = 100;
    public int NP_Charger = 2;
    // Start is called before the first frame update
    void Start()
    {
        Win.SetActive(false);
        Lose.SetActive(false);
        SyncUi();
        StartCoroutine(SecondUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_HP_num == 0)
        {
            Debug.Log("Win");
            Win.SetActive(true);
        }
        if (Enemy_HP_num == 0)
        {
            Debug.Log("Lose");
            Lose.SetActive(true);
        }
    }
    public void SyncUi() 
    {
        Enemy_NP.text = Enemy_NP_num.ToString();
        Player_NP.text = Player_NP_num.ToString();
        Enemy_HP.text = Enemy_HP_num.ToString();
        Player_HP.text = Player_HP_num.ToString();
    }

    private IEnumerator SecondUpdate() 
    {
        Player_NP_num = Player_NP_num + NP_Charger;
        Enemy_NP_num = Enemy_NP_num + NP_Charger;
        SyncUi();
        yield return new WaitForSeconds(1f);
    }
    public void OnPasswordEnter(string input) 
    {
        if (input == "pAttack")
        { }
    }

}

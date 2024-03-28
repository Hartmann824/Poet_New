using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attack : MonoBehaviour
{
    [SerializeField] Text CountdownUI;
    [SerializeField] Text Enemy_NP;
    [SerializeField] Text Player_NP;
    [SerializeField] Text Enemy_HP;
    [SerializeField] Text Player_HP;
    [SerializeField] Text AttackType;
    [SerializeField] InputField passwordInputField;
    public int Enemy_NP_num = 0;
    public int Enemy_HP_num = 100;
    public int Player_NP_num = 0;
    public int Player_HP_num = 100;
    public int CheckP = 0;
    public int CountDownTime = 10;
    private IEnumerator countdownCoroutine;
    public bool defendfailed = false;
    public bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {

        Enemy_NP.text = Enemy_NP_num.ToString();
        Enemy_HP.text = Enemy_HP_num.ToString();
        Player_NP.text = Player_NP_num.ToString();
        Player_HP.text = Player_HP_num.ToString();
        countdownCoroutine = Countdown();
        passwordInputField.onEndEdit.AddListener(OnPasswordEnter);

    }

    // Update is called once per frame
    void Update()
    {
        if (Player_HP_num == 0)
        {
            Debug.Log("You lose");
            CancelInvoke("SecondCheck");
        }
        if (Enemy_HP_num == 0)
        {
            Debug.Log("You Win");
            CancelInvoke("SecondCheck");
        }
    }

    public void StartGame()
    {
        InvokeRepeating("SecondCheck", 1, 1);
    }

    public void SencondCheck() 
    {
        Player_NP_num++;
        Enemy_NP_num = Enemy_NP_num + 2;
        if (Enemy_NP_num >= 10)
        {

            LightAttackCheck();
        }
        if (Enemy_NP_num == 100)
        {
            HeavyAttackCheck();
        }
    }
    public void LightAttackCheck() 
    {
        if (CheckP >= 0)
        {
            System.Random rand = new System.Random();
            string[] choices = { "Attack1", "Attack2", "Skip"};
            string randomChoice = choices[rand.Next(choices.Length)];
            AttackType.text = randomChoice;
            if (randomChoice != "Skip") 
            {
                CancelInvoke("SecondCheck");
                StartCoroutine(countdownCoroutine);
            }
           

        }
    }
    public void HeavyAttackCheck() 
    {
        AttackType.text = "HeavyAttack";
        CancelInvoke("SecondCheck");
        StartCoroutine(countdownCoroutine);

    }

    private IEnumerator Countdown()
    {
        int countdown = 10;
        defendfailed = false;
        while (countdown > 0 && !defendfailed)
        {
            if (!isPaused)
            {
                CountdownUI.text = countdown.ToString();
                yield return new WaitForSeconds(1f); // µ¥«Ý1¬í
                countdown--;
            }
            else
            {
                yield return null;
            }
        }
        if (countdown == 0)
        {
            defendfailed = true;
            AttackResult();
            StopCoroutine(countdownCoroutine);
        }
        if (defendfailed == true) 
        {
            StopCoroutine(countdownCoroutine);
            AttackResult();
            InvokeRepeating("SecondCheck", 1, 1);
        }
    }

    private void OnPasswordEnter(string input) 
    {
        string password = AttackType.text.ToString();
        if (input == password) // °²³]"password"¬O¥¿½Tªº±K½X
        {
            Debug.Log("±K½X¥¿½T¡A²×¤î­Ë­p®É¡I");
            StopCoroutine(countdownCoroutine);
            defendfailed = false;
            InvokeRepeating("SecondCheck", 1, 1);
        }
        else
        {
            Debug.Log("±K½X¿ù»~");
        }
    }

    public void AttackResult() 
    {
        string ATC = AttackType.text.ToString();
        switch (ATC) 
        {
            case "Attack1":
                Player_HP_num = Player_HP_num - 10;
                break;
            case "Attack2":
                Player_HP_num = Player_HP_num - 20;
                break;
            case "HeavyAttack":
                Player_HP_num = Player_HP_num - 50;
                break;
        }
    }


}

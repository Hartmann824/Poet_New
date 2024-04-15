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
    [SerializeField] GameObject Win;
    [SerializeField] GameObject Lose;
    public int Enemy_NP_num = 0;
    public int Enemy_HP_num = 100;
    public int Player_NP_num = 0;
    public int Player_HP_num = 100;
    public int CheckP = 0;
    public int CountDownTime = 10;
    private IEnumerator countdownCoroutine;
    public bool defendfailed = false;
    public bool isPaused = false;
    public int countdown = 10;
    // Start is called before the first frame update
    void Start()
    {
        SyncUI();
        Win.SetActive(false);
        Lose.SetActive(false);
        countdownCoroutine = Countdown();
        passwordInputField.onEndEdit.AddListener(OnPasswordEnter);
        StartGame();

    }

    // Update is called once per frame
    void Update()
    {
        if (Player_HP_num == 0)
        {
            Debug.Log("You lose");
            CancelInvoke("SecondCheck");
            Win.SetActive(true);
        }
        if (Enemy_HP_num == 0)
        {
            Debug.Log("You Win");
            CancelInvoke("SecondCheck");
            Lose.SetActive(true);
        }
    }

    public void StartGame()
    {
        InvokeRepeating("SecondCheck", 1, 1);
    }

    public void SecondCheck() 
    {
        if (Player_NP_num <= 100)
        { 
            Player_NP_num = Player_NP_num +5;
        }
        if(Enemy_NP_num <= 100)
        {
            Enemy_NP_num = Enemy_NP_num + 2;
            Debug.Log("Enemy NP Added");
        }

        SyncUI();
        if (Enemy_NP_num >= 10)
        {
            LightAttackCheck();
        }
        else if (Enemy_NP_num == 100)
        {
            HeavyAttackCheck();
        }
        Debug.Log("Checked");
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
                Enemy_NP_num = Enemy_NP_num - 10;

                countdown = 10;
                SyncUI();
                StartCoroutine(countdownCoroutine);
            }
           

        }
    }
    public void HeavyAttackCheck() 
    {
        Enemy_NP_num = Enemy_NP_num - 100;
        AttackType.text = "HeavyAttack";

        countdown = 10;
        SyncUI();
        StartCoroutine(countdownCoroutine);

    }

    private IEnumerator Countdown()
    {
        defendfailed = false;
        CancelInvoke("SecondCheck");
        while (countdown >= 0 && defendfailed == false)
        {
            if (!isPaused)
            {
                CountdownUI.text = countdown.ToString();
                countdown--;
                Debug.Log(countdown);
                yield return new WaitForSeconds(1f); // µ¥«Ý1¬í
                
            }
            else
            {
                yield return null;
            }
        }
        if (countdown <= 0)
        {
            AttackResult();
            StartGame();
            StopCoroutine(countdownCoroutine);
        }
        if (defendfailed == true) 
        {
            AttackResult();
            StartGame();
            StopCoroutine(countdownCoroutine);
        }
    }

    private void OnPasswordEnter(string input) 
    {
        string password = AttackType.text.ToString();
        if (input == password) // °²³]"password"¬O¥¿½Tªº±K½X
        {
            passwordInputField.text = "";
            Debug.Log("±K½X¥¿½T¡A²×¤î­Ë­p®É¡I");
            defendfailed = false;
            StartGame();
            StopCoroutine(countdownCoroutine);
        }
        else
        {
            passwordInputField.text = "";
            PlayerAttack();
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
        SyncUI();
        StartGame();
        
    }

    public void SyncUI()
    {
        Enemy_NP.text = Enemy_NP_num.ToString();
        Enemy_HP.text = Enemy_HP_num.ToString();
        Player_NP.text = Player_NP_num.ToString();
        Player_HP.text = Player_HP_num.ToString();
    }

    public void PlayerAttack() 
    {
        string[] attack_list = {"Player Attack 1", "Player Attack 2"  , "Player Attack 3" };
        string ATC = AttackType.text.ToString();
        if (ATC == "Player Attack 1" || ATC == "Player Attack 2")
        {
            if (Player_NP_num >= 10)
            {
                Player_NP_num = Player_NP_num - 10;
                switch (ATC)
                {
                    case "Player Attack 1":
                        Player_HP_num = Player_HP_num - 10;
                        break;
                    case "Player Attack 2":
                        Player_HP_num = Player_HP_num - 20;
                        break;
                }
            }
        }

        if (ATC == "Player Attack 3" && Player_NP_num == 100)
        {
            Player_NP_num = Player_NP_num - 100;
            Player_HP_num = Player_HP_num - 50;
        }
        else
        {
            Player_NP_num = Player_NP_num - 10;
        }
        SyncUI();
        StartGame();
    }
}

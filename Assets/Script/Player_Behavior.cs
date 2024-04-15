using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Behavior : MonoBehaviour
{
    public int Player_HP = 50;
    public int Player_NP = 0;
    [SerializeField] Text Player_HP_UI;
    [SerializeField] Text Player_NP_UI;
    [SerializeField] InputField inputField;

    private Dictionary<string, float> cooldowns =new Dictionary<string, float>();
    // Start is called before the first frame update
    void Start()
    {
        SyncUI();
        inputField.onEndEdit.AddListener(OnSpellEnter);
        //skills cooldown time
        cooldowns.Add("regenerate", 5f);
        cooldowns.Add("recharge", 10f);
        cooldowns.Add("LightAttack",0f);
        cooldowns.Add("HeavyAttack", 10f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void action(string input) 
    {
        //check if its in the dictionary
        if (cooldowns.ContainsKey(input))
        {
            //check if its on cooling time
            if (!IsCoolDown(input))
            {
                switch (input)
                {
                    case "regenerate":
                        Player_HP = Player_HP + 10;
                        break;
                    case "recharge":
                        Player_NP = Player_NP + 10;
                        break;
                    case "LightAttack":
                        Debug.Log("LightAttack");
                        break;
                    case "HeavyAttack":
                        Debug.Log("HeavyAttack");
                        break;
                }
                inputField.text = "";
                StartCoroutine(CooldownTimer(input, cooldowns[input]));
            }

            //if its on cooling time 
            else
            {
                Debug.Log("技能正在冷卻中...");
                inputField.text = "";
            }
        }
        else
        {
            Debug.Log("沒有這招欸");
            inputField.text = "";
        }
        
        CheckNum();
    }

    public void OnSpellEnter(string input)
    {
        string spell = input;
        action(spell);
    }

    public void SyncUI() 
    {
        Player_HP_UI.text = Player_HP.ToString();
        Player_NP_UI.text = Player_NP.ToString();
    }

    public void CheckNum()
    {
        Player_NP = Mathf.Clamp(Player_NP,0,100 );
        Player_HP = Mathf.Clamp(Player_HP, 0, 100);
        SyncUI();
    }

    private IEnumerator CooldownTimer(string skillName, float cooldown)
    {
        cooldowns[skillName] = Time.time + cooldown;
        while (Time.time < cooldowns[skillName])
        {
            yield return null;
        }
    }

    private bool IsCoolDown(string SkillName) 
    {
        return Time.time < cooldowns[SkillName];
    }


}

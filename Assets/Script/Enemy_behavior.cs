using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_behavior : MonoBehaviour
{
    [SerializeField] Text Enemy_HP_UI;
    [SerializeField] Text Enemy_NP_UI;
    [SerializeField] Text EnemyAction;
    [SerializeField] InputField inputField;
    public int Enemy_HP = 100;
    public int Enemy_NP = 0;
    public bool isSecletable = false;
    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();
    private Dictionary<string, float> defendtime = new Dictionary<string, float>();
    private List<string> availableSkills = new List<string>(); 
    // Start is called before the first frame update
    void Start()
    {
        SyncUI();
        cooldowns.Add("regenerate", 5f);
        cooldowns.Add("recharge", 10f);
        cooldowns.Add("LightAttack", 0f);
        cooldowns.Add("HeavyAttack", 10f);
        defendtime.Add("LightAttack", 5f);
        defendtime.Add("HeavyAttack", 10f);
        foreach (string skillName in cooldowns.Keys)
        {
            availableSkills.Add(skillName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSecletable) 
        {
            StartCoroutine(Selection());
        }
    }

    public void action(string SelectedAction) 
    {

        if (defendtime.ContainsKey(SelectedAction))
        {
            EnemyAction.text = SelectedAction;
        }
        else 
        {
            Debug.Log("Enemy Action¡G" + SelectedAction);
        }
        

    }

    private IEnumerator Selection() 
    {
        isSecletable = true;
        yield return new WaitForSeconds(5f);

        System.Random rand = new System.Random();
        string selection = availableSkills[rand.Next(availableSkills.Count)];
        action(selection);
        isSecletable = false;
        
    }

    private IEnumerator CoolDownTimer(string skillName, float cooldown) 
    {
        availableSkills.Remove(skillName);
        cooldowns[skillName] = Time.time + cooldown;
        while (Time.time < cooldowns[skillName])
        {
            yield return null;
        }
        availableSkills.Add(skillName);
    }

    public void SyncUI()
    {
        Enemy_HP_UI.text = Enemy_HP.ToString();
        Enemy_NP_UI.text = Enemy_NP.ToString();
    }

    private IEnumerator Defending(string skillName, float DefendingTime) 
    {
        isSecletable = false;
        defendtime[skillName] = Time.time + DefendingTime;
        while (Time.time < defendtime[skillName])
        {
            yield return null;
        }
        isSecletable = true;
    }

    
}

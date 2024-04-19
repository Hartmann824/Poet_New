using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_behavior : MonoBehaviour
{
    [SerializeField] Text Enemy_HP_UI;
    [SerializeField] Text Enemy_NP_UI;
    [SerializeField] Text EnemyAction;
    [SerializeField] InputField inputField;
    [SerializeField] Text CountDownUI;
    [SerializeField] Text tof;
    [SerializeField] GameObject Manager;
    //[SerializeField] GameObject TestButtom;
    public int Enemy_HP = 100;
    public int Enemy_NP = 0;
    public bool isSelectable = true;

    private Dictionary<string, float> cooldowns = new Dictionary<string, float>();
    private Dictionary<string, float> defendtime = new Dictionary<string, float>();
    private List<string> availableSkills = new List<string>();
    private Coroutine defendingCoroutine;
    private MainControl MainControl;
    // Start is called before the first frame update
    void Start()
    {
        SyncUI();
        MainControl = Manager.GetComponent<MainControl>();
        cooldowns.Add("regenerate", 5f);
        cooldowns.Add("recharge", 10f);
        cooldowns.Add("LightAttack", 0f);
        cooldowns.Add("HeavyAttack", 10f);
        defendtime.Add("LightAttack", 5f);
        defendtime.Add("HeavyAttack", 10f);
        isSelectable = true;
        foreach (string skillName in cooldowns.Keys)
        {
            availableSkills.Add(skillName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelectable) 
        {
            StartCoroutine(Selection());
        }
        tof.text = isSelectable.ToString();
    }

    public void action(string SelectedAction) 
    {

        if (defendtime.ContainsKey(SelectedAction))
        {
            EnemyAction.text = SelectedAction;
            defendingCoroutine = StartCoroutine(Defending(SelectedAction, defendtime[SelectedAction]));
            StartCoroutine(CoolDownTimer(SelectedAction, cooldowns[SelectedAction]));
        }
        else 
        {
            Debug.Log("Enemy Action：" + SelectedAction);
            isSelectable = true;
            StartCoroutine(CoolDownTimer(SelectedAction, cooldowns[SelectedAction]));
        }
        

    }

    private IEnumerator Selection() 
    {
        Debug.Log("start selection");
        isSelectable = false;
        yield return new WaitForSeconds(5f);

        System.Random rand = new System.Random();
        if (availableSkills.Any()) 
        {
            string selection = availableSkills[rand.Next(availableSkills.Count)];
            //Debug.Log(availableSkills);
            MainControl.CheckAttack(selection);
            action(selection);
        }
        //isSelectable = true;
        
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
        isSelectable = false;
        float remainingTime = DefendingTime;
        while (remainingTime > 0)
        {
            CountDownUI.text = remainingTime.ToString("F0"); // 顯示剩餘秒數
            remainingTime -= Time.deltaTime;
            yield return null;
        }
        CountDownUI.text = ""; // 清空倒數UI
        isSelectable = true;
    }
    public void StopDefending()
    {
        if (defendingCoroutine != null)
        {
            CountDownUI.text = ""; 
            StopCoroutine(defendingCoroutine);
            isSelectable = true;
        }
    }

    public int getnumber(string request)
    {
        switch (request)
        {
            case "HP":
                return Enemy_HP;
            case "NP":
                return Enemy_NP;
            default:
                return 0;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatDisplayUI : MonoBehaviour
{
    [SerializeField]
    private GameObject healthText;
    [SerializeField]
    private GameObject strText;
    [SerializeField]
    private GameObject agiText;
    [SerializeField]
    private GameObject conText;
    [SerializeField]
    private GameObject intText;
    [SerializeField]
    private GameObject armorText;
    [SerializeField]
    private GameObject magicArmorText;

    [SerializeField]
    private GameObject statUpBtns;

    private CharacterData unit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (unit != null)
        {
            healthText.GetComponent<TextMeshProUGUI>().text = "HP: " + unit.current_health + "/" + unit.attributes.GetAttribute(Attributes.MaxHealth).GetValue();
            strText.GetComponent<TextMeshProUGUI>().text = "Str: " + unit.attributes.GetAttribute(Attributes.Strength).GetValue();
            agiText.GetComponent<TextMeshProUGUI>().text = "Agi: " + unit.attributes.GetAttribute(Attributes.Agility).GetValue();
            conText.GetComponent<TextMeshProUGUI>().text = "Con: " + unit.attributes.GetAttribute(Attributes.Constitution).GetValue();
            intText.GetComponent<TextMeshProUGUI>().text = "Int: " + unit.attributes.GetAttribute(Attributes.Intelligence).GetValue();
            armorText.GetComponent<TextMeshProUGUI>().text = "Armor: " + unit.attributes.GetAttribute(Attributes.Armor).GetValue();
            magicArmorText.GetComponent<TextMeshProUGUI>().text = "Magic Armor: " + unit.attributes.GetAttribute(Attributes.MagicArmor).GetValue();

            statUpBtns.SetActive(unit.IsUpgradeAvaliable());
        }
    }

    public void Setup(CharacterData unit)
    {
        this.unit = unit;
    }

    public void AddStr() { unit.UpgradeAttribute(Attributes.Strength); }
    public void AddAgi() { unit.UpgradeAttribute(Attributes.Agility); }
    public void AddCon() { unit.UpgradeAttribute(Attributes.Constitution); }
    public void AddInt() { unit.UpgradeAttribute(Attributes.Intelligence); }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject shopUI;
    [SerializeField] Slider healthBar;
    [SerializeField] TMP_Text healthText;
    [SerializeField] Slider shieldBar;
    [SerializeField] TMP_Text shieldText;
    [SerializeField] Slider powerBar;
    [SerializeField] TMP_Text powerText;
    [SerializeField] TMP_Text moneyText;
    //info panel
    [SerializeField] GameObject infoPanel;
    [SerializeField] TMP_Text partName;
    [SerializeField] TMP_Text partDesc;
    [SerializeField] TMP_Text sellPrice;
    [SerializeField] TMP_Text partPrice;
    [SerializeField] TMP_Text partCost;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShipStats stats = ShipController.stats;
        
        healthBar.value = (float)stats.health/stats.maxHealth;
        healthText.text = stats.health.ToString()+"/"+stats.maxHealth.ToString();
        if(stats.maxShield != 0)shieldBar.value = (float)stats.shield/stats.maxShield;
        else shieldBar.value = 0;
        shieldText.text = stats.shield.ToString()+"/"+stats.maxShield.ToString();
        powerBar.value = (float)stats.power/stats.maxPower;
        powerText.text = stats.power.ToString()+"/"+stats.maxPower.ToString();
        moneyText.text = stats.money.ToString();
    }

    public void SetInfoPanel(string pn, string pd, int pp, float pc, float partCooldown)
    {
        partName.text = pn;
        partDesc.text = pd;
        sellPrice.text = "Sell: " + (pp/2).ToString();
        partPrice.text = "Price: " + pp.ToString();
        float cost = pc / partCooldown;
        partCost.text = "Power: " + string.Format("{0:F1}",cost)+"/s";

        infoPanel.SetActive(true);
    }
    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
    }
    public void ShopOn()
    {
        shopUI.SetActive(true);
    }
    public void ShopOff()
    {
        shopUI.SetActive(false);
        CloseInfoPanel();
    }
}


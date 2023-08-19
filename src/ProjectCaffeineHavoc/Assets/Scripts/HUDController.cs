using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{

    public GameObject player;
    private CharacterInteractionScript playerInteractionScript;
    private GameObject heldItem = null;
    private GameObject coffeeNameText;
    private GameObject IngradientListText;

    void Start()
    {

        // Player, HeldItem ve HUD referanslarını al
        player = GameObject.Find("Player");
        playerInteractionScript = player.GetComponent<CharacterInteractionScript>();
        heldItem = playerInteractionScript.heldItem;
        coffeeNameText = GameObject.Find("CoffeeName");
        IngradientListText = GameObject.Find("IngradientList");
        
        
    }

    void Update()
    {
        // player elinde bir şey tutuyorsa
        heldItem = playerInteractionScript.heldItem;
        if (heldItem != null && heldItem.CompareTag("CoffeeHolder"))
        {
            updateHeldItem();
        } 
        else
        {
            // Eğer elinde bir şey tutmuyorsa metinleri temizle
            coffeeNameText.GetComponent<TMPro.TextMeshProUGUI>().text = "";
            IngradientListText.GetComponent<TMPro.TextMeshProUGUI>().text = "";
        }

    }

 
    public void updateHeldItem()
    {
        // Tutulan öğenin CupGlassContainerScript'ini al
        CupGlassContainerScript heldItemScript = heldItem.GetComponent<CupGlassContainerScript>();
        
        // Kahve adı metinini güncelle
        coffeeNameText.GetComponent<TMPro.TextMeshProUGUI>().text = heldItemScript.coffeeName;

        // İçerik listesi metnini güncelle
        string ingradientList = "";
        foreach (string ingradient in heldItemScript.state)
        {
            ingradientList += "-" + ingradient + "\n";
        }

        IngradientListText.GetComponent<TMPro.TextMeshProUGUI>().text = ingradientList;

    }

    public void changeOrderText(string orderCoffeeName)
    {
        // Order yazısını güncelle
        GameObject orderText = GameObject.Find("CurrentOrder");
        orderText.GetComponent<TMPro.TextMeshProUGUI>().text = "Current Order:\n" + orderCoffeeName;
    }

}

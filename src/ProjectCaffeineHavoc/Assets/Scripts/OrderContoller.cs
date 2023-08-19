using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class OrderContoller : MonoBehaviour
{

    // List of coffees are 
    string[] coffeeList = {
    "Vanilla Latte",
    "Vegan Vanilla Latte",
    "Iced Vanilla Latte",
    "Vegan Iced Vanilla Latte",
    "Caramel Macchiato",
    "Vegan Caramel Macchiato",
    "Iced Caramel Macchiato",
    "Vegan Iced Caramel Macchiato",
    "Iced Caramel Vanilla Latte",
    "Vegan Iced Caramel Vanilla Latte",
    "Caramel Iced Coffee",
    "Vegan Caramel Iced Coffee",
    "Espresso",
    "Cold Brew",
    "Drip Coffee"
    };


    public string coffeeName;
    public string orderCoffeeName;
    public GameObject HUD;
    public HUDController HUDControllerScript;
    public CharacterInteractionScript playerInteractionScript;


    void Start()
    {
        // Player Ve Hud referanslarını al
        GameObject player = GameObject.Find("Player");
        HUD = GameObject.Find("Canvas");
        HUDControllerScript = HUD.GetComponent<HUDController>();
        playerInteractionScript = player.GetComponent<CharacterInteractionScript>();
    
        createOrder();
    }

    void Update()
    {


        // player elinde bir şey tutuyorsa
        GameObject heldItem = playerInteractionScript.heldItem;
        if (heldItem != null && heldItem.CompareTag("CoffeeHolder"))
        {
            // Tutulan öğenin CupGlassContainerScript'ini al
            CupGlassContainerScript heldItemScript = heldItem.GetComponent<CupGlassContainerScript>();
            // Kahve adını güncelle
            coffeeName = heldItemScript.coffeeName;
            checkOrder();
        }
    }

    void createOrder()
    {
        // Order Oluşturma

        // Rastgele bir kahve seç
        int randomIndex = Random.Range(0, coffeeList.Length);
        orderCoffeeName = coffeeList[randomIndex];

        // HUD üzerindeki sipariş metnini güncelle
        HUDControllerScript.changeOrderText(orderCoffeeName);
    }

    void checkOrder()
    {
        // Elimdeki kahvenin orderdaki kahveyle aynı olup olmadığını kontrol et
        if (coffeeName == orderCoffeeName)
        {
            Debug.Log("Order Completed");
            // Order tamamlandıysa yeni bir sipariş oluştur
            createOrder();
        }
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
    public GameObject player;
    public CharacterInteractionScript playerScript;
    public CupGlassContainerScript heldItemScript;
    public string syrupType;

    // List of machines to be from
    public enum machines {  CoffeeGrinder,                            
                            Kettle,
                            EspressoMachine,
                            SyrupDispenser,
                            FrenchPress,
                            DripCoffeeMaker,
                            MilkFrother,
                            IceCloset,
                            Trash,
                            Container};
    public machines machineType; // what type of machine is this object

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    private void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<CharacterInteractionScript>();
    }

    private void Update()
    {
        playerScript = player.GetComponent<CharacterInteractionScript>();
        if (playerScript.heldItem != null && playerScript.heldItem.CompareTag("CoffeeHolder"))
        {
            heldItemScript = playerScript.heldItem.GetComponent<CupGlassContainerScript>();
        }
    }

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    public void useMachine() // machines that are used with empty hand
    {

    }

    public void fillGlass() // machines that are used with glass/cup/container in hand
    {
        if (machineType == machines.Container)
        {
            heldItemScript.state.Add("ColdBrew");
        }
        else if (machineType == machines.Kettle)
        {
            //Debug.Log("add hot water to player.heldItem"); // heldItemScript.addIngradient(Hot water ingradient)
            heldItemScript.state.Add("HotWater");
        }
        else if (machineType == machines.IceCloset)
        {
            heldItemScript.state.Add("Ice");
        }
        if (machineType == machines.CoffeeGrinder)
        {
            heldItemScript.state.Add("CoffeeGround");
        }
        else if (machineType == machines.EspressoMachine)
        {
            //Debug.Log("add espresso to player.heldItem"); // heldItemScript.addIngradient(espresso ingradient)
            heldItemScript.state.Add("Espresso");
        }
        else if (machineType == machines.SyrupDispenser)
        {
            //Debug.Log("add syrup player.heldItem"); // heldItemScript.addIngradient(syrup ingradient)
            heldItemScript.state.Add(syrupType);
        }
        else if (machineType == machines.FrenchPress)
        {
            //Debug.Log("add frenchpress player.heldItem"); // heldItemScript.addIngradient(frencpress ingradient)
            heldItemScript.state.Add("Frenchpress");
        }
        else if (machineType == machines.DripCoffeeMaker)
        {
            //Debug.Log("add drip coffee player.heldItem"); // heldItemScript.addIngradient(drip coffee ingradient)
            heldItemScript.state.Add("DripCoffee");
        }
        else if (machineType == machines.Trash)
        {
            //Debug.Log("delete player.heldItem"); // heldItemScript.addIngradient(drip coffee ingradient)

            // playerScript.heldItem.transform.SetParent(null);
            // Destroy(playerScript.heldItem);
            // playerScript.heldItem = null;

            heldItemScript.state = new HashSet<string>();
        }

        else if (machineType == machines.MilkFrother) // froth the milk only if character is holding a coffeeHolder with only milk or veganmilk in it.
        {
            //Debug.Log("froth the milk if player.heldItem.state equals to {Milk} or {VeganMilk}, make character.heldItem = same coffeeholder with same milks frothed version");
            if (heldItemScript.state.SetEquals(new HashSet<string>(new[] { "Milk" })))
            {
                heldItemScript.state = new HashSet<string>(new[] { "FrothedMilk" });
            }
            else if (heldItemScript.state.SetEquals(new HashSet<string>(new[] { "VeganMilk" })))
            {
                heldItemScript.state = new HashSet<string>(new[] { "FrothedMilk" });
            }
        }


        heldItemScript.transformToCoffee();
    }
}

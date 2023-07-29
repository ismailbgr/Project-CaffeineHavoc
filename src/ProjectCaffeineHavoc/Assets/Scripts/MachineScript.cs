using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
    public GameObject player;
    public CharacterInteractionScript playerScript;
    public CupGlassContainerScript heldItemScript;

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
                            Blender};// blender is not active for now.
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
        if (machineType == machines.CoffeeGrinder)
        {
            Debug.Log("put coffee grounds to player.heldItem"); // how to set prefab to players hand(collider/rigidbody/tag vs de eklenmesi laz�m burada)
        }
        else if (machineType == machines.IceCloset)
        {
            Debug.Log("put ice to player.heldItem");
        }
    }

    public void fillGlass() // machines that are used with glass/cup/container in hand
    {
        if (machineType == machines.Kettle)
        {
            //Debug.Log("add hot water to player.heldItem"); // heldItemScript.addIngradient(Hot water ingradient)
            heldItemScript.state.Add("Ice");
        }
        else if (machineType == machines.EspressoMachine)
        {
            //Debug.Log("add espresso to player.heldItem"); // heldItemScript.addIngradient(espresso ingradient)
            heldItemScript.state.Add("Espresso");
        }
        else if (machineType == machines.SyrupDispenser)
        {
            //Debug.Log("add syrup player.heldItem"); // heldItemScript.addIngradient(syrup ingradient)
            heldItemScript.state.Add("Syrup");
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

            playerScript.heldItem.transform.SetParent(null);
            Destroy(playerScript.heldItem);
            playerScript.heldItem = null;
        }

        else if (machineType == machines.MilkFrother) // froth the milk only if character is holding a coffeeHolder with only milk or veganmilk in it.
        {
            //Debug.Log("froth the milk if player.heldItem.state equals to {Milk} or {VeganMilk}, make character.heldItem = same coffeeholder with same milks frothed version");
            if (heldItemScript.state.SetEquals(new HashSet<string>(new[] { "Milk" })))
            {
                heldItemScript.state.Add("FrothedMilk");
            }
            else if (heldItemScript.state.SetEquals(new HashSet<string>(new[] { "VeganMilk" })))
            {
                heldItemScript.state.Add("FrothedVeganMilk");
            }
        }

        /*if machineType == machines.Blender BLENDER IS DISABLED FOR NOW*/

        heldItemScript.transformToCoffee();
    }
}

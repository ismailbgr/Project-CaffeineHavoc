using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
    public GameObject player;
    public CharacterInteractionScript playerScript;
    public CupGlassContainerScript heldItemScript;
    public string syrupType;
    public AudioSource audioSource;

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
    public machines machineType;


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


    public void useMachine() // machines that are used with empty hand
    {
        //failsafe ve işimizi ileride kolaylaştırma amaçlı burada bırakıldı. eğer ileride böyle bir makine eklenirse burası dolacak.
    }

    public void fillGlass() // machines that are used with glass/cup/container in hand
    {
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        if (machineType == machines.Container)
        {
            heldItemScript.state.Add("ColdBrew");
        }
        else if (machineType == machines.Kettle)
        {
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
            heldItemScript.state.Add("Espresso");
        }
        else if (machineType == machines.SyrupDispenser)
        {
            heldItemScript.state.Add(syrupType);
        }
        else if (machineType == machines.FrenchPress)
        {
            heldItemScript.state.Add("Frenchpress");
        }
        else if (machineType == machines.DripCoffeeMaker)
        {
            heldItemScript.state.Add("DripCoffee");
        }
        else if (machineType == machines.Trash)
        {

            heldItemScript.state = new HashSet<string>();
        }

        else if (machineType == machines.MilkFrother) // froth the milk only if character is holding a coffeeHolder with only milk or veganmilk in it.
        {
            if (heldItemScript.state.SetEquals(new HashSet<string>(new[] { "Milk" })))
            {
                heldItemScript.state = new HashSet<string>(new[] { "FrothedMilk" });
            }
            else if (heldItemScript.state.SetEquals(new HashSet<string>(new[] { "VeganMilk" })))
            {
                heldItemScript.state = new HashSet<string>(new[] { "VeganFrothedMilk" });
            }
        }

        if (audioSource != null && audioSource.clip != null)
        {
            // Play the audio clip
            audioSource.Play();
        }

        heldItemScript.transformToCoffee();
    }
}

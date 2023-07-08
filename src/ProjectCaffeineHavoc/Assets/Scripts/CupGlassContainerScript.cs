using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupGlassContainerScript : MonoBehaviour
{
    // List of coffeeHolders to be from
    public enum coffeeHolders { Cup, Glass, Container };
    public coffeeHolders coffeeHolderType; // what type of coffeeHolders is this object

    public HashSet<string> state = new HashSet<string>(); // what does this coffeeholder has as ingradient in it.

    private void Start()
    {
    }

    // ######################## ######################## ######################## ######################## ######################## //

    public void Update()
    {
        transformToCoffee();
    }
    
    // ######################## ######################## ######################## ######################## ######################## //

    public void addIngradient(GameObject ingradient)
    { // character is putting ingradients content inside this coffeeHolder object(like adding hot water in to glass with coffee in it)

        // state of ingradient object(we said it stores its contents)
        IngradientScript ingradientScript = ingradient.GetComponent<IngradientScript>();
        // add content of ingradient object into this coffeeHolder objects state
        foreach (var contents in ingradientScript.state)
        {
            state.Add(contents);
        }

    }

    public void pourInto(GameObject lookedCoffeeHolder)
    { // character is merging 2 coffeeHolder objects ingradients(like pouring hot water from a glass water to glass with coffee in it to create glass with hot water and coffee)
     
        // state of second coffeeHolder object(it stores its ingradients)
        CupGlassContainerScript lookedCoffeeHolderScript = lookedCoffeeHolder.GetComponent<CupGlassContainerScript>();
        // add ingradients of held coffeeHolder into lookedCoffeeHolder objects state
        foreach (var ingradient in state)
        {
            lookedCoffeeHolderScript.state.Add(ingradient);
        }

        // empty the held coffeeHolder.
        state = new HashSet<string>();
    }

    // ######################## ######################## ######################## ######################## ######################## //

    // Bir bardaktaki karýþýmýn hangi kahveye dönüþtüðünü bulabilmek için string coffeeName; diye bir þey tutulabilir belki.
    public void transformToCoffee()
    { // always checking if any of coffeeHolders created coffe with ingradients it has inside it

        if (coffeeHolderType == coffeeHolders.Cup || coffeeHolderType == coffeeHolders.Glass)
        {
            if (state.SetEquals(new HashSet<string>(new[] { "Espresso", "FrothedMilk", "VanillaSyrup" })))
                Debug.Log("Created Vanilla Latte.");
            if (state.SetEquals(new HashSet<string>(new[] { "Espresso", "FrothedMilk", "VanillaSyrup", "Ice" })))
                Debug.Log("Created Iced Vanilla Latte.");
            if (state.SetEquals(new HashSet<string>(new[] { "CaramelSyrup", "Espresso", "FrothedMilk" })))
                Debug.Log("Created Caramel Macchiato.");
            if (state.SetEquals(new HashSet<string>(new[] { "CaramelSyrup", "Espresso", "FrothedMilk", "Ice" })))
                Debug.Log("Created Iced Caramel Macchiato.");
            if (state.SetEquals(new HashSet<string>(new[] { "VanillaSyrup", "CaramelSyrup", "Espresso", "FrothedMilk", "Ice" })))
                Debug.Log("Created Iced Caramel Vanilla Latte.");
            if (state.SetEquals(new HashSet<string>(new[] { "DripBrewedCoffee", "Milk", "CaramelSyrup", "Ice" })))
                Debug.Log("Created Caramel Iced Coffee.");
        }
        else if (coffeeHolderType == coffeeHolders.Container)
        {
            if (state.SetEquals(new HashSet<string>(new[] { "CoffeeGround", "ColdWater" })))
                Debug.Log("Created Brew Concentrate Container.");
            if (state.SetEquals(new HashSet<string>(new[] { "CoffeeGround", "ColdWater", "Filter" })))
                Debug.Log("Created Cold Brew.");
        }
    }

    
}

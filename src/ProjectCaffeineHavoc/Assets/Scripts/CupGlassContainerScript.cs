using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupGlassContainerScript : MonoBehaviour
{
    // List of coffeeHolders to be from
    public enum coffeeHolders { Cup, Glass, Container };
    public coffeeHolders coffeeHolderType; // what type of coffeeHolders is this object
    string coffeeName = "";

    public HashSet<string> state = new HashSet<string>(); // what does this coffeeholder has as ingradient in it.

    private void Start()
    {
    }

    // ######################## ######################## ######################## ######################## ######################## //

    public void Update()
    {
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
        lookedCoffeeHolderScript.transformToCoffee(); // checking if coffeholder is a coffee

        // empty the held coffeeHolder.
        state = new HashSet<string>();
    }

    // ######################## ######################## ######################## ######################## ######################## //

    // Bir bardaktaki kar���m�n hangi kahveye d�n��t���n� bulabilmek i�in string coffeeName; diye bir �ey tutulabilir belki.
    public void transformToCoffee()
    { // always checking if any of coffeeHolders created coffe with ingradients it has inside it

        if (coffeeHolderType == coffeeHolders.Cup || coffeeHolderType == coffeeHolders.Glass)
        {
            if (state.SetEquals(new HashSet<string>(new[] { "Espresso", "FrothedMilk", "VanillaSyrup" })))
                coffeeName = "Vanilla Latte";
            else if (state.SetEquals(new HashSet<string>(new[] { "Espresso", "FrothedMilk", "VanillaSyrup", "Ice" })))
                coffeeName = "Iced Vanilla Latte";
            else if (state.SetEquals(new HashSet<string>(new[] { "CaramelSyrup", "Espresso", "FrothedMilk" })))
                coffeeName = "Caramel Macchiato";
            else if (state.SetEquals(new HashSet<string>(new[] { "CaramelSyrup", "Espresso", "FrothedMilk", "Ice" })))
                coffeeName = "Iced Caramel Macchiato";
            else if (state.SetEquals(new HashSet<string>(new[] { "VanillaSyrup", "CaramelSyrup", "Espresso", "FrothedMilk", "Ice" })))
                coffeeName = "Iced Caramel Vanilla Latte";
            else if (state.SetEquals(new HashSet<string>(new[] { "DripBrewedCoffee", "Milk", "CaramelSyrup", "Ice" })))
                coffeeName = "Caramel Iced Coffee";
            //else if (state.SetEquals(new HashSet<string>(new[] { "Espresso", "FrothedMilk", "ChocolateSyrup", "Ice" })))
            //    coffeeName = "Mocha Frappuccino"; BLENDER NEEDED SO THIS IS DISABLED FOR NOW
            else
                coffeeName = "";
        }
        else if (coffeeHolderType == coffeeHolders.Container)
        {
            if (state.SetEquals(new HashSet<string>(new[] { "CoffeeGround", "ColdWater" })))
                coffeeName = "Brew Concentrate Container";
            else if (state.SetEquals(new HashSet<string>(new[] { "CoffeeGround", "ColdWater", "Filter" })))
                coffeeName = "Cold Brew";
            else
                coffeeName = "";
        }
    }

    
}

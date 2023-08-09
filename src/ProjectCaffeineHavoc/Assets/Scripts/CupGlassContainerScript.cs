using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupGlassContainerScript : MonoBehaviour
{
    // List of coffeeHolders to be from
    public enum coffeeHolders { Cup, Glass};
    public coffeeHolders coffeeHolderType; // what type of coffeeHolders is this object
    public string coffeeName = "";


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
        lookedCoffeeHolderScript.transformToCoffee(); // checking if coffeholder is a coffee

        // empty the held coffeeHolder.
        state = new HashSet<string>();
        coffeeName = "";
    }

    // ######################## ######################## ######################## ######################## ######################## //

    // Bir bardaktaki kar���m�n hangi kahveye d�n��t���n� bulabilmek i�in string coffeeName; diye bir �ey tutulabilir belki.
    public void transformToCoffee()
    { // always checking if any of coffeeHolders created coffe with ingradients it has inside it
        if (state.SetEquals(new HashSet<string>(new[] { "Espresso", "FrothedMilk", "VanillaSyrup" })))
            coffeeName = "Vanilla Latte";
        else if (state.SetEquals(new HashSet<string>(new[] { "Espresso", "VeganFrothedMilk", "VanillaSyrup" })))
            coffeeName = "Vegan Vanilla Latte";
        else if (state.SetEquals(new HashSet<string>(new[] { "Espresso", "FrothedMilk", "VanillaSyrup", "Ice" })))
            coffeeName = "Iced Vanilla Latte";
        else if (state.SetEquals(new HashSet<string>(new[] { "Espresso", "VeganFrothedMilk", "VanillaSyrup", "Ice" })))
            coffeeName = "Vegan Iced Vanilla Latte";
        else if (state.SetEquals(new HashSet<string>(new[] { "CaramelSyrup", "Espresso", "FrothedMilk" })))
            coffeeName = "Caramel Macchiato";
        else if (state.SetEquals(new HashSet<string>(new[] { "CaramelSyrup", "Espresso", "VeganFrothedMilk" })))
            coffeeName = "Vegan Caramel Macchiato";
        else if (state.SetEquals(new HashSet<string>(new[] { "CaramelSyrup", "Espresso", "FrothedMilk", "Ice" })))
            coffeeName = "Iced Caramel Macchiato";
        else if (state.SetEquals(new HashSet<string>(new[] { "CaramelSyrup", "Espresso", "VeganFrothedMilk", "Ice" })))
            coffeeName = "Vegan Iced Caramel Macchiato";
        else if (state.SetEquals(new HashSet<string>(new[] { "VanillaSyrup", "CaramelSyrup", "Espresso", "FrothedMilk", "Ice" })))
            coffeeName = "Iced Caramel Vanilla Latte";
        else if (state.SetEquals(new HashSet<string>(new[] { "VanillaSyrup", "CaramelSyrup", "Espresso", "VeganFrothedMilk", "Ice" })))
            coffeeName = "Vegan Iced Caramel Vanilla Latte";
        else if (state.SetEquals(new HashSet<string>(new[] { "DripCoffee", "Milk", "CaramelSyrup", "Ice" })))
            coffeeName = "Caramel Iced Coffee";
        else if (state.SetEquals(new HashSet<string>(new[] { "DripCoffee", "VeganMilk", "CaramelSyrup", "Ice" })))
            coffeeName = "Vegan Caramel Iced Coffee";
        else if (state.SetEquals(new HashSet<string>(new[] { "Espresso"})))
            coffeeName = "Espresso";
        else if (state.SetEquals(new HashSet<string>(new[] { "ColdBrew" })))
            coffeeName = "Cold Brew";
        else if (state.SetEquals(new HashSet<string>(new[] { "DripCoffee" })))
            coffeeName = "Drip Coffee";
        else if (state.SetEquals(new HashSet<string>(new[] { "DripCoffee", "Espresso" })))
            coffeeName = "DEBUG Coffee // Anne eli değmiş gibi";
        else
            coffeeName = "";
    }
}

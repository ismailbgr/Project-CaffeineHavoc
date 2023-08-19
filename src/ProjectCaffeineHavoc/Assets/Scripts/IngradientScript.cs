using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngradientScript : MonoBehaviour
{
    // List of ingradients to be from
    public enum ingradients {HotWater, ColdWater,
                       CoffeeLid, Straw, Filter,
                       CoffeeBean, CoffeeGround,
                       Milk, VeganMilk, FrothedMilk, FrothedVeganMilk,
                       Ice,
                       ChocolateSyrup, CaramelSyrup, VanillaSyrup};

    public ingradients ingradientType; // what type of ingradient is this object
    public HashSet<string> state = new HashSet<string>(); // what does this ingradient has as content

    void Start()
    {
        // add ingradientType as content of this object
        state.Add(ingradientType.ToString());
    }

}
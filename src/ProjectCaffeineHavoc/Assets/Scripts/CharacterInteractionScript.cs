using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractionScript : MonoBehaviour
{
    private Camera playerCamera;

    public GameObject lookingObject;
    public GameObject heldItem = null;

    public bool destroy_item = false;

    private MachineScript useMachineScript;
    private CupGlassContainerScript coffeeHolderScript;


    public GameObject handBook;
    public bool isHandBookOpen = false;

    private CupGlassContainerScript prefabSwapScript;
    public GameObject emptyCupPrefab;
    public GameObject iceCupPrefab;
    public GameObject waterCupPrefab;
    public GameObject milkCupPrefab;
    public GameObject coffee1CupPrefab;
    public GameObject coffee2CupPrefab;
    public GameObject coffee3CupPrefab;

    private void Start()
    {
        handBook = GameObject.Find("Handbook");
        handBook.transform.localScale = new Vector3(0, 0, 0);
        playerCamera = GameObject.Find("playerCamera").GetComponent<Camera>();
    }

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    private void Update()
    {
        mouseAction();

        // Handbook açma kapama
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHandBookOpen)
            {
                isHandBookOpen = false;
                handBook.transform.localScale = new Vector3(0, 0, 0);
            }
            
            else
            {
                isHandBookOpen = true;
                handBook.transform.localScale = new Vector3(1, 1, 1);
            }

        }

        if (heldItem != null)
        {
            updateHeldItem();
        }

    }

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    public void mouseAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lookingObject = LookingAtObject();

            if (lookingObject != null && (heldItem == null))
            { // if we are not holding anything and looking at something: pick up/use that thing
                if (lookingObject.CompareTag("Ingradient") || lookingObject.CompareTag("CoffeeHolder"))
                {
                    pickUpItem(lookingObject);
                }

                // ileride boş elle kullanılacak makine eklenirse bu mantığı kullanacak
                else if (lookingObject.CompareTag("Machine"))
                {
                    if (Vector3.Distance(lookingObject.transform.position, transform.position) < 3f)
                    {
                        useMachineScript = lookingObject.GetComponent<MachineScript>();
                        useMachineScript.useMachine();
                    }
                }

            }
            else if (lookingObject != null && heldItem != null)
            { // if we are holding something and looking at something: 
                if (lookingObject.CompareTag("Ingradient") || lookingObject.CompareTag("CoffeeHolder"))
                { // kahvenin içine malzeme ekleme
                    if (Vector3.Distance(lookingObject.transform.position, transform.position) < 3f)
                    {
                        putInItem(heldItem, lookingObject);
                    }
                }
                else if (lookingObject.CompareTag("Machine"))
                { // if we are looking at a machine:
                    if (heldItem.CompareTag("CoffeeHolder"))
                    { // and holding a coffeeHolder: fill the coffeeHolder with machines output
                        if (Vector3.Distance(lookingObject.transform.position, transform.position) < 3f)
                        {
                            useMachineScript = lookingObject.GetComponent<MachineScript>();
                            useMachineScript.fillGlass();
                        }
                    }
                    else
                    { // if we are holding an Ingradient and looking at a machine: do nothing 
                        // filling a machines ingradient space can be done here in the future(like putting coffee beans to coffee grinder of putting ingradients to frenchpress)
                    }
                }
                else if(lookingObject.CompareTag("Customer")){
                    
                    heldItem.transform.SetParent(null);
                    lookingObject.GetComponent<CustomerMovement>().is_picked_order = true;
                    Destroy(heldItem); // delete the used item
                    heldItem = null; 

                }
                else
                { // we are not looking at an item or machine, than put the heldItem down.
                    putDownItem();
                }
            }

            transformPrefab();
        }
    }

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    public GameObject LookingAtObject() // return looking item
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

    public void updateHeldItem()
    { // set heldItems position such that it is bottom right corner of the screen for 16:9
        heldItem.transform.position = playerCamera.transform.position + playerCamera.transform.forward * 0.6f +
                                                                        playerCamera.transform.right * 0.4f +
                                                                        playerCamera.transform.up * -0.3f;

        if (heldItem.CompareTag("CoffeeHolder"))
        {
            coffeeHolderScript = heldItem.GetComponent<CupGlassContainerScript>();
            // ice prefabı için ayrı bir durum
            if (coffeeHolderScript.state.SetEquals(new HashSet<string>(new[] { "Ice" })))
            {
                heldItem.transform.rotation = Quaternion.Euler(0f, 225f, 0f);
                return;
            }
        }
            heldItem.transform.rotation = Quaternion.Euler(270f, 235f, 0f);
    }

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    private void pickUpItem(GameObject obj)
    { // pick up the object obj
        if (Vector3.Distance(transform.position, obj.transform.position) < 3f)
        {
            heldItem = obj;
            heldItem.transform.SetParent(transform);
        }
    }

    private void putDownItem()
    { // put the heldItem at where is the character looking
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Vector3.Distance(hit.point, transform.position) < 3f)
            {
                heldItem.transform.position = hit.point + new Vector3(0f, 0.02f, 0f);
                if (heldItem.CompareTag("CoffeeHolder"))
                {
                    coffeeHolderScript = heldItem.GetComponent<CupGlassContainerScript>();
                    // ice prefabı için ayrı bir durum
                    if (coffeeHolderScript.state.SetEquals(new HashSet<string>(new[] { "Ice" })))
                    {
                        heldItem.transform.rotation = Quaternion.Euler(0f, 225f, 0f);
                    }
                    else
                        heldItem.transform.rotation = Quaternion.Euler(270f, 235f, 0f);
                }
                else
                    heldItem.transform.rotation = Quaternion.Euler(270f, 235f, 0f);
            }
        }
        if (Vector3.Distance(hit.point, transform.position) < 3f)
        {
            heldItem.transform.SetParent(null);
            heldItem = null;
        }
    }

    private void putInItem(GameObject item1, GameObject item2) // item1: held, item2: looked
    {
        GameObject newItem = null; // item to hold at the end.
        if (item1.CompareTag("Ingradient") && item2.CompareTag("CoffeeHolder"))
        { // add ingradient type of items content into coffeeHolder type of item
            GameObject itemIngra = item1; // assign ingradient to item1 by default
            GameObject itemCH = item2; // assign coffeeholder to item2 by default

            coffeeHolderScript = itemCH.GetComponent<CupGlassContainerScript>();
            coffeeHolderScript.addIngradient(itemIngra);
            coffeeHolderScript.transformToCoffee(); // checking if coffeholder is a coffee
            newItem = itemCH; // store the new created item
            heldItem.transform.SetParent(null);
            heldItem = null;
            Destroy(itemIngra);
        }
        else if (item1.CompareTag("CoffeeHolder") && item2.CompareTag("CoffeeHolder"))
        { // if item1 and item2 are coffeeHolder, merge them into which the character is looking
            coffeeHolderScript = item1.GetComponent<CupGlassContainerScript>();
            coffeeHolderScript.pourInto(item2); // pour item1 into item2, now we will hold the empty coffeeHolder
            transformPrefabNotHeldItem(item2);
            newItem = item1;
            heldItem.transform.SetParent(null);
            heldItem = null;
        }
        else
        { // if both item1 and item2 are ingradient do nothing
            return;
        }

        heldItem = newItem;
        heldItem.transform.SetParent(transform);
    }

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    // elimizdeki kahvelerin içeriklerine göre prefabları değiştiriyoruz
    // eğer elimizdeki kahvede
    // hiçbir şey yoksa boş kahve prefabını
    // sadece buz varsa buzlu kahve prefabını
    // sadece su varsa su bardağı prefabını
    // sadece kahve varsa kahve bardağı prefabını
    // sadece süt varsa sütlü bardak prefabını

    // diğer prefabler karışıma göre koyulaşıyor

    public void transformPrefab()
    {

        if (heldItem != null && heldItem.CompareTag("CoffeeHolder"))
        {
            prefabSwapScript = heldItem.GetComponent<CupGlassContainerScript>();
            HashSet<string> oldState = prefabSwapScript.state;
            Vector3 oldPos = heldItem.transform.position;

            if (oldState.SetEquals(new HashSet<string>()))
            {
                Destroy(heldItem);
                heldItem = Instantiate(emptyCupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
            }
            else if (oldState.SetEquals(new HashSet<string>(new[] { "Ice" })))
            {
                Destroy(heldItem);
                heldItem = Instantiate(iceCupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
            }
            else if (oldState.SetEquals(new HashSet<string>(new[] { "HotWater" })))
            {
                Destroy(heldItem);
                heldItem = Instantiate(waterCupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
            }
            else if (oldState.SetEquals(new HashSet<string>(new[] { "Espresso" })) ||
                     oldState.SetEquals(new HashSet<string>(new[] { "ColdBrew" })) ||
                     oldState.SetEquals(new HashSet<string>(new[] { "DripCoffee" })))
            {
                Destroy(heldItem);
                heldItem = Instantiate(coffee3CupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
            }
            else if (oldState.Overlaps(new HashSet<string> { "Milk", "VeganMilk", "FrothedMilk", "VeganFrothedMilk" }) && 
                    !oldState.Overlaps(new HashSet<string> { "Espresso", "ColdBrew", "DripCoffee" }))
            {
                Destroy(heldItem);
                heldItem = Instantiate(milkCupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
            }
            else if (oldState.Overlaps(new HashSet<string> { "Espresso", "ColdBrew", "DripCoffee" }) &&
                     oldState.Overlaps(new HashSet<string> { "Milk", "VeganMilk", "FrothedMilk", "VeganFrothedMilk" }) &&
                     oldState.Overlaps(new HashSet<string> { "Ice", "HotWater" }))
            {
                Destroy(heldItem);
                heldItem = Instantiate(coffee1CupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
            }
            else if (oldState.Overlaps(new HashSet<string> { "Milk", "VeganMilk", "FrothedMilk", "VeganFrothedMilk" }) &&
                     oldState.Overlaps(new HashSet<string> { "Espresso", "ColdBrew", "DripCoffee" }))
            {
                Destroy(heldItem);
                heldItem = Instantiate(coffee2CupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
            }

            prefabSwapScript = heldItem.GetComponent<CupGlassContainerScript>();
            prefabSwapScript.state = oldState;
        }
    }

    public void transformPrefabNotHeldItem(GameObject cup)
    {
        prefabSwapScript = cup.GetComponent<CupGlassContainerScript>();
        HashSet<string> oldState = prefabSwapScript.state;
        Vector3 oldPos = cup.transform.position;

        if (oldState.SetEquals(new HashSet<string>()))
        {
            Destroy(cup);
            cup = Instantiate(emptyCupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
        }
        else if (oldState.SetEquals(new HashSet<string>(new[] { "Ice" })) && 
                !oldState.Overlaps(new HashSet<string> { "Espresso", "ColdBrew", "DripCoffee", "Milk", "VeganMilk", "FrothedMilk", "VeganFrothedMilk" }))  
        {
            Destroy(cup);
            cup = Instantiate(iceCupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
        }
        else if (oldState.SetEquals(new HashSet<string>(new[] { "HotWater" })))
        {
            Destroy(cup);
            cup = Instantiate(waterCupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
        }
        else if (oldState.SetEquals(new HashSet<string>(new[] { "Espresso" })) ||
                    oldState.SetEquals(new HashSet<string>(new[] { "ColdBrew" })) ||
                    oldState.SetEquals(new HashSet<string>(new[] { "DripCoffee" })))
        {
            Destroy(cup);
            cup = Instantiate(coffee3CupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
        }
        else if (oldState.Overlaps(new HashSet<string> { "Milk", "VeganMilk", "FrothedMilk", "VeganFrothedMilk" }) &&
                !oldState.Overlaps(new HashSet<string> { "Espresso", "ColdBrew", "DripCoffee" }))
        {
            Destroy(cup);
            cup = Instantiate(milkCupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
        }
        else if (oldState.Overlaps(new HashSet<string> { "Espresso", "ColdBrew", "DripCoffee" }) &&
                    oldState.Overlaps(new HashSet<string> { "Milk", "VeganMilk", "FrothedMilk", "VeganFrothedMilk" }) &&
                    oldState.Overlaps(new HashSet<string> { "Ice", "HotWater" }))
        {
            Destroy(cup);
            cup = Instantiate(coffee1CupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
        }
        else if (oldState.Overlaps(new HashSet<string> { "Milk", "VeganMilk", "FrothedMilk", "VeganFrothedMilk" }) &&
                    oldState.Overlaps(new HashSet<string> { "Espresso", "ColdBrew", "DripCoffee" }))
        {
            Destroy(cup);
            cup = Instantiate(coffee2CupPrefab, oldPos, Quaternion.Euler(270f, 235f, 0f));
        }

        prefabSwapScript = cup.GetComponent<CupGlassContainerScript>();
        prefabSwapScript.state = oldState;
    }
}

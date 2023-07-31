using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractionScript : MonoBehaviour
{
    private Camera playerCamera;

    public GameObject lookingObject;
    public GameObject heldItem = null;

    private MachineScript useMachineScript;
    private CupGlassContainerScript coffeeHolderScript;


    public GameObject handBook;
    public bool isHandBookOpen = false;

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
            //Debug.Log("looking at: " + lookingObject + " while holding: " + heldItem);

            if (lookingObject != null && (heldItem == null))
            { // if we are not holding anything and looking at something: pick up/use that thing
                //Debug.Log("functions with empyt hand.");
                if (lookingObject.CompareTag("Ingradient") || lookingObject.CompareTag("CoffeeHolder"))
                {
                    //Debug.Log("picking up " + lookingObject);
                    pickUpItem(lookingObject);
                }

                else if (lookingObject.CompareTag("Machine"))
                {
                    //Debug.Log("using " + lookingObject);
                    if (Vector3.Distance(lookingObject.transform.position, transform.position) < 3f)
                    {
                        useMachineScript = lookingObject.GetComponent<MachineScript>();
                        useMachineScript.useMachine();
                    }
                }
            }
            else if (lookingObject != null && heldItem != null)
            { // if we are holding something and looking at something: 
                //Debug.Log("functions with full hand.");
                if (lookingObject.CompareTag("Ingradient") || lookingObject.CompareTag("CoffeeHolder"))
                { // if we are looking at something that can be merged: merge lookingObject with heldItem
                  //Debug.Log("mixing " + heldItem + " with " + lookingObject);
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
                else
                { // we are not looking at an item or machine, than put the heldItem down.
                    //Debug.Log("putting down " + heldItem);
                    putDownItem();
                }
            }
        }

    }

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    public GameObject LookingAtObject() // return looking item
    { // return the object that character is looking
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
        heldItem.transform.rotation = playerCamera.transform.rotation;
    }

    /* FOR DEBUGGING AT updateHeldItem()
    if (heldItem.CompareTag("CoffeeHolder"))
    {
        string logMessage = "";
        CupGlassContainerScript ffff = heldItem.GetComponent<CupGlassContainerScript>();
        logMessage = "CoffeeHolder state:" + string.Join(", ", ffff.state);
        Debug.Log(logMessage);
    }
    else if (heldItem.CompareTag("Ingradient"))
    {
        string logMessage = "";
        IngradientScript ffff = heldItem.GetComponent<IngradientScript>();
        logMessage = "Ingradient state:" + string.Join(", ", ffff.state);
        Debug.Log(logMessage);
    }
    */

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
                heldItem.transform.rotation = Quaternion.identity;
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
            heldItem.transform.SetParent(null); // empty the hend step1
            heldItem = null; // empty the hend step2
            Destroy(itemIngra); // delete the used item
        }
        else if (item1.CompareTag("CoffeeHolder") && item2.CompareTag("CoffeeHolder"))
        { // if item1 and item2 are coffeeHolder, merge them into which the character is looking
            coffeeHolderScript = item1.GetComponent<CupGlassContainerScript>();
            coffeeHolderScript.pourInto(item2); // pour item1 into item2, now we will hold the empty coffeeHolder
            newItem = item1; // store the new created item
            heldItem.transform.SetParent(null); // empty the hend step1
            heldItem = null; // empty the hend step2
        }
        else
        { // if both item1 and item2 are ingradient do nothing
            return;
        }

        heldItem = newItem; // hold new created item step1
        heldItem.transform.SetParent(transform); // hold new created item step2
    }

    // ######################## ######################## ######################## ######################## ######################## ######################## //

}

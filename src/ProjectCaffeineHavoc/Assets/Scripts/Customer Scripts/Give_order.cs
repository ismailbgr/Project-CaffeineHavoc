using UnityEngine;

public class Give_order : MonoBehaviour
{
    public LayerMask interactableLayer;
    private Camera playerCamera;

    private GameObject Player;
    
    public GameObject lookingObject;

    private void Start()
    {
        // playerCamera = GameObject.Find("playerCamera").GetComponent<Camera>();
        // Player = GameObject.Find("Player");
    }

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0)) // Sol tıklama kontrolü
        // {
        //     lookingObject = LookingAtObject();
        //     if (lookingObject != null)
        //     { 
        //         if (lookingObject.CompareTag("Customer"))
        //         {
        //             if (lookingObject.GetComponent<CustomerMovement>().is_in_pickup_place) {
        //                 lookingObject.GetComponent<CustomerMovement>().is_picked_order = true;
        //                 Player.GetComponent<CharacterInteractionScript>().destroy_item = true;
        //             }
        //         }
        //     }
            
        // }
    }

    public GameObject LookingAtObject() { // return the object that character is looking
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

}

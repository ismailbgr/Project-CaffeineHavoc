using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private GameObject cloneItem;
    private bool isRespawning;

    float spawnDistance = 0.5f;

    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

   

    private void Update()
    {
        // if item is far away from spawn point make it spawn
        if (Vector3.Distance(transform.position, initialPosition) > spawnDistance)
        {
            Respawn();
        }
    }

   

    public void Respawn()
    {
        // her eşyanın 1 kere klonlanma hakkı var, şu andaki eşya klonlanmadıysa klonla diyorum.
        // (tezgahtaki eşyayı aldıktan sonra tekrardan onu klonlayamıyorsun, onun yerine yeni gelen eşyayı klonluyorsun.)
        if (cloneItem == null && !isRespawning)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

   

    private IEnumerator RespawnCoroutine()
    {

        //0.05 saniye gecikmeli respawn.

        isRespawning = true;
        yield return new WaitForSeconds(0.05f); 

        cloneItem = Instantiate(gameObject, initialPosition, initialRotation); 
        cloneItem.name = gameObject.name + "c";
        isRespawning = false;
    }

}

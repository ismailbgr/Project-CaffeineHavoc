using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    // respawn olma iþlemine "klonlanma" dedim.
    // yeni obje yaratmak için gerekli variable'lar
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private GameObject cloneItem;
    private bool isRespawning;

    // objeleri içine koyup Unity'de düzenlemek için kullandýðým folder

    float spawnDistance = 0.5f;

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    private void Start()
    {
        // yeni objeyi tezgahtaki orijinal yere klonlamak için baþlangýç pozisyonlarýný tutuyorum
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    private void Update()
    {
        // if item is far away from spawn point make it spawn
        if (Vector3.Distance(transform.position, initialPosition) > spawnDistance)
        {
            Respawn();
        }
    }

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    public void Respawn()
    {
        // her eþyanýn 1 kere klonlanma hakký var, þu andaki eþya klonlanmadýysa klonla diyorum.
        // (tezgahtaki eþyayý aldýktan sonra tekrardan onu klonlayamýyorsun, onun yerine yeni gelen eþyayý klonluyorsun.)
        if (cloneItem == null && !isRespawning)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    private IEnumerator RespawnCoroutine()
    {
        // arka arkaya sürekli klonlanmasýn diye isRespawning var, þu anda klonlanmak için 3 saniyenin geçmesini bekliyorsak yeniden klonlama iþlemine girmiyoruz.
        isRespawning = true;
        yield return new WaitForSeconds(0.05f); // 3 sn gecikme var eþya direkt respawn olmasýn diye.

        cloneItem = Instantiate(gameObject, initialPosition, initialRotation); // bu fonksiyon sanýrým yeni obje kopyasý yaratýyor
        cloneItem.name = gameObject.name + "c"; // objenin adýna "c" ekliyorum, her kopyalanan obje orijinal objesinin yanýna "c" alýyor.
        isRespawning = false;
    }

}

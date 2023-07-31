using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnItem : MonoBehaviour
{
    // respawn olma i�lemine "klonlanma" dedim.
    // yeni obje yaratmak i�in gerekli variable'lar
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private GameObject cloneItem;
    private bool isRespawning;

    // objeleri i�ine koyup Unity'de d�zenlemek i�in kulland���m folder

    float spawnDistance = 0.5f;

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    private void Start()
    {
        // yeni objeyi tezgahtaki orijinal yere klonlamak i�in ba�lang�� pozisyonlar�n� tutuyorum
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
        // her e�yan�n 1 kere klonlanma hakk� var, �u andaki e�ya klonlanmad�ysa klonla diyorum.
        // (tezgahtaki e�yay� ald�ktan sonra tekrardan onu klonlayam�yorsun, onun yerine yeni gelen e�yay� klonluyorsun.)
        if (cloneItem == null && !isRespawning)
        {
            StartCoroutine(RespawnCoroutine());
        }
    }

    // ######################## ######################## ######################## ######################## ######################## ######################## //

    private IEnumerator RespawnCoroutine()
    {
        // arka arkaya s�rekli klonlanmas�n diye isRespawning var, �u anda klonlanmak i�in 3 saniyenin ge�mesini bekliyorsak yeniden klonlama i�lemine girmiyoruz.
        isRespawning = true;
        yield return new WaitForSeconds(0.05f); // 3 sn gecikme var e�ya direkt respawn olmas�n diye.

        cloneItem = Instantiate(gameObject, initialPosition, initialRotation); // bu fonksiyon san�r�m yeni obje kopyas� yarat�yor
        cloneItem.name = gameObject.name + "c"; // objenin ad�na "c" ekliyorum, her kopyalanan obje orijinal objesinin yan�na "c" al�yor.
        isRespawning = false;
    }

}

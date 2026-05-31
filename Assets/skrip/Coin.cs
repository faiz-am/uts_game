using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something touched coin");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player touched coin");

            CoinCounter.instance.AddCoin();

            Destroy(gameObject);
        }
    }
}
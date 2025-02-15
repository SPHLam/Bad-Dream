using UnityEngine;

public class PhantomTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            Phantom phantom = GetComponentInParent<Phantom>();
            phantom.SetCharging();
        }
    }
}

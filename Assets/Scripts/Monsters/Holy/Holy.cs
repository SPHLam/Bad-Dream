using UnityEngine;

public class Holy : MonoBehaviour
{
    private bool _isPlayerColliding;
    private Player _player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            _isPlayerColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player != null)
        {
            _isPlayerColliding = false;
        }
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {

    }
}

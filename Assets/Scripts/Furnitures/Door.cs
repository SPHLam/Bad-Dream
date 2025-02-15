using UnityEngine;

public class Door : MonoBehaviour
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

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        HandleGoToNextRoom();
    }

    private void HandleGoToNextRoom()
    {
        if (_isPlayerColliding && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.LoadNextRoom();
        }
    }
}

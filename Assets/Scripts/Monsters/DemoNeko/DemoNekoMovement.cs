using System.Collections;
using UnityEngine;

public class DemoNekoMovement : MonoBehaviour
{
    private float _idleStateTime = 0.5f;
    private float _turnToFollowingTime = 2f;
    private bool _beingSpotted = false;
    private bool _chasingMode = false;
    private float _moveSpeed = 9f;
    private float _chasingSpeed = 18f;
    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAngry())
        {
            ActivateHuntMode();
        }
        else
        {
            HandleBetweenIdleFollowing();
            RunningTowardPlayer();
        }
    }

    public bool IsAngry()
    {
        return _idleStateTime == 0f;
    }

    public void ActivateHuntMode()
    {
        StartCoroutine(ActivateHuntModeCoroutine());
    }

    private IEnumerator ActivateHuntModeCoroutine()
    {
        _moveSpeed = 0;
        yield return new WaitForSeconds(3f);
        _chasingMode = true;
        RunningTowardPlayer();
    }

    private void HandleBetweenIdleFollowing()
    {
        if (_beingSpotted)
        {
            _idleStateTime = Mathf.Max(0, _idleStateTime - (Time.deltaTime * 2 / 3));
        }
        else
        {
            _idleStateTime = Mathf.Min(_idleStateTime + Time.deltaTime, _turnToFollowingTime);
        }
    }

    private void RunningTowardPlayer()
    {
        Vector2 direction = new Vector2(_player.transform.position.x - transform.position.x, 0).normalized;

        if (!IsChasingMode())
        {
            if (CanFollowPlayer())
                transform.position += (Vector3)direction * _moveSpeed * Time.deltaTime;
        }
        else
            transform.position += (Vector3)direction * _chasingSpeed * Time.deltaTime;
    }

    public bool CanFollowPlayer()
    {
        return _idleStateTime == _turnToFollowingTime && !_player.IsHiding();
    }

    public bool IsChasingMode()
    {
        return _chasingMode;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "Flashlight")
        {
            _beingSpotted = true;
        }
        else if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Neko touches Player");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Flashlight")
        {
            _beingSpotted = false;
        }
    }
}

using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    public PlayerMovement playerMovement;

    /* HIDE INFO */
    private bool _isHiding;

    /* HEALTH INFO */
    private bool _isDead;

    /* ANIMATION INFO */
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    /* LIGHT INFO*/
    private Light2D _flashLight;
    public bool isFlashlightOn = false;
    private bool _isOutOfBattery = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _flashLight = GetComponentInChildren<Light2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        ToggleFlashlight(false);
    }

    // Update is called once per frame
    void Update()
    {
        HandleFlipAnimation();
        HandleToggleFlashlight();
    }

    private void HandleFlipAnimation()
    {
        float moveInput = playerMovement.inputHandler.MoveInput;
        if (moveInput != 0)
        {
            // Animation
            spriteRenderer.flipX = moveInput < 0;
            // Light
            _flashLight.transform.position = new Vector3(transform.position.x + (1.35f * moveInput), _flashLight.transform.position.y, _flashLight.transform.position.z);
            _flashLight.transform.rotation = Quaternion.Euler(0, 0, -90 * moveInput);
        }
    }

    #region Hiding
    public bool IsHiding()
    {
        return _isHiding;
    }

    public void SetHidingState()
    {
        _isHiding = !_isHiding;
        gameObject.GetComponent<Renderer>().enabled = !_isHiding;
        isFlashlightOn = false;
        _flashLight.gameObject.SetActive(isFlashlightOn);
    }
    #endregion

    #region Die

    public void Die()
    {
        _isDead = true;
        gameObject.SetActive(false);
    }

    #endregion

    #region Flashlight

    private void HandleToggleFlashlight()
    {
        if (!_isOutOfBattery)
        {
            if (playerMovement.inputHandler.FlashlightPressed == true)
            {
                ToggleFlashlight(!isFlashlightOn);
            }
        }
        else
        {
            ToggleFlashlight(false);
        }
    }

    private void ToggleFlashlight(bool toggle)
    {
        isFlashlightOn = toggle;
        _flashLight.gameObject.SetActive(isFlashlightOn);
    }

    public void SetBatteryDied()
    {
        _isOutOfBattery = true;
    }

    public void BatteryCharged()
    {
        _isOutOfBattery = false;
    }

    #endregion
}

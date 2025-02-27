using UnityEngine;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;

    private PlayerControls playerControls;

    public bool moveRight = false;
    public bool moveLeft = false;
    public int xInputValue = 0;
    public bool jumpInput = false;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new();

            playerControls.PlayerMovement.Right.performed += i => moveRight = true;
            playerControls.PlayerMovement.Right.canceled += i => moveRight = false;

            playerControls.PlayerMovement.Left.performed += i => moveLeft = true;
            playerControls.PlayerMovement.Left.canceled += i => moveLeft = false;

            playerControls.PlayerMovement.Jump.performed += i => jumpInput = true;
        }

        playerControls.Enable();
    }

    private void Update()
    {
        if (moveLeft) { xInputValue = -1; }
        else if (moveRight) { xInputValue = 1; }
        else { xInputValue = 0; }

        HandleJumpInput();
    }

    private void HandleJumpInput()
    {
        if (jumpInput) { jumpInput = false; }
    }
}

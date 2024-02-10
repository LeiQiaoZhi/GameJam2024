using Logging;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    [SerializeField] private Character character_;
    [SerializeField] private Transform transform_;
    private InputAction moveAction_;
    PlayerCharacterController characterController_;

    private bool isMoving_ = false;
    // Reference to main Camera
    private Camera mainCam_;
    // Cache for previous direction
    // private Vector3 prevDir_ = Vector3.zero;

    void Start()
    {
        if (inputController == null)
        {
            XLogger.LogError("InputController is not set in CharacterController");
        }
        else
        {
            mainCam_ = Camera.main;
            moveAction_ = inputController.GetMoveAction();
            moveAction_.started += _ctx => isMoving_ = true;
            moveAction_.canceled += _ctx => isMoving_ = false;
        }

    }

    private void Update()
    {
        /**
         * The character will always face towards the direction the camera points
         * at; in addition, it will move if an input is detected
         */
        // We could choose to ignore camera rotation if it's not changed
        // However, a branch could actually be more expensive than just calculate
        // TODO: benchmark this afterwards
        // if (prevDir_ != camFwd)
        // {
        //     camFwd.y = 0;
        //     transform.rotation = Quaternion.LookRotation(camFwd);
        // }

        if (!isMoving_) return; // If we have no input, we rotate the camera and return
        var amount = moveAction_.ReadValue<Vector2>();
        var movementVec = new Vector3(-amount.y, 0f, amount.x);
        XLogger.Log(Category.Input, "Character move amount: " + amount);

        transform_.rotation = Quaternion.LookRotation(movementVec);
        XLogger.Log(Category.Input, "Character faces: " + Vector3.Normalize(amount));
        var unitMovement = Vector3.Normalize(movementVec);
        transform_.position += unitMovement * character_.Speed_;
    }
}

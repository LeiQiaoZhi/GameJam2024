using Logging;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] private InputController inputController_;
    [SerializeField] private Character characterStats_;
    [SerializeField] private Transform characterTransform_;
    private InputAction moveAction_;
    PlayerCharacterController characterController_;

    private bool isMoving_ = false;
    // Reference to main Camera
    private Camera mainCam_;
    // Cache for previous direction
    private Vector3 dirVec_ = Vector3.zero;

    void Start()
    {
        if (inputController_ == null)
        {
            XLogger.LogError("InputController is not set in CharacterController");
        }
        else
        {
            mainCam_ = Camera.main;
            moveAction_ = inputController_.GetMoveAction();
            moveAction_.started += _ctx => isMoving_ = true;
            moveAction_.canceled += _ctx => isMoving_ = false;
        }

    }

    private void RotateWithCamera()
    {
        var cameraDirection = mainCam_.transform.forward;
        cameraDirection.y = 0;

        if (dirVec_ != cameraDirection)
        {
            characterTransform_.forward = cameraDirection;
            dirVec_ = cameraDirection;
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
        RotateWithCamera();

        if (!isMoving_) return; // If we have no input, we rotate the camera and return
        Vector2 amount = moveAction_.ReadValue<Vector2>();
        Quaternion alignRotation = Quaternion.FromToRotation(Vector3.forward, dirVec_);
        Vector3 rawMovementVec = new Vector3(amount.x, 0f, amount.y);
        Vector3 movementVec = (alignRotation * rawMovementVec).normalized;
        XLogger.Log(Category.Input, "Character move amount: " + amount);

        // characterTransform_.rotation = Quaternion.LookRotation(movementVec);
        XLogger.Log(Category.Input, "Character faces: " + Vector3.Normalize(amount));
        characterTransform_.rotation = Quaternion.LookRotation(movementVec);
        characterTransform_.position += movementVec * characterStats_.Speed_;
    }
}

using Logging;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerCharacterController : MonoBehaviour
{
    [SerializeField] private InputController inputController_;
    [SerializeField] private Character characterStats_;
    [SerializeField] private Rigidbody character_;
    private Transform characterTransform_;
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
            characterTransform_ = character_.transform;

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

    private void FixedUpdate()
    {
        // Currently, since we're using a cylindrical collision box,
        // we don't care about physical rotation. Rotations are therefore
        // done with transform only
        RotateWithCamera();

        // We use rigid body velocity to control character motion
        // This way movement on the map complies with the rest of the physics model
        if (!isMoving_)
        { // If no user input, set velocity to 0
            character_.velocity = Vector3.zero;
            return;
        }
        else // Having this else clause is unneccesary but will be optimized by the compiler
        {
            Vector2 amount = moveAction_.ReadValue<Vector2>();
            Quaternion alignRotation = Quaternion.FromToRotation(Vector3.forward, dirVec_);
            Vector3 amount3D = new Vector3(amount.x, 0f, amount.y);
            Vector3 movementVec = (alignRotation * amount3D).normalized;

            // characterTransform_.rotation = Quaternion.LookRotation(movementVec);
            characterTransform_.rotation = Quaternion.LookRotation(movementVec);
            character_.velocity = movementVec * 10 * characterStats_.Speed_;

            XLogger.Log(Category.Input, "Character move amount: " + movementVec);
            XLogger.Log(Category.Input, "Character facing direction: " + movementVec);
        }
    }
}

using Logging;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private InputController inputController;
    private InputAction moveAction_;

    private bool isMoving_ = false;
    
    void Start()
    {
        if (inputController == null)
        {
            XLogger.LogError("InputController is not set in CharacterController");
        }
        else
        {
            moveAction_ = inputController.GetMoveAction();
            moveAction_.started += _ctx => isMoving_ = true;
            moveAction_.canceled += _ctx => isMoving_ = false;
        }
        
    }

    private void Update()
    {
        if (!isMoving_) return;
        
        var amount = moveAction_.ReadValue<Vector2>();
        XLogger.Log(Category.Input,"Character move amount: " + amount);
        
        // TODO: apply movement
    }

}
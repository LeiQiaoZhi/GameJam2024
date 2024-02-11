using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
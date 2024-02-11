using UnityEngine;

public abstract class ITargetSelector : MonoBehaviour
{
    /// Target currently selected by the target selector
    public Collider Target_ { get; protected set; }
    /// Unique Identifier
    private int uid_;
    private int framesPerUpdate_ = 30;

    public void Start()
    {
        UpdateTarget();
    }

    /// Round robin target update based on uid
    public void Update()
    {
        if (Time.frameCount % framesPerUpdate_ == uid_ % framesPerUpdate_)
            UpdateTarget();
    }

    protected abstract void UpdateTarget();
}

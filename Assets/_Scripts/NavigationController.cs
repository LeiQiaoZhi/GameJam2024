using UnityEngine;
using UnityEngine.AI;
using Logging;

/// <summary>
/// This module controls the navigation of an object
/// For now, attach a navigation mesh agent to this controller.
/// Everytime you click a position on the map, it resets the
/// agent's target to that position.
/// </summary>
[Tooltip("Controls the agents navigation; sets target on mouse click")]
public class NavigationController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent_;
    [SerializeField] private Vector3 destination_;
    [SerializeField] private bool mouseNavigation_;
    private Camera cam_;

    public void Start()
    {
        cam_ = Camera.main;
    }

    public void Update()
    {
        if (mouseNavigation_ && Input.GetMouseButtonDown(0))
        {
            Ray ray = cam_.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                destination_ = hit.point;
                agent_.SetDestination(destination_);
                XLogger.Log(Category.Input, "Mouse position: " + Input.mousePosition);
                XLogger.Log(Category.Input, "Destination: " + destination_);
            }
        }
    }
}

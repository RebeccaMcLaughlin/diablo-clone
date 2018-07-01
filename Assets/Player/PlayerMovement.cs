using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float walkStopMoveRadius = 0.2f;
    [SerializeField] float attackStopMoveRadius = 0.2f;

    ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;

    bool isInDirectMode = false; // TODO: Consider making static
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;

    // Use this for initialization
    void Start ()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        m_Cam = Camera.main.transform;
        currentDestination = transform.position;
	}

    // For key presses and whatnt
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.G)) // TODO: Change to menu UI, currently uses G for Gamepad
        {
            isInDirectMode = !isInDirectMode;
        }
    }

    // Fixed update is called in sync with physics
    void FixedUpdate()
    { 
        if (isInDirectMode)
        {
            ProcessDirectMovement();
        } else
        {
            ProcessMouseMovement();
        }
    }

    private void ProcessDirectMovement ()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
        m_Move = v * m_CamForward + h * m_Cam.right;

        m_Character.Move(m_Move, false, false);
    }

    private void ProcessMouseMovement ()
    {
        clickPoint = cameraRaycaster.hit.point;

        if (Input.GetMouseButton(0))
        {
            switch (cameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    currentDestination = ShortDestination(clickPoint, walkStopMoveRadius);
                    break;
                case Layer.Enemy:
                    currentDestination = ShortDestination(clickPoint, attackStopMoveRadius);
                    print("Not moving to enemy");
                    break;
                default:
                    print("Layer is not walkable or enemy");
                    return;
            }
        WalkToDestination();
        } else
        {
            m_Character.Move(Vector3.zero, false, false);
        }
    }

    private void WalkToDestination()
    {
        var playerToClickPoint = currentDestination - transform.position;
        m_Character.Move(playerToClickPoint, false, false);
    }

    // Subtract the clicked point distance from the move stop radius
    Vector3 ShortDestination ( Vector3 destination, float shortening )
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, currentDestination);
        Gizmos.DrawSphere(currentDestination, 0.1f);
        Gizmos.DrawSphere(clickPoint, 0.15f);

         Gizmos.color = new Color(255f, 0f, 255f, 0.5f);
         Gizmos.DrawWireSphere(transform.position, attackStopMoveRadius);

    }
}

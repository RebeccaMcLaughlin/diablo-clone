using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour {
    [SerializeField] float walkStopMoveRadius = 0.2f;

    ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

	// Use this for initialization
	void Start () {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        m_Character = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
	}

    // Fixed update is called in sync with physics
    void FixedUpdate () {
        var playerToClickPoint = Vector3.zero;

        if (Input.GetMouseButton(0))
        {
            switch (cameraRaycaster.layerHit)
            {
                case Layer.Walkable:
                    playerToClickPoint = cameraRaycaster.hit.point - transform.position;
                    break;
                case Layer.Enemy:
                    print("Not moving to enemy");
                    break;
                default:
                    print("Layer is not walkable or enemy");
                    return;
            }

        }

        m_Character.Move(playerToClickPoint, false, false);
    }
}

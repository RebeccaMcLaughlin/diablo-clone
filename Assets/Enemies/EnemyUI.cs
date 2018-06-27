using UnityEngine;

// Add a UI Socket transform to your enemy
// Attack this script to the socket
// Link to a canvas prefab that contains NPC UI
public class EnemyUI : MonoBehaviour {

    // Works around Unity 5.5's lack of nested prefabs
    [Tooltip("The UI canvas prefab")]
    [SerializeField]
    GameObject enemyCanvasPrefab = null;
    CapsuleCollider capsuleCollider;
    Camera cameraToLookAt;

    // Use this for initialization 
    void Start()
    {
        // Get height of enemy collider
        capsuleCollider = GetComponent<CapsuleCollider>();
        cameraToLookAt = Camera.main;

        // Add height to the transform position
        float newY = transform.position.y + (capsuleCollider.height * transform.localScale.y) * 1.15f;
        Vector3 newPosition = new Vector3(transform.position.x, newY, transform.position.z);
        Instantiate(enemyCanvasPrefab, newPosition, transform.rotation, transform);
    }

    // Update is called once per frame 
    void LateUpdate()
    {
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }
}
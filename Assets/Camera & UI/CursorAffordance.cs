using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {
    [SerializeField] Texture2D walkCursor = null;
    [SerializeField] Texture2D attackCursor = null;
    [SerializeField] Texture2D errorCursor = null;

    [SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);

    // TODO: Replace with a serialised array of 2d textures so we can say
    // If layer = 0, use cursor 0 whenever notifyLayerChangeObservers is called
    [SerializeField] const int walkableLayerNumber = 9;
    [SerializeField] const int enemyLayerNumber = 10;

    CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
       cameraRaycaster = GetComponent<CameraRaycaster>();
       cameraRaycaster.notifyLayerChangeObservers += UpdateCursor;
    }

    void UpdateCursor (int newLayer) {
        print(newLayer);
        switch (newLayer)
        {
            case walkableLayerNumber:
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                break;
            case enemyLayerNumber:
                Cursor.SetCursor(attackCursor, cursorHotspot, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(errorCursor, cursorHotspot, CursorMode.Auto);
                return;
        }
    }

    // TODO: Consider de-registering UpdateCursor on leaving all game scenes
}

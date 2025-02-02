using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private Camera cam;
    private UnityEngine.Vector3 ResetCamera;
    private UnityEngine.Vector3 Origin;
    private UnityEngine.Vector3 Diference;
    private bool Drag = false;
    private bool RightClickInitiated;
    public Unicell followedUnicell;

    void Start()
    {
        Instance = this;

        cam = Camera.main;
        ResetCamera = Camera.main.transform.position;
        RightClickInitiated = false;

        cam.orthographicSize = 10.0f;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (Drag == false)
            {
                Drag = true;
                followedUnicell = null;
                Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            Drag = false;
        }
        if (Drag == true)
        {
            Camera.main.transform.position = Origin - Diference;
        }

        // Reset Camera with middle click
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            Camera.main.transform.position = ResetCamera;
        }

        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0)
        {
            cam.orthographicSize -= (Input.mouseScrollDelta.y * (cam.orthographicSize / 20));

            if (cam.orthographicSize < 1f)
            {
                cam.orthographicSize = 1f;
            }
            else if (cam.orthographicSize > 200.0f)
            {
                cam.orthographicSize = 200.0f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (followedUnicell != null)
        {
            cam.transform.position = new Vector3(followedUnicell.transform.position.x, followedUnicell.transform.position.y, cam.transform.position.z);
        }
    }

    public void FollowUnicell(Unicell unicell)
    {
        followedUnicell = unicell;
    }
}

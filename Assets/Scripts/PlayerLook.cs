using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform lookIndicator;
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 shootDir = mousePos - new Vector3(transform.position.x, transform.position.y, 0);

        lookIndicator.SetLocalPositionAndRotation(new Vector3(shootDir.x, shootDir.y, transform.position.z).normalized * 1f, Quaternion.Euler(new Vector3(0,0,AngleBetweenVector3(transform.position, mousePos))));
        
    }

    private float AngleBetweenVector3(Vector3 vec1, Vector3 vec2)
    {
        Vector3 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector3.Angle(Vector3.right, diference) * sign - 90;
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 5f;

    private Vector3 offset;

    private void Start()
    {
        //Mendapatkan offset antara target dan kamera
        offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        //Menempatkan posisi untuk kamera
        Vector3 targetCamPos = target.position + offset;

        //Set posisi camera dengan smoothing
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
using UnityEngine;

public class CameraDriver : MonoBehaviour
{
    [SerializeField]
    private Vector3 currentPosition;
    //private Quaternion currentRotation;

    [SerializeField]
    private float cameraMovingSpeed = 0.05f;
    //private float cameraRotatingSpeed = 0.05f;

    private void Start()
    {
        currentPosition = transform.position;
        //currentRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentPosition, cameraMovingSpeed);
        //transform.position = currentPosition;
        //transform.rotation = Quaternion.Lerp(transform.rotation, currentRotation, cameraRotatingSpeed);
    }

    public void MoveToPosition(Vector3 position)
    {
        //Debug.Log(position);
        currentPosition = position;
    }

    public void MoveOffset(Vector3 positionOffset)
    {
        MoveToPosition(currentPosition + positionOffset);
    }

    public void MoveToPositionInstantly(Vector3 instantPosition)
    {
        MoveToPosition(instantPosition);        
        transform.position = instantPosition;    
    }

    public void MoveToPositionOffsetInstantly(Vector3 instantOffset)
    {
        MoveToPositionInstantly(currentPosition + instantOffset);
    }

    //public void RotateTo(Quaternion rotation)
    //{
    //    currentRotation = rotation;
    //}

    //public void RotateOffset(Quaternion rotationOffset)
    //{
    //    currentRotation = transform.rotation * rotationOffset;
    //}


}

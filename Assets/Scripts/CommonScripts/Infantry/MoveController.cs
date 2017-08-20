using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody rigid;
    
    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 move)
    {
        if (rigid != null)
        {
            rigid.velocity = new Vector3(move.x, rigid.velocity.y, move.z);
        }
    }

    public void MoveTo(Vector3 worldPosition)
    {
        if (rigid != null)
        {
            
        }
    }

    public void LookAt(Quaternion targetRotation)
    {
        if (rigid != null)
        {
            transform.rotation = targetRotation;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{ 

    public bool IsDetected => _currentGround;
    private Vector2 _size;
    private Vector2 _offset;
    [SerializeField]

    private CapsuleCollider2D _col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator E_IgnoreGround(Collider2D ground)
    {
        _IgnoringGround _ground;
    }

    public void Awake()
    {

    }
    public void StopIgnoringGround()
    {
        if(_ignoringGround != null)
        {
            StopCoroutine(_ignoreCoroutin);
            Physics2D.IgnoreCollision
        }
    }

    private void FixedUpdate()
    {

    }

    private void OnDrawGizmos()
    {
        
    }
}

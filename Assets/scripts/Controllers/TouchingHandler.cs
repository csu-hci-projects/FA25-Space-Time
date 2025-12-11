using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingHandler : MonoBehaviour
{
    public ContactFilter2D castFilter;
    LayerMask wallMask;
    public float groundDistance = 0.05f;
    public float wallDistance = 0.2f;
    public float ceilingDistance = 0.05f;
    Animator animator;
    Collider2D touchCol;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    RaycastHit2D[] wallHits = new RaycastHit2D[5];
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    [SerializeField]
    private bool _isGrounded = true;
    private bool _isOnWall = false;
    private bool _isOnCeiling = false;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsGrounded { 
        get {
            return _isGrounded;
        } private set {
            _isGrounded = value;
            animator.SetBool(AnimStr.isGrounded,value);
        }
    }

    public bool IsOnWall { 
        get {
            return _isOnWall;
        } private set {
            _isOnWall = value;
            animator.SetBool(AnimStr.isOnWall, value);
        }
    }

    public bool IsOnCeiling { 
        get {
            return _isOnCeiling;
        } private set {
            _isOnCeiling = value;
            animator.SetBool(AnimStr.isOnCeiling, value);
        }
    }

    void Awake()
    {
        touchCol = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        wallMask = LayerMask.GetMask("Map", "Enemy", "Player");
        castFilter = new ContactFilter2D();
        castFilter.layerMask = wallMask;
        
    }

    void FixedUpdate()
    {
        IsGrounded = touchCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnCeiling = touchCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
        castFilter.useLayerMask = true;
        IsOnWall = touchCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        castFilter.useLayerMask = false;
    }
}

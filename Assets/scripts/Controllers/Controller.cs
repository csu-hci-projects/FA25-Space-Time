using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public int id;
    protected bool _isFacingRight = true;
    protected SortedList<int, GameObject> currentTouching;
    
    protected bool IsFacingRight { 
        get {
            return _isFacingRight;
        } private set {
            if(_isFacingRight != value) {
                transform.localScale *= new Vector2(-1,1);
            }
            _isFacingRight = value;
        }
    }

    protected void SetFacingDirection(float moveInput)
    {
        if (moveInput > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void SetTouching(GameObject other, int imp, bool b)
    {
        if (!b)
        {
            currentTouching.Remove(imp);
            return;
        }
        if (b && !currentTouching.ContainsKey(imp))
        {
            currentTouching.Add(imp, other);
            return;
        }
    }

    public void OnInteract(bool context)
    {
        if (currentTouching.Count != 0)
        {
            GameObject g = currentTouching.Values.Last();
            Interactable interact = g.GetComponent<Interactable>();
            if (context)
            {
                interact.OnInteract(id);
            }
            else
            {
                interact.OnCancelInteract(id);
            }
        }
        // If touching an interactable, interact with it.
    }
    public abstract void Die();
}

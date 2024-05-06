using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseRBController : MonoBehaviour
{
    #region Variables
    [Header("Base Properties")]
    [Tooltip("Weight in kg")]
    public float weight = 0f;

    protected Rigidbody rb;
    #endregion

    #region Built-in Methods
    public virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb)
        {
            rb.mass = weight;
        }
    }

    void FixedUpdate()
    {
        if (rb)
        {
            HandlePhysics();
        }
    }
    #endregion

    #region Custom Methods
    protected virtual void HandlePhysics() { }
    #endregion
}

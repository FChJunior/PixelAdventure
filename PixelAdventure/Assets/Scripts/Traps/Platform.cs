using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Slider Platform")]
    [SerializeField] private SliderJoint2D chain;
    [SerializeField] private SpriteRenderer platform;
    [SerializeField] private Transform follow;
    public Transform _follow { get { return follow; } }
    [SerializeField] private Rigidbody2D rig;
    [SerializeField] private Animator anim;
    [SerializeField] private bool horizontal;
    [SerializeField] private int typePlatform;
    public int _typePlatform { get { return typePlatform; } }
    [SerializeField] private Vector2 size;
    [SerializeField] private bool right;
    public bool _right { get { return right; } set { right = value; } }
    [SerializeField] private bool invert;
    [SerializeField][Range(0, 100)] private float speed;
    [SerializeField][Range(0, 90)] private float limits;

    private JointTranslationLimits2D translationLimits2D;
    private JointMotor2D motor2D;
    void Start()
    {
        Orientation();
        if (typePlatform == 0) anim.SetBool("Action", true);
        else if (typePlatform == 1) anim.SetBool("Action", false);

        motor2D.maxMotorTorque = 10000;
    }
    void Orientation()
    {
        if (horizontal)
        {
            size = new Vector2(2 * limits, 0.25f);
            chain.angle = 0;
            rig.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            size = new Vector2(0.25f, 2 * limits);
            chain.angle = 90;
            rig.constraints = RigidbodyConstraints2D.FreezePositionX;
            anim.SetBool("Action", false);
        }

        platform.size = size;
        rig.freezeRotation = true;
        translationLimits2D.max = limits;
        translationLimits2D.min = -limits;

        chain.limits = translationLimits2D;

    }
    void FixedUpdate()
    {
        Moved();
    }

    private void LateUpdate()
    {
        follow.position = transform.position;
    }
    void Moved()
    {
        if (typePlatform == 0)
        {
            if (chain.limitState == JointLimitState2D.UpperLimit && !invert)
                StartCoroutine(Invert());
            else if (chain.limitState == JointLimitState2D.LowerLimit && !invert)
                StartCoroutine(Invert());
        }
        else if (typePlatform == 1)
        {
            if (chain.limitState == JointLimitState2D.UpperLimit || chain.limitState == JointLimitState2D.LowerLimit)
                anim.SetBool("ON", false);
            else
                anim.SetBool("ON", true);
        }


        if (right) motor2D.motorSpeed = speed;
        else motor2D.motorSpeed = -speed;

        chain.motor = motor2D;
    }
    IEnumerator Invert()
    {
        invert = true;
        anim.SetBool("ON", false);
        yield return new WaitForSeconds(1f);
        anim.SetBool("ON", true);
        right = !right;
        yield return new WaitForSeconds(0.5f);
        invert = false;
    }

}

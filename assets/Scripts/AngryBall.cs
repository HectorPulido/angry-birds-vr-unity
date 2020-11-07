using UnityEngine;

public class AngryBall : MonoBehaviour
{

    private Rigidbody rb;
    private Vector3 distance;
    private TrailRenderer trail;

    public Line line;
    public float maxVelocity;
    public Transform pivot;
    public float springRange;
    public float distanceThreshold;
    public OVRInput.Controller controller;
    public Transform[] controllers;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();

        GoToInitialPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            MoveTowardsTarget();
        }
        else
        {
            SearchTarget();
        }

        if (InitialPositionIsPressed())
        {
            GoToInitialPosition();
        }
    }

    bool InitialPositionIsPressed()
    {
        return OVRInput.Get(OVRInput.Button.Three, controller);
    }
    bool PrimaryIndexTriggerIsPressed()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, controller) > 0.5f;
    }

    void GoToInitialPosition()
    {
        transform.position = pivot.position;
        rb.isKinematic = true;
        trail.enabled = false;
        line.birdInLine = true;
        trail.Clear();
    }

    void MoveTowardsTarget()
    {
        if (!PrimaryIndexTriggerIsPressed())
        {
            line.birdInLine = false;
            trail.enabled = true;
            rb.isKinematic = false;
            rb.velocity = -distance.normalized * maxVelocity * distance.magnitude / springRange;

            target = null;
            return;
        }

        distance = target.position - pivot.position;

        if (distance.magnitude > springRange)
        {
            distance = distance.normalized * springRange;
        }

        transform.position = distance + pivot.position;
    }

    void SearchTarget()
    {
        for (int i = 0; i < controllers.Length; i++)
        {
            if (Vector3.Distance(controllers[i].position, transform.position) < distanceThreshold)
            {
                if (PrimaryIndexTriggerIsPressed())
                {
                    target = controllers[i];
                    return;
                }
            }
        }
    }
}

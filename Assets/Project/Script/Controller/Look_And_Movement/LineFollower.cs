using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class LineFollower : MonoBehaviour
{
    [Header("Settings")]
    public float followSpeed = 2f, maxSpeed = 4f, rotateSpeed;
    public Transform vertexPathTransform;
    public float stopDistance;
    public bool stopped;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void Follow(LineRenderer lineR)
    {
        Vector3[] poses = new Vector3[lineR.positionCount];
        lineR.GetPositions(poses);
        BezierPath bezier = new BezierPath(poses);
        VertexPath path = new VertexPath(bezier, vertexPathTransform, 0.1f);
        StartCoroutine(Following(path));
    }

    IEnumerator Following(VertexPath path)
    {
        Vector3 lastPoint = path.GetPointAtTime(1f, EndOfPathInstruction.Stop);
        float befDist = 0.5f;
        if (stopped == false)
        {
            while (DistanceXZ(transform.position, lastPoint) > 0.5f) {
                float distance = Vector3.Distance(this.transform.position, lastPoint);

                if (distance < stopDistance)
                {
                    stopped = true;
                    body.velocity = Vector3.zero;
                }

                body.velocity = Vector3.ClampMagnitude(body.velocity, maxSpeed);
                body.AddForce(transform.forward * followSpeed, ForceMode.VelocityChange);
                float dist = path.GetClosestDistanceAlongPath(transform.position);
                if (dist < befDist)
                    dist = befDist;
                else
                    befDist = dist;

                Vector3 nextPoint = path.GetPointAtDistance(dist + followSpeed, EndOfPathInstruction.Stop);
                Vector3 diff = nextPoint - transform.position;
                diff.y = 0;
                diff.Normalize();
                Debug.DrawLine(transform.position, nextPoint, Color.red);
                Debug.DrawRay(transform.position, diff, Color.gray);
                float angle = Mathf.Atan2(diff.x, diff.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), rotateSpeed * Time.fixedDeltaTime);
                yield
                return new WaitForFixedUpdate();
            }
            body.velocity = Vector3.zero;
        }

    }

    float DistanceXZ(Vector3 v1, Vector3 v2)
    {
        v1.y = 0;
        v2.y = 0;
        return Vector3.Distance(v1, v2);
    }

    public void stopPlayer()
    {
        maxSpeed = 0f;
        followSpeed = 0f;
    }

}
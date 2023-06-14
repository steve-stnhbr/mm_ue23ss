using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject follow;
    [SerializeField] Vector3 positionDiff;

    private void FixedUpdate()
    {
        transform.position = follow.transform.position + positionDiff;
    }

}

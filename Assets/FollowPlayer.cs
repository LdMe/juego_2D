using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform objective;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - objective.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objective.transform.position + offset;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonBehaviour : Switch
{
    public GameObject[] relativeGameObjects;

    public bool moveCollidedExplicitly;

    Vector3[] relativePositions;
    Vector3 positionBefore;

    List<GameObject> collided;

    Animator animator;

    protected override void DoWhileOffFixed()
    {
    }

    protected override void DoWhileOnFixed()
    {
    }

    protected override void SwitchOff(EnumActor actor)
    {
        Debug.Log("Off");
        animator.SetBool("Active", false);
    }

    protected override void SwitchOn(EnumActor actor)
    {
        Debug.Log("On");
        animator.SetBool("Active", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        collided = new List<GameObject>();
        animator = GetComponentInParent<Animator>();
        relativePositions = new Vector3[relativeGameObjects.Length];
        for (int i = 0; i < relativeGameObjects.Length; i++)
        {
            relativePositions[i] = relativeGameObjects[i].transform.position - transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != positionBefore)
        {
            for (int i = 0; i < relativeGameObjects.Length; i++)
            {
                relativeGameObjects[i].transform.position = relativePositions[i] + transform.position;
            }

            foreach (GameObject gameObject in collided)
            {
                gameObject.transform.position += transform.position - positionBefore;
            }
        }
        positionBefore = transform.position;
    }

    public void OnCollisionEnter(Collision collision)
    {
        collided.Add(collision.gameObject);
    }

    public void OnCollisionExit(Collision collision)
    {
        collided.Remove(collision.gameObject);
    }
}

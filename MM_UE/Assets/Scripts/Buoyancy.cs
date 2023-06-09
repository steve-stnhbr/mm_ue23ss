using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour
{
    public Transform[] floaters;
    public float underWaterDrag = 3f;
    public float underWaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = .05f;
    public float floatingPower = 15f;
    bool underwater;
    int floatersUnderwater;
    public float waterHeight;

    public bool sinkOnPlayerCollision;
    public float sinkingSpeed = 5;
    bool sinking;
    float initialFloatingPower;
    public int floatIncreaseSteps = 15;
    public int floatIncreaseAmount = 25;

    public Vector2 waveForceRange = new Vector2(-.5f, .5f);
    public float waveForceStrength = .3f;

    float noiseOffset;

    // Start is called before the first frame update
    void Start()
    {
        initialFloatingPower = floatingPower;
        noiseOffset = Random.Range(-1, 1);
    }

    void FixedUpdate()
    {
        if (sinking)
        {
            floatingPower *= (1/sinkingSpeed);
        }
        else if (floatingPower < initialFloatingPower)
        {
            floatingPower += floatIncreaseAmount;
            //floatingPower *= floatIncreaseAmount;
        }
        floatersUnderwater = 0;
        foreach (Transform floater in floaters) {
            float diff = floater.position.y - waterHeight;
            if (diff < 0)
            {
                Vector3 force = Vector3.up * floatingPower * Mathf.Abs(diff);
                GetComponent<Rigidbody>().AddForceAtPosition(force, floater.position, ForceMode.Force);
                floatersUnderwater++;
                if (!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }
            }
            else if (underwater && floatersUnderwater == 0)
            {
                underwater = false;
                SwitchState(false);
            }
        }
        GetComponent<Rigidbody>().AddForceAtPosition(
            new Vector3(0, Mathf.PerlinNoise(Time.time, noiseOffset) * floatingPower * waveForceStrength, 0),
            transform.position + new Vector3(
                Unity.Mathematics.math.remap(0, 1, waveForceRange.x * transform.localScale.x, waveForceRange.y * transform.localScale.x, Mathf.PerlinNoise(transform.position.x + noiseOffset, Time.time)), 
                0,
                Unity.Mathematics.math.remap(0, 1, waveForceRange.x * transform.localScale.x, waveForceRange.y * transform.localScale.x, Mathf.PerlinNoise(Time.time, transform.position.z + noiseOffset))
            ),
            ForceMode.Force
        );
    }

    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            GetComponent<Rigidbody>().drag = underWaterDrag;
            GetComponent<Rigidbody>().angularDrag = underWaterAngularDrag;
        } 
        else
        {
            GetComponent<Rigidbody>().drag = airDrag;
            GetComponent<Rigidbody>().angularDrag = airAngularDrag;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (sinkOnPlayerCollision && collision.gameObject.layer == LayerMask.NameToLayer("HumanPlayer"))
        {
            sinking = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (sinkOnPlayerCollision && collision.gameObject.layer == LayerMask.NameToLayer("HumanPlayer"))
        {
            sinking = false;
            floatingPower = initialFloatingPower / (floatIncreaseAmount * floatIncreaseSteps); // * Mathf.Pow(1/floatIncreaseAmount, floatIncreaseSteps);
            Debug.Log("Set floatingPower to " + floatingPower + " from " + initialFloatingPower / (floatIncreaseAmount * floatIncreaseSteps));
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class DashEffect : MonoBehaviour
{

    TrailRenderer trailRenderer;
    private void Awake()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    public void SetEnabled(bool enabled) => trailRenderer.enabled = enabled;

}

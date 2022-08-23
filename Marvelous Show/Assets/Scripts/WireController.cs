using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    [SerializeField]
    public bool connected = false;
    [SerializeField]
    private GameObject wireSprite;

    [SerializeField, Range(0f, 1f)]
    private float maxDistanceToClick = 0.5f;
    [SerializeField, Range(0f, 1f)]
    private float snapDistance = 2f;

    [SerializeField, Range(0f, 100f)]
    private float scaleDownScalingUpFactor = 0.5f;
    private float distanceOfClick = 0f;
    private Vector2 worldMouse = Vector2.zero;
    private bool holding = false;
    private Material wireSpriteMaterial;

    private float cosa;
    [SerializeField]
    private Transform wireStart;
    [SerializeField]
    private Transform wireEnd;

    private void Start()
    {
        wireSpriteMaterial = wireSprite.GetComponent<Renderer>().material;
    }

    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
            UpdateClickVariables();
        }

        if (Input.GetMouseButtonDown(0))
        {
            UpdateClickVariables();
            if (distanceOfClick < maxDistanceToClick)
            {
                holding = true;
                connected = false;
            }
        }

        if (Input.GetMouseButtonUp(0) && holding)
        {
            holding = false;
            if (Vector2.Distance(worldMouse, wireEnd.position) <= snapDistance)
            {
                UpdateScale(wireEnd.position);
                UpdatePosition(wireEnd.position);
                UpdateRotation(wireEnd.position);
                connected = true;
            }
        }

        if (holding)
        {
            UpdateScale(worldMouse);
            UpdatePosition(worldMouse);
            UpdateRotation(worldMouse);
        }
    }

    private void UpdateScale(Vector2 endPosition)
    {
        var distanceOfClick = Vector2.Distance(endPosition, wireStart.position);
        var currentScale = wireSprite.transform.localScale;
        var scaledDistance = distanceOfClick * scaleDownScalingUpFactor;
        wireSprite.transform.localScale = new Vector3(scaledDistance, currentScale.y, currentScale.z);
        wireSpriteMaterial.mainTextureScale = new Vector2(scaledDistance, 1f);
    }

    private void UpdatePosition(Vector2 endPosition)
    {
        var wireStartPosition = wireStart.position;
        wireSprite.transform.position = new Vector2((wireStartPosition.x + endPosition.x) / 2f, (wireStartPosition.y + endPosition.y) / 2f);
    }

    private void UpdateRotation(Vector2 endPosition)
    {
        Vector2 wireStartDirection = (((Vector2)wireStart.position) - endPosition).normalized;
        cosa = Vector2.Dot(wireStartDirection, Vector2.right);
        var degrees = Mathf.Acos(cosa) * Mathf.Rad2Deg;
        degrees = Vector2.Dot(wireStartDirection, Vector2.up) < 0 ? -degrees : degrees;
        wireSprite.transform.rotation = Quaternion.Euler(0f, 0f, degrees);
    }
    private void UpdateClickVariables()
    {
        worldMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        distanceOfClick = Vector2.Distance(worldMouse, wireStart.position);
    }
}

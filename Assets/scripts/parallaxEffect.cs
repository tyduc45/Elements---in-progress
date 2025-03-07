using UnityEngine;

public class parallaxEffect : MonoBehaviour
{
    // bind camera
    public Camera cam;

    // bind target
    public Transform followTarget;

    //starting position of background
    Vector2 startingPosition;

    // starting z position (depth)
    float startingZ;
    // distance between background and target
    float zBackgroundToTarget => transform.position.z - followTarget.transform.position.z;
    // how much has camera moved since the start point in the background
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    // if in the back , then it should be fazr plane, other wise is the near plane
    float clipPlane => cam.transform.position.z + (zBackgroundToTarget > 0 ? cam.farClipPlane: cam.nearClipPlane);

    // calculate parallax factor
    float parallaxFactor => Mathf.Abs(zBackgroundToTarget) / clipPlane;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        // calculate background position, with differnet speed
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;
        //update background position
        transform.position = new Vector3(newPosition.x, startingPosition.y, startingZ);
    }
}

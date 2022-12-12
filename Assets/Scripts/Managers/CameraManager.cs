using System.Linq;
using UnityEngine;

public class CameraManager: MonoBehaviour
{
    private static Transform target;
    private static bool isPovThirdPerson;
    private static Vector3 back;
    private static float minDistance, maxDistance, currDistance, sliderDistance;
    private static float minHeight, maxHeight, currHeight, sliderHeight;
    private static Collider[] currColliders;

    void Awake()
    {
        // The camera is the target's child as to inherit its position
        target = GameObject.FindGameObjectWithTag("Player").transform;
        transform.SetParent(target);
    }

    void Start()
    {
        isPovThirdPerson = true;

        minDistance = -0.2f;
        maxDistance = 4f;
        currDistance = maxDistance;
        sliderDistance = (maxDistance - minDistance) / 5;

        minHeight = 1.8f;
        maxHeight = 2.5f;
        currHeight = maxHeight;
        sliderHeight = (maxHeight - minHeight) / 5;

        currColliders = new Collider[10];
    }

    void LateUpdate()
    {
        int i;
        bool anyColliderGotNulled = false;

        back = -target.forward * currDistance;
        back.y = currHeight;
        transform.position = target.position + back;

        // Reset the values once all the collided with objects are far enough
        if (isPovThirdPerson && currDistance != maxDistance)
        {
            if (currColliders[0] == null)
            {
                currDistance = maxDistance;
                currHeight = maxHeight;
            }
            else
            {
                for (i = 0; i < currColliders.Length; ++i)
                {
                    if (currColliders[i] == null)
                        break;
                    else if (Vector3.Distance(currColliders[i].transform.position, transform.position) > 13f)
                    {
                        currColliders[i] = null;
                        anyColliderGotNulled = true;
                    }
                }

                if (anyColliderGotNulled)
                    currColliders = currColliders.OrderBy(e => e != null).ToArray();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPovThirdPerson && other.CompareTag("CameraCollide") && currDistance > 0f)
        {
            // The minimum values put the camera in 1st person POV
            currDistance -= sliderDistance;
            currHeight -= sliderHeight;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        int i;
        if (isPovThirdPerson && other.CompareTag("CameraCollide"))
        {
            // Add new colliding object to array
            for (i = 0; i < currColliders.Length; ++i)
            {
                if (currColliders[i] != null)
                {
                    if (currColliders[i].name == other.name)
                        break;
                }
                else
                    currColliders[i] = other;
            }
        }
    }

    public static void SwitchCameraMode()
    {
        isPovThirdPerson = !isPovThirdPerson;

        if (!isPovThirdPerson)
        {
            currDistance = minDistance;
            currHeight = minHeight;
        }
    }
}

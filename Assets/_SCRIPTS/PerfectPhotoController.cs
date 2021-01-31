using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectPhotoController : MonoBehaviour
{
    float m_MaxDistance;
    bool m_HitDetect;

    Collider m_Collider;
    RaycastHit m_Hit;

    void Start()
    {
        //Choose the distance the Box can reach to
        m_MaxDistance = 100f;
        m_Collider = GetComponent<Collider>();
    }

    void FixedUpdate()
    {
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale, transform.forward, out m_Hit, transform.rotation, m_MaxDistance);
        if (m_HitDetect)
        {
            //Output the name of the Collider your Box hit
            //Debug.Log("Hit : " + m_Hit.collider.name);
            GameManager.Instance.dotCenter.color = Color.green;
        }
        else
        {
            GameManager.Instance.dotCenter.color = Color.white;
        }
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, transform.localScale);
        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, transform.localScale);
        }
    }

    public void CaptureGhost()
    {
        if (m_Hit.collider != null)
        {
            if (m_Hit.collider.CompareTag("Ghost"))
            {
                GameManager.Instance.CaptureGhost(false);
            }
            else if (m_Hit.collider.CompareTag("Celebrity"))
            {
                GameManager.Instance.CaptureGhost(true);
            }
            Destroy(m_Hit.collider.gameObject);
        }
    }

    public void DestroyGhost()
    {
        if (m_Hit.collider != null)
        {
            if (m_Hit.collider.CompareTag("Ghost"))
            {
                GameManager.Instance.DestroyGhost(false);
            }
            else if (m_Hit.collider.CompareTag("Celebrity"))
            {
                GameManager.Instance.DestroyGhost(true);
            }
            Destroy(m_Hit.collider.gameObject);
        }
    }
}

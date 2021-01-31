using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeShotController : MonoBehaviour
{
    public Vector3 target = new Vector3();

    float speed = 30.0f;

    bool calledSmoke = false;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Check if the position of the cube and sphere are approximately equal.
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            // Swap the position of the cylinder.
            target *= -1.0f;
            if (!calledSmoke)
            {
                calledSmoke = true;
                GameManager.Instance.AddSmoke();
            }
            
        }
    }
}

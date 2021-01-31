using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private Transform target;

    private float movementDuration = 2.0f;
    private float waitBeforeMoving = 2.0f;
    private bool hasArrived = false;

    public SmokeShotController smokeAttack;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);

        if (!hasArrived)
        {
            hasArrived = true;
            Vector3 area = SpawnController.Instance.size;
            Vector3 instPosition = transform.position + new Vector3(
                Random.Range(-area.x / 2, area.x / 2),
                Random.Range(-area.y / 2, area.y / 2),
                Random.Range(-area.z / 2, area.z / 2));
            StartCoroutine(MoveToPoint(instPosition));
        }
    }

    private IEnumerator MoveToPoint(Vector3 targetPos)
    {
        float timer = 0.0f;
        Vector3 startPos = transform.position;

        while (timer < movementDuration)
        {
            timer += Time.deltaTime;
            float t = timer / movementDuration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            transform.position = Vector3.Lerp(startPos, targetPos, t);

            yield return null;
        }

        if (Random.Range(0f, 1f) > 0.5f)
        {
            ShootSmoke();
        }
        

        yield return new WaitForSeconds(waitBeforeMoving);
        hasArrived = false;
    }

    void ShootSmoke()
    {
        SmokeShotController smoke = Instantiate(smokeAttack, transform.position, Quaternion.identity);
        smoke.target = target.position;
        smoke.transform.SetParent(GameManager.Instance.GameParent);
    }
}

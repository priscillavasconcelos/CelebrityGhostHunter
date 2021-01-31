using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private static SpawnController _instance;
    public static SpawnController Instance { get { return _instance; } }

    [SerializeField] List<GhostController> ghosts = new List<GhostController>();

    [SerializeField] internal Vector3 size;

    [SerializeField] Transform cam;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnGhost", 0f, 6f);
    }

    void SpawnGhost()
    {
        Vector3 instPosition = transform.position + new Vector3(
                Random.Range(-size.x / 2, size.x / 2), 
                Random.Range(-size.y / 2, size.y / 2), 
                Random.Range(-size.z / 2, size.z / 2));

        GhostController ghost = Instantiate(ghosts[Random.Range(0, ghosts.Count)], instPosition, Quaternion.identity);
        ghost.SetTarget(cam);
        ghost.transform.SetParent(GameManager.Instance.GameParent);

        GameManager.Instance.AddGhostsInRoom();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawCube(transform.position, size);
    }

}

using UnityEngine;

public class RoadScript : MonoBehaviour
{
    private static Transform player;
    private static GameObject prevRoad;
    private static GameObject road;

    [SerializeField] private GameObject roadPrefab;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        if(prevRoad == null && gameObject.name == "Floor2D")
        {
            prevRoad = gameObject;
        }
        else
        {
            road = gameObject;
        }
    }

    private void Update()
    {
        if((player.position.x - transform.position.x) > 100f)
        {
            print("need to spawn new road");
            print(prevRoad.name);
            print(road.name);
            if (prevRoad == gameObject)
            {
                prevRoad = road;
                road = Instantiate(roadPrefab);

                road.transform.position = new Vector3(prevRoad.transform.position.x + 100f, prevRoad.transform.position.y, prevRoad.transform.position.z);

                Destroy(gameObject);
            }
        }
    }
}

using UnityEngine;

public class RoadScript : MonoBehaviour
{
    private static Transform player;
    private static Vector3 prevRoad;
    private static GameObject prevRoadObject;
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
            prevRoad = gameObject.transform.position;
        }
        else
        {
            road = gameObject;
        }
    }

    private void Update()
    {
        if (player == null){
            player = Camera.main.GetComponent<CameraController>().sidePlayer.transform;
        }

        if((player.position.x - transform.position.x) > 100f)
        {
            print("need to spawn new road" + prevRoadObject.name/* + " " + road.name*/);
            if (prevRoadObject == gameObject)
            {
                prevRoadObject = road;
                prevRoad = road.transform.position;
                road = Instantiate(roadPrefab);

                road.transform.position = new Vector3(prevRoad.x + 100f, prevRoad.y, prevRoad.z);

                Debug.Log(road.name);

                Destroy(gameObject);
            }
        }
    }
}

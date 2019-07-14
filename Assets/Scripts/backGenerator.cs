using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] availableRooms;
    public List<GameObject> currentRooms;
    private float screenWidthInPoints;


    public GameObject[] availableObjects;
    public List<GameObject> objects;

    public float objectsMinDistance = 5.0f;
    public float objectsMaxDistance = 10.0f;

    public float objectsMinY = -1.4f;
    public float objectsMaxY = 1.4f;

    void Start()
    {
        float height = 2.0f * Camera.main.orthographicSize;
        screenWidthInPoints = height * Camera.main.aspect;

        StartCoroutine(GeneratorCheck());


    }

    // Update is called once per frame
    void Update()
    {

    }

    void AddRoom(float farthestRoomEndX)
    {
        
        int randomRoomIndex = Random.Range(0, availableRooms.Length);
        GameObject room = (GameObject)Instantiate(availableRooms[randomRoomIndex]);        
        float roomWidth = room.transform.Find("Ground").localScale.x;
        float roomCenter = farthestRoomEndX + roomWidth * 0.5f;       
        room.transform.position = new Vector3(roomCenter, 0, 0);
        currentRooms.Add(room);
    }

    private void GenerateRoomIfRequired()
    {
        
        List<GameObject> roomsToRemove = new List<GameObject>();
        bool addRooms = true;
        float playerX = transform.position.x;
        //Debug.Log("Pos " + playerX);
        float removeRoomX = playerX - screenWidthInPoints;
        float addRoomX = playerX + screenWidthInPoints;
        float farthestRoomEndX = 0;
        foreach (var room in currentRooms)
        {
            float roomWidth = room.transform.Find("Ground").localScale.x;
            float roomStartX = room.transform.position.x ;
            Debug.Log("roomStartX" + roomStartX);
            float roomEndX = roomStartX + 166f;
            if (roomStartX > addRoomX)
            {
                addRooms = false;
            }
            if (roomEndX < removeRoomX)
            {
                roomsToRemove.Add(room);
            }
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndX);
        }
        foreach (var room in roomsToRemove)
        {
            currentRooms.Remove(room);
            Destroy(room);
        }
        if (addRooms)
        {
            AddRoom(farthestRoomEndX);
        }
    }
    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateRoomIfRequired();
            GenerateObjectsIfRequired();

            yield return new WaitForSeconds(0.25f);
        }
    }

    void AddObject(float lastObjectX)
    {

        int randomIndex = Random.Range(0, availableObjects.Length);
        GameObject obj = (GameObject)Instantiate(availableObjects[randomIndex]);
        float objectPositionX = lastObjectX + Random.Range(objectsMinDistance, objectsMaxDistance);
        float randomY = Random.Range(objectsMinY, objectsMaxY);
        obj.transform.position = new Vector3(objectPositionX, randomY, 0);
        objects.Add(obj);
    }

    void GenerateObjectsIfRequired()
{
    
    float playerX = transform.position.x;
    float removeObjectsX = playerX - screenWidthInPoints;
    float addObjectX = playerX + screenWidthInPoints;
    float farthestObjectX = 0;
    
    List<GameObject> objectsToRemove = new List<GameObject>();
    foreach (var obj in objects)
    {
        
        float objX = obj.transform.position.x;
        farthestObjectX = Mathf.Max(farthestObjectX, objX);
        if (objX < removeObjectsX) 
        {           
            objectsToRemove.Add(obj);
        }
    }
    foreach (var obj in objectsToRemove)
    {
        objects.Remove(obj);
        Destroy(obj);
    }
    if (farthestObjectX < addObjectX)
    {
        AddObject(farthestObjectX);
    }
}


}

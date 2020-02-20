using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorObjManager : MonoBehaviour
{
    public int totalObjects = 100;
    int currentObjects = 0;
    public Vector2 spawnRadiusExtents;
    public GameObject[] mySpawnables;
    [Range(0, 100)] public float[] spawnableSaturation;
    float totalSaturation = 0;
    GameObject player;
    Vector3 centrePoint;
    [SerializeField] private List<GameObject> myObjList;
    Collider myCol;

    private void Start() {
        myCol = GetComponent<Collider>();
        centrePoint = Camera.main.transform.position;
        //SpawnObjectsInit();
    }

    void GetSaturation() {
        for (int i = 0; i < spawnableSaturation.Length; i++) {
            totalSaturation += spawnableSaturation[i];
        }
    }
    public void PlayerCollision(RaycastHit floor) {
        //centrePoint = floor.point;
        centrePoint = Camera.main.transform.position;
        //Debug.Log("It Worked!");
        RemoveObjects();
        SpawnObjects();
    }

    /*void SpawnObjectsInit() {
         while (currentObjects < totalObjects) {
             //spawn object at position Vector3(rand inside radius, height of terrain at this point, rand inside radius);
             GameObject mySpawningObj = MySpawningObject();
             Vector2 myHoriz = MyHorizontalPositionInit();
             float mySpawnHeight = MySpawnHeight(myHoriz);
             Vector3 mySpawnPos = new Vector3(myHoriz.x, mySpawnHeight, myHoriz.y);
             SpawnAndAddList(mySpawningObj, mySpawnPos);
             currentObjects++;
         }
     }

     Vector2 MyHorizontalPositionInit() {
         Vector2 pos;
         Vector3 dir;
         float dist = Random.Range(0, spawnRadiusExtents.y);
         dir = new Vector3(
             centrePoint.x + Random.Range(-1, 1),
             0,
             centrePoint.z + Random.Range(-1, 1)
             );
         dir = Vector3.Normalize(dir - centrePoint) * dist;
         pos = new Vector2(centrePoint.x + dir.x, centrePoint.z + dir.z);
         return pos;
     }*/

    void SpawnObjects() {
        //create objects within radius - radius should be larger than fog.
        //add all objects to myObjList within radius.
        while (currentObjects < totalObjects) {
            //spawn object at position Vector3(rand inside radius, height of terrain at this point, rand inside radius);
            GameObject mySpawningObj = MySpawningObject();
            Vector2 myHoriz = MyHorizontalPosition();
            float mySpawnHeight = MySpawnHeight(myHoriz);
            Vector3 mySpawnPos = new Vector3(myHoriz.x, mySpawnHeight, myHoriz.y);

            /*
            float objX = mySpawningObj.GetComponent<Collider>().bounds.extents.x;
            float objZ = mySpawningObj.GetComponent<Collider>().bounds.extents.z;
            for (int i = 0; i < myObjList.Count; i++) {
                Vector3 myPlace = myObjList[i].transform.position;
                float myX = myObjList[i].GetComponent<Collider>().bounds.extents.x;
                float myZ = myObjList[i].GetComponent<Collider>().bounds.extents.z;
                if (!(mySpawnPos.x + objX > myPlace.x + myX ||
                    mySpawnPos.z + objZ > myPlace.z + myZ ||
                    mySpawnPos.x - objX > myPlace.x - myX ||
                    mySpawnPos.z - objZ > myPlace.z - myZ)) {
                    mySpawnPos.x -= 2000f;
                    mySpawningObj.name = "Underworld " + mySpawningObj.name;
                    break;
                }

            }
            */

            SpawnAndAddList(mySpawningObj, mySpawnPos);
            currentObjects++;
        }
    }

    void SpawnAndAddList(GameObject spawn, Vector3 pos) {
        GameObject myObj =
        Instantiate(spawn, pos, Quaternion.identity);
        myObjList.Add(myObj);
    }

    GameObject MySpawningObject() {
        GetSaturation();
        GameObject resultObj;
        int n = 0;
        float[] mySatPercentage = new float[spawnableSaturation.Length];
        for (int i = 0; i < mySpawnables.Length; i++) {
            float mySelSat = spawnableSaturation[i];
            mySatPercentage[i] = ((mySelSat / totalSaturation) * Random.Range(0f, totalSaturation));
            //Debug.Log ((mySelSat / totalSaturation) + Random.Range(0, totalSaturation));
            //Debug.Log(mySatPercentage[i]);
        }
        for (int i = 0; i < mySatPercentage.Length; i++) {
            //if x[n] > totalSaturation
            if (mySatPercentage[i] > mySatPercentage[n]) {
                n = i;
            }
            // Debug.Log(n);
        }
        resultObj = mySpawnables[n];
        return resultObj;
    }

    Vector2 MyHorizontalPosition() {
        Vector2 pos;
        Vector3 spawnVector;

        //Pick a random direction.
        Vector3 directionVector = Vector3.zero;
        while (directionVector == Vector3.zero) {
            directionVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        }
        directionVector = directionVector.normalized;
        //the distance based on the radius extents expressed as "magnitude"
        float magnitude = Random.Range(spawnRadiusExtents.x, spawnRadiusExtents.y);

        spawnVector = directionVector * magnitude;
        pos = new Vector2(centrePoint.x + spawnVector.x, centrePoint.z + spawnVector.z);
        return pos;

        /*
         *                                                    * Deprecated - used online guide instead. Is now working.
         *                                                   THIS ISN'T WORKING FOR SOME REASON D:
         *                                                   For example: centrePoint = Vector3(10, 2, -15);
         *                                                   spawnRadiusExtents.x = 20
         *                                                   spawnRadiusExtents.y = 40
         *                                                   Vector3 pos3;
         *                                                   Vector3 dir;
         *                                                   float dist = Random.Range(spawnRadiusExtents.x, spawnRadiusExtents.y);
         *                                                   dir = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1));
         *                                                   dir = dir.normalized * dist;
         *                                                   pos3 = centrePoint + dir;
         *                                                   pos = new Vector2(pos3.x, pos3.z);
         *                                                   pos = new Vector2(centrePoint.x + dir.x, centrePoint.z + dir.z);
        */
    }

    float MySpawnHeight(Vector2 terrainPos) {
        //This is working fine :>
        float height;
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(terrainPos.x, centrePoint.y + spawnRadiusExtents.x, terrainPos.y), Vector3.down);
        myCol.Raycast(ray, out hit, 2 * spawnRadiusExtents.y);
        if (hit.collider == myCol) {
            height = hit.point.y;
        }
        else {
            height = centrePoint.y - spawnRadiusExtents.y;
        }
        return height;
    }

    void RemoveObjects() {
        //this is.... working??? I can't tell until I fix the horizontal thing.
        for (int i = 0; i < myObjList.Count; i++) {
            //Debug.Log(Mathf.Abs(Vector3.Magnitude(centrePoint - myObjList[i].transform.position)));
            if (Mathf.Abs(Vector3.Magnitude(centrePoint - myObjList[i].transform.position)) > spawnRadiusExtents.x) {
                //Debug.Log(Mathf.Abs(Vector3.Magnitude(myObjList[i].transform.position + centrePoint)));
                GameObject toDestroy = myObjList[i];
                myObjList.Remove(toDestroy);
                Destroy(toDestroy);
                currentObjects--;
            }
            //Debug.Log(currentObjects);
        }
        //remove objects outside and greater than spawnRadiusExtents.x and lower than -spawnRadiusExtents.x - inner radius should be larger than fog. (perhaps only remove objects thataren't actively bieng rendered?)
        //Remove all objects that aren't bieng rendered and are outside the radius
    }
}

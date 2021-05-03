using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Ghost : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] movementPoints;
    bool differencePointsSaw = true;
    Vector3 differencePoints;
    int differencePointsCount;
    bool turnBack;
    void Start()
    {
        movementPoints = new GameObject[transform.childCount];
        for (int i = 0; i < movementPoints.Length; i++)
        {
            movementPoints[i] = transform.GetChild(0).gameObject;
            movementPoints[i].transform.SetParent(transform.parent); //MovementPoint leri objeden dışarı çıkarmmaızı sağlar.
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movementWayPoints();
    }

    void movementWayPoints()
    {
        if (differencePointsSaw)
        {
            differencePoints = (movementPoints[differencePointsCount].transform.position - transform.position).normalized;
            differencePointsSaw = false;
        }
        float difference = Vector3.Distance(transform.position, movementPoints[differencePointsCount].transform.position);
        transform.position += differencePoints * Time.deltaTime * 10;

        if (difference < 0.5f)
        {
            differencePointsSaw = true;
            if (differencePointsCount == movementPoints.Length - 1)
            {
                turnBack = false;
            }
            else if (differencePointsCount == 0)
            {
                turnBack = true;
            }
            if (turnBack)
            {
                differencePointsCount++;
            }
            else
            {
                differencePointsCount--;
            }
        }

    }

#if UNITY_EDITOR
    void OnDrawGizmos() 
    {
        for (int i = 0; i < transform.childCount ; ++i)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.GetChild(i).transform.position, 1);
        }
        for (int i = 0; i < transform.childCount - 1 ; ++i) 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i+1).transform.position);
        }
   
    }
#endif

}

#if UNITY_EDITOR
[CustomEditor(typeof(Saw))]
[System.Serializable]
class GhostEditor : Editor
{
    public override void OnInspectorGUI() //
    {
        Saw script = (Saw)target;
        if (GUILayout.Button("Add", GUILayout.MinWidth(100),GUILayout.Width(100))) //BUTTON SIZE
        {
            GameObject newGameObject = new GameObject();
            newGameObject.transform.parent = script.transform; // saw ın altına gameobject yaratmamızı sağladı
            newGameObject.transform.position = script.transform.position;
            newGameObject.name = script.transform.childCount.ToString(); // 1-2-3 şeklinde arttırmamızı sağlar
        }
    }
    
}
#endif
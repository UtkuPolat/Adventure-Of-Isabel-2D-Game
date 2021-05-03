using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject[] movementPoints;
    bool differencePointsSaw = true;
    Vector3 differencePoints;
    int differencePointsCount;
    int speed = 5;
    bool turnBack;

    GameObject character;
    public GameObject bullet;
    float fireTime = 0;
    RaycastHit2D raycastHit2D;

    public Sprite frontSide;
    public Sprite backSide;
    SpriteRenderer spriteRenderer;
    public LayerMask layerMask;
    void Start()
    {
        movementPoints = new GameObject[transform.childCount];
        character = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < movementPoints.Length ; i++)
        {
            movementPoints[i] = transform.GetChild(0).gameObject;
            movementPoints[i].transform.SetParent(transform.parent); //MovementPoint leri objeden dışarı çıkarmmaızı sağlar.
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        enemySawPlayer();
        if (raycastHit2D.collider.tag == "Player") 
        {
            speed = 8;
            spriteRenderer.sprite = frontSide;
            fireToPlayer();
        }
        else
        {
            speed = 4;
            spriteRenderer.sprite = backSide;
        }
        movementWayPoints();
    }

    void fireToPlayer() 
    {
        fireTime += Time.deltaTime;
        if (fireTime > Random.Range(0.2f,1))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            fireTime = 0;
        }
    }

    void enemySawPlayer()
    {
        Vector3 rayLookingSide = character.transform.position - transform.position;
        raycastHit2D = Physics2D.Raycast(transform.position, rayLookingSide, 1000, layerMask);
        Debug.DrawLine(transform.position, raycastHit2D.point, Color.magenta);
         
    }

    void movementWayPoints() 
    {
        if (differencePointsSaw)
        {
            differencePoints = (movementPoints[differencePointsCount].transform.position - transform.position).normalized;
            differencePointsSaw = false;
        }
        float difference = Vector3.Distance(transform.position, movementPoints[differencePointsCount].transform.position);
        transform.position += differencePoints * Time.deltaTime * speed;

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
            if(turnBack)
            {
                differencePointsCount++;
            }
            else
            {
                differencePointsCount--;
            }
        }
        
    }

    public Vector2 getDirection()
    {
        return (character.transform.position - transform.position).normalized; 
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
[CustomEditor(typeof(Enemy))]
[System.Serializable]
class EnemyEditor : Editor
{
    public override void OnInspectorGUI() //
    {
        Enemy script = (Enemy)target;
        if (GUILayout.Button("Add", GUILayout.MinWidth(100),GUILayout.Width(100))) //BUTTON SIZE
        {
            GameObject newGameObject = new GameObject();
            newGameObject.transform.parent = script.transform; // saw ın altına gameobject yaratmamızı sağladı
            newGameObject.transform.position = script.transform.position;
            newGameObject.name = script.transform.childCount.ToString(); // 1-2-3 şeklinde arttırmamızı sağlar
        }
        EditorGUILayout.PropertyField(serializedObject.FindProperty("layerMask")); // dışarı değişken açmak için editör koduna yazılmalı
        EditorGUILayout.PropertyField(serializedObject.FindProperty("frontSide"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("backSide"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bullet"));
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
    
}
#endif
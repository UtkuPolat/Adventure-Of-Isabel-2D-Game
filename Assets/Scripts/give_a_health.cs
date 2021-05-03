using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class give_a_health : MonoBehaviour
{
    public Sprite []animationcube;
    SpriteRenderer spritebil;
    float time = 0;
    int animationcubecount = 0;
    // Start is called before the first frame update
    void Start()
    {
        spritebil = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.1f)
        {
            spritebil.sprite = animationcube[animationcubecount++];
            if (animationcube.Length==animationcubecount)
            {
                animationcubecount = animationcube.Length-1;
            }
            time = 0;
        }
    }
}

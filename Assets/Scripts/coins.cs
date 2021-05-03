using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coins : MonoBehaviour
{
    public Sprite[] animationcube;
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
        if (time > 0.03f)
        {
            spritebil.sprite = animationcube[animationcubecount++];
            if (animationcube.Length == animationcubecount)
            {
                animationcubecount = 0;
            }
            time = 0;
        }
    }
}

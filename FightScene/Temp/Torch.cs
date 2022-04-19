using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] Light Light;
    [SerializeField] Vector2 random = new Vector2(.9f, 1.1f);
    [SerializeField] float Time = 0.3f;
    [SerializeField] float CD = 0.3f;

    // Update is called once per frame
    void Update()
    {
        if (CD > 0)
        {
            CD -= UnityEngine.Time.deltaTime;
            return;
        }
        Light.range = Random.Range(random.x, random.y);
        CD = Time;
    }
}

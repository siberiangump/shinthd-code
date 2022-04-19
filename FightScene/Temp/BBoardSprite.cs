using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BBoardSprite : MonoBehaviour
{
    [SerializeField] Camera Camera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = new Vector3(this.transform.rotation.x, this.transform.rotation.y, Camera.transform.rotation.z);
        this.transform.rotation = Camera.transform.rotation;
    }
}

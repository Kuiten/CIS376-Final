using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    protected CameraController camera;
    void Start()
    {   
        camera = GameObject.Find("Main Camera").GetComponent<CameraController>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 65) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            //Pass in true if the player is the one who picked up the collectible
            camera.IncrementScore(true);
        }else if(other.gameObject.CompareTag("AI")){
            camera.IncrementScore(false);
        }
        //Deactivate the object 
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //player fell off the map -- load defeat screen
        if(gameObject.transform.position.y < -70){
            SceneManager.LoadScene("Defeat", LoadSceneMode.Single);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        rb.AddForce(movement, ForceMode.Impulse); 
    }
    //Play pickup sound effect
     void OnTriggerEnter(Collider collision){
        if(collision.gameObject.CompareTag("PickUp")){
            audio.Play();
        }
     }
}

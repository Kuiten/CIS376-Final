using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIController : MonoBehaviour
{
     int currentLocation = 0;
     UnityEngine.AI.NavMeshAgent nav;
     GameObject[] collectibles;
     GameObject[] stage2Collectibles;
     private Rigidbody rb;
     bool stageSwitch = false;
     GameObject player;
     State state;
     protected CameraController camera;

     public enum State{
        collecting, 
        chasing
     }
    // Start is called before the first frame update
    void Start()
    {
        state = State.collecting;
        rb = GetComponent<Rigidbody>(); 
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        camera = GameObject.Find("Main Camera").GetComponent<CameraController>();
        player = GameObject.Find("Player");
        collectibles = new GameObject[8];
        stage2Collectibles = new GameObject[8];
        //load stage 1 collectibles
        collectibles[0] = GameObject.Find("Star Pick Up");
        for(int i = 1; i < collectibles.Length; i++){
            collectibles[i] = GameObject.Find("Star Pick Up (" + i + ")");
        }
        //load stage 2 collectibles
        stage2Collectibles[0] = GameObject.Find("Sphere Pick Up");
         for(int i = 1; i < stage2Collectibles.Length; i++){
            stage2Collectibles[i] = GameObject.Find("Sphere Pick Up (" + i + ")");
        }
        nav.SetDestination(collectibles[currentLocation].transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        //if current object tracking to is picked up switch to the next
        if(!collectibles[currentLocation].activeSelf && state != State.chasing){
            currentLocation = (currentLocation + 1) % collectibles.Length;
            if(!nav.pathPending){
                nav.SetDestination(collectibles[currentLocation].transform.position);
            }
        }
        int[] score = camera.getScore();
        if(score[1] - score[0] >= 2){
            state = State.chasing;
        }else{
            state = State.collecting;
        }
        if(state == State.chasing){
            nav.SetDestination(player.transform.position);
        }
        if(state == State.collecting){
            nav.SetDestination(collectibles[currentLocation].transform.position);
        }
        //if a stage switch occurs get next collectibles
        if(stageSwitch){
            stageSwitch = false;
            //change the collectibles 
            for(int i = 0; i < collectibles.Length; i++){
                collectibles[i] = stage2Collectibles[i];
            }
            //pick new starting collectible 
            currentLocation = Random.Range(0, 8);
            //disable navmesh
            nav.enabled = false;
            rb.isKinematic = false;
            StartCoroutine(switchStage());
        }

        //Ball fell off the map -- load win screen
        if(gameObject.transform.position.y < -70){
            SceneManager.LoadScene("Victory", LoadSceneMode.Single);
        }

    }

    IEnumerator restartNav(){
        yield return new WaitForSeconds(1);
        //give control back to navMeshAgent
        nav.enabled = true;
        rb.isKinematic = true;
        //reset waypoint destination
        nav.SetDestination(collectibles[currentLocation].transform.position);
    }

    void OnCollisionEnter(Collision collision){
        //if collision with player occurs allow physics engine to take over
        if(collision.gameObject.CompareTag("Player")){
            //give control back to physics engine
            nav.enabled = false;
            rb.isKinematic = false;
            //apply force from collision
            rb.AddForce(collision.impulse * -1, ForceMode.Impulse);
            //wait 1 second before giving control back to nav Mesh agent
            StartCoroutine(restartNav());
        }
    }

    public void setStageSwitch(){
        stageSwitch = true;
    }

    //wait 3 seconds before restarting nevMesh -- allows the AI ball to fall to new stage
    IEnumerator switchStage(){
        yield return new WaitForSeconds(3);
        nav.enabled = true;
        rb.isKinematic = true;
        //reset waypoint destination
        nav.SetDestination(collectibles[currentLocation].transform.position);
    }
}

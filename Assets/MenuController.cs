using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    IEnumerator switchToCredits(){
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }
    // Start is called before the first frame update
    void Start()
    {   
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")){
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        }
        StartCoroutine(switchToCredits());
    }
}

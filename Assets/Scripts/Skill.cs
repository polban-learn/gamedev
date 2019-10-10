using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour{
    private LevelManager t_LevelManager;

    //Initialization
    void Start(){
        t_LevelManager = FindObjectOfType<LevelManager> ();
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player"){
            t_LevelManager.AddSkill ();
            Destroy (gameObject);
        }
    }
}

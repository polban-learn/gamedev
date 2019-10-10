using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private LevelManager t_LevelManager;
    private GameObject mario;
    // Start is called before the first frame update
    void Start()
    {
        t_LevelManager = FindObjectOfType<LevelManager> ();
		mario = FindObjectOfType<Mario> ().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			t_LevelManager.MarioPowerDown ();
		}
	}
}

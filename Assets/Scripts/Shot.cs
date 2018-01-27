using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {
    public GameObject Explosion;
    public AIBase owner;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter( Collision coll)
    {
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider coll)
    {
        VRPlayer player = coll.gameObject.GetComponent<VRPlayer>();
        if (player)
        {
            player.Die();
            if(owner)
            {
                owner.KillTarger();
            }
            Instantiate(Explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}

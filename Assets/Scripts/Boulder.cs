﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : UsableBase {
    public float speed = 2f;
    public List<Animator> spriteAnimators = new List<Animator>();
    public string ActiveDescription = "Running boulder, destroy everything.";
    private bool isActivated = false;

    // Update is called once per frame
    void Update () {
        if (isActivated)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            Ray checkRay = new Ray(transform.position, transform.forward);
            Debug.DrawRay(checkRay.origin, checkRay.direction * 1.525f, Color.red);
            RaycastHit rayHit;
            if (Physics.Raycast(checkRay, out rayHit, 1.525f))
            {
                if (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
                {
                    TurnBoulder();
                }
            }
        }
	}

    public void Activate()
    {
        isActivated = true;
        Buttons[0].Active = false;
        Desctription = ActiveDescription;
        MazeLevelManager.Instance.Usable.UpdateView();
        for (int i = 0; i < spriteAnimators.Count; ++i)
        {
            spriteAnimators[i].enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        /*if(collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            TurnBoulder();
        }*/
    }

    public void TurnBoulder()
    {
        float randomValue = Random.Range(-1, 1);
        bool canRight = true;
        bool canLeft = true;
        RaycastHit rayHit;

        Ray checkRight = new Ray(transform.position, transform.right);
        if (Physics.Raycast(checkRight, out rayHit, 2f))
        {
            if (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                print("cant right");
                canRight = false;
            }
        }

        Ray checkLeft = new Ray(transform.position, -transform.right);
        if (Physics.Raycast(checkLeft, out rayHit, 2f))
        {
            if (rayHit.transform.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                print("cant left");
                canLeft = false;
            }
        }

        if (canRight && canLeft)
        {
            int dir = 1;
            if (randomValue < 0)
            {
                dir = -1;
            }
            transform.Rotate(Vector3.up * 90 * dir);
        }
        else if (canLeft)
        {
            transform.Rotate(Vector3.up * 90 * -1);
        }
        else if (canRight)
        {
            transform.Rotate(Vector3.up * 90);
        }
        else
        {
            transform.Rotate(Vector3.up * 180);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(enabled && isActivated)
        {
            if (other.GetComponent<VRPlayer>())
            {
                MazeLevelManager.Instance.vrPlayer.Die();
                return;
            }
            AIBase tmp = other.GetComponent<AIBase>();
            if (tmp)
            {
                tmp.Die();
            }
        }
    }
}

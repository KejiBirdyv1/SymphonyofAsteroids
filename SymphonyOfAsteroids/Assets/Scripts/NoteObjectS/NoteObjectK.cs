using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObjectK : MonoBehaviour
{
    BeatScroller beatScroller;

    public bool canBePressed;
    public KeyCode keyToPress;

    public float frameRateNormal = 120f;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    void Start()
    {
        beatScroller = gameObject.GetComponentInParent<BeatScroller>();
    }

    void Update()
    {
        // K- notes move Left on start
        transform.Translate(Vector3.left / frameRateNormal);

        if (Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);

                // Values are based the position of the note hitting the boxes (A and J)

                if(Mathf.Abs(transform.position.y) > 2.6f)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 2.3f)
                {
                    Debug.Log("Good Hit");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, transform.rotation);
                }
                else
                {
                    Debug.Log("Perfect Hit");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, transform.rotation);
                }
                Destroy(this.gameObject, .2f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Activator")    
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.activeInHierarchy)
        {
            if (collision.tag == "Activator")
            {
                canBePressed = false;

                GameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, transform.rotation);
                Destroy(this.gameObject, .2f);
            }
        }
    }
}

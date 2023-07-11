using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();

                // Values are based the position of the note hitting the boxes (A and J)

                if(Mathf.Abs(transform.position.y) > 2.6f)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                }
                else if (Mathf.Abs(transform.position.y) > 2.3f)
                {
                    Debug.Log("Good Hit");
                    GameManager.instance.NormalHit();
                }
                else
                {
                    Debug.Log("Perfect Hit");
                    GameManager.instance.PerfectHit();
                }
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
            }
        }
    }
}

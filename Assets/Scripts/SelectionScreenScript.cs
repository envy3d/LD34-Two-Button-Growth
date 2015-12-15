using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SelectionScreenScript : MonoBehaviour
{
    public SpriteRenderer p1Ready;
    public SpriteRenderer p2Ready;
    public SpriteRenderer p3Ready;
    public SpriteRenderer p4Ready;

    void Start()
    {
	
	}

	void Update()
    {
	    if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Title");
        }
	}
}

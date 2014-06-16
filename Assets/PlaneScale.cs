using UnityEngine;
using System.Collections;

public class PlaneScale : MonoBehaviour {

    private float planeWidth;
    private float planeHeight;

	// Use this for initialization
	void Start () {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;


        float worldScreenHeight = Screen.height/13.5f;
        float worldScreenWidth = Screen.width/13.5f;

        Vector3 xWidth = transform.localScale;
        xWidth.x = worldScreenWidth / width ;
        transform.localScale = xWidth;
        // transform.localScale.x = worldScreenWidth / width;
        Vector3 yHeight = transform.localScale;
        yHeight.y = worldScreenHeight / height ;
        transform.localScale = yHeight;
       // transform.localScale.y = worldScreenHeight / height;
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

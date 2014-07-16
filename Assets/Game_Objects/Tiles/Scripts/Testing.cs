using UnityEngine;
using System.Collections;

public class Testing : MonoBehaviour {

	public int materialIndex = 0;
	public Vector2 uvAnimationRate = new Vector2( 1.0f, 0.0f );
	public string textureName = "forest1";
	
	Vector2 uvOffset = Vector2.zero;
	
	void LateUpdate()
	{
		uvOffset += ( uvAnimationRate * Time.deltaTime );
		if( renderer.enabled )
		{
			renderer.materials[ materialIndex ].SetTextureOffset( textureName, uvOffset );
		}
	}


}

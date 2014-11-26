using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUITexture))]
public class MC_SwitchTexture : MonoBehaviour
{
	public Material linkedMat;
	public Texture[] textures;
	private int index = 0;
	
	void Update ()
	{
		if(guiTexture.GetScreenRect().Contains(Input.mousePosition))
		{
			guiTexture.color = new Color(0.65f,0.65f,0.65f,0.5f);
			
			if(Input.GetMouseButtonDown(0))
				NextTexture();
			else if(Input.GetMouseButtonDown(2))
				PrevTexture();
		}
		else
		{
			guiTexture.color = Color.gray;
		}
	}
	
	private void NextTexture()
	{
		index++;
		if(index >= textures.Length)
			index = 0;
		ReloadTexture();
	}
	private void PrevTexture()
	{
		index--;
		if(index < 0)
			index = textures.Length-1;
		ReloadTexture();
	}
	private void ReloadTexture()
	{
		linkedMat.SetTexture("_MatCap", textures[index]);
		guiTexture.texture = textures[index];
	}
}

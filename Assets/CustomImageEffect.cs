using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CustomImageEffect : MonoBehaviour
{
	
	public Material custImageEffect;
	
	
	void Start()
	{
		Camera cam = GetComponent<Camera>();
		cam.depthTextureMode = cam.depthTextureMode | DepthTextureMode.Depth;
	}
	
	
	private void OnRenderImage(RenderTexture  src, RenderTexture  dst)
	{
		Graphics.Blit(src,dst,custImageEffect);
	}
}

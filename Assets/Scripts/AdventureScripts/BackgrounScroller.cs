using UnityEngine;
using System.Collections;

public class BackgrounScroller : MonoBehaviour
{
	[Tooltip("Time in seconds for the background to make one full loop.")]
	public float fullScrollSpeed = 5.0f;

	void Update()
	{
		MeshRenderer myMR = GetComponent<MeshRenderer>();
		Material myMat = myMR.material;
		Vector2 offset = myMat.mainTextureOffset;
		offset.x += Time.deltaTime / fullScrollSpeed;
		myMat.mainTextureOffset = offset;
	}
}

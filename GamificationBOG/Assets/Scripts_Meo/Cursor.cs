using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
	public GameObject cursorImage;
    void Update()
    {
		cursorImage.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
	}
}

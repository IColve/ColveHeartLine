using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour {

		

	public GameObject grid;
	public GameObject prefab;
	public InputField lineNumInput;
	public InputField sphereNumInput;
	public InputField liNumInput;
	public Button createButton;
	public Dropdown chooseDrop;


	private int chooseType = 0;
	private float range;
	private float lineCount;
	private float liCount;
	private List<GameObject> objList = new List<GameObject>();
	private bool isClear;
	
	void Start()
	{
		createButton.onClick.AddListener(CreateHeart);
		chooseDrop.onValueChanged.AddListener(Choose);
		Choose(0);
	}

	private void Choose(int typeNum)
	{
		ClearSphere();
		chooseType = typeNum;
		if (chooseType == 0)
		{
			lineCount = 300;
			range = 20;
			liCount = 1.5f;
		}
		else if (chooseType == 1)
		{
			range = 3;
			lineCount = 200;
			liCount = 10;
		}
		sphereNumInput.text = range.ToString();
		lineNumInput.text = lineCount.ToString();
		liNumInput.text = liCount.ToString();
	}

	private void CreateHeart()
	{
		if (isClear)
		{
			return;
		}
		float range = Convert.ToInt64(sphereNumInput.text);
		float lineCount = Convert.ToInt64(lineNumInput.text);
		float li = float.Parse(liNumInput.text);
		if (chooseType == 0)
		{
			float test = range;
			for (float j = range - 1; j >= 0; j-=0.3f)
			{
				for (int i = 0; i < lineCount; i++)
				{
					GameObject obj = GetPrefab();
					Vector2 vec2 = GetVec2(i,range);
					obj.transform.position = new Vector3(vec2.y,vec2.x,(test -j) * li);
					obj.transform.SetParent(grid.transform);
					if (li != 0)
					{
						GameObject obj1 = GetPrefab();
						obj1.transform.position = new Vector3(vec2.y,vec2.x,-obj.transform.position.z);
						obj1.transform.SetParent(grid.transform);
					}
				}
				range -= 0.3f;
			}
		}
		else if (chooseType == 1)
		{
			for (float j = 0; j < range; j+=0.05f)
			{
				for (float i = 0; i < lineCount; i+=0.3f)
				{
					Vector2 vec2 = new Vector2(Mathf.Pow(Mathf.Sin(i),3) * 16,
						13 * Mathf.Cos(i) - 5 * Mathf.Cos(2 * i) - 2 * Mathf.Cos(3 * i) - Mathf.Cos(4 * i));
					GameObject obj = GetPrefab();
					obj.transform.localPosition = new Vector3(vec2.x * j,vec2.y * j,(range - j) * li);
					obj.transform.SetParent(grid.transform);
					if (li != 0)
					{
						GameObject obj1 = GetPrefab();
						obj1.transform.localPosition = new Vector3(vec2.x * j,vec2.y * j,-obj.transform.position.z);
						obj1.transform.SetParent(grid.transform);
					}
				}
			}
		}
	}
	
	private void ClearSphere()
	{
		isClear = true;
		for (int i = grid.transform.childCount - 1; i >= 0; i--)
		{
			GameObject obj = grid.transform.GetChild(i).gameObject; 
			obj.transform.SetParent(transform);
			SetPrefab(obj);
		}
		isClear = false;
	}
	
	private Vector2 GetVec2(int num,float range)
	{
		float x = range * (2 * Mathf.Cos(num) - Mathf.Cos(2 * num));
		float y = range * (2 * Mathf.Sin(num) - Mathf.Sin(2 * num));
		return new Vector2(x,y);
	}


	private GameObject GetPrefab()
	{
		if (objList.Count > 0)
		{
			GameObject obj = objList[0];
			objList.Remove(obj);
			obj.SetActive(true);
			return obj;
		}
		return (GameObject)Instantiate(prefab);
	}

	private void SetPrefab(GameObject obj)
	{
		objList.Add(obj);
		obj.SetActive(false);
	}
}

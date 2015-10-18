using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//
// Script Name: PoolerScript
//Script by: Victor L Josey
// Description: C
// (c) 2015 Shoori Studios LLC  All rights reserved.

public class PoolerScript : MonoBehaviour {


    public static PoolerScript current;
    public GameObject pooledObject;
    public int poolerAmount = 5;
    public bool willGrow = true;

    List<GameObject> pooledObjects;

    public GameObject dissolvePooledObject;
    List<GameObject> dissolvePooledObjects;

    public GameObject bombPooledObject;
    List<GameObject>  bombPooledObjects;

    public GameObject dissolveDarkPooledObject;
    List<GameObject> dissolveDarkPooledObjects;

    public GameObject addPooledObject;
    List<GameObject>  addPooledObjects;

    public GameObject attachItemPooledObject;
    List<GameObject>  attachItemPooledObjects;


    void Awake()
    {
        current = this;

    }

	// Use this for initialization
	void Start () 
    {
        pooledObjects = new List<GameObject>();
        dissolvePooledObjects = new List<GameObject>();
        bombPooledObjects = new List<GameObject>();
        dissolveDarkPooledObjects = new List<GameObject>();
        addPooledObjects = new List<GameObject>();
        attachItemPooledObjects = new List<GameObject>();

        for(int i =0; i < poolerAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.SetActive(false);
            pooledObjects.Add(obj);

            obj = (GameObject)Instantiate(dissolvePooledObject);
            obj.SetActive(false);
            dissolvePooledObjects.Add(obj);

            obj = (GameObject)Instantiate(bombPooledObject);
            obj.SetActive(false);
            bombPooledObjects.Add(obj);

            obj = (GameObject)Instantiate(dissolveDarkPooledObject);
            obj.SetActive(false);
            dissolveDarkPooledObjects.Add(obj);

            obj = (GameObject)Instantiate(addPooledObject);
            obj.SetActive(false);
            addPooledObjects.Add(obj);

            obj = (GameObject)Instantiate(attachItemPooledObject);
            obj.SetActive(false);
            attachItemPooledObjects.Add(obj);

        }
	}
	
    public GameObject GetPooledObject(string poolName)
    {

        switch(poolName)
        {
            case "PoolObject" :
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy)
                {
                    return pooledObjects[i];
                }
            }

                if (willGrow)
                {
                    GameObject obj = (GameObject)Instantiate(pooledObject);
                    pooledObjects.Add(obj);
                    return obj;
                }
                break;

            case "Dissolve FX":
                for (int i = 0; i < dissolvePooledObjects.Count; i++)
                {
                    if (!dissolvePooledObjects[i].activeInHierarchy)
                    {
                        return dissolvePooledObjects[i];
                    }
                }

                if (willGrow)
                {
                    GameObject obj = (GameObject)Instantiate(dissolvePooledObject);
                    dissolvePooledObjects.Add(obj);
                    return obj;
                }
                break;

            case "Bomb FX":
                for (int i = 0; i < bombPooledObjects.Count; i++)
                {
                    if (!bombPooledObjects[i].activeInHierarchy)
                    {
                        return bombPooledObjects[i];
                    }
                }

                if (willGrow)
                {
                    GameObject obj = (GameObject)Instantiate(bombPooledObject);
                    bombPooledObjects.Add(obj);
                    return obj;
                }
                break;

            case "Dark FX":
                for (int i = 0; i < dissolveDarkPooledObjects.Count; i++)
                {
                    if (!dissolveDarkPooledObjects[i].activeInHierarchy)
                    {
                        return dissolveDarkPooledObjects[i];
                    }
                }

                if (willGrow)
                {
                    GameObject obj = (GameObject)Instantiate(dissolveDarkPooledObject);
                    dissolveDarkPooledObjects.Add(obj);
                    return obj;
                }
                break;

            case "AddPeg FX":
                for (int i = 0; i < addPooledObjects.Count; i++)
                {
                    if (!addPooledObjects[i].activeInHierarchy)
                    {
                        return addPooledObjects[i];
                    }
                }

                if (willGrow)
                {
                    GameObject obj = (GameObject)Instantiate(addPooledObject);
                    addPooledObjects.Add(obj);
                    return obj;
                }
                break;

            case "AttachItem FX":
                for (int i = 0; i < attachItemPooledObjects.Count; i++)
                {
                    if (!attachItemPooledObjects[i].activeInHierarchy)
                    {
                        return attachItemPooledObjects[i];
                    }
                }

                if (willGrow)
                {
                    GameObject obj = (GameObject)Instantiate(attachItemPooledObject);
                    attachItemPooledObjects.Add(obj);
                    return obj;
                }
                break;
        }

         return null;
    }
}

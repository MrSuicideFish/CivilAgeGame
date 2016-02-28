using UnityEngine;
using System;
using System.Linq;
using System.Collections;

/// <summary>
/// GameObjectManager is responsible for the pooling and instantiating of commonly-used objects.
/// i.e. UI, health bars, and bullets
/// </summary>
public sealed class GameObjectManager : MonoBehaviour
{
    public static GameObject objectPool;
    public static GameObject ObjectPool
    {
        get
        {
            if ( !objectPool )
            {
                objectPool = new GameObject( "OBJECT_POOL_CONTAINER" );
            }

            return objectPool;
        }
    }

    public GameObject[] PreloadedObjectPool;

    void Awake( )
    {
        foreach ( GameObject obj in PreloadedObjectPool )
            PoolObject( obj );
    }

    /// <summary>
    /// Returns and instance of a pooled object
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static GameObject GetObject(string name )
    {
        return GameObject.Instantiate( ObjectPool.transform.FindChild( name ).gameObject );
    }

    /// <summary>
    /// Returns and instance of a pooled object
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static GameObject GetObject( Type type )
    {
        return GameObject.Instantiate( ObjectPool.transform.GetComponentInChildren( type ).gameObject );
    }

    /// <summary>
    /// Adds an object to the pool
    /// </summary>
    /// <param name="poolObjPath"></param>
    public static void PoolObject( string poolObjPath )
    {
        GameObject newPoolObj = Resources.Load<GameObject>( poolObjPath );
        newPoolObj = GameObject.Instantiate( newPoolObj );

        //Removes "(Clone)" from the end of a gameObject before adding
        newPoolObj.name = newPoolObj.name.Substring( 0, newPoolObj.name.Length - 7 );

        newPoolObj.SetActive( false );
        newPoolObj.transform.SetParent( ObjectPool.transform, true );
    }

    /// <summary>
    /// Adds an object to thte pool
    /// </summary>
    /// <param name="poolObj"></param>
    public static void PoolObject(GameObject poolObj )
    {
        GameObject newPoolObj = GameObject.Instantiate( poolObj );

        //Removes "(Clone)" from the end of a gameObject before adding
        newPoolObj.name = newPoolObj.name.Substring( 0, newPoolObj.name.Length - 7 );

        newPoolObj.SetActive( false );
        newPoolObj.transform.SetParent( ObjectPool.transform, true );
    }

    /// <summary>
    /// Returns whether or not the pool contains the specified object
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static bool HasObject(string name)
    {
        return ObjectPool.transform.FindChild( name ) != null;
    }
}
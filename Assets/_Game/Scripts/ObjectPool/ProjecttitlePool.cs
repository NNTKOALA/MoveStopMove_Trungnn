using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ProjecttitlePool : ObjectPool<Projecttitle>
{
    public override Projecttitle GetPooledObject()
    {
        // if the pool is not large enough, instantiate a new PooledObjects
        if (stack.Count == 0)
        {
            Projecttitle newInstance = Instantiate(objectToPool);
            newInstance.Pool = this;
            return newInstance;
        }
        // otherwise, just grab the next one from the list
        Projecttitle nextInstance = stack.Pop();
        nextInstance.gameObject.SetActive(true);
        return nextInstance;
    }

    public override void ReturnToPool(Projecttitle pooledObject)
    {
        stack.Push(pooledObject);
        pooledObject.transform.SetParent(parent);
        pooledObject.gameObject.SetActive(false);
    }

    protected override void SetupPool()
    {
        stack = new Stack<Projecttitle>();
        Projecttitle instance = null;
        for (int i = 0; i < initPoolSize; i++)
        {
            instance = Instantiate(objectToPool, parent);
            instance.Pool = this;
            instance.gameObject.SetActive(false);
            stack.Push(instance);
        }
    }

    public int Count => stack.Count;
}

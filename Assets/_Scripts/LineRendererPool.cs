using System.Collections.Generic;
using UnityEngine;

public class LineRendererPool : MonoBehaviour
{
    public GameObject lineRendererPrefab; // Prefab with LineRenderer component
    public int poolSize = 10;

    private List<LineRenderer> pool = new();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(lineRendererPrefab, transform);
            obj.SetActive(false);
            pool.Add(obj.GetComponent<LineRenderer>());
        }
    }

    public LineRenderer GetLineRenderer(int _index)
    {
        LineRenderer line;
        if (pool.Count > _index)
        {
            line = pool[_index];
            line.gameObject.SetActive(true);
            return line;
        }

        // Optional: create more objects if pool is empty
        GameObject newObj = Instantiate(lineRendererPrefab, transform);
        newObj.SetActive(true);
        line = newObj.GetComponent<LineRenderer>();
        pool.Add(line);
        return line;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_index">inclusive</param>
    public void DeactivateFrom(int _index)
    {
        for (int i = _index; i < pool.Count; i++)
        {
            pool[i].gameObject.SetActive(false);
        }
    }

}
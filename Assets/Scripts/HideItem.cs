using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HideItem : MonoBehaviour
{
    private Renderer[] _renderers;
    private Dictionary<Material, Material> _materials = new Dictionary<Material, Material>();

    private void Awake()
    {
        CollectMaterials();
    }

    private void CollectMaterials()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        foreach (var renderer in _renderers)
        {
            if (!_materials.ContainsKey(renderer.material))
            {
                var newMaterial = new Material(renderer.material);
                newMaterial.SetFloat("_Comparer", 8);
                _materials.Add(renderer.material, newMaterial);
            }
        }
    }

    public void CompleteDetect()
    {
        GetComponent<Collider>().enabled = false;
        ChangeMaterials();
        transform.parent = null;
        var camera = Camera.main.transform;
        transform.DOMove(camera.position + camera.forward, 0.5f);
        transform.DORotateQuaternion(camera.rotation * Quaternion.Euler(0, 90, 0), 0.5f);
        Destroy(gameObject, 2f);
    }

    private void ChangeMaterials()
    {
        foreach (var renderer in _renderers)
        {
            foreach (var pairMaterials in _materials)
            {
                if (renderer.sharedMaterial == pairMaterials.Key)
                {
                    renderer.material = pairMaterials.Value;
                }
            }
        }
    }
}
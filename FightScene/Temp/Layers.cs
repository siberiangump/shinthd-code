using System.Collections;
using System.Collections.Generic;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class Layers : MonoBehaviour
{
    [SerializeField] private SortiongLayerEnum SortiongLayer;
    [SerializeField] int LayerOrder;

    MeshRenderer MeshR;

    private void ApplyLayerData(int layerId, int layerOrder, MeshRenderer meshRenderer)
    {
        meshRenderer.sortingLayerID = layerId;
        meshRenderer.sortingOrder = layerOrder;
    }

    private void ValideteComponents()
    {
        if (MeshR == null)
            MeshR = this.GetComponent<MeshRenderer>();
    }

#if UNITY_EDITOR
	private void OnValidate()
	{
        ValideteComponents();
        ApplyLayerData((int)SortiongLayer, LayerOrder, MeshR);
    }


	[ContextMenu("CreateEnum")]
    public void CreateEnum()
    {
        SortingLayer[] layers = SortingLayer.layers;
        string text = "public enum SortiongLayerEnum {";
        foreach (SortingLayer item in layers)
        {
            text += $"{item.name} = {item.id}, ";
        }
        text += "}";
        string path = "Assets/Code/Generated/SortiongLayerEnum.cs";

        if (File.Exists(path))
            File.Delete(path);
        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(text);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
    }
#endif

}

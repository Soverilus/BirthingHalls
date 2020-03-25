using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRandomiser : MonoBehaviour
{
    public MeshRenderer[] myMeshRenderers;
    public Material[] myMaterials;
    List<Material> myMaterialList = new List<Material>();

    void Start() {
        for (int i = 0; i < myMeshRenderers.Length; i++) {
            myMeshRenderers[i].material = ReturnMaterial();
        }
    }

    Material ReturnMaterial() {
        Material resultMaterial = myMeshRenderers[0].material;
        //temporary thing for easier use
        int m = Random.Range(0, myMaterials.Length);
        resultMaterial = myMaterials[m];

        for (int i = 0; i < myMaterials.Length; i++) {

        }
        return resultMaterial;
    }
}
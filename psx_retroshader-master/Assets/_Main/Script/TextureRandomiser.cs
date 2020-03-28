using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureRandomiser : MonoBehaviour
{
    public MeshRenderer[] myMeshRenderers;
    public Material[] myMainMaterials;
    public Material[] myMiscMaterials;
    List<Material> myMaterialList = new List<Material>();

    void Start() {
        for (int i = 0; i < myMeshRenderers.Length; i++) {
            if (i == 0) {
                myMeshRenderers[i].material = ReturnMaterial(true);
            }
            else {
                myMeshRenderers[i].material = ReturnMaterial(false);
            }
        }
    }

    Material ReturnMaterial(bool isMain) {
        Material resultMaterial = myMeshRenderers[0].material;
        //temporary thing for easier use
        if (isMain) {
            int m = Random.Range(0, myMainMaterials.Length);
            resultMaterial = myMainMaterials[m];
        }
        else {
            int m = Random.Range(0, myMiscMaterials.Length);
            resultMaterial = myMiscMaterials[m];
        }

        return resultMaterial;
    }
}
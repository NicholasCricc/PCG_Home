using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : MonoBehaviour
{
    public static List<Material> MaterialList(){
    List<Material> MaterialList = new List<Material>();

    Material redMaterial = new Material(Shader.Find("Specular"));
    redMaterial.color = Color.red;

    Material blueMaterial = new Material(Shader.Find("Specular"));
    blueMaterial.color = Color.blue;

    Material yellowMaterial = new Material(Shader.Find("Specular"));
    yellowMaterial.color = Color.yellow;

    Material greenMaterial = new Material(Shader.Find("Specular"));
    greenMaterial.color = Color.green;

    Material magentaMaterial = new Material(Shader.Find("Specular"));
    magentaMaterial.color = Color.magenta;

    Material cyanMaterial = new Material(Shader.Find("Specular"));
    cyanMaterial.color = Color.cyan;

    MaterialList.Add(redMaterial);
    MaterialList.Add(blueMaterial);
    MaterialList.Add(yellowMaterial);
    MaterialList.Add(greenMaterial);
    MaterialList.Add(magentaMaterial);
    MaterialList.Add(cyanMaterial);

    return MaterialList;

}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

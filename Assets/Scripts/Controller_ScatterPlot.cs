using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
    Notes:
    Class,Alcohol,MalicAcid,Ash,AshAlcalinity,Magnesium,TotalPhenols,Flavanoids,NonflavanoidPhenols,Proanthocyanins,ColorIntensity,Hue,OD280/OD315,Proline
*/
public class Controller_ScatterPlot : MonoBehaviour
{
    [SerializeField]
    TextMeshPro _text_X_Axis;
    [SerializeField]
    TextMeshPro _text_Y_Axis;
    [SerializeField]
    TextMeshPro _text_Z_Axis;
    [SerializeField]
    GameObject PlotPrefab;
    [SerializeField]
    List<Material> Materials = new List<Material>();
    [SerializeField]
    Transform CameraLookAt;

    float _bigX = -Mathf.Infinity;
    float _bigY = -Mathf.Infinity;
    float _bigZ = -Mathf.Infinity;
    float _smallX = Mathf.Infinity;
    float _smallY = Mathf.Infinity;
    float _smallZ = Mathf.Infinity;

    float middleX, middleY, middleZ;

    private void Start()
    {
        List<Dictionary<string, float>> data = DatasetReader.Read("wine");

        //PRINT READ CSV VALUES
        //for (var i = 0; i < data.Count; i++)
        //{
        //    print("Class " + data[i]["Class"] + " " +
        //           "Alcohol " + data[i]["Alcohol"] + " " +
        //           "MalicAcid " + data[i]["MalicAcid"] + " " +
        //           "Ash " + data[i]["Ash"]);
        //}

        for (var i = 0; i < data.Count; i++)
        {
            _text_X_Axis.text = "Alcohol";
            _text_Y_Axis.text = "MalicAcid";
            _text_Z_Axis.text = "Ash";

            GameObject currentPlot = GameObject.Instantiate(PlotPrefab,
                                                            new Vector3(data[i]["Alcohol"], data[i]["MalicAcid"], data[i]["Ash"]) * 10,
                                                            Quaternion.identity);
            currentPlot.GetComponentInChildren<MeshRenderer>().material = Materials[((int)data[i]["Class"])-1];
            currentPlot.GetComponentInChildren<TextMeshPro>().text = data[i]["Class"].ToString();
            currentPlot.transform.SetParent(this.transform);
            
            if(data[i]["Alcohol"] > _bigX)
            {
                _bigX = data[i]["Alcohol"];
            }
            if (data[i]["MalicAcid"] > _bigY)
            {
                _bigY = data[i]["MalicAcid"];
            }
            if (data[i]["Ash"] > _bigZ)
            {
                _bigZ = data[i]["Ash"];
            }
            if (data[i]["Alcohol"] < _smallX)
            {
                _smallX = data[i]["Alcohol"];
            }
            if (data[i]["MalicAcid"] < _smallY)
            {
                _smallY = data[i]["MalicAcid"];
            }
            if (data[i]["Ash"] < _smallZ)
            {
                _smallZ = data[i]["Ash"];
            }

            middleX = ((_bigX + _smallX) / 2) * 10;
            middleY = ((_bigY + _smallY) / 2) * 10;
            middleZ = ((_bigZ + _smallZ) / 2) * 10;

            CameraLookAt.transform.position = new Vector3(middleX, middleY, middleZ);

        }

    }
}
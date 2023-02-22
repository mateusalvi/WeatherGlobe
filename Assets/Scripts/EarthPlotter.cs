using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPlotter : MonoBehaviour
{
    [SerializeField]
    GameObject _cityPlotPrefab = null;

    [SerializeField]
    float _radius;

    List<Dictionary<string, object>> data;

    private void Awake()
    {
        data = DatasetReader.Read("GlobalLandTemperaturesByMajorCity");

        //for (var i = 0; i < 10; i++)
        //{
        //    print("Date " + data[i]["dt"] + " " +
        //           "AverageTemperature " + data[i]["AverageTemperature"] + " " +
        //           "AverageTemperatureUncertainty " + data[i]["AverageTemperatureUncertainty"] + " " +
        //           "City " + data[i]["City"] + " " +
        //           "Country " + data[i]["Country"] + " " +
        //           "Latitude " + data[i]["Latitude"] + " " +
        //           "Longitude " + data[i]["Longitude"]);
        //}
    }

    void Start()
    {

        for (var i = 0; i < 10; i++)//for (var i = 0; i < data.Count; i++)
        {
            string currentText = data[i]["Latitude"].ToString();
            string latitudeNorS = currentText.Substring(currentText.Length - 1);
            currentText = data[i]["Longitude"].ToString();
            string longitudeLorW = currentText.Substring(currentText.Length - 1);

            float longitude;

            Debug.Log("STRINGS: " + latitudeNorS + longitudeLorW);

            //GameObject CurrentPlot = Instantiate(_cityPlotPrefab, Quaternion.Euler(_radius, 0, 0), (), this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System.Globalization;

public class EarthPlotter : MonoBehaviour
{
    [SerializeField]
    GameObject _cityPlotPrefab = null;

    [SerializeField]
    float _radius = 0.5f;

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
        float latitude;
        float longitude;

        string currentCity = null;
        
        for (var i = 0; i < data.Count; i++)
        {
            if (currentCity != data[i]["City"].ToString())
            {

                currentCity = data[i]["City"].ToString();

                float temp;

                string currentText = data[i]["Latitude"].ToString();
                if (currentText.Contains('N'))
                {
                    currentText = currentText.Remove(currentText.Length - 1);
                    float.TryParse(currentText, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out temp);
                    latitude = temp;
                }
                else
                {
                    currentText = currentText.Remove(currentText.Length - 1);
                    float.TryParse(currentText, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out temp);
                    latitude = -temp;
                }

                currentText = data[i]["Longitude"].ToString();
                if (currentText.Contains('E'))
                {
                    currentText = currentText.Remove(currentText.Length - 1);
                    float.TryParse(currentText, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out temp);
                    longitude = temp;
                }
                else
                {
                    currentText = currentText.Remove(currentText.Length - 1);
                    float.TryParse(currentText, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out temp);
                    longitude = -temp;
                }

                latitude *= Mathf.Deg2Rad;
                longitude *= Mathf.Deg2Rad;

                float x = _radius * Mathf.Cos(latitude) * Mathf.Cos(longitude);
                float y = _radius * Mathf.Sin(latitude);
                float z = _radius * Mathf.Cos(latitude) * Mathf.Sin(longitude);

                GameObject CurrentInstance = Instantiate(_cityPlotPrefab, this.transform.position + new Vector3 (x,y,z), Quaternion.identity);
                Controller_CityPlot CurrentCityPlot = CurrentInstance.GetComponent<Controller_CityPlot>();
                CurrentInstance.transform.forward = (this.transform.position - CurrentInstance.transform.position);
                CurrentInstance.transform.SetParent(this.transform);
                CurrentCityPlot.CityName.text = data[i]["City"].ToString();
                CurrentCityPlot.CityTemperature.text = data[i]["AverageTemperature"].ToString();
            }
        }
    }
}

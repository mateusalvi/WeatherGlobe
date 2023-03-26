using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;
using System.Globalization;
using Unity.VisualScripting;

public class Controller_CityPlot : MonoBehaviour
{
    public float TemperatureFloat = 0;

    int TemperatureInt = 0;
    
    [SerializeField]
    public TextMeshPro CityName = null;
    
    [SerializeField]
    public TextMeshPro CityTemperature = null;
    
    [SerializeField]
    Material PlotMaterial = null;
    
    [SerializeField]
    Texture2D _colorRampTexture = null;
    
    [SerializeField]
    Renderer _plotRenderer = null;

    [SerializeField]
    Vector3 _outOfFocusScale = Vector3.one;

    [SerializeField]
    private float _transparency;

    [SerializeField]
    Vector3 _temperaturePointedOfset = new (0.0f, -0.2f, 0.0f);

    [SerializeField]
    float _scaleInAnimationTime = 0.2f;

    Vector3 _temperatureNaturalScale = Vector3.one;

    Vector3 _nameNaturalScale = Vector3.one;

    Vector3 _temperatureTextNaturalPosition;

    Vector3 _temperatureTextOfsetPosition;

    Transform _temperatureTextTransform;

    Transform _cityNameTextTransform;

    Color _currentColor = Color.white;

    Coroutine _nameScaleCoroutine = null;
    Coroutine _temperatureScaleCoroutine = null;
    Coroutine _temperatureTransformCoroutine = null;

    private void Start()
    {
        //float.TryParse(CityTemperature.text, out TemperatureFloat);
        //TemperatureInt = (int)TemperatureFloat;
        //Debug.Log(TemperatureInt);
        //_currentColor = _colorRampTexture.GetPixel(TemperatureInt + 64, 0);
        //_plotRenderer.material.color = _currentColor;
        _cityNameTextTransform = CityName.transform;
        _temperatureTextTransform = CityTemperature.transform;

        _nameNaturalScale = _cityNameTextTransform.localScale;
        _temperatureNaturalScale = CityTemperature.transform.localScale;
        
        _temperatureTextNaturalPosition = CityTemperature.transform.localPosition;
        _temperatureTextOfsetPosition = _temperatureTextNaturalPosition + _temperaturePointedOfset;

        _cityNameTextTransform.localScale = Vector3.zero;      
        CityTemperature.transform.localScale = _outOfFocusScale;
        
    }

    public void UpdatePlot(string cityTemp)
    {
        float.TryParse(cityTemp, out TemperatureFloat);
        TemperatureFloat = Mathf.Round(TemperatureFloat * 100.0f) * 0.01f;
        CityTemperature.text = (TemperatureFloat + "\n °C");
        //Casted to int to be used as texture cordinates:
        TemperatureInt = (int)TemperatureFloat;
        _currentColor = _colorRampTexture.GetPixel(TemperatureInt + 64, 0);
        _currentColor.a = _transparency;
        _plotRenderer.material.color = _currentColor;
    }

    public void EnableText()
    {
        StopAllCoroutines();
        StartCoroutine(SmoothScale(_cityNameTextTransform, _nameNaturalScale));

        StartCoroutine(SmoothScale(_temperatureTextTransform, _temperatureNaturalScale));
        //_textTransform.localScale = _naturalScale;
        StartCoroutine(SmoothTransform(_temperatureTextTransform, _temperatureTextOfsetPosition));
    
    }

    public void DisableText()
    {
        StopAllCoroutines();
        StartCoroutine(SmoothScale(_cityNameTextTransform, Vector3.zero));

        StartCoroutine(SmoothScale(_temperatureTextTransform, _outOfFocusScale));
        //_textTransform.localScale = _outOfFocusScale;
        StartCoroutine(SmoothTransform(_temperatureTextTransform, _temperatureTextNaturalPosition));
    }

    IEnumerator SmoothScale(Transform objectTransform, Vector3 newScale)
    {
        WaitForEndOfFrame endOfFrame = new();
        float animationTime = 0f;

        while (animationTime <= _scaleInAnimationTime)
        {
            objectTransform.localScale = Vector3.Lerp(objectTransform.localScale, newScale, animationTime * _scaleInAnimationTime);
            animationTime += Time.deltaTime;
            yield return endOfFrame;
        }
    }

    IEnumerator SmoothTransform(Transform objectTransform, Vector3 newPosition)
    {
        WaitForEndOfFrame endOfFrame = new();
        float animationTime = 0f;

        while (animationTime <= _scaleInAnimationTime)
        {
            objectTransform.localPosition = Vector3.Lerp(objectTransform.localPosition, newPosition, animationTime * _scaleInAnimationTime);
            animationTime += Time.deltaTime;
            yield return endOfFrame;
        }
    }
}

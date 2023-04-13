using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Controller_NavigationMenu : MonoBehaviour
{
    [SerializeField]
    EarthPlotter _globe;

    [SerializeField]
    TextMeshPro _yearText;
    [SerializeField]
    TextMeshPro _monthText;

    int _selectedMonth = 01;

    int _selectedYear = 2000;

    private void Start()
    {
        UpdateDate();
    }

    public void IncrementYear()
    {
        _selectedYear += 1;
        UpdateDate();
    }
    public void DecrementYear()
    {
        _selectedYear -= 1;
        UpdateDate();
    }
    public void IncrementMonth()
    {
        _selectedMonth += 1;
        if (_selectedMonth > 12)
        {
            _selectedMonth = 1;
        }
        UpdateDate();
    }
    public void DecrementMonth()
    {
        _selectedMonth -= 1;
        if (_selectedMonth == 0)
        {
            _selectedMonth = 12;
        }
        UpdateDate();
    }

    void UpdateDate()
    {
        _yearText.text = _selectedYear.ToString();
        if (_selectedMonth < 10)
        {
            _monthText.text = "0" + _selectedMonth.ToString();
        }
        else
        {
            _monthText.text = _selectedMonth.ToString();
        }
        string date = _yearText.text + "-" + _monthText.text + "-01";
        Debug.Log(date);
        _globe.ChangeCitiesDates(date);
    }
}

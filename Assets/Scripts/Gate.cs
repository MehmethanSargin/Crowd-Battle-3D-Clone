using TMPro;
using UnityEngine;

public class Gate : MonoBehaviour
{
    
    public GateType _gateType;
    public int _value;
    
    [SerializeField] private TextMeshProUGUI _text;

    private void OnValidate()
    {
        if (_text==null) return;
        switch (_gateType)
        {
            case GateType.Add:
                _text.text = "+" ;
                break;
            case GateType.Multiply:
                _text.text = "x";
                break;
            
        }

        _text.text += _value;
    }


    public enum GateType
    {
        Add,
        Multiply
    }
}

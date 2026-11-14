using TMPro;
using UnityEngine;

public class HintText : MonoBehaviour
{
    [SerializeField] private TMP_Text label;

    public void Show(string message)
    {
        label.text = message;
        label.gameObject.SetActive(true);
    }
}
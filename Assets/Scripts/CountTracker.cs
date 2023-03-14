using UnityEngine;
using TMPro;
public class CountTracker : MonoBehaviour
{
    public int count = 0;
    [SerializeField] private TMP_Text countText;
    private void Update()
    {
        countText.text = count.ToString();
    }
    public void IncrementCount()
    {
        count++;
        if (count % 10 == 0)
        {
            
        }
    }
}

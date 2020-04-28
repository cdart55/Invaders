using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Sprite healthFull;
    public Sprite healthEmpty;

    public List<Image> healthDisplays;

    public void AddOneToHealth()
    {
        foreach (Image display in healthDisplays)
        {
            Sprite s = display.GetComponent<Image>().sprite;

            if (s == healthEmpty)
            {
                display.GetComponent<Image>().sprite = healthFull;
                return;
            }
        }
    }

    public void removeOneFromHealth()
    {
        for(int i = healthDisplays.Count - 1;  i >= 0; i--)
        {
            Sprite s = healthDisplays[i].GetComponent<Image>().sprite;
            
            if (s.Equals(healthFull))
            {
                healthDisplays[i].GetComponent<Image>().sprite = healthEmpty;
                return;
            }
        }
    }
}

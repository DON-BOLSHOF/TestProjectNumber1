using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets.MenuWidgets
{
    public class ProgressBarWidget : MonoBehaviour
    {
        [SerializeField] private Image _bar;
        [SerializeField] private TextMeshProUGUI _percent;

        public void SetBarValue(float percent)
        {
            _bar.fillAmount = percent;
            _percent.text = (percent * 100).ToString(CultureInfo.InvariantCulture) + "%";
        }
    }
}
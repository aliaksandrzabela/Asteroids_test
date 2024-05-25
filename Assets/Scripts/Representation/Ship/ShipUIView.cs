using TMPro;
using UnityEngine;

namespace Asteroids.View
{
    public class ShipUIView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textCoordinates;
        [SerializeField] TextMeshProUGUI textAngle;
        [SerializeField] TextMeshProUGUI textSpeed;
        [SerializeField] TextMeshProUGUI textLazerShootCount;
        [SerializeField] TextMeshProUGUI textLazerCooldown;

        public void SetCoordinates(string value)
        {
            textCoordinates.text = value;
        }

        public void SetAngle(string value)
        {
            textAngle.text = value;
        }

        public void SetSpeed(string value)
        {
            textSpeed.text = value;
        }

        public void SetTextLazerShootCount(string value)
        {
            textLazerShootCount.text = value;
        }

        public void SetLazerCooldown(string value)
        {
            textLazerCooldown.text = value;
        }
    }
}

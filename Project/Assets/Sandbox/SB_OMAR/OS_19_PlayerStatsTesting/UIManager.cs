using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sandbox.Omar.PlayerStatsTesting
{
    public class UIManager : MonoBehaviour
    {
        private bool spiritLevelPressed;
        [SerializeField] private GameObject playerReportBackgroundPanel;
        [SerializeField] private GameObject playerStatisticsPanel;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #region Player Reports

        public void SpiritLevelPressed()
        {
            if (!spiritLevelPressed)
            {
                ViewPlayerStatistics();
                spiritLevelPressed = true;
            }
            else
            {
                StartCoroutine(DisableUIElementsAfterSeconds(0, new[] { playerStatisticsPanel, playerReportBackgroundPanel }));
                spiritLevelPressed = false;
            }
        }
        public void ViewPlayerStatistics()
        {
            playerReportBackgroundPanel.SetActive(true);
            playerStatisticsPanel.SetActive(true);
        }

        #endregion

        // Coroutine used to disable multiple UI elements after a set period of time
        IEnumerator DisableUIElementsAfterSeconds(int timeToDisplay, GameObject[] UIElements)
        {
            yield return new WaitForSeconds(timeToDisplay);
            foreach (GameObject UIElement in UIElements)
            {
                UIElement.SetActive(false);
            }
        }

    }

}

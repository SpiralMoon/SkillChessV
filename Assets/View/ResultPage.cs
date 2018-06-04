using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model.Impl;
using Assets.Model.SceneParameter;
using Assets.Service;

namespace Assets.View
{
    public class ResultPage : MonoBehaviour, IPage
    {
        public Button BtnGoToHome;

        public Text TxtMessage;

        public Text TxtWhiteMoving;

        public Text TxtWhiteMovingCount;

        public Text TxtWhiteKill;

        public Text TxtWhiteKillCount;

        public Text TxtWhiteDeath;

        public Text TxtWhiteDeathCount;

        public Text TxtWhiteDamage;

        public Text TxtWhiteDamageCount;

        public Text TxtBlackMoving;

        public Text TxtBlackMovingCount;

        public Text TxtBlackKill;

        public Text TxtBlackKillCount;

        public Text TxtBlackDeath;

        public Text TxtBlackDeathCount;

        public Text TxtBlackDamage;

        public Text TxtBlackDamageCount;

        private void Awake()
        {
            BtnGoToHome.onClick.AddListener(OnClickGoToHome);

            SetTextSize();
            SetTextValue();
        }

        public void NextPage(string pageName)
        {
            SceneManager.LoadSceneAsync(pageName);
        }

        public void SetTextSize()
        {
            
        }

        public void SetTextValue()
        {
            var param = PageParameterDispatcher.Instance().GetPageParameter() as ResultPageParameter;
            var whiteDashBoard = param.WhiteDashBoard;
            var blackDashBoard = param.BlackDashBoard;

            /*TxtWhiteMoving;
            TxtWhiteKill;
            TxtWhiteDeath;
            TxtWhiteDamage;
            TxtBlackMoving;
            TxtBlackKill;
            TxtBlackDeath;
            TxtBlackDamage;*/

            TxtWhiteMovingCount.text = whiteDashBoard.MovingCount + "";
            TxtWhiteKillCount.text = whiteDashBoard.KillCount + "";
            TxtWhiteDeathCount.text = whiteDashBoard.DeathCount + "";
            TxtWhiteDamageCount.text = whiteDashBoard.Damage + "";
            TxtBlackMovingCount.text = blackDashBoard.MovingCount + "";
            TxtBlackKillCount.text = blackDashBoard.KillCount + "";
            TxtBlackDeathCount.text = blackDashBoard.DeathCount + "";
            TxtBlackDamageCount.text = blackDashBoard.Damage + "";
        }

        private void OnClickGoToHome()
        {
            NextPage("LobbyPage");
        }
    }
}

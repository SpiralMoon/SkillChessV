using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model.Impl;
using Assets.Model.SceneParameter;
using Assets.Service;
using Assets.Support.Language;

namespace Assets.View
{
    public class ResultPage : MonoBehaviour, IPage
    {
        private TextResource _textResource;

        public Button BtnGoToHome;

        public Text TxtMessage;

        public GameObject TxtWhiteMoving;

        public GameObject TxtWhiteKill;

        public GameObject TxtWhiteDeath;

        public GameObject TxtWhiteDamage;

        public GameObject TxtBlackMoving;

        public GameObject TxtBlackKill;

        public GameObject TxtBlackDeath;

        public GameObject TxtBlackDamage;

        private void Awake()
        {
            _textResource = TextResource.GetInstance();

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

            BtnGoToHome.GetComponentInChildren<Text>().text = _textResource.GetText(TextCode.GO_TO_LOBBY);

            TxtWhiteMoving.transform.Find("TXT_MovingCount").GetComponent<Text>().text = _textResource.GetText(TextCode.SUM_MOVE_COUNT);
            TxtWhiteKill.transform.Find("TXT_KillCount").GetComponent<Text>().text = _textResource.GetText(TextCode.SUM_KILL_COUNT);
            TxtWhiteDeath.transform.Find("TXT_DeathCount").GetComponent<Text>().text = _textResource.GetText(TextCode.SUM_DEATH_COUNT);
            TxtWhiteDamage.transform.Find("TXT_Damage").GetComponent<Text>().text = _textResource.GetText(TextCode.SUM_DAMAGE);
            TxtBlackMoving.transform.Find("TXT_MovingCount").GetComponent<Text>().text = _textResource.GetText(TextCode.SUM_MOVE_COUNT);
            TxtBlackKill.transform.Find("TXT_KillCount").GetComponent<Text>().text = _textResource.GetText(TextCode.SUM_KILL_COUNT);
            TxtBlackDeath.transform.Find("TXT_DeathCount").GetComponent<Text>().text = _textResource.GetText(TextCode.SUM_DEATH_COUNT);
            TxtBlackDamage.transform.Find("TXT_Damage").GetComponent<Text>().text = _textResource.GetText(TextCode.SUM_DAMAGE);

            TxtWhiteMoving.transform.Find("TXT_Count").GetComponent<Text>().text = whiteDashBoard.MovingCount + "";
            TxtWhiteKill.transform.Find("TXT_Count").GetComponent<Text>().text = whiteDashBoard.KillCount + "";
            TxtWhiteDeath.transform.Find("TXT_Count").GetComponent<Text>().text = whiteDashBoard.DeathCount + "";
            TxtWhiteDamage.transform.Find("TXT_Count").GetComponent<Text>().text = whiteDashBoard.Damage + "";
            TxtBlackMoving.transform.Find("TXT_Count").GetComponent<Text>().text = blackDashBoard.MovingCount + "";
            TxtBlackKill.transform.Find("TXT_Count").GetComponent<Text>().text = blackDashBoard.KillCount + "";
            TxtBlackDeath.transform.Find("TXT_Count").GetComponent<Text>().text = blackDashBoard.DeathCount + "";
            TxtBlackDamage.transform.Find("TXT_Count").GetComponent<Text>().text = blackDashBoard.Damage + "";
        }

        private void OnClickGoToHome()
        {
            NextPage("LobbyPage");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Assets.Model.Impl;
using Assets.Model;
using Assets.Support;
using Assets.Support.Language;
using Assets.Service;

public class LoginPage : MonoBehaviour, IPage
{
    private NetworkManager _networkManager;

    private TextResource _textResource;

    private Setting _setting;

    private bool _isShowSignIn;

    public GameObject PnlSignIn;

    public GameObject PnlSignUp;

    public Button BtnSignIn;

    public Button BtnSignUp;

    public Button BtnFacebookSignIn;

    public Button BtnFacebookSignUp;

    public Button BtnShowSignIn;

    public Button BtnShowSignUp;

    public Button BtnBack;

    public InputField InpSignInEmail;

    public InputField InpSignInPassword;

    public InputField InpSignUpEmail;

    public InputField InpSignUpPassword;

    public InputField InpNickname;

    public Toggle TglEnabledAutoLogin;

    public Text TxtMessage;

    public string Email
    {
        get { return (_isShowSignIn) ? InpSignInEmail.text: InpSignUpEmail.text; }
        set { InpSignInEmail.text = value;  }
    }

    public string Password
    {
        get { return (_isShowSignIn) ? InpSignInPassword.text : InpSignUpPassword.text; }
        set { InpSignInPassword.text = value; }
    }

    public string Nickname
    {
        get { return InpNickname.text; }
    }

    public string ErrorMessage
    {
        set { TxtMessage.text = value; }
    }

    public bool IsShowSignIn
    {
        get { return _isShowSignIn;  }
        set
        {
            _isShowSignIn = value;
            if (_isShowSignIn)
            {
                PnlSignUp.SetActive(false);
                PnlSignIn.SetActive(true);
            }
            else
            {
                PnlSignIn.SetActive(false);
                PnlSignUp.SetActive(true);
            }
        }
    }

    private void Awake()
    {
        _networkManager = NetworkManager.GetInstance();
        _setting = Setting.GetInstance();
        _textResource = TextResource.GetInstance();

        IsShowSignIn = true;

        // 자동 로그인이 설정된 경우
        if(_setting.IsEnabledAutoLogin)
        {
            TglEnabledAutoLogin.isOn = true;

            Email = _setting.Email;
            Password = _setting.Password;

            OnClickBtnSignIn();
        }

        BtnSignIn.onClick.AddListener(OnClickBtnSignIn);
        BtnSignUp.onClick.AddListener(OnClickBtnSignUp);
        BtnFacebookSignIn.onClick.AddListener(OnClickBtnFacebookSignIn);
        BtnFacebookSignUp.onClick.AddListener(OnClickBtnFacebookSignUp);
        BtnShowSignIn.onClick.AddListener(OnClickBtnShowSignIn);
        BtnShowSignUp.onClick.AddListener(OnClickBtnShowSignUp);
        BtnBack.onClick.AddListener(OnClickBtnBack);
        TglEnabledAutoLogin.onValueChanged.AddListener(OnChangedTglEnabledAutoLogin);

        SetTextSize();
        SetTextValue();
    }

    private async void OnClickBtnSignIn()
    {
        if (Email.Trim() == null || Email.Trim() == "")
        {
            ErrorMessage = _textResource.GetText(TextCode.THIS_IS_NOT_EMAIL_FORMAT);
            return;
        }
        if (Password.Trim() == null || Password.Trim() == "")
        {
            ErrorMessage = _textResource.GetText(TextCode.THIS_IS_NOT_PASSWORD_FORMAT);
            return;
        }
        if (!Email.IsEmail())
        {
            ErrorMessage = _textResource.GetText(TextCode.THIS_IS_NOT_EMAIL_FORMAT);
            return;
        }

        // 로그인
        var httpStatus = await _networkManager.SignIn(Email, Password);

        if (httpStatus.HttpCode == HttpCode.OK)
        {
            _setting.Email = Email;
            _setting.Password = Password;

            NextPage("LobbyPage");
        }
        else if (httpStatus.HttpCode == HttpCode.AccountNotFounded)
        {
            ErrorMessage = _textResource.GetText(TextCode.LOGIN_FAILED);
            return;
        }
    }

    private async void OnClickBtnSignUp()
    {
        if(Email.Trim() == null || Email.Trim() == "")
        {
            ErrorMessage = _textResource.GetText(TextCode.THIS_IS_NOT_EMAIL_FORMAT);
            return;
        }
        if (Password.Trim() == null || Password.Trim() == "")
        {
            ErrorMessage = _textResource.GetText(TextCode.THIS_IS_NOT_PASSWORD_FORMAT);
            return;
        }
        if(Nickname.Trim() == null || Nickname.Trim() == "")
        {
            ErrorMessage = _textResource.GetText(TextCode.THIS_IS_NOT_NICKNAME_FORMAT);
            return;
        }
        if (!Email.IsEmail())
        {
            ErrorMessage = _textResource.GetText(TextCode.THIS_IS_NOT_EMAIL_FORMAT);
            return;
        }

        // 회원가입
        var httpStatus = await _networkManager.SignUp(Email, Password, Nickname);

        if (httpStatus.HttpCode == HttpCode.OK)
        {
            OnClickBtnSignIn();
        }
        else if (httpStatus.HttpCode == HttpCode.AccountDuplicated)
        {
            ErrorMessage = _textResource.GetText(TextCode.SIGN_UP_FAILED);
            return;
        }
    }

    private void OnClickBtnFacebookSignIn()
    {

    }

    private void OnClickBtnFacebookSignUp()
    {

    }

    private void OnClickBtnShowSignIn()
    {
        IsShowSignIn = true;
    }

    private void OnClickBtnShowSignUp()
    {
        IsShowSignIn = false;
    }

    private void OnClickBtnBack()
    {
        NextPage("TitlePage");
    }

    private void OnChangedTglEnabledAutoLogin(bool value)
    {
        _setting.IsEnabledAutoLogin = value;
    }

    public void SetTextSize()
    {

    }

    public void SetTextValue()
    {
        // 로그인 화면
        InpSignInEmail.placeholder.GetComponent<Text>().text = _textResource.GetText(TextCode.EMAIL_ID);
        InpSignInPassword.placeholder.GetComponent<Text>().text = _textResource.GetText(TextCode.PASSWORD);
        BtnSignIn.GetComponent<Text>().text = _textResource.GetText(TextCode.LOGIN);
        BtnShowSignUp.GetComponent<Text>().text = _textResource.GetText(TextCode.SIGN_UP);
        BtnFacebookSignIn.GetComponent<Text>().text = _textResource.GetText(TextCode.LOGIN_WITH_FACEBOOK);
        TglEnabledAutoLogin.GetComponentInChildren<Text>().text = _textResource.GetText(TextCode.REMEMBER_ME);

        // 회원가입 화면
        InpSignUpEmail.placeholder.GetComponent<Text>().text = _textResource.GetText(TextCode.EMAIL_ID);
        InpSignUpPassword.placeholder.GetComponent<Text>().text = _textResource.GetText(TextCode.PASSWORD);
        InpNickname.placeholder.GetComponent<Text>().text = _textResource.GetText(TextCode.NICKNAME);
        BtnSignUp.GetComponent<Text>().text = _textResource.GetText(TextCode.SIGN_UP);
        BtnShowSignIn.GetComponent<Text>().text = _textResource.GetText(TextCode.LOGIN);
        BtnFacebookSignUp.GetComponent<Text>().text = _textResource.GetText(TextCode.SIGN_UP_WITH_FACEBOOK);
    }

    public void NextPage(string pageName)
    {
        SceneManager.LoadSceneAsync(pageName);
    }
}

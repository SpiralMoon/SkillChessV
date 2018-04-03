using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Assets.Support;

namespace Assets.Model
{
    /// <summary>
    /// 로컬 설정파일.
    /// </summary>
    public class Setting
    {
        private bool _isEnabledAutoLogin;

        private Country _country;

        private string _email;

        private string _password;

        private static Setting _instance;

        /// <summary>
        /// 자동 로그인.
        /// </summary>
        public bool IsEnabledAutoLogin
        {
            get
            {
                if (PlayerPrefs.HasKey("IsEnabledAutoLogin"))
                {
                    _isEnabledAutoLogin = bool.Parse(PlayerPrefs.GetString("IsEnabledAutoLogin"));
                }

                return _isEnabledAutoLogin;
            }
            set
            {
                _isEnabledAutoLogin = value;
                PlayerPrefs.SetString("IsEnabledAutoLogin", _isEnabledAutoLogin.ToString());
            }
        }

        /// <summary>
        /// 설정되있는 언어.
        /// </summary>
        public Country Country
        {
            get
            {
                if (PlayerPrefs.HasKey("Country"))
                {
                    _country = (Country) PlayerPrefs.GetInt("Country");
                }

                return _country;
            }
            set
            {
                _country = value;
                PlayerPrefs.SetInt("Country", (int)_country);
            }
        }

        /// <summary>
        /// 자동 로그인 때 쓰일 아이디.
        /// </summary>
        public string Email
        {
            get
            {
                if (PlayerPrefs.HasKey("Email"))
                {
                    _email = PlayerPrefs.GetString("Email");
                }

                return _email;
            }
            set
            {
                _email = value;
                PlayerPrefs.SetString("Email", _email);
            }
        }

        /// <summary>
        /// 자동 로그인 때 쓰일 비밀번호.
        /// </summary>
        public string Password
        {
            get
            {
                if (PlayerPrefs.HasKey("Password"))
                {
                    _password = PlayerPrefs.GetString("Password");
                }

                return _password;
            }
            set
            {
                _password = value;
                PlayerPrefs.SetString("Password", _password);
            }
        }

        public float Volume
        {
            get
            {
                if (PlayerPrefs.HasKey("Volume"))
                {
                    return PlayerPrefs.GetFloat("Volume");
                }
                else
                {
                    return 0.5f;
                }
            }
            set
            {
                PlayerPrefs.SetFloat("Volume", value);
            }
        }

        public float Quality
        {
            get
            {
                if (PlayerPrefs.HasKey("Quality"))
                {
                    return PlayerPrefs.GetFloat("Quality");
                }
                else
                {
                    return 3f;
                }
            }
            set
            {
                PlayerPrefs.SetFloat("Quality", value);
            }
        }

        public Setting() { }

        public static Setting GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Setting();
            }

            return _instance;
        }
    }
}

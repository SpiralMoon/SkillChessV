using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Assets.Model.SceneParameter;

namespace Assets.Service
{
    public class PageParameterDispatcher : MonoBehaviour
    {
        private static PageParameterDispatcher _instance = null;

        private PageParameter PageParameter;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
        }

        public static PageParameterDispatcher Instance()
        {
            return _instance;
        }

        public void SetPageParameter(PageParameter param)
        {
            this.PageParameter = param;
        }

        public PageParameter GetPageParameter()
        {
            return this.PageParameter;
        }
    }
}

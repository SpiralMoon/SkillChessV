using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Model.Impl
{
    /// <summary>
    /// 화면 인터페이스
    /// </summary>
    interface IPage
    {
        /// <summary>
        /// 모든 페이지는 Awake 단계에서 Text 크기를 조절해주어야 함.
        /// </summary>
        void SetTextSize();

        /// <summary>
        /// 모든 페이지는 Awake 단계에서 Text 내용을 할당해주어야 함.
        /// </summary>
        void SetTextValue();

        /// <summary>
        /// 모든 페이지는 다른 화면으로 전환할 때 이 함수를 호출하여야 함.
        /// </summary>
        void NextPage(string pageName);
    }
}

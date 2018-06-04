using Assets.Support;

namespace Assets.Model.SceneParameter
{
    public class ResultPageParameter : PageParameter
    {
        /// <summary>
        /// Pattern.FINISH or Pattern.SURRENDER
        /// </summary>
        public Pattern Pattern;

        /// <summary>
        /// BattlePage에서 ResultForm을 보낸 플레이어의 색
        /// Pattern.FINISH => winner color
        /// Pattern.SURRENDER => loser color
        /// </summary>
        public string Color;

        public DashBoard WhiteDashBoard;

        public DashBoard BlackDashBoard;
    }
}

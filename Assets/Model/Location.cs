namespace Assets.Model
{
    /// <summary>
    /// 기물의 좌표를 표시하는 객체
    /// </summary>
    public class Location
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Location() { }

        public Location(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}

namespace Blinko_5_minute.darkStore
{
    public class DarkStore
    {
        public int id { get; set; }
        public double _Xcord { get; set; }
        public double _Ycord { get; set; }
        public string _name { get; set; }
        public DarkStore(double xcord, double ycord, string name)
        {
            _Xcord = xcord;
            _Ycord = ycord;
            _name = name;
        }

        public DarkStore()
        {

        }

        public double GetDistance(double x, double y)
        {
            return Math.Sqrt(Math.Pow(_Xcord -x, 2) + Math.Pow(_Ycord - y, 2));
        }
    }
}

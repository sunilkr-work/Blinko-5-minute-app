namespace Blinko_5_minute.darkStore
{
    public class DarkStore
    {
       public Guid id;
       public double _Xcord;
       public double _Ycord;
       public string _name;
        public DarkStore(double xcord, double ycord, string name)
        {
            _Xcord = xcord;
            _Ycord = ycord;
            _name = name;
        }

        public double GetDistance(double x, double y)
        {
            return Math.Sqrt(Math.Pow(_Xcord -x, 2) + Math.Pow(_Ycord - y, 2));
        }
    }
}

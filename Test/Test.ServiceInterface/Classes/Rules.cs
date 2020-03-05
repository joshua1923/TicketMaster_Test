using Test.ServiceInterface.Interfaces;

namespace Test.ServiceInterface.Classes
{
    public class Rules : IRules
    {
        public bool IsAMultipleOf(int n, int m)
        {
            if (n % m == 0)
            {
                return true;
            }

            return false;
        }
    }
}

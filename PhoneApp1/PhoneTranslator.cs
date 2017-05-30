using System.Linq;
using System.Text;

namespace PhoneApp1
{
    public class PhoneTranslator
    {
        string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string Numbers = "22233344455566677778889999";

        public string ToNumber(string alfanumericPhoneNumber)
        {

            var NumericPhoneNumber = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(alfanumericPhoneNumber))
            {
                foreach (var c in alfanumericPhoneNumber)
                {

                    if ("0123456789".Contains(c))
                    {
                        NumericPhoneNumber.Append(c);
                    }
                    else
                    {
                        var Index = Letters.IndexOf(c);
                        if (Index >= 0)
                        {
                            NumericPhoneNumber.Append(Numbers[Index]);
                        }
                    }
                }
            }
            return NumericPhoneNumber.ToString();
        }

    }
}
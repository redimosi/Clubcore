namespace Clubcore.Domain.AggregatesModel
{
    public class PhoneNumber
    {
        private readonly string _number;

        public PhoneNumber(string number)
        {
            if (!IsValid(number))
            {
                throw new ArgumentException("Invalid phone number format.");
            }

            _number = NormalizeNumber(number);
        }

        public string Number => _number;

        public static bool IsValid(string number)
        {
            number = NormalizeNumber(number);
            if (string.IsNullOrEmpty(number)) return false;
            if(!number.Substring(1).All(char.IsDigit)) return false;
            if(!number.StartsWith('+')) return false;
            if(number.StartsWith("+41"))
            {
                // Swiss number, check length
                if (number.Length != 12)
                {
                    return false;
                }
            }
            return true;
        }

        private static string NormalizeNumber(string number)
        {
            number = number.Replace(" ", "");
            if (number.StartsWith('+'))
            {
                // already in international format
                return number;
            }
            if(number.StartsWith("00"))
            {
                // exchange leading 00 with correct + as international prefix
                return string.Concat("+", number.AsSpan(2));
            }
            if(number.StartsWith('0') && number.Length == 10)
            {
                // local number, add exchange 0 with +41 for Switzerland as international prefix
                return string.Concat("+41", number.AsSpan(1));
            }
            else
            {
                return string.Empty;
            }
        }
    }
}

using System.Text;

namespace Cleverence1
{
    public class EncodeDecode
    {
        public string Encode(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;

            StringBuilder encoded = new StringBuilder();
            char current = input[0];
            int count = 1;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == current)
                {
                    count++;
                }
                else
                {
                    encoded.Append(current); 
                    if (count > 1) encoded.Append(count); 
                    current = input[i];
                    count = 1;
                }
            }

            // Последняя группа
            encoded.Append(current);
            if (count > 1) encoded.Append(count);

            return encoded.ToString();
        }

        public string Decode(string input)
        {
            StringBuilder decoded = new StringBuilder();
            StringBuilder numberBuffer = new StringBuilder(); 

            foreach (char current in input)
            {
                if (char.IsDigit(current))
                {
                    numberBuffer.Append(current); 
                }
                else
                {
                    if (numberBuffer.Length == 0)
                    {
                        decoded.Append(current);
                    }
                    else
                    {
                        int count = int.Parse(numberBuffer.ToString());
                        for (int j = 0; j < count; j++)
                            decoded.Append(current);
                        numberBuffer.Clear(); 
                    }
                }
            }
            return decoded.ToString();
        }

        public string[] Handle(string input)
        {
            string encoded = Encode(input);
            string decoded = Decode(encoded);
            string[] result = [encoded, decoded];
            return result;
        }
    }
}


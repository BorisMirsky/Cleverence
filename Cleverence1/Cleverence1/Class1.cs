using System;
using System.Text;


namespace Cleverence1
{
    public class EncodeDecode
    {
        public string Encode(string input)
        {

            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            else if (input.Length == 1)
            {
                return input;
            }

            StringBuilder encoded = new StringBuilder();
            char temp = input[0];
            int count = 1;

            for (int i = 1; i < input.Length; i++)
            {
                if (input[i] == temp)
                {
                    count++;
                }
                else
                {
                    if (count == 1)
                    {
                        encoded.Append(temp);
                        temp = input[i];
                    }
                    else
                    {
                        encoded.Append(count);
                        encoded.Append(temp);
                        temp = input[i];
                        count = 1;
                    }
                }
            }

            if (count != 1)
            {
                encoded.Append(count);
            }
            encoded.Append(temp);

            return encoded.ToString();
        }

        public string Decode(string input)
        {

            string temp = String.Empty;
            StringBuilder decoded = new StringBuilder();

            foreach (char current in input)
            {
                if (char.IsDigit(current))
                    temp += current;
                else
                {
                    if (temp == String.Empty)
                        decoded.Append(current);
                    else
                    {
                        int count = int.Parse(temp);
                        temp = String.Empty;
                        for (int j = 0; j < count; j++)
                            decoded.Append(current);
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


using System;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace feistel
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class Feistel
    {
        // The feistel class is used for encryption with a feistel cypher.
        /*
            -----       -----       -----
           |  A  |     |  K  |     |  B  |
            -----       -----       -----
              |           |           |
              V         -----         |
             XOR <-----|  F  |<-------/
              |         -----        /
              \              /------/                repeating /ROUNDS/ times
               \            /
                \------------------\
                          /         \
                         /           |
            -----       /           -----
           |  A  |<----/           |  B  |
            -----                   -----
        */
        private readonly int ROUNDS = 1;

        public Feistel(int rounds)
        {
            ROUNDS = rounds;
        }

        public string Encode(string message, string key)
        {
            int A = Convert.ToInt32(message.Remove(message.Length / 2), 2);
            int B = Convert.ToInt32(message.Substring(message.Length / 2), 2);
            int integerKey = Convert.ToInt32(key, 2);

            for (int i = 0; i < ROUNDS; i++)
            {
                int copyB = B;

                B = A ^ CoddingFunction(B, integerKey, message.Length);
                A = copyB;

                integerKey = RotateLeft(integerKey, key.Length);
            }

            string stringA = Convert.ToString(A, 2);
            string stringB = Convert.ToString(B, 2);

            string result = Filler(stringA, message.Length / 2);
            result += Filler(stringB, message.Length / 2);
            return result;
        }

        public static string Filler(string mess, int lenght)
        {
            // The filler method is used for adding "0" at the start of a string 
            // till string lenght not equal the given one
            //
            // Filler("100101", 8) --> "00100101"
            //                          ^^

            if (mess.Length >= lenght) { return mess; }

            StringBuilder builder = new StringBuilder(lenght);

            for (int i = 0; i < lenght - mess.Length; i++)
            {
                builder.Append('0');
            }
            builder.Append(mess);

            return builder.ToString();
        }

        public static int RotateLeft(int number, int lenght)
        {
            // The RotateLeft method is used for cyclic bit rotation
            // /lenght/ is used only for bit strings with zeroes at start
            // if /lenght/ < binary /number/ lenght it does nothing
            //
            // RotateLeft(113, 8) --> 226
            //             ^           ^
            //         01110001    11100010

            string stringBinaryNumber = Filler(Convert.ToString(number, 2), lenght);
            string rotateResult = stringBinaryNumber.Substring(1) + stringBinaryNumber.Remove(1);
            return Convert.ToInt32(rotateResult, 2);

        }

        private static int CoddingFunction(int message, int key, int n)
        {
            // Specific given function

            int modulus = (int)Math.Pow(2, n / 2);

            int PartOne = (key * (key + 1) / 2) % modulus;
            int PartTwo = ExponentiationByModulus(message, key, modulus);

            return (PartOne + PartTwo) % modulus;
        }


        public static int ExponentiationByModulus(int number, int degree, int modulus)
        {
            // Memory efficient eponentiation by modulus

            number %= modulus;

            BitArray degreeBinaryRepresentation = new BitArray(new int[] { degree });

            int finalResult = 1;
            if (degreeBinaryRepresentation[0])
                finalResult = number;

            int CalculationResult = number;
            for (int i = 1; i <= (int)Math.Log(degree, 2); i++)
            {
                CalculationResult *= CalculationResult;
                CalculationResult %= modulus;

                if (degreeBinaryRepresentation[i])
                {
                    finalResult *= CalculationResult;
                    finalResult %= modulus;
                }
            }

            return finalResult % modulus;
        }
    }

}

using System;
namespace CAXMLDocumentation
{

    internal class Program
    {
        private static void Main(string[] args)
        {
            //Generator.LastIdSequence
            do
            {
                Console.WriteLine("First Name:");
                var fname = Console.ReadLine();
                Console.WriteLine("Last Name:");
                var lname = Console.ReadLine();
                Console.WriteLine("Hire Date:");
                DateTime? hireDate = null;

                if (DateTime.TryParse(Console.ReadLine(), out DateTime hDate))
                {
                    hireDate = hDate;
                }
                var empId = Generator.GenerateId(fname, lname, hireDate);
                var randomPassword = Generator.GenerateRandomPassword(8);
                Console.WriteLine($"{{\n Id:{empId}, \n Fname: {fname}, \n Lname: {lname}, \n hire date: {randomPassword}, \n }}");
            }
            while (1 == 1);
        }
    }
    /// <summary>
    /// The main Generator class
    /// </summary>
    /// <remarks>
    /// This class can generat employee Ids and random passwords
    /// </remarks>
    public class Generator
    {
       /// <value>
       /// value of last Id sequence
       /// 
       /// </value>
        public static int LastIdSequence { get; set; } = 1;
        /// <summary>
        /// Generrate Employee Id by processing  <paramref name="fname"/>,<paramref name="lname"/> and <paramref name="hireDate"/>
        /// <list type="bullet"
        /// <item>
        /// <term>II</term>
        /// <description>Employee Initials (first letter of lname <paramref name="lname"/> and first letter of <paramref name="fname"/></description>
        /// </item>
        ///    /// <item>
        /// <term>YY</term>
        /// <description>hire Date 2 digit year</description>
        /// </item>
        ///    /// <item>
        /// <term>MM</term>
        /// <description>hire Date 2 digit month</description>
        /// </item>
        /// <item>
        /// <term>MM</term>
        /// <description>hire Date 2 digit day</description>
        /// </item>
        /// <item>
        /// <term>SS</term>
        /// <description>Sequence No. (01-99)</description>
        /// </item>
        /// </list>
        /// </summary>

        /// <param name="fname"></param>
        /// <param name="lname"></param>
        /// <param name="hireDate"></param>
        /// <example>
        /// <code>
        /// var id = Generator.Generate("John","Smith", new DateTime(2024,08,21,0,0,0)
        /// Conssole.WriteLine(id);
        /// </code>
        /// </example>
        /// <returns> employee Id as a string</returns>
        /// <exception cref="System.InvalidOperationException"> Thrown when <paramref name="fname"/>is null</exception>
        /// <exception cref="System.InvalidOperationException"> Thrown when <paramref name="lname"/>is null </exception>
        /// <exception cref="System.InvalidOperationException"> Thrown when <paramref name="hireDate"/>is in the past </exception> 
        /// See <see cref="Generator.GenerateRandomPassword(int)"/> to Generate Random password 
       
        public static string GenerateId(string fname, string lname, DateTime? hireDate)
        {
            //II YY MM DD 01 
            if (fname is null)
                throw new InvalidOperationException($"{nameof(fname)} can not be null");
            if (lname is null)
                throw new InvalidOperationException($"{nameof(lname)} can not be null");
            if (hireDate is null)
            {
                hireDate = DateTime.Now;
            }
            else
            {
                if (hireDate.Value < DateTime.Now)
                    throw new InvalidOperationException($"{nameof(hireDate)} can not be  in the past");
            }
            var yy = hireDate.Value.ToString("yy");
            var mm = hireDate.Value.ToString("MM");
            var dd = hireDate.Value.ToString("dd");
            var code = $" {lname.ToUpper()[0]}{fname.ToUpper()[0]}{yy}{mm}{dd}{(LastIdSequence++).ToString().PadLeft(2, '0')}";
            return code;
        }
        public static string GenerateRandomPassword(int length)
        {
            const string ValidScope = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var result = "";
            Random rnd = new Random();
            while (0 < length--)
            {
                result += (ValidScope[rnd.Next(ValidScope.Length)]);
            }
            return result;
        }
    }
}
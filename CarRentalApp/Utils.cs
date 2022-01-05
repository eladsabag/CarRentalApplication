using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRentalApp
{
    public static class Utils
    {
        public static bool FormIsOpen(string name)
        {
            //Check if window is already open
            var OpenForms = Application.OpenForms.Cast<Form>();
            var isOpen = OpenForms.Any(q => q.Name == name);
            return isOpen;
        }

        public static string HashPassword(string password)
        {
            SHA256 sha = SHA256.Create();

            //convert the input string to a byte array and compute the hash
            byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes(password));

            //create a new stringbuilder to collect the bytes
            //and create a string
            StringBuilder sb = new StringBuilder();

            //loop through each byte if the hashed data
            //and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }


            var hashed_password = sb.ToString();
            return hashed_password;
        }

        public static string DefaultHashPassword()
        {
            SHA256 sha = SHA256.Create();

            //convert the input string to a byte array and compute the hash
            byte[] data = sha.ComputeHash(Encoding.UTF8.GetBytes("Password@123"));

            //create a new stringbuilder to collect the bytes
            //and create a string
            StringBuilder sb = new StringBuilder();

            //loop through each byte if the hashed data
            //and format each one as a hexadecimal string
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }


            var hashed_password = sb.ToString();
            return hashed_password;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProfileClassLibrary
{
    public enum Gender
    {
        Male,
        Female,
        Notdetetmined
    }
    public abstract class Person
    {

        protected Gender _gender;
        protected DateTime _birthday;
        protected string _lastName, _firstName, _patronymic, _phoneNumber, _reserveEmail, _password;

        public Person(string email)=> GetAllDataAboutPerson(email);

        public int Id { get;set; }
        public string LastName { get => _lastName;}
        public string Patronymic { get => _patronymic;}
        public string Name{ get =>_firstName;}
        public Gender Gender{ get => _gender; }
        public DateTime BirthDate { get => _birthday; }

        private void GetAllDataAboutPerson(string email)
        {
            SqlConnection sqlConnection = new SqlConnection(@"Data Source=DESKTOP-HOHELQO;Initial Catalog=allData;Integrated Security=True");
            string querystring = $"SELECT UPD.FirstName,UPD.LastName,UPD.Patronymic,UPD.Number,UPD.Birthday,UPD.Gender,PLD.ReserveEmail,PP.Password FROM UserPersonalData AS UPD,PersonalLoginData AS PLD,PersonalPassword AS PP WHERE UPD.Id=PP.UserPersonalDataId AND UPD.Id = PLD.UserPersonalDataId AND PLD.Email='{email}'; ";
            SqlCommand sqlCommand = new SqlCommand(querystring, sqlConnection);
            sqlConnection.Open();
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            try
            {
                while (sqlDataReader.Read())
                {
                    _lastName = Convert.ToString(sqlDataReader["LastName"]);
                    _firstName = Convert.ToString(sqlDataReader["FirstName"]);
                    _patronymic = Convert.ToString(sqlDataReader["Patronymic"]);
                    _phoneNumber = Convert.ToString(sqlDataReader["Number"]);
                    try
                    {
                        _birthday = Convert.ToDateTime(sqlDataReader["Birthday"]);
                    }
                    catch (Exception)
                    {
                        _birthday = DateTime.Now;
                    }
                    try
                    {
                        _gender = (Gender)(Convert.ToInt32(sqlDataReader["Gender"]));
                    }
                    catch (Exception)
                    {
                        _gender = (Gender)2;
                    }
                    _reserveEmail = Convert.ToString(sqlDataReader["ReserveEmail"]);
                    _password = Convert.ToString(sqlDataReader["Password"]);
                }
            }
            catch (Exception)
            {
                throw;
            }
            sqlConnection.Close();
        }
    }
}

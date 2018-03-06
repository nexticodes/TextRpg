using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TextRpg;
using System;

namespace TextRpg.Models
{
    public class GameUser
    {
        private int _id;
        private string _name;
        private string _username;
        private string _password;
        private string _email;
        private int _roomNumber;
        private static Character _character;

        public GameUser(string name, string username, string password, string email, int roomNumber)
        {
            _name = name;
            _username = username;
            _password = password;
            _email = email;
            _roomNumber = roomNumber;
        }

        public int GetId()
        {
            return _id;
        }

        public void SetId(int id)
        {
            _id = id;
        }

        public int GetRoomNumber()
        {
            return _roomNumber;
        }

        public void SetRoomNumber(int num)
        {
            _roomNumber = num;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetUserName()
        {
            return _username;
        }

        public string GetPassword()
        {
            return _password;
        }

        public string GetEmail()
        {
            return _email;
        }

        public Character GetCharacter()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM characters WHERE user_id = @user_id;";
            var userIdPara = new MySqlParameter("@user_id", _id);
            cmd.Parameters.Add(userIdPara);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int id =  0;
            string name = "";
            int level = 0;
            int exp = 0;
            int maxHp = 0;
            int hp = 0;
            int ad = 0;
            int iq = 0;
            int dex = 0;
            int lck = 0;
            int charisma = 0;
            int armor = 0;

            while (rdr.Read())
            {
                id =  rdr.GetInt32(0);
                name = rdr.GetString(1);
                level = rdr.GetInt32(2);
                exp = rdr.GetInt32(3);
                maxHp = rdr.GetInt32(4);
                hp = rdr.GetInt32(5);
                armor = rdr.GetInt32(6);
                ad = rdr.GetInt32(7);
                iq = rdr.GetInt32(8);
                dex = rdr.GetInt32(9);
                lck = rdr.GetInt32(10);
                charisma = rdr.GetInt32(11);
            }
            Character thisCharacter = new Character(name, level, exp, maxHp, hp, armor, ad, iq, dex, lck, charisma, id);
            thisCharacter.SetId(id);
            return thisCharacter;

        }
    }
}

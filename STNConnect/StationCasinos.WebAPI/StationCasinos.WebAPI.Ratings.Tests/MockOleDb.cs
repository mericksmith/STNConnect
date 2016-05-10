using StationCasinos.Repository.Interface.Ratings;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationCasinos.WebAPI.Ratings.Tests
{
    public class MockOleDbCommand : IOleDbCommand
    {
        private OleDbCommand _command = new OleDbCommand();

        public string CommandText { get; set; }

        public int CommandTimeout { get; set; }

        public CommandType CommandType { get; set; }

        public IOleDbConnection Connection { get; set; }

        public OleDbParameterCollection Parameters
        {
            get
            {
                return _command.Parameters;
            }
        }

        public OleDbParameter CreateParameter()
        {
            return _command.CreateParameter();
        }

        public int ExecuteNonQuery()
        {

            try
            {
                if (Parameters.Contains("ERRORCODE") && Parameters["ACTION"].Value.ToString() == "A") //Add
                {
                    Parameters["TRANS#"].Value = GenerateNumber(11) + "000";
                    Parameters["ERRORCODE"].Value = "00";
                }
                else //Void and Update
                {
                    string trans = Parameters["TRANS#"].Value.ToString();
                    string seq = Parameters["SEQ#"].Value.ToString();

                    string newSeq = (int.Parse(seq) + 1).ToString().PadLeft(3, '0');
                    Parameters["SEQ#"].Value = newSeq;
                    Parameters["RTNCDE"].Value = "00";
                }

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public void Prepare()
        {
        }

        private static string GenerateNumber(int length)
        {
            Random random = new Random();
            string r = "";
            int i;
            for (i = 1; i <= length; i++)
            {
                r += random.Next(0, 9).ToString();
            }
            return r;
        }
    }

    public class MockOleDbConnection : IOleDbConnection
    {
        private string _connString;
        private ConnectionState _state;

        public void SetConnectionString(string connString)
        {
            _connString = connString;
        }

        private OleDbConnection _connection;

        public OleDbConnection Connection
        {
            get
            {
                return _connection;
            }

            set
            {
                _connection = value;
            }
        }

        public ConnectionState State
        {
            get
            {
                return _state;
            }
        }

        public void Open()
        {
            _state = ConnectionState.Open;
        }

        public void Close()
        {
            _state = ConnectionState.Closed;
        }
    }
}

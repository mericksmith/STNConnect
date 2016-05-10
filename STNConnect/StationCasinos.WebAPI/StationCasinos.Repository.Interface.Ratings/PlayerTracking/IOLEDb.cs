using System;
using System.Data;
using System.Data.OleDb;

namespace StationCasinos.Repository.Interface.Ratings
{

    #region "OleDbCommand"

    public interface IOleDbCommand
    {
        OleDbParameterCollection Parameters { get; }

        CommandType CommandType { get; set; }

        string CommandText { get; set; }

        int CommandTimeout { get; set; }

        OleDbParameter CreateParameter();

        void Prepare();

        int ExecuteNonQuery();

        IOleDbConnection Connection { get; set; }
    }

    public class MyOleDbCommand : IOleDbCommand
    {
        private OleDbCommand _command = new OleDbCommand();
        private IOleDbConnection _connection;

        public string CommandText
        {
            get { return _command.CommandText; }

            set { _command.CommandText = value; }
        }

        public int CommandTimeout
        {
            get { return _command.CommandTimeout; }

            set { _command.CommandTimeout = value; }
        }

        public CommandType CommandType
        {
            get { return _command.CommandType; }

            set { _command.CommandType = value; }
        }

        public OleDbParameterCollection Parameters
        {
            get { return _command.Parameters; }
        }

        public int ExecuteNonQuery()
        {
            return _command.ExecuteNonQuery();
        }

        public void Prepare()
        {
            _command.Prepare();
        }

        public OleDbParameter CreateParameter()
        {
            return _command.CreateParameter();
        }

        public IOleDbConnection Connection
        {
            set
            {
                _connection = value;
                _command.Connection = value.Connection;
            }
            get
            {
                return _connection;
            }
        }
    }

    #endregion


    #region "OleDbConnection"

    public interface IOleDbConnection
    {
        ConnectionState State { get; }

        void Open();

        void Close();

        OleDbConnection Connection { get; set; }

        void SetConnectionString(string connString);
    }

    
    public class MyOleDbConnection : IOleDbConnection
    {
        private OleDbConnection _connection;

        //public MyOleDbConnection(string connString)
        //{
        //    _connection = new OleDbConnection(connString);
        //}

        public void SetConnectionString(string connString)
        {
            Connection = new OleDbConnection(connString);
        }


        public OleDbConnection Connection {
            get { return _connection; }

            set { _connection = value; }
        }

        public ConnectionState State
        {
            get
            {
                return _connection.State;
            }
        }

        public void Close()
        {
            _connection.Close();
        }

        public void Open()
        {
            _connection.Open();
        }
    }
    
    #endregion

}

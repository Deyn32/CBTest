using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CBTest.models;

namespace CBTest.services.impl
{
    public class SqlServiceImpl : SqlService
    {
        public List<ParticipantInfo> findAll()
        {
            using (var connection = new SQLiteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SQLiteCommand insertSQL = connection.CreateCommand();
                insertSQL.CommandText = "SELECT * FROM ParticipantInfo";
            }

            return null;
        }

        public void SaveAccount(Account account)
        {
            using (var connection = new SQLiteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SQLiteCommand insertSQL = connection.CreateCommand();
                insertSQL.CommandText = "INSERT INTO Account (DateIn, Status, CBRBIC, Number, CK, Type, Participant)" +
                    " VALUES (@DateIn,@Status,@CBRBIC,@Number,@CK,@Type,@Participant)";
                insertSQL.Parameters.Add("DateIn", DbType.Date).Value = account.DateIn;
                insertSQL.Parameters.Add("Status", DbType.String).Value = account.Status;
                insertSQL.Parameters.Add("CBRBIC", DbType.String).Value = account.CbrBic;
                insertSQL.Parameters.Add("Number", DbType.String).Value = account.Number;
                insertSQL.Parameters.Add("CK", DbType.String).Value = account.CK;
                insertSQL.Parameters.Add("Type", DbType.String).Value = account.Type;
                insertSQL.Parameters.Add("Participant", DbType.Int32).Value = account.Participant;
                try
                {
                    insertSQL.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void SaveParticipantInfo(ParticipantInfo participantInfo)
        {
            using (var connection = new SQLiteConnection("Data Source=usersdata.db"))
            {
                connection.Open();
                SQLiteTransaction transaction = null;
                transaction = connection.BeginTransaction();
                SQLiteCommand insertSQL = connection.CreateCommand();
                insertSQL.CommandText ="INSERT INTO ParticipantInfo (Status, Name, RegionName, Address, Country, DateIn, BIC)" +
                    " VALUES (@Status,@Name,@RegionName,@Address,@Country,@DateIn,@BIC)";
                insertSQL.Parameters.Add("Status", DbType.String).Value = participantInfo.Status;
                insertSQL.Parameters.Add("Name", DbType.String).Value = participantInfo.Name;
                insertSQL.Parameters.Add("RegionName", DbType.String).Value = participantInfo.RegionName;
                insertSQL.Parameters.Add("Address", DbType.String).Value =  participantInfo.Address;
                insertSQL.Parameters.Add("Country", DbType.String).Value = participantInfo.Country;
                insertSQL.Parameters.Add("DateIn", DbType.Date).Value = participantInfo.DateIn;
                insertSQL.Parameters.Add("BIC", DbType.String).Value = participantInfo.Bic;
                try
                {
                    insertSQL.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                participantInfo.Id = (int)connection.LastInsertRowId;

                transaction.Commit();
            }
        }
    }
}

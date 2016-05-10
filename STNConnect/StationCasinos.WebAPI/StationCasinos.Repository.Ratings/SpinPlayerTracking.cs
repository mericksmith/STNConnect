using StationCasinos.EnterpriseObjects.Ratings;
using StationCasinos.Repository.Interface.Lookup;
using StationCasinos.Repository.Interface.Ratings;
using StationCasinos.Repository.Interface.Ratings.PlayerTracking;
using StationCasinos.WebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Threading.Tasks;
using StationCasinos.WebAPI.Utility.Interface;
using System.Text;

namespace StationCasinos.Repository.Ratings
{
    public class SpinPlayerTracking : IPlayerTracking
    {
        private string CMSPGM = ConfigurationManager.AppSettings["CMSPGM"];
        private string CMS_UPDATE_PROC = ConfigurationManager.AppSettings["CMS_UPDATE_PROC"];
        private string CMS_ADD_VOID_PROC = ConfigurationManager.AppSettings["CMS_ADD_VOID_PROC"];
        private int CMSTimeoutMillis => int.Parse(ConfigurationManager.AppSettings["CMSTimeoutMillis"]); 

        private string AddActionId = ConfigurationManager.AppSettings["AddActionId"];
        private string VoidActionId = ConfigurationManager.AppSettings["VoidActionId"];
        private string UpdateActionId = ConfigurationManager.AppSettings["UpdateActionId"];
        private string EmptyTransId = ConfigurationManager.AppSettings["EmptyTransId"];
        private string CMSUserId => ConfigurationManager.AppSettings["CMSUserId"]; 
        private string GenericCMSErrorMessage => ConfigurationManager.AppSettings["GenericCMSErrorMessage"];

        private ILookupRepository _lookupRepository;
        private IOleDbConnection _connection;
        private IOleDbCommand _command;
        private ILogging _logging;

        public SpinPlayerTracking(ILookupRepository lookupRepository, IOleDbConnection connection, IOleDbCommand command, ILogging logging)
        {
            _lookupRepository = lookupRepository;
            _connection = connection;
            _command = command;
            _logging = logging;
        }
        
        public EventRating Create<T>(EventRating rating, GameCodeType gameCodeType) where T : EventRating
        {
            return VoidAndCreate(rating, gameCodeType);
        }

        public EventRating Void<T>(EventRating rating, GameCodeType gameCodeType) where T : EventRating
        {
            return VoidAndCreate(rating, gameCodeType);
        }

        public EventRating Update<T>(EventRating rating, GameCodeType gameCodeType) where T : EventRating
        {
            EObjectRatingAction ratingAction = null;
            EObjectRatingSession ratingSession = null;
            EObjectRating ratingObject = null;

            rating.ExtractRatingObjects(out ratingAction, out ratingSession, out ratingObject);

            var transId = ratingObject.ratingHostId.Substring(0, 11);
            var seqNum = ratingObject.ratingHostId.Substring(11, 3);

            try
            {
                //Amount patron originally paid - amount won. This means if they lost, the negative actualWin will be positive.
                decimal winLoss = ratingAction.totalBuyIn - ratingAction.actualWin;

                //We need to find the property code from int.
                var propertyCode = _lookupRepository.GetPropertyCode(rating.PropertyCode);

                var info = CMS_UpdateRatings(propertyCode,
                                                ratingObject.patronId,
                                                transId,
                                                seqNum,
                                                winLoss,
                                                ratingAction.actualWin,
                                                ratingObject.pointsEarned
                                            );

                if (info.Success)
                {
                    ratingObject.ratingHostId = string.Format("{0:00000000000}{1:000}", info.TransactionID, info.SequenceNumber); //send the CMSId back.
                    _logging.Write(string.Format("Successful Update, CMS record: {0}", ratingObject.ratingHostId), "SpinPlayerTracking.Update");
                }
                    
                else
                {
                    //Write this out so we can see specific CMS errors
                    _logging.Write(info.ErrorDescription, "CMS_ADD_VOID_SPORTS_RATINGS");
                    throw new Exception(GenericCMSErrorMessage);
                }
            }
            catch (Exception ex)
            {                
                throw new WebAPIException(System.Net.HttpStatusCode.Conflict, ex);
            }

            return rating;
        }
        
        private EventRating VoidAndCreate(EventRating rating, GameCodeType gameCodeType)
        {

            EObjectRatingAction ratingAction = null;
            EObjectRatingSession ratingSession = null;
            EObjectRating ratingObject = null;

            rating.ExtractRatingObjects(out ratingAction, out ratingSession, out ratingObject);

            string ActionId = AddActionId;
            string transId = EmptyTransId;
            int seqNum = 0;

            if (ratingObject.ratingStatus == ratingStatus.Void)
            {
                transId = ratingObject.ratingHostId.Substring(0, 11);
                seqNum = int.Parse(ratingObject.ratingHostId.Substring(11, 3));
                ActionId = VoidActionId;
            }
            
            //We need to find the property code from int.
            var propertyCode = _lookupRepository.GetPropertyCode(rating.PropertyCode);

            //We need to find game code based on gameId and locationId
            var gameCodeInfo = _lookupRepository.GetGameCodeInfo(ratingSession.gameId, gameCodeType, ratingSession.locationId);

            try
            {
                decimal winLoss = ratingAction.totalBuyIn - ratingAction.actualWin;

                var info = CMS_ADD_VOID_SPORTS_RATINGS(propertyCode,
                                            ratingObject.patronId,
                                            ratingAction.totalBuyIn,
                                            ratingAction.actualWin,
                                            winLoss,
                                            ratingObject.pointsEarned,
                                            ratingSession.shiftId,
                                            rating.transDateTime,
                                            gameCodeInfo,
                                            ActionId,
                                            transId);

                if (info.Success)
                {
                    ratingObject.ratingHostId = string.Format("{0:00000000000}{1:000}", info.TransactionID, info.SequenceNumber); //send the CMSId back.
                    _logging.Write(string.Format("Successful {0}, CMS record: {1}", ratingObject.ratingStatus.ToString(), ratingObject.ratingHostId), "SpinPlayerTracking.VoidAndCreate");
                }
                else
                {
                    //Write this out so we can see specific CMS errors
                    _logging.Write(info.ErrorDescription, "CMS_ADD_VOID_SPORTS_RATINGS");
                    throw new Exception(GenericCMSErrorMessage);
                }
            }
            catch (Exception ex)
            {
                throw new WebAPIException(System.Net.HttpStatusCode.Conflict, ex);
            }

            return rating;
        }


        public RatingInformation CMS_UpdateRatings(string propertyCode,
                                                   string patronId,
                                                   string transactionId,
                                                   string sequenceNumber,
                                                   decimal winLoss,
                                                   decimal coinOut,
                                                   decimal points)
        {
            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = CMSPGM + CMS_UPDATE_PROC;
            _command.CommandTimeout = 5;

            _command.Parameters.Add(BuildParameter(_command, "PROP", OleDbType.Char, ParameterDirection.InputOutput, paramLength["PROP"]));
            _command.Parameters.Add(BuildParameter(_command, "SYID", OleDbType.Decimal, ParameterDirection.InputOutput, paramLength["SYID"]));
            _command.Parameters.Add(BuildParameter(_command, "TRANS#", OleDbType.Decimal, ParameterDirection.InputOutput, paramLength["TRANS#"]));
            _command.Parameters.Add(BuildParameter(_command, "SEQ#", OleDbType.Decimal, ParameterDirection.InputOutput, paramLength["SEQ#"]));
            _command.Parameters.Add(BuildParameter(_command, "ACTION", OleDbType.Char, ParameterDirection.InputOutput, paramLength["ACTION"]));
            _command.Parameters.Add(BuildParameter(_command, "WINLOSS", OleDbType.Decimal, ParameterDirection.InputOutput, paramLength["WINLOSS"]));
            _command.Parameters.Add(BuildParameter(_command, "COINOUT", OleDbType.Decimal, ParameterDirection.InputOutput, paramLength["COINOUT"]));
            _command.Parameters.Add(BuildParameter(_command, "POINTS", OleDbType.Decimal, ParameterDirection.InputOutput, paramLength["POINTS"]));
            _command.Parameters.Add(BuildParameter(_command, "INITUSR", OleDbType.Char, ParameterDirection.InputOutput, paramLength["INITUSR"]));
            _command.Parameters.Add(BuildParameter(_command, "RTNCDE", OleDbType.Char, ParameterDirection.InputOutput, paramLength["RTNCDE"]));
            _command.Parameters.Add(BuildParameter(_command, "RTNDESC", OleDbType.Char, ParameterDirection.InputOutput, paramLength["RTNDESC"]));

            _command.Parameters["PROP"].Value = propertyCode;
            _command.Parameters["SYID"].Scale = 0;
            _command.Parameters["SYID"].Precision = 7;
            _command.Parameters["SYID"].Value = patronId;
            _command.Parameters["TRANS#"].Scale = 0;
            _command.Parameters["TRANS#"].Precision = 11;
            _command.Parameters["TRANS#"].Value = transactionId;
            _command.Parameters["SEQ#"].Scale = 0;
            _command.Parameters["SEQ#"].Precision = 3;
            _command.Parameters["SEQ#"].Value = sequenceNumber;
            _command.Parameters["WINLOSS"].Scale = 2;
            _command.Parameters["WINLOSS"].Precision = 9;
            _command.Parameters["WINLOSS"].Value = winLoss;
            _command.Parameters["COINOUT"].Scale = 2;
            _command.Parameters["COINOUT"].Precision = 9;
            _command.Parameters["COINOUT"].Value = Convert.ToDouble(coinOut);
            _command.Parameters["POINTS"].Scale = 2;
            _command.Parameters["POINTS"].Precision = 9;
            _command.Parameters["POINTS"].Value = points;
            _command.Parameters["INITUSR"].Value = CMSUserId;
            _command.Parameters["ACTION"].Value = UpdateActionId;
            _command.Parameters["RTNCDE"].Value = "";
            _command.Parameters["RTNDESC"].Value = "";


            return ExecuteCMSCommand(_command);

        }

        private RatingInformation CMS_ADD_VOID_SPORTS_RATINGS(string propertyCode,
                                                            string patronId,
                                                            decimal coinIn,
                                                            decimal coinOut,
                                                            decimal winLoss,
                                                            decimal points,
                                                            string shiftId,
                                                            DateTime transDate,
                                                            GameCodeInfo gameCodeInfo,
                                                            string actionID,
                                                            string transactionID)
        {
            string pDate;
            string pTime;
            string pPoints;
            string pCoinIn;
            string pCoinout;
            string pWinLoss;


            _command.CommandType = CommandType.StoredProcedure;
            _command.CommandText = CMSPGM + CMS_ADD_VOID_PROC;
            _command.CommandTimeout = 1;

            _command.Parameters.Add(BuildParameter(_command, "PROP", OleDbType.Char, ParameterDirection.InputOutput, paramLength["PROP"]));
            _command.Parameters.Add(BuildParameter(_command, "SYID", OleDbType.Char, ParameterDirection.InputOutput, paramLength["SYID"]));
            _command.Parameters.Add(BuildParameter(_command, "TRANS#", OleDbType.Char, ParameterDirection.InputOutput, paramLength["TRANS#"]));
            _command.Parameters.Add(BuildParameter(_command, "RATEDATE", OleDbType.Char, ParameterDirection.InputOutput, paramLength["RATEDATE"]));
            _command.Parameters.Add(BuildParameter(_command, "TIMEIN", OleDbType.Char, ParameterDirection.InputOutput, paramLength["TIMEIN"]));
            _command.Parameters.Add(BuildParameter(_command, "SHIFT", OleDbType.Char, ParameterDirection.InputOutput, paramLength["SHIFT"]));
            _command.Parameters.Add(BuildParameter(_command, "WINLOSS", OleDbType.Char, ParameterDirection.InputOutput, paramLength["WINLOSS"]));
            _command.Parameters.Add(BuildParameter(_command, "COININ", OleDbType.Char, ParameterDirection.InputOutput, paramLength["COININ"]));
            _command.Parameters.Add(BuildParameter(_command, "COINOUT", OleDbType.Char, ParameterDirection.InputOutput, paramLength["COINOUT"]));
            _command.Parameters.Add(BuildParameter(_command, "POINTS", OleDbType.Char, ParameterDirection.InputOutput, paramLength["POINTS"]));
            _command.Parameters.Add(BuildParameter(_command, "THEO", OleDbType.Char, ParameterDirection.InputOutput, paramLength["THEO"]));
            _command.Parameters.Add(BuildParameter(_command, "GAMECODE", OleDbType.Char, ParameterDirection.InputOutput, paramLength["GAMECODE"]));
            _command.Parameters.Add(BuildParameter(_command, "LEVEL", OleDbType.Char, ParameterDirection.InputOutput, paramLength["LEVEL"]));
            _command.Parameters.Add(BuildParameter(_command, "ACTION", OleDbType.Char, ParameterDirection.InputOutput, paramLength["ACTION"]));
            _command.Parameters.Add(BuildParameter(_command, "USER", OleDbType.Char, ParameterDirection.InputOutput, paramLength["USER"]));
            _command.Parameters.Add(BuildParameter(_command, "ERRORCODE", OleDbType.Char, ParameterDirection.InputOutput, paramLength["ERRORCODE"]));
            _command.Parameters.Add(BuildParameter(_command, "ERRORTEXT", OleDbType.Char, ParameterDirection.InputOutput, paramLength["ERRORTEXT"]));

            pDate = transDate.ToString("MM/dd/yyyy");
            pTime = transDate.ToString("hhmm");

            pCoinIn = Convert.ToInt32(coinIn * 100).ToString().PadLeft(9, '0');
            pPoints = Convert.ToInt32(points * 100).ToString().PadLeft(9, '0');
            pCoinout = "000000000"; //default
            pWinLoss = "000000000"; //default
                
            string pTheo = Convert.ToInt32(((gameCodeInfo.TheoPct * coinIn) * 100)).ToString().PadLeft(9, '0'); 

            //Check, if the ticket is scored and its points are not yet processed. In this case 
            //we will send both the values required for 'Adding' and 'updating' the rating in CMS.
            //Just FYI, When we 'add' rating we send handle, rest of the param are same as 'update' process except we add two more parameter,
            //these are  'Value' and 'Risk' of that transactions.

            //WinLoss <> -1 and CoinOut <> -1 states that ticket has already been scored, so we need to add the rating and also update the winloss
            if (actionID == "A" && winLoss != -1)// && CoinOut != -1)
            {
                if (coinOut < 0)
                    pCoinout = string.Format("{0:-00000000}", coinOut * -100);
                else
                    pCoinout = Convert.ToInt32(coinOut * 100).ToString().PadLeft(9, '0');

                if (winLoss < 0)
                    pWinLoss = string.Format("{0:-00000000}", winLoss * -100);
                else
                    pWinLoss = Convert.ToInt32(winLoss * 100).ToString().PadLeft(9, '0');

            }

            _command.Parameters["PROP"].Value = propertyCode; //Prop in WIN
            _command.Parameters["SYID"].Value = patronId; //PlayerID in WIN
            _command.Parameters["TRANS#"].Value = transactionID; //CMSID in WIN
            _command.Parameters["RATEDATE"].Value = pDate; //Same in WIN
            _command.Parameters["TIMEIN"].Value = pTime; //Same in WIN
            _command.Parameters["SHIFT"].Value = string.IsNullOrWhiteSpace(shiftId) ? "1" : shiftId; //Hardcoded "1" in WIN
            _command.Parameters["WINLOSS"].Value = pWinLoss; //Same in WIN
            _command.Parameters["COININ"].Value = pCoinIn; //pAmount in WIN
            _command.Parameters["COINOUT"].Value = pCoinout; //Same in WIN
            _command.Parameters["POINTS"].Value = pPoints; //Same in WIN
            _command.Parameters["THEO"].Value = pTheo; //Same in WIN, but derived back when value put into PlayerRating object.
            _command.Parameters["GAMECODE"].Value = gameCodeInfo.GameCode;  //Same in WIN
            _command.Parameters["LEVEL"].Value = "1"; //Same in WIN
            _command.Parameters["ACTION"].Value = actionID; //Same in WIN
            _command.Parameters["USER"].Value = CMSUserId; //pUserID (padded to 4 '0' digits) in WIN
            _command.Parameters["ERRORCODE"].Value = " "; //Same in WIN
            _command.Parameters["ERRORTEXT"].Value = " "; //Same in WIN

            return ExecuteCMSCommand(_command);
        }

        private RatingInformation ExecuteCMSCommand(IOleDbCommand cmd)
        {
            RatingInformation info = new RatingInformation();

            //Output CMS params for all Commands in case we need to figure out something that went wrong.
            StringBuilder sb = new StringBuilder();
            sb.Append("Executing CMS Command. Params:");
            foreach (OleDbParameter parm in cmd.Parameters)
            {
                sb.Append(parm.ParameterName == null ? " nullName" : parm.ParameterName.ToString() + ": ");
                sb.Append(parm.Value == null ? "" : parm.Value.ToString() + ", ");
            }

            _logging.Write(sb.ToString(), "SpinPlayerTracking.ExecuteCMSCommand");

            var task = Task.Run(async () =>
            {
                var taskInternal = ExecuteCMSCommandAsync(cmd);
                await taskInternal;
                info = taskInternal.Result;
            });

            task.Wait();

            return info;
        }

        private async Task<RatingInformation> ExecuteCMSCommandAsync(IOleDbCommand cmd)
        {

            RatingInformation output = new RatingInformation();

            var task = Task.Factory.StartNew(() =>
            {
                var blah = ConfigurationManager.ConnectionStrings["AS400ConnectionString"];
                

                _connection.SetConnectionString(blah.ConnectionString);

                try
                {
                    _connection.Open();

                    cmd.Connection = _connection;

                    cmd.Prepare();

                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    if (cmd.Parameters["ACTION"].Value.ToString() == "U") //UPDATE proc has different parameter names for return than ADD/VOID
                    {
                        if (cmd.Parameters["RTNCDE"].Value.ToString() == "00")
                        {
                            output.Success = true;
                            output.ErrorCode = "00";

                            output.TransactionID = cmd.Parameters["TRANS#"].Value.ToString();
                            output.SequenceNumber = int.Parse(cmd.Parameters["SEQ#"].Value.ToString());
                        }
                        else
                        {
                            output.Success = false;
                            output.ErrorCode = cmd.Parameters["RTNCDE"].Value.ToString();
                            output.ErrorDescription = cmd.Parameters["RTNDESC"].Value.ToString();
                        }
                    }
                    else
                    {
                        output.ErrorCode = Convert.ToString(cmd.Parameters["ERRORCODE"].Value);

                        if (cmd.Parameters["ERRORCODE"].Value.ToString() == "00")
                        {
                            output.TransactionID = cmd.Parameters["TRANS#"].Value.ToString();
                            output.Success = true;
                        }
                        else
                        {
                            output.Success = false;

                            string errorText = cmd.Parameters["ERRORTEXT"].Value.ToString();

                            int nulPos = errorText.IndexOf(Convert.ToChar(0));

                            output.ErrorDescription = (nulPos >= 0 ? errorText.Remove(nulPos).Trim() : errorText.Trim());
                        }
                    }

                    return output;
                }
                catch (Exception ex)
                {
                    output.Success = false;
                    output.ErrorDescription = ex.Message;
                    return output;
                }
                finally
                {
                    CloseConnection(_connection);
                }
            });

            //Only allow this request to be attempted for specified delay amount.
            if (await Task.WhenAny(task, Task.Delay(this.CMSTimeoutMillis)) == task)
            {
                return output;
            }
            else
            {
                try
                {
                    task.Dispose(); //Attempt to free up resources.
                }
                catch (Exception)
                {
                    //Swallow this error.
                }
                finally
                {
                    task = null;
                }
                
                throw new WebAPIException(System.Net.HttpStatusCode.InternalServerError, "Internal rating system timeout. Please ensure all request parameters are correct.");
            }
            
        }

        #region "CMS Utility"

        public class RatingInformation
        {
            public string ErrorCode { get; set; }
            public string ErrorDescription { get; set; }
            public string TransactionID { get; set; }
            public int SequenceNumber { get; set; }
            public bool Success { get; set; }
        }

        private void CloseConnection(IOleDbConnection cnnAS400)
        {
            if (cnnAS400 != null && cnnAS400.State == ConnectionState.Open)
                cnnAS400.Close();
        }

        private OleDbParameter BuildParameter(IOleDbCommand cmd, String paramName, OleDbType paramType, ParameterDirection paramDir, int paramSize, object Value = null)
        {
            OleDbParameter prmOut;
            prmOut = cmd.CreateParameter();
            prmOut.ParameterName = paramName;
            prmOut.OleDbType = paramType;
            prmOut.Direction = paramDir;
            prmOut.Size = paramSize;
            if (Value != null)
                prmOut.Value = Value;

            return prmOut;
        }

        private bool ValidateParamLength(string paramName, string paramValue)
        {
            return paramValue.Length <= paramLength[paramName] ? true : false;
        }

        private Dictionary<string, int> paramLength = new Dictionary<string, int>() {
            {"PROP", 2},
            {"SYID", 7},
            {"TRANS#", 11},
            {"RATEDATE", 10},
            {"TIMEIN", 4},
            {"SHIFT", 1},
            {"WINLOSS", 9},
            {"COININ", 9},
            {"COINOUT", 9},
            {"POINTS", 9},
            {"THEO", 9},
            {"GAMECODE", 2},
            {"LEVEL", 1},
            {"ACTION", 1},
            {"USER", 4},
            {"ERRORCODE", 2},
            {"ERRORTEXT", 255},
            {"SEQ#", 3},
            {"INITUSR", 4},
            {"RTNCDE", 2},
            {"RTNDESC", 255}
        };

        #endregion
    }
}

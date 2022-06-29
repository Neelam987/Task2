using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Task2.Models
{
    public class dataLayer
    {
        public static object ExcuteNonQuery(string procedureName, object parameters)
        {
            dynamic Result = null;
            using (IDbConnection db = ORMConnection.GetConnection())
            {
                try
                {
                    db.Open();
                    Result = db.Execute(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    if (Convert.ToInt16(Result) > 0)
                    {
                        Result = ResponseResult.SuccessResponse("Success");
                    }
                    else
                    {
                        Result = ResponseResult.FailedResponse("Failed");
                    }
                }
                catch (Exception ex)
                {
                    Result = ResponseResult.ExceptionResponse("Internal Server Error", ex.Message.ToString());
                }
                finally
                {
                    db.Close();
                }
            }
            return Result;
        }
        public static async Task<object> ExcuteNonQueryAsync(string procedureName, object parameters)
        {
            dynamic Result = null;
            using (IDbConnection db = ORMConnection.GetConnection())
            {
                try
                {
                    db.Open();
                    Result = await db.ExecuteAsync(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    if (Convert.ToInt16(Result) > 0)
                    {
                        Result = ResponseResult.SuccessResponse("Success");
                    }
                    else
                    {
                        Result = ResponseResult.FailedResponse("Failed");
                    }
                }
                catch (Exception ex)
                {
                    Result = ResponseResult.ExceptionResponse("Internal Server Error", ex.Message.ToString());
                }
                finally
                {
                    db.Close();
                }
            }
            return Result;
        }


        public static object Query(string procedureName, object parameters)
        {
            dynamic Result = null;
            using (IDbConnection db = ORMConnection.GetConnection())
            {
                try
                {
                    db.Open();
                    Result = db.Query<object>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    Result = ResponseResult.SuccessResponse("Success", Result);
                }
                catch (Exception ex)
                {
                    Result = ResponseResult.ExceptionResponse("Internal Server Error", ex.Message.ToString());
                }
                finally
                {
                    db.Close();
                }
            }
            return Result;
        }
        public static async Task<object> QueryAsync(string procedureName, object parameters)
        {
            dynamic Result = null;
            using (IDbConnection db = ORMConnection.GetConnection())
            {
                try
                {
                    db.Open();
                    Result = await db.QueryAsync<object>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    Result = ResponseResult.SuccessResponse("Success", Result);
                }
                catch (Exception ex)
                {
                    Result = ResponseResult.ExceptionResponse("Internal Server Error", ex.Message.ToString());
                }
                finally
                {
                    db.Close();
                }
            }
            return Result;
        }


        public static object QueryFirstOrDefault(string procedureName, object parameters)
        {
            dynamic Result = null;
            using (IDbConnection db = ORMConnection.GetConnection())
            {
                try
                {
                    db.Open();
                    Result = db.QueryFirstOrDefault<object>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    Result = ResponseResult.SuccessResponse("Success", Result);
                }
                catch (Exception ex)
                {
                    Result = ResponseResult.ExceptionResponse("Internal Server Error", ex.Message.ToString());
                }
                finally
                {
                    db.Close();
                }
            }
            return Result;
        }
        public static async Task<object> QueryFirstOrDefaultAsync(string procedureName, object parameters)
        {
            dynamic Result = null;
            using (IDbConnection db = ORMConnection.GetConnection())
            {
                try
                {
                    db.Open();
                    Result = await db.QueryFirstOrDefaultAsync<object>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                    Result = ResponseResult.SuccessResponse("Success", Result);
                }
                catch (Exception ex)
                {
                    Result = ResponseResult.ExceptionResponse("Internal Server Error", ex.Message.ToString());
                }
                finally
                {
                    db.Close();
                }
            }
            return Result;
        }

        public static async Task<object> QueryFirstOrDefaultAsyncWithDBResponse(string procedureName, object parameters)
        {
            dynamic Result = null;
            using (IDbConnection db = ORMConnection.GetConnection())
            {
                try
                {
                    db.Open();
                    Result = await db.QueryFirstOrDefaultAsync<object>(procedureName, parameters, commandType: CommandType.StoredProcedure, commandTimeout: 600);
                }
                catch (Exception ex)
                {
                    Result = ResponseResult.ExceptionResponse("Internal Server Error", ex.Message.ToString());
                }
                finally
                {
                    db.Close();
                }
            }
            return Result;
        }
    }

    public static class ResponseResult
    {
        public static object SuccessResponse(object ResponseMessage)
        {
            var result = new { responseCode = 200, responseMessage = ResponseMessage };
            return result;
        }

        public static object SuccessResponse(object ResponseMessage, object ResponseResult)
        {
            var result = new { responseCode = 200, responseMessage = ResponseMessage, responseResult = ResponseResult };
            return result;
        }

        public static object FailedResponse(object ResponseMessage)
        {
            var result = new { responseCode = 0, responseMessage = ResponseMessage };
            return result;
        }

        public static object FailedResponse(object ResponseMessage, object ResponseResult)
        {
            var result = new { responseCode = 0, responseMessage = ResponseMessage, responseResult = ResponseResult };
            return result;
        }

        public static object ExceptionResponse(object ResponseMessage)
        {
            var result = new { responseCode = 500, responseMessage = ResponseMessage };
            return result;
        }

        public static object ExceptionResponse(object ResponseMessage, object ResponseResult)
        {
            var result = new { responseCode = 500, responseMessage = ResponseMessage, responseResult = ResponseResult };
            return result;
        }
    }
}

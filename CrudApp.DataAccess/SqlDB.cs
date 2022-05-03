using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp.DataAccess
{
    public class SqlDB : IDisposable
    {
        private int sqlExecuteTiming = 500;
        private string connectionstring = "Server=DESKTOP-NI42V80;Database=Layers;Trusted_Connection=True;";
        private bool disposedValue;

        public string ExecuteDeleteQuery(String procedureName, SqlParameter[] sqlParameter, string InLineQuery)
        {
            string result = string.Empty;
            Random Rnd = new Random();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            if (string.IsNullOrEmpty(InLineQuery))
                            {
                                sqlCommand.CommandText = procedureName;
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                sqlCommand.Parameters.AddRange(sqlParameter);
                            }
                            else
                            {
                                sqlCommand.CommandText = InLineQuery;
                                sqlCommand.CommandType = CommandType.Text;
                            }
                            sqlCommand.CommandTimeout = sqlExecuteTiming;
                            sqlConnection.Open();
                            sqlDataAdapter.DeleteCommand = sqlCommand;
                            result = sqlCommand.ExecuteNonQuery().ToString();
                            if (sqlParameter != null)
                                result = sqlCommand.Parameters["@result"].Value.ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                    System.Threading.Thread.Sleep(Rnd.Next(1000, 5000));
                else
                {
                    throw new Exception("Database Error: " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Application Error: " + ex.Message, ex);
            }
            return result;
        }

        public string ExecuteInsertQuery(String procedureName, SqlParameter[] sqlParameter, string InLineQuery)
        {
            string result = string.Empty;
            Random Rnd = new Random();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            if (string.IsNullOrEmpty(InLineQuery))
                            {
                                sqlCommand.CommandText = procedureName;
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                sqlCommand.Parameters.AddRange(sqlParameter);
                            }
                            else
                            {
                                sqlCommand.CommandText = InLineQuery;
                                sqlCommand.CommandType = CommandType.Text;
                            }
                            sqlCommand.CommandTimeout = sqlExecuteTiming;
                            sqlConnection.Open();
                            sqlDataAdapter.InsertCommand = sqlCommand;
                            result = sqlCommand.ExecuteNonQuery().ToString();
                            if (sqlParameter != null)
                                result = sqlCommand.Parameters["@result"].Value.ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                    System.Threading.Thread.Sleep(Rnd.Next(1000, 5000));
                else
                {
                    throw new Exception("Database Error: " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Application Error: " + ex.Message, ex);
            }
            return result;
        }

        public string ExecuteUpdateQuery(String procedureName, SqlParameter[] sqlParameter, string InLineQuery)
        {
            string result = string.Empty;
            Random Rnd = new Random();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            if (string.IsNullOrEmpty(InLineQuery))
                            {
                                sqlCommand.CommandText = procedureName;
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                sqlCommand.Parameters.AddRange(sqlParameter);
                            }
                            else
                            {
                                sqlCommand.CommandText = InLineQuery;
                                sqlCommand.CommandType = CommandType.Text;
                            }
                            sqlCommand.CommandTimeout = sqlExecuteTiming;
                            sqlDataAdapter.UpdateCommand = sqlCommand;
                            sqlConnection.Open();
                            result = sqlCommand.ExecuteNonQuery().ToString();
                            if (sqlParameter != null)
                                result = sqlCommand.Parameters["@result"].Value.ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                    System.Threading.Thread.Sleep(Rnd.Next(1000, 5000));
                else
                {
                    throw new Exception("Database Error: " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Application Error: " + ex.Message, ex);
            }
            return result;
        }

        public void ExecuteSelectQueryWithDataTable(String procedureName, SqlParameter[] sqlParameter, string InLineQuery, out DataTable dataTable)
        {
            dataTable = new DataTable();
            Random Rnd = new Random();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            if (string.IsNullOrEmpty(InLineQuery))
                            {
                                sqlCommand.CommandText = procedureName;
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                sqlCommand.Parameters.AddRange(sqlParameter);
                            }
                            else
                            {
                                sqlCommand.CommandText = InLineQuery;
                                sqlCommand.CommandType = CommandType.Text;
                            }
                            sqlCommand.CommandTimeout = sqlExecuteTiming;
                            sqlDataAdapter.SelectCommand = sqlCommand;
                            sqlDataAdapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                    System.Threading.Thread.Sleep(Rnd.Next(1000, 5000));
                else
                {
                    throw new Exception("Database Error: " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Application Error: " + ex.Message, ex);
            }
        }

        public void ExecuteSelectQueryWithDataSet(String procedureName, SqlParameter[] sqlParameter, string InLineQuery, out DataSet dataSet)
        {
            dataSet = new DataSet();
            Random Rnd = new Random();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            if (string.IsNullOrEmpty(InLineQuery))
                            {
                                sqlCommand.CommandText = procedureName;
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                sqlCommand.Parameters.AddRange(sqlParameter);
                            }
                            else
                            {
                                sqlCommand.CommandText = InLineQuery;
                                sqlCommand.CommandType = CommandType.Text;
                            }
                            sqlCommand.CommandTimeout = sqlExecuteTiming;
                            sqlDataAdapter.SelectCommand = sqlCommand;
                            sqlDataAdapter.Fill(dataSet);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                    System.Threading.Thread.Sleep(Rnd.Next(1000, 5000));
                else
                {
                    throw new Exception("Database Error: " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Application Error: " + ex.Message, ex);
            }
        }

        public string ExecuteScalarQuery(String procedureName, SqlParameter[] sqlParameter, string InLineQuery)
        {
            string result = string.Empty;
            Random Rnd = new Random();
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionstring))
                {
                    using (SqlCommand sqlCommand = new SqlCommand("", sqlConnection))
                    {
                        using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                        {
                            if (string.IsNullOrEmpty(InLineQuery))
                            {
                                sqlCommand.CommandText = procedureName;
                                sqlCommand.CommandType = CommandType.StoredProcedure;
                                sqlCommand.Parameters.AddRange(sqlParameter);
                            }
                            else
                            {
                                sqlCommand.CommandText = InLineQuery;
                                sqlCommand.CommandType = CommandType.Text;
                            }
                            sqlCommand.CommandTimeout = sqlExecuteTiming;
                            sqlDataAdapter.SelectCommand = sqlCommand;
                            sqlConnection.Open();
                            var dbresult = sqlCommand.ExecuteScalar();
                            if (dbresult == null)
                                result = "";
                            else
                                result = dbresult.ToString();
                            if (sqlParameter != null)
                                result = sqlCommand.Parameters["@result"].Value.ToString();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 1205)
                    System.Threading.Thread.Sleep(Rnd.Next(1000, 5000));
                else
                {
                    throw new Exception("Database Error: " + ex.Message, ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Application Error: " + ex.Message, ex);
            }
            return result;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SQLDB()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

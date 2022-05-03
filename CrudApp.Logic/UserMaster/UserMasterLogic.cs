using CrudApp.DataAccess;
using CrudApp.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudApp.Logic.UserMaster
{
    public class UserMasterLogic : IDisposable
    {
        private string table = "usermaster";
        private string Primary_Key = "id";
        private string column = "id,Name,Mobile";
        private string order_by = "";
        private string where = "";


        public DataSet GetList(CommonModel model)
        {
            try
            {
                using (SqlDB mySql = new SqlDB())
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@result", SqlDbType.VarChar),
                        new SqlParameter("@column", column),
                        new SqlParameter("@table", table),
                        new SqlParameter("@order_by", order_by),
                        new SqlParameter("@where", where),
                        new SqlParameter("@start_index", model.start_index),
                        new SqlParameter("@paging_size", model.paging_size)
                    };
                    parameter[0].Direction = ParameterDirection.Output;
                    parameter[0].Size = 100;
                    DataSet dataSet = new DataSet();
                    mySql.ExecuteSelectQueryWithDataSet("USP_SEL_Listing", parameter, "", out dataSet);
                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public string Delete(CommonModel model)
        {
            try
            {
                using (SqlDB mySql = new SqlDB())
                {
                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@result", SqlDbType.VarChar),
                        new SqlParameter("@id", model.id),
                        new SqlParameter("@table", table),
                        new SqlParameter("@primary_key", Primary_Key),

                };
                    parameter[0].Direction = ParameterDirection.Output;
                    parameter[0].Size = 100;
                    return mySql.ExecuteScalarQuery("USP_DEL", parameter, "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }

        public DataTable GetListById(CommonModel model)
        {
            try
            {
                using (SqlDB mySql = new SqlDB())
                {

                    SqlParameter[] parameter = new SqlParameter[]
                    {
                        new SqlParameter("@result", SqlDbType.VarChar),
                        new SqlParameter("@id", model.id),
                        new SqlParameter("@column", "*"),
                        new SqlParameter("@table", table),
                        new SqlParameter("@primary_key", Primary_Key)
                    };
                    parameter[0].Direction = ParameterDirection.Output;
                    parameter[0].Size = 100;
                    DataTable dataTable = new DataTable();
                    mySql.ExecuteSelectQueryWithDataTable("USP_SEL_BY_ID", parameter, "", out dataTable);
                    return dataTable;
                }
            }
            catch
            {
                throw new Exception();
            }
        }

        public string SaveAndUpdate(UserMasterModel model)
        {
            try
            {
                using (SqlDB mySql = new SqlDB())
                {
                    SqlParameter[] parameter;
                    if (string.IsNullOrEmpty(model.id) || model.id=="0")
                    {
                        parameter = new SqlParameter[]
                       {
                            new SqlParameter("@result", SqlDbType.VarChar),

                            new SqlParameter("@name", model.Name),
                            new SqlParameter("@mobile", model.Mobile),
                            new SqlParameter("@choice", "Insert")
                    };
                    }
                    else
                    {
                        parameter = new SqlParameter[]
                      {
                            new SqlParameter("@result", SqlDbType.VarChar),
                            new SqlParameter("@id", model.id),
                           new SqlParameter("@name", model.Name),
                            new SqlParameter("@mobile", model.Mobile),
                            new SqlParameter("@choice", "Update")
                    };

                    }
                    parameter[0].Direction = ParameterDirection.Output;
                    parameter[0].Size = 100;
                    return mySql.ExecuteScalarQuery("USP_INS_UPD_USERMASTER", parameter, "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~FunctionClass() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}

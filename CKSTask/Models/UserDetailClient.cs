using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CKSTask.Models
{
    public class UserDetailClient
    {
        string conStr = ConfigurationManager.ConnectionStrings["CKSTaskConnection"].ConnectionString;
        public UserDetails GetUserDetails()
        {
            try
            {
                SqlConnection myconnection = new SqlConnection(conStr);
                myconnection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetUserDetails", myconnection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    DataSet ResultDS = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(ResultDS);

                    UserDetails result = new UserDetails();
                    List<UserDetails> list = new List<UserDetails>();
                    foreach (DataRowView dr in ResultDS.Tables[0].DefaultView)
                    {
                        UserDetails obj = new UserDetails();
                        obj.UserDetailID = Convert.ToInt64(dr["UserDetailID"]);
                        obj.Name = Convert.ToString(dr["Name"]);
                        obj.DOB = Convert.ToString(dr["DOB"]);
                        //obj.PatientDOB = objConversion.ConvertObjectNullableDatetime(dr["PatientDOB"]);
                        obj.Mobile = Convert.ToString(dr["Mobile"]);
                        obj.Gender = Convert.ToString(dr["Gender"]);
                        obj.Email = Convert.ToString(dr["Email"]);
                       
                        list.Add(obj);
                    }
                    result.lstUserDetails = list;
                 
                    myconnection.Close();
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error Message:" + ex.Message + ",InnerException:" + ex.InnerException + ",Source:" + ex.Source);
            }
        }

        public UserDetails SaveUserDetails(UserDetails collection)
        {
            try
            {
                string id = "0";
                UserDetails objresult = new UserDetails();
                SqlConnection myconnection = new SqlConnection(conStr);
                myconnection.Open();
                using (SqlCommand command = new SqlCommand("sp_SaveUserDetails", myconnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Name", Convert.ToString(collection.Name)));
                    command.Parameters.Add(new SqlParameter("@DOB", Convert.ToString(collection.DOB)));
                    command.Parameters.Add(new SqlParameter("@Gender", collection.Gender));
                    command.Parameters.Add(new SqlParameter("@Mobile", collection.Mobile));
                    command.Parameters.Add(new SqlParameter("@Email", Convert.ToString(collection.Email)));
                    object result = command.ExecuteScalar();
                  
                    if (result != null)
                    {
                        objresult.UserDetailID = Convert.ToInt64(result);
                    }
                }
                myconnection.Close();
                return objresult;

            }
            catch (Exception ex)
            {
                throw new Exception("Error Message:" + ex.Message + ",InnerException:" + ex.InnerException + ",Source:" + ex.Source);
            }
        }
        public string UpdateUserDetails(UserDetails collection)
        {
            try
            {
                string id = "0";
                SqlConnection myconnection = new SqlConnection(conStr);
                myconnection.Open();
                using (SqlCommand command = new SqlCommand("sp_UpdateUserDetails", myconnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserDetailID", Convert.ToInt64(collection.UserDetailID)));
                    command.Parameters.Add(new SqlParameter("@Name", Convert.ToString(collection.Name)));
                    command.Parameters.Add(new SqlParameter("@DOB", Convert.ToString(collection.DOB)));
                    command.Parameters.Add(new SqlParameter("@Gender", collection.Gender));
                    command.Parameters.Add(new SqlParameter("@Mobile", collection.Mobile));
                    command.Parameters.Add(new SqlParameter("@Email", Convert.ToString(collection.Email)));
                    int i = command.ExecuteNonQuery();
                    if (i > 0)
                    {
                        id = i.ToString();
                    }
                }
                myconnection.Close();
                return id;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string DeleteUserDetails(UserDetails collection)
        {
            try
            {
                string id = "0";
                SqlConnection myconnection = new SqlConnection(conStr);
                myconnection.Open();
                using (SqlCommand command = new SqlCommand("sp_DeleteUserDetails", myconnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserDetailID", Convert.ToInt64(collection.UserDetailID)));
                    int i = command.ExecuteNonQuery();
                    if (i > 0)
                    {
                        id = i.ToString();
                    }
                }
                myconnection.Close();
                return id;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string SaveUserAddressDetails(UserDetails collection)
        {
            try
            {
                string id = "0";
                SqlConnection myconnection = new SqlConnection(conStr);
                myconnection.Open();
                using (SqlCommand command = new SqlCommand("sp_SaveUserAddressDetails", myconnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Flag", Convert.ToString(collection.Flag)));
                    command.Parameters.Add(new SqlParameter("@UserDetailID", Convert.ToInt64(collection.UserDetailID)));
                    command.Parameters.Add(new SqlParameter("@Address1", Convert.ToString(collection.Address1)));
                    command.Parameters.Add(new SqlParameter("@Address2", Convert.ToString(collection.Address2)));
                    command.Parameters.Add(new SqlParameter("@City", collection.City));
                    command.Parameters.Add(new SqlParameter("@State", collection.State));
                    command.Parameters.Add(new SqlParameter("@Pincode", Convert.ToString(collection.Pincode)));
                    int i = command.ExecuteNonQuery();
                    if (i > 0)
                    {
                        id = i.ToString();
                    }
                }
                myconnection.Close();
                return id;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
     
        public UserDetails GetUserDetailsByID(Int64 UserDetailID)
        {
            try
            {
                SqlConnection myconnection = new SqlConnection(conStr);
                myconnection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetUserDetailsByID", myconnection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@UserDetailID", Convert.ToInt64(UserDetailID)));

                    DataSet ResultDS = new DataSet();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(ResultDS);

                    UserDetails result = new UserDetails();
                    foreach (DataRowView dr in ResultDS.Tables[0].DefaultView)
                    {
                        result.UserDetailID = Convert.ToInt64(dr["UserDetailID"]);
                        result.Name = Convert.ToString(dr["Name"]);
                        result.DOB = Convert.ToString(dr["DOB"]);
                        result.Gender = Convert.ToString(dr["Gender"]);
                        result.Mobile = Convert.ToString(dr["Mobile"]);
                        result.Email = Convert.ToString(dr["Email"]);
                    }
                    List<UserDetails> list = new List<UserDetails>();
                    foreach (DataRowView dr in ResultDS.Tables[1].DefaultView)
                    {
                        UserDetails obj = new UserDetails();
                        obj.UserDetailID = Convert.ToInt64(dr["UserDetailID"]);
                        obj.UserAddressID = Convert.ToInt64(dr["UserAddressID"]);
                        obj.Address1 = Convert.ToString(dr["Address1"]);
                        obj.Address2 = Convert.ToString(dr["Address2"]);
                        obj.City = Convert.ToString(dr["City"]);
                        obj.State = Convert.ToString(dr["State"]);
                        obj.Pincode = Convert.ToString(dr["Pincode"]);
                        list.Add(obj);
                    }
                    result.lstUserDetails = list;
                    myconnection.Close();
                    return result;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error Message:" + ex.Message + ",InnerException:" + ex.InnerException + ",Source:" + ex.Source);
            }
        }
    }
}

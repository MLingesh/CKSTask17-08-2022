using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CKSTask.Models;

namespace CKSTask.Controllers
{
    public class HomeController : Controller
    {
        UserDetailClient UserDetailsClient = new UserDetailClient();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            LoadDocumentTypeList();
            return View();
        }

        [HttpPost]
        public ActionResult GetUserDetails()
        {
            try
            {               
                UserDetails obj = new UserDetails();
                var searchresult = UserDetailsClient.GetUserDetails();               
                return Json(searchresult);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void LoadDocumentTypeList()
        {
            try
            {
                List<SelectListItem> Gender = new List<SelectListItem>();
                Gender.Add(new SelectListItem { Text = "Male", Value = "Male" });
                Gender.Add(new SelectListItem { Text = "Female", Value = "Female" });
                Gender.Add(new SelectListItem { Text = "Other", Value = "Other" });
                ViewBag.GenderList = Gender;
            }
            catch (Exception ex)
            {
               
            }
        }

        [HttpPost]
        public ActionResult SaveUserDetails(string Name, string DOB, string Gender,string Mobile,string Email)
        {
            ResultResponse resultres = new ResultResponse();
            try
            {
                UserDetails objuser = new UserDetails();
                objuser.Name = Name;
                objuser.DOB = DOB;
                objuser.Gender = Gender;
                objuser.Mobile = Mobile;                
                string encruptvalue = EncodePasswordToBase64(Email);
                objuser.Email = encruptvalue;

                if (ModelState.IsValid)
                {
                    DataSet ds = new DataSet();
                    var UserDetailID = UserDetailsClient.SaveUserDetails(objuser);
                    if (Convert.ToInt32(UserDetailID.UserDetailID) > 0)
                    {
                        resultres.message = "Success";
                        resultres.ID = UserDetailID.UserDetailID.ToString();
                        return Json(resultres);
                    }
                }
                return Json(resultres);
            }
            catch (Exception ex)
            {               
                resultres.message = ex.Message;
                return Json(resultres);
            }

        }

        [HttpPost]
        public ActionResult UpdateUserDetails(string UserDetailID,string Name, string DOB, string Gender, string Mobile, string Email)
        {
            ResultResponse resultres = new ResultResponse();
            try
            {
                UserDetails objuser = new UserDetails();
                objuser.Name = Name;
                objuser.DOB = DOB;
                objuser.Gender = Gender;
                objuser.Mobile = Mobile;
                objuser.Email = Email;
                objuser.UserDetailID = Convert.ToInt64(UserDetailID);

                if (ModelState.IsValid)
                {
                    DataSet ds = new DataSet();
                    var s = UserDetailsClient.UpdateUserDetails(objuser);
                    if (Convert.ToInt32(s) > 0)
                    {
                        resultres.message = "Success";
                        return Json(resultres);
                    }
                }
                return Json(resultres);
            }
            catch (Exception ex)
            {
                resultres.message = ex.Message;
                return Json(resultres);
            }

        }

        [HttpPost]
        public ActionResult DeleteUserDetails(string UserDetailID)
        {
            ResultResponse resultres = new ResultResponse();
            try
            {
                UserDetails objuser = new UserDetails();
               
                objuser.UserDetailID = Convert.ToInt64(UserDetailID);

                if (ModelState.IsValid)
                {
                    DataSet ds = new DataSet();
                    var s = UserDetailsClient.DeleteUserDetails(objuser);
                    if (Convert.ToInt32(s) > 0)
                    {
                        resultres.message = "Success";
                        return Json(resultres);
                    }
                }
                return Json(resultres);
            }
            catch (Exception ex)
            {
                resultres.message = ex.Message;
                return Json(resultres);
            }

        }

        [HttpPost]
        public ActionResult SaveUserAddressDetails(string UserDetailID, string Address1, string Address2, string City, string State, string Pincode)
        {
            ResultResponse resultres = new ResultResponse();
            try
            {
                UserDetails objuser = new UserDetails();
                objuser.UserDetailID =Convert.ToInt64(UserDetailID);
                objuser.Address1 = Address1;
                objuser.Address2 = Address2;
                objuser.City = City;
                objuser.State = State;
                objuser.Pincode = Pincode;
                objuser.Flag = "Save";
                if (ModelState.IsValid)
                {
                    DataSet ds = new DataSet();
                    var s = UserDetailsClient.SaveUserAddressDetails(objuser);
                    if (Convert.ToInt32(s) > 0)
                    {
                        resultres.message = "Success";
                        return Json(resultres);
                    }
                }
                return Json(resultres);
            }
            catch (Exception ex)
            {
                resultres.message = ex.Message;
                return Json(resultres);
            }

        }

        [HttpPost]
        public ActionResult UpdateUserAddressDetails(string UserAddressID, string Address1, string Address2, string City, string State, string Pincode)
        {
            ResultResponse resultres = new ResultResponse();
            try
            {
                UserDetails objuser = new UserDetails();
                objuser.UserDetailID = Convert.ToInt64(UserAddressID);
                objuser.Address1 = Address1;
                objuser.Address2 = Address2;
                objuser.City = City;
                objuser.State = State;
                objuser.Pincode = Pincode;
                objuser.Flag = "Update";
                if (ModelState.IsValid)
                {
                    DataSet ds = new DataSet();
                    var s = UserDetailsClient.SaveUserAddressDetails(objuser);
                    if (Convert.ToInt32(s) > 0)
                    {
                        resultres.message = "Success";
                        return Json(resultres);
                    }
                }
                return Json(resultres);
            }
            catch (Exception ex)
            {
                resultres.message = ex.Message;
                return Json(resultres);
            }

        }
        [HttpPost]
        public ActionResult DeleteUserAddressDetails(string UserAddressID)
        {
            ResultResponse resultres = new ResultResponse();
            try
            {
                UserDetails objuser = new UserDetails();
                objuser.UserDetailID = Convert.ToInt64(UserAddressID);
                
                objuser.Flag = "Delete";
                if (ModelState.IsValid)
                {
                    DataSet ds = new DataSet();
                    var s = UserDetailsClient.SaveUserAddressDetails(objuser);
                    if (Convert.ToInt32(s) > 0)
                    {
                        resultres.message = "Success";
                        return Json(resultres);
                    }
                }
                return Json(resultres);
            }
            catch (Exception ex)
            {
                resultres.message = ex.Message;
                return Json(resultres);
            }

        }
        public ActionResult Edit(int ID)
        {
            LoadDocumentTypeList();
            var obj = UserDetailsClient.GetUserDetailsByID(Convert.ToInt64(ID));
            return View(obj);
        }
        public ActionResult Details(int ID)
        {
            LoadDocumentTypeList();
            var obj = UserDetailsClient.GetUserDetailsByID(Convert.ToInt64(ID));
            return View(obj);
        }

               
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        //this function Convert to Decord your Password
        public string DecodeFrom64(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }
    }
}
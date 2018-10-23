using System;

namespace Common
{
    public class ServiceExeption : Exception
    {
        public ServiceExeption(string message, Exception e)
        {
            var str = e.ToString();
            if (str.Contains("conflicted with the FOREIGN KEY constraint"))
            {
                throw new Exception("خطای سیستمی ارتباط بین جداول");
            }
            else if (str.Contains("Cannot insert duplicate key"))
            {
                throw new Exception("کد وارد شده تکراری می باشد");
            }
            //else if (str.Contains("Cannot insert duplicate key") && str.Contains("Title"))
            //{
            //    throw new Exception("عنوان وارد شده تکراری می باشد");
            //}
            else if (str.Contains("Source sequence doesn't contain any elements"))
            {
                throw new Exception("اطلاعاتی با شناسه ارسال شده موجود نمی باشد");
            }
            else if (str.Contains("IDENTITY_INSERT is set to OFF"))
            {
                throw new Exception("ثبت در فیلد identity");
            }
            else
            {
                throw new Exception(message);
            }
        }

    }
}

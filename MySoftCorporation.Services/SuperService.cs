using MySoftCorporation.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace MySoftCorporation.Services
{
    public class SuperService
    {
        protected string GetDetail(Exception exception)
        {
            return exception.Message.ToString();
        }

        protected string GetValidationDetail(DbEntityValidationException dbEntityValidationException)
        {
            string Error = string.Empty;
            foreach (var eve in dbEntityValidationException.EntityValidationErrors)
            {
                Error += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    Error += "<br />" + string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                }
            }
            return Error;
        }
    }
}
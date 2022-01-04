using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripFinder.BLL
{
    public class WSResponse<T>
    {
        public bool IsSuccess { set; get; }

        public T data { set; get; }

        public string ErrorMessage { set; get; }

        public WSResponse<T> ReturnWS(bool IsSuccess, T ResponseData, string ErrorMessage)
        {
            WSResponse<T> result = new WSResponse<T>();
            result.IsSuccess = IsSuccess;
            result.data = ResponseData;
            result.ErrorMessage = ErrorMessage;
            return result;
        }
    }
}

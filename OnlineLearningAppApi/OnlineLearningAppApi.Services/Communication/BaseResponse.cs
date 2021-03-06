using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningAppApi.Services.Communication
{
    public class BaseResponse<T>
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public bool IsException { get; set; }
        public T Resource { get; private set; }

        public BaseResponse(bool success, string meessage = null, bool isException = false, T resource = default)
        {
            Success = success;
            Message = meessage;
            IsException = isException;
            Resource = resource;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BugTicketingSystem.BL
{
    public class GeneralResult
    {
        public bool Success { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ResultError[] Errors { get; set; } = null!; 
        public static GeneralResult Fail(ResultError [] errors)
        {
            return new GeneralResult
            {
                Success = false,
                Errors = errors
            };
        }
       
        public static GeneralResult Ok()
        {
            return new GeneralResult { Success = true };
        }

    }
    public class ResultError
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string  Code { get; set; } = string.Empty;
        public string  Message { get; set; } = string.Empty;
    }

    public class GeneralResult<T> : GeneralResult
    {

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T Data { get; set; }

        public static GeneralResult<T> Ok(T data)
        {
            return new GeneralResult<T> { Success = true, Data = data };

        }
        public static GeneralResult<T> Fail(ResultError[] errors)
        {
            return new GeneralResult<T>
            {
                Data = default ,
                Success = false,
                Errors = errors
            }; 
        }

    }
}

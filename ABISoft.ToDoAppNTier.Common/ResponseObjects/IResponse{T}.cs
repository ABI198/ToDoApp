using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABISoft.ToDoAppNTier.Common.ResponseObjects
{
    public interface IResponse<T> : IResponse 
    {
        public T Data { get; set; }
        List<CustomValidationError> ValidationErrors { get; set; }
    }
}

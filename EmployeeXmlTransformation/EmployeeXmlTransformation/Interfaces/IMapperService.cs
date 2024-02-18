using EmployeeXmlTransformation.Models;
using TransformationLogic.Models;

namespace EmployeeXmlTransformation.Interfaces
{
    public interface IMapperService
    {
        InputDataDto Map(InputData inputData);
    }
}

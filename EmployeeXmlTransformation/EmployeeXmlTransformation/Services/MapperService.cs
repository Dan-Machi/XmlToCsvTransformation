using EmployeeXmlTransformation.Interfaces;
using EmployeeXmlTransformation.Models;
using TransformationLogic.Models;

namespace EmployeeXmlTransformation.Services
{
    public class MapperService : IMapperService
    {
        public InputDataDto Map(InputData inputData)
        {
            return new InputDataDto()
            {
                InputFolder = inputData.InputFolder,
                OutputFolder = inputData.OutputFolder,
            };
        }
    }
}

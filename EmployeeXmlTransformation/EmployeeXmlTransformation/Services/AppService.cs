using EmployeeXmlTransformation.Interfaces;
using EmployeeXmlTransformation.Models;
using TransformationLogic.Interfaces;

namespace EmployeeXmlTransformation.Services
{
    public class AppService : IAppService
    {
        private const string InputFolderName = "Input";
        private const string OutputFolderName = "Output";

        private InputData _inputData;
        private readonly IMapperService _mapperService;
        private readonly ITransformationService _transformationService;

        public AppService(ITransformationService transformationService, IMapperService mapperService)
        {
            _inputData = new InputData();
            _transformationService = transformationService;
            _mapperService = mapperService;
        }

        public void Run()
        {
            InputFolder();
            OutputFolder();
            _transformationService.Transform(_mapperService.Map(_inputData));
        }

        private void InputFolder()
        {
            Console.WriteLine(
                "Select input folder: \n" +
                "Type \"D\" for default folder \n" +
                "Type \"C\" for custom folder \n");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D:
                    {
                        _inputData.InputFolder = Path.Combine(Environment.CurrentDirectory, InputFolderName);
                        break;
                    }
                case ConsoleKey.C:
                    {
                        _inputData.InputFolder = CustomFolder();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void OutputFolder()
        {
            Console.WriteLine(
                "\nSelect output folder: \n" +
                "Type \"D\" for default folder \n" +
                "Type \"C\" for custom folder \n");

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.D:
                    {
                        var outputFolderPath = Path.Combine(Environment.CurrentDirectory, OutputFolderName);

                        if (!Directory.Exists(outputFolderPath))
                        {
                            Directory.CreateDirectory(outputFolderPath);
                        }

                        _inputData.OutputFolder = Path.Combine(outputFolderPath);
                        break;
                    }
                case ConsoleKey.C:
                    {
                        _inputData.OutputFolder = CustomFolder();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        private string CustomFolder()
        {
            Console.WriteLine("\nProvide path to folder: \n");
            var folderPath = Console.ReadLine();

            while (true)
            {
                if (Directory.Exists(folderPath))
                {
                    return folderPath;
                }
                else
                {
                    Console.WriteLine($"\nFolder with path {folderPath} does not exist. Please provide valid folder path.");
                }
            }
        }
    }
}

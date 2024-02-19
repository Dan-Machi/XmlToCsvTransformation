using EmployeeXmlTransformation.Interfaces;
using EmployeeXmlTransformation.Models;
using log4net;
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
        private readonly ILog _logger;

        public AppService(ITransformationService transformationService, IMapperService mapperService, ILog logger)
        {
            _inputData = new InputData();
            _transformationService = transformationService;
            _mapperService = mapperService;
            _logger = logger;
        }

        public void Run()
        {
            try
            {
                InputFolder();
                OutputFolder();
                _transformationService.Transform(_mapperService.Map(_inputData));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        private void Menu(string folderType)
        {
            Console.WriteLine(
                $"Select {folderType} folder: \n" +
                "Type \"D\" for default folder \n" +
                "Type \"C\" for custom folder \n");
        }

        private void InputFolder()
        {
            Menu("input");

            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D:
                        {
                            _inputData.InputFolder = Path.Combine(Environment.CurrentDirectory, InputFolderName);
                            _logger.Info($"Input folder {_inputData.InputFolder}");
                            return;
                        }
                    case ConsoleKey.C:
                        {
                            var inputFolder = CustomFolder();
                            var xmlFiles = Directory.GetFiles(inputFolder, "*.xml");

                            if (!xmlFiles.Any())
                            {
                                _logger.Warn($"There are no xml files in {inputFolder}");
                                Console.WriteLine($"There are no xml files in {inputFolder}");
                                break;
                            }

                            _inputData.InputFolder = inputFolder;
                            _logger.Info($"Input folder {_inputData.InputFolder}");
                            return;
                        }
                    default:
                        {
                            _logger.Warn("Wrong key input.");
                            Menu("input");
                            break;
                        }
                }

            }
        }

        private void OutputFolder()
        {
            Menu("output");

            while (true)
            {
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
                            _logger.Info($"Output folder {_inputData.OutputFolder}");
                            return;
                        }
                    case ConsoleKey.C:
                        {
                            _inputData.OutputFolder = CustomFolder();
                            _logger.Info($"Output folder {_inputData.OutputFolder}");
                            return;
                        }
                    default:
                        {
                            _logger.Warn("Wrong key input.");
                            Menu("output");
                            break;
                        }
                }
            }
        }

        private string CustomFolder()
        {
            Console.WriteLine("\nProvide path to folder: \n");

            while (true)
            {
                var folderPath = Console.ReadLine();
                if (Directory.Exists(folderPath))
                {
                    return folderPath;
                }
                else
                {
                    _logger.Warn($"Invalid folder path: {folderPath}");
                    Console.WriteLine($"\nFolder with path {folderPath} does not exist. Please provide valid folder path.");
                }
            }
        }
    }
}

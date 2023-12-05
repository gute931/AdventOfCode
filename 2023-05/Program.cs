Console.WriteLine("Uppgift 2023-12-05!");
enum block { none, seeds, seed_to_soil, soil_to_fertilizer, fertilizer_to_water, water_to_light, light_to_temperature, temperature_to_humidity, humidity_to_location };
namespace Application
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] _filedata = File.ReadAllLines("./testdata.txt");
            string[] Seeds = new string[1];

            block currentBlock = block.none;




            for (int i = 0; i < _filedata.Length; i++)
            {
                string[] _fileParts = _filedata[i].Split(" :".ToCharArray(), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (_fileParts[0] == "") currentBlock = block.none;
                else if (currentBlock == block.none)
                {
                    switch (_fileParts[0])
                    {
                        case "seeds":
                            currentBlock = block.seeds;
                            Seeds = _fileParts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                            break;
                        case "seed-to-soil":
                            currentBlock = block.seed_to_soil;
                            break;
                        case "soil-to-fertilizer":
                            currentBlock = block.soil_to_fertilizer;
                            break;
                        case "fertilizer-to-water":
                            currentBlock = block.fertilizer_to_water;
                            break;
                        case "water-to-light":
                            currentBlock = block.water_to_light;
                            break;
                        case "light-to-temperature":
                            currentBlock = block.light_to_temperature;
                            break;
                        case "temperature-to-humidity":
                            currentBlock = block.temperature_to_humidity;
                            break;
                        case "humidity-to-location":
                            currentBlock = block.humidity_to_location;
                            break;
                        default:
                            break;
                    }

                }
                else if (currentBlock != block.none)
                {

                }



            }
        }
    }
}
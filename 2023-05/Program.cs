using _2023_05;
using Application;
using System.ComponentModel.Design;
using System.Diagnostics;

Console.WriteLine("Uppgift 2023-12-05!");


string[] _filedata = File.ReadAllLines("./data.txt");
string[] Seeds = new string[1];

block currentBlock = block.none;

List<ItemMapp> seed_to_soil = new List<ItemMapp>();
List<ItemMapp> soil_to_fertilizer = new List<ItemMapp>();
List<ItemMapp> fertilizer_to_water = new List<ItemMapp>();
List<ItemMapp> water_to_light = new List<ItemMapp>();
List<ItemMapp> light_to_temperature = new List<ItemMapp>();
List<ItemMapp> temperature_to_humidity = new List<ItemMapp>();
List<ItemMapp> humidity_to_location = new List<ItemMapp>();


for (int i = 0; i < _filedata.Length; i++)
{
    string[] _fileParts = _filedata[i].Split(" :".ToCharArray(), StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    if (_fileParts.Length == 0) currentBlock = block.none;
    else if (currentBlock == block.none)
    {
        switch (_fileParts[0])
        {
            case "seeds":
                currentBlock = block.seeds;
                Seeds = _filedata[i].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
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
        switch (currentBlock)
        {
            case block.seed_to_soil:
                seed_to_soil.Add(new ItemMapp(_filedata[i]));
                break;
            case block.soil_to_fertilizer:
                soil_to_fertilizer.Add(new ItemMapp(_filedata[i]));
                break;
            case block.fertilizer_to_water:
                fertilizer_to_water.Add(new ItemMapp(_filedata[i]));
                break;
            case block.water_to_light:
                water_to_light.Add(new ItemMapp(_filedata[i]));
                break;
            case block.light_to_temperature:
                light_to_temperature.Add(new ItemMapp(_filedata[i]));
                break;
            case block.temperature_to_humidity:
                temperature_to_humidity.Add(new ItemMapp(_filedata[i]));
                break;
            case block.humidity_to_location:
                humidity_to_location.Add(new ItemMapp(_filedata[i]));
                break;
            default:
                break;
        }
    }
}

long S1 = int.MaxValue;
/*
foreach (var _seed in Seeds)
{
    long _seedNo = long.Parse(_seed);

    long _soil = translateValue(seed_to_soil, _seedNo);
    long _fertilizer = translateValue(soil_to_fertilizer, _soil);
    long _water = translateValue(fertilizer_to_water, _fertilizer);
    long _light = translateValue(water_to_light, _water);
    long _temp = translateValue(light_to_temperature, _light);
    long _humidity = translateValue(temperature_to_humidity, _temp);
    long _location = translateValue(humidity_to_location, _humidity);

    // Console.WriteLine($"Seed:{_seedNo} -> Soil:{_soil} -> Fertilizer:{_fertilizer} -> Water:{_water} -> Light:{_light} -> Temp:{_temp} -> Humidity:{_humidity} -> Location:{_location}.");

    S1 = Math.Min(S1, _location);
}
*/
long S2 = int.MaxValue;
for (int i = 0; i < Seeds.Length; i += 2)
{
    long _seedfrom = long.Parse(Seeds[i]);
    long _seedto = _seedfrom + long.Parse(Seeds[i + 1]) - 1;
    List<SeedGroup> _start = new List<SeedGroup>() { new SeedGroup(_seedfrom, _seedto) };

    List<SeedGroup> _soils = translateValueGrp(seed_to_soil, _start);
    /*
    SeedGroup[] _fertilizers = translateValueGrp(soil_to_fertilizer, _soils);
    SeedGroup[] _waters = translateValueGrp(fertilizer_to_water, _fertilizers);
    SeedGroup[] _lights = translateValueGrp(water_to_light, _waters);
    SeedGroup[] _temps = translateValueGrp(light_to_temperature, _lights);
    SeedGroup[] _humiditys = translateValueGrp(temperature_to_humidity, _temps);
    SeedGroup[] _locations = translateValueGrp(humidity_to_location, _humiditys);
    // Console.WriteLine($"Seed:{_seedNo + j} -> Soil:{_soil} -> Fertilizer:{_fertilizer} -> Water:{_water} -> Light:{_light} -> Temp:{_temp} -> Humidity:{_humidity} -> Location:{_location}.");

    foreach (var item in _locations)
    {
        S2 = Math.Min(S2, item.ItemFrom + item.Offset);
    }
    */

}


Console.WriteLine($"S1:{S1}");
Console.WriteLine($"S2:{S2}");
// data loaded
Console.ReadLine();


List<SeedGroup> translateValueGrp(List<ItemMapp> map, List<SeedGroup> valueGrps)
{
    List<SeedGroup> result = new List<SeedGroup>();
    foreach (var item in valueGrps)
    {
        long _from = item.ItemFrom;
        long _to = item.ItemTo;
        List<ItemMapp> _res = map.Where(w => w.InValueFrom <= item.ItemTo && w.InValueTo >= item.ItemFrom).OrderBy(o => o.InValueFrom).ToList();
        foreach (var _r in _res)
        {
            Console.WriteLine($"GrpStrt:{item.ItemFrom} <-> GrpEnd:{item.ItemTo} between ValueFrom:{_r.InValueFrom} <-> ValueTo:{_r.InValueTo} # Offset:{_r.Offset}");
            long _startDiff =  _r.InValueFrom - _from ;
            long _endDiff = _r.InValueTo - _to;
            if (_startDiff < 0)
            {
                SeedGroup _a = new SeedGroup(_r.InValueFrom, _to);
            }



        }
    }
    return result;
}

long translateValue(List<ItemMapp> map, long value)
{
    long _outValue = value;
    ItemMapp[] _mapObj = map.Where(w => w.InValueFrom <= value && w.InValueTo >= value).ToArray();
    if (_mapObj.Length == 1) _outValue = _mapObj[0].GetPointer(value);
    return _outValue;
}